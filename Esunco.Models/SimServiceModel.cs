using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class SimServiceModel : BaseModel
    {
        public string Number { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public long Price { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}
