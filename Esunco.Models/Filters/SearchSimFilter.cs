using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Filters
{
    public class SearchSimFilter
    {
        public SimType SimType { get; set; }

        public string PreCode { get; set; }
        public string Num4 { get; set; }
        public string Num5 { get; set; }
        public string Num6 { get; set; }
        public string Num7 { get; set; }
        public string Num8 { get; set; }
        public string Num9 { get; set; }
        public string Num10 { get; set; }
        public long MinPrice { get; set; }
        public long MaxPrice { get; set; }

        public long LastLoadedId { get; set; }
    }


    public class SearchPackFilter
    {
        public PackType PackType { get; set; }

        public string PreCode { get; set; }
        public long LastLoadedId { get; set; }
        public long MaxPrice { get; set; }
        public long MinPrice { get; set; }
    }
}
