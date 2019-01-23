using Esunco.Logics.Contexts;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Logics.TaskJobs
{



    public class UpdateSimStatusJob : IJob
    {
        public void Execute()
        {
            using (var ctx = new ServiceContext())
            {
                ctx.UpdateSimStatus();
            }
        }
    }
}
