using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class UserModel : BaseModel
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "نام نمایشی")]
        public string DisplayName
        {
            get
            {
                return Username;
            }
        }

        [Display(Name = "نقش کاربر")]
        public Role Role { get; set; }

        [Display(Name = "آخرین زمان ورود")]
        public DateTime LastLoggedInTime { get; set; }

        [Display(Name = "وضعیت")]
        public UserStatus Status { get; set; }

      
    }
}
