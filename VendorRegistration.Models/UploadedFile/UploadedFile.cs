using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.UploadedFile
{
    public class UploadedFile
    {
        private string name;
        private string base64String;
        private IBrowserFile Ibrowser;

        public UploadedFile(string name, string base64String, IBrowserFile Ibrowser)
        {
            this.name = name;
            this.base64String = base64String;
            this.Ibrowser = Ibrowser;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Base64String
        {
            get { return base64String; }
            set { base64String = value; }
        }

        public IBrowserFile IBrowser
        {
            get { return Ibrowser; }
            set { Ibrowser = value; }
        }
    }
}
