using AcoreX.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcoreX.Helper;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using System.Data.Entity;
using AcoreX.TKUtil.Data;
using System.Configuration;

namespace Esunco.Data
{
    public class TKDataContext : TelerikDataContext
    {

        public TKDataContext()
            : base(new EntityModel(), ConfigurationManager.ConnectionStrings["EsuncoConnection"].ConnectionString)
        {

        }
    }
}
