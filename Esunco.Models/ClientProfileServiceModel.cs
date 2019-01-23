using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class ClientProfileServiceModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname
        {
            get
            {
                var result = string.Format("{0} {1}", Firstname, Lastname);
                if (String.IsNullOrWhiteSpace(result))
                    return "";
                return result;
            }
        }
        public long Credit { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string NationalCode { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string OfficeCode { get; set; }
        public string Token { get; set; }

        public bool IsCompleted
        {
            get
            {
                return !new string[] { Firstname, Lastname, NationalCode, Mobile }.Any(c => String.IsNullOrWhiteSpace(c));
            }
        }

        public AccountType AccountType { get; set; }

    }
}
