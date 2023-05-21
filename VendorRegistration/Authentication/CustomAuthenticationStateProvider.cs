using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using VendorRegistration.Models.Authentication;
using VendorRegistration.Services.Authentication;

namespace VendorRegistration.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        private User cachedUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService, IConfiguration configuration)
        {
            this.jsRuntime = jsRuntime;
            this.userService = userService;
            this.configuration = configuration;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (cachedUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User tmp = JsonSerializer.Deserialize<User>(userAsJson);
                    if (tmp != null)
                    {
                        identity = SetupClaimsForUser(tmp);
                    }
                }
            }
            else
            {
                identity = SetupClaimsForUser(cachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        public void ValidateLogin(string username, string password, string passcode)
        {
            Console.WriteLine("Validating log in");

            if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
            if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();
            try
            {
                User user = userService.ValidateUser(username, password, configuration["CustomAuthentication:Url"], passcode);
                identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
            }
            catch (Exception e)
            {
                throw e;
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public void Logout()
        {
            cachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public ClaimsIdentity SetupClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));


            if (user.Role.Contains("admin"))
            {
                claims.Add(new Claim("Role", "Admin"));
            }
            if (user.Role.Contains("user"))
            {
                claims.Add(new Claim("Role", "User"));
            }
            if (user.Role.Contains("hhcUser"))
            {
                claims.Add(new Claim("Role", "HHCUser"));
            }

            claims.Add(new Claim("Company", $"{user.CompanyId}"));

            claims.Add(new Claim("Domain", user.Domain));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }

        //////////// Trent's Authentication for Companies
        public void ValidateLoginForCompany(string username, string password, int companyId, string company)
        {
            Console.WriteLine("Validating log in");

            if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
            if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();
            try
            {
                User user = new User();
                user.UserName = username;
                user.Password = password;
                user.Domain = "";
                user.Company = company;
                user.CompanyId = companyId;
                user.Role = "user";

                //User user = userService.ValidateUser(username, password, configuration["CustomAuthentication:Url"], passcode);
                identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
            }
            catch (Exception e)
            {
                throw e;
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
    }
}
