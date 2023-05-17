using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.ArcGis
{
    public class ArcGisAddress
    {
        public Spatialreference? spatialReference { get; set; }
        public List<Candidate>? candidates { get; set; }
    }

    public class Spatialreference
    {
        public int wkid { get; set; }
        public int latestWkid { get; set; }
    }

    public class Candidate
    {
        public string? address { get; set; }
        public XYLocation? location { get; set; }
        public float? score { get; set; }
        public Extent? extent { get; set; }
    }

    public class XYLocation
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Extent
    {
        public float xmin { get; set; }
        public float ymin { get; set; }
        public float xmax { get; set; }
        public float ymax { get; set; }
    }
}
