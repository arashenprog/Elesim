
using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PackServiceModel : BaseModel
    {
        public string Title { get;  set; }

        public long Price { get;  set; }

        public DateTime CreateTime { get;  set; }

        public List<string> Numbers { get;  set; }
        public PackType Type { get;  set; }

    }
}
