using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public abstract class BaseModel
    {
       [Display(Name = "شناسه")]
        public long ID { get; set; }
    }
}
