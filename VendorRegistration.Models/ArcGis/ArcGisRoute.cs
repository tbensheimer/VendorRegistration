using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.ArcGis
{
    public class ArcGisRoute
    {
        public Routes? routes { get; set; }
    }

    public class Routes
    {
        public List<Feature>? features { get; set; }
    }

    public class Feature
    {
        public Attributes? attributes { get; set; }
    }

    public class Attributes
    {
        public float Total_Kilometers { get; set; }
        public float Total_Miles { get; set; }
        public float Shape_Length { get; set; }
    }
}
