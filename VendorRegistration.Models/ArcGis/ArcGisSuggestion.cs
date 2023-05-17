using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.ArcGis
{
    public class ArcGisSuggestion
    {
        public List<Suggestion>? suggestions { get; set; }
    }

    public class Suggestion
    {
        public string? text { get; set; }
        public string? magicKey { get; set; }
        public bool isCollection { get; set; }
    }
}

