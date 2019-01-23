using System;
using System.Collections.Generic;


namespace Esunco.Models
{
    public class AuctionServiceModel : BaseModel
    {
        public long BasePrice { get; set; }
        public long YourPrice { get; set; }

        public long MaxPrice { get;  set; }

        public string Title { get;  set; }
        public string Province { get;  set; }

        public List<string> Numbers { get;  set; }

        public DateTime StartTime { get;  set; }

        public long TotalLeftSeconds { get;  set; }

        public bool IsWinner { get; set; }
    }
}
