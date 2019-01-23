using AcoreX.Data.Repository;
using Esunco.Data;
using Esunco.Logics.TaskJobs;
using Esunco.Models.Enum;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Logics
{
    public class Setup
    {

        public static void StartTasks()
        {
            var registry = new Registry();
            registry.Schedule<UpdateAuctionStatusJob>().ToRunNow().AndEvery(2).Minutes();
            registry.Schedule<UpdateSimStatusJob>().ToRunNow().AndEvery(5).Minutes();
            JobManager.Initialize(registry);
        }

        public void CreateInitialUsers()
        {
            var uow = new UnitOfWork(new TKDataContext());

            using (var rep = new Repository<UserDataEntity>(uow))
            {
                var salt = AcoreX.Security.Encryptor.GenerateSalt();
                var admin = new UserDataEntity()
                {
                    RoleID = (byte)Role.Administrator,
                    LastActivityTime = DateTime.Now,
                    RegisterTime = DateTime.Now,
                    PasswordSalt = salt,
                    Password = AcoreX.Security.Encryptor.GeneratePassword("admin", salt),
                    Username = "Admin"
                };
                if (!rep.Items.Any(c => c.Username == admin.Username))
                    rep.Save(admin);
                //
                var user = new UserDataEntity()
                {
                    RoleID = (byte)Role.Administrator,
                    RegisterTime = DateTime.Now,
                    LastActivityTime = DateTime.Now,
                    PasswordSalt = salt,
                    Password = AcoreX.Security.Encryptor.GeneratePassword("123", salt),
                    Username = "user"
                };
                if (!rep.Items.Any(c => c.Username == user.Username))
                    rep.Save(user);
                //
                uow.SaveChanges();


            }
        }
    }
}
