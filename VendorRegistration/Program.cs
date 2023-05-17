using BlazorDownloadFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using VendorRegistration.Authentication;
using VendorRegistration.Data;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.ArcGis;
using VendorRegistration.Services.Authentication;
using VendorRegistration.Services.Background_Tasks;
using VendorRegistration.Services.Data.AccountService;
using VendorRegistration.Services.Data.AddressService;
using VendorRegistration.Services.Data.AuthorizedContactsService;
using VendorRegistration.Services.Data.CategoryService;
using VendorRegistration.Services.Data.CompanyService;
using VendorRegistration.Services.Data.CompanyTypesAndCatsService;
using VendorRegistration.Services.Data.EmailAttachmentsService;
using VendorRegistration.Services.Data.EmployeeDataService;
using VendorRegistration.Services.Data.NotificationRecipientsService;
using VendorRegistration.Services.Data.NotificationsHistoryService;
using VendorRegistration.Services.Data.TypeService;
using VendorRegistration.Services.Data.VendorLinksService;
using VendorRegistration.Services.FunctionsService.EmailSenderService;
using VendorRegistration.Services.FunctionsService.EmailValidation;
using VendorRegistration.Services.FunctionsService.EncryptionService;
using VendorRegistration.Services.FunctionsService.RegisterService;
using VendorRegistration.Services.FunctionsService.TypeCatCompanyChangesService;
using VendorRegistration.Services.Theme;
using VendorRegistration.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IUserService, HhcAuthenticatorUserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<EmployeeDataService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<TypeService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<VendorLinksService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CompanyTypesAndCatsService>();
builder.Services.AddScoped<AuthorizedContactsService>();
builder.Services.AddScoped<NotificationsHistoryService>();
builder.Services.AddScoped<EmailAttachmentsService>();
builder.Services.AddScoped<NotificationRecipientsService>();
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<TypeCatCompanyChangesService>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddScoped<EmailValidationService>();
builder.Services.AddScoped<RegisterService>();

builder.Services.AddScoped<ArcGisService>();
builder.Services.AddDbContext<VendorDataDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("VendorsDb"),
    optionsBuilder => optionsBuilder.MigrationsAssembly("VendorRegistration")));
builder.Services.AddScoped<StateContainer>();
builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);

builder.Services.AddTransient<IHostedService, VendorLinksCleanupService>();
builder.Services.AddTransient<IHostedService, UnverifiedCompaniesCleanupService>();
builder.Services.AddTransient<IHostedService, NotificationsHistoryCleanupService>();


//Add roles here, make sure they match whats in Authenticator: dbo.Application and dbo.Role
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeHahpdc1", a =>
      a.RequireAuthenticatedUser().RequireClaim("Domain", "HAHPDC1"));

    options.AddPolicy("AdminOrSuperUser", policy =>
       policy.RequireAssertion(context =>
           context.User.HasClaim(c =>
            c.Type == "Role" && (c.Value == "Admin" || c.Value == "SuperUser"))));

    options.AddPolicy("User", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
    c.Type == "Role" && (c.Value == "User" || c.Value == "user"))));

    options.AddPolicy("HHCUser", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
    c.Type == "Role" && (c.Value == "HHCUser"))));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
