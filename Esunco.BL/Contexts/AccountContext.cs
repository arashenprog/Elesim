using AcoreX.Data.Repository;
using AcoreX.Helper;
using AcoreX.Security;
using AcoreX.Utility;
using AcoreX.Utility.Globalization;
using AcoreX.Utility.Persian;
using Esunco.Models;
using Esunco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.ComponentModel.DataAnnotations;

namespace Esunco.Logics.Contexts
{

    public enum ClientDisplayFilter
    {
        [Display(Name = "همه")]
        All = 0,
        [Display(Name = "لیست سفید")]
        WhiteList = 1,
        [Display(Name = "لیست سیاه")]
        BlackList = 2
    }

    /// <summary>
    /// کلاس عملیات حساب های کاربری
    /// </summary>
    public class AccountContext : BaseContext
    {
        #region Cunstructors
        public AccountContext()
        {
        }

        internal AccountContext(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion


        #region  User

        public bool Authenticat(string username, string password)
        {
            using (var rep = new Repository<UserDataEntity>(base.UnitOfWork))
            {
                var user = rep.Items.FirstOrDefault(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new LoginFailedException();
                }
                if (user.Password != AcoreX.Security.Encryptor.GeneratePassword(password, user.PasswordSalt))
                {
                    throw new LoginFailedException();
                }

                user.LastActivityTime = DateTime.Now;
                base.UnitOfWork.SaveChanges();
                return true;
            }
        }

        public PaginatedList<UserModel> GetUserList()
        {
            using (var rep = new Repository<UserDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<UserModel>(rep.Items.ProjectTo<UserModel>());
            }
        }

        public UserModel NewUser()
        {
            var model = new UserModel();
            return model;
        }

        public UserModel SaveUser(UserModel model)
        {
            using (var rep = new Repository<UserDataEntity>(UnitOfWork))
            {
                var entity = rep.Items.FirstOrDefault(c => c.ID == model.ID);
                if (entity == null)
                {
                    entity = new UserDataEntity();
                    entity.RegisterTime =
                    entity.LastActivityTime = DateTime.Now;
                    entity.Username = model.Username;
                    if (string.IsNullOrWhiteSpace(model.Password))
                        throw new RequiredFieldException<UserModel>(c => c.Password);
                }
                entity.RoleID = (byte)model.Role;
                entity.Enabled = model.Status == Models.Enum.UserStatus.Enabled;
                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    var salt = AcoreX.Security.Encryptor.GenerateSalt();
                    entity.PasswordSalt = salt;
                    entity.Password = AcoreX.Security.Encryptor.GeneratePassword(model.Password, salt);
                }
                rep.Save(entity);
                UnitOfWork.SaveChanges();
                return FindUser(entity.ID);
            }
        }

        public UserModel FindUser(long id)
        {
            return base.Find<UserModel, UserDataEntity>(id);
        }

        public UserModel FindUser(string username)
        {
            return base.Find<UserModel, UserDataEntity>(c => c.Username == username);
        }

        public void DeleteUser(params long[] list)
        {
            base.Delete<UserDataEntity>(list);
        }


        #endregion

        #region  Client

        public PaginatedList<ClientModel> GetClientList(ClientDisplayFilter filter)
        {
            using (var rep = new Repository<ClientDataEntity>(base.UnitOfWork))
            {
                var all = filter == ClientDisplayFilter.All;
                var blackList = filter == ClientDisplayFilter.BlackList;
                var whiteList = filter == ClientDisplayFilter.WhiteList;

                return new PaginatedList<ClientModel>(rep.Items.Where(c =>
                     all ||
                    (
                        (blackList && c.Blocked) ||
                        (whiteList && !c.Blocked))
                    ).ProjectTo<ClientModel>());
            }
        }

        public PaginatedList<ClientModel> GetClientBlockedList()
        {
            using (var rep = new Repository<ClientDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<ClientModel>(rep.Items.Where(c => c.Blocked).ProjectTo<ClientModel>());
            }
        }

        public void BlockedUsers(params long[] list)
        {
            using (var rep = new Repository<ClientDataEntity>(base.UnitOfWork))
            {
                rep.Items.Where(c => list.Contains(c.ID)).ForEach(c => c.Blocked = true);
                UnitOfWork.SaveChanges();
            }
        }

        public void UnblockBlockedUsers(params long[] list)
        {
            using (var rep = new Repository<ClientDataEntity>(base.UnitOfWork))
            {
                rep.Items.Where(c => list.Contains(c.ID)).ForEach(c => c.Blocked = false);
                UnitOfWork.SaveChanges();
            }
        }

        public ClientModel NewClient()
        {
            var model = new ClientModel();
            return model;
        }

        public ClientModel SaveClient(ClientModel model)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                var entity = repClient.Items.FirstOrDefault(c => c.ID == model.ID);
                if (entity == null)
                {
                    entity = new ClientDataEntity();
                }
                model.CopyTo(entity);
                entity.Blocked = model.BlackList == Models.Enum.YesNo.Yes;
                entity.AccountType = (byte)model.AccountType;
                repClient.Save(entity);
                UnitOfWork.SaveChanges();
                return FindClient(entity.ID);
            }
        }

        public ClientModel FindClient(long id)
        {
            return base.Find<ClientModel, ClientDataEntity>(id);
        }

        public void DeleteClient(params long[] list)
        {
            base.Delete<ClientDataEntity>(list);
        }


        public int SendLoginSMS(string mobile)
        {
            using (var ctx = new Repository<ClientDataEntity>(UnitOfWork))
            {
                if (!Validation.IsValidIRMobile(mobile))
                    throw new HandledException("شماره موبایل صحیح نمی باشد.");

                var item = ctx.Items.FirstOrDefault(c => c.Mobile == mobile);
                if (item == null)
                    throw new HandledException("شماره همراه شما در سیستم ثبت نشده است. لطفا ثبت نام کنید.");
                TimeSpan diff = DateTime.Now - item.SMSCodeCreatedTime;
                if (diff.TotalSeconds < 60)
                {
                    return item.SMSCode;
                }
                item.SMSCodeCreatedTime = DateTime.Now;
                item.SMSCode = AcoreX.Security.Token.GenerateKeyPass();
                ctx.Save(item);
                UnitOfWork.SaveChanges();
                SendSMS(item.Mobile, item.SMSCode);
                return item.SMSCode;
            }
        }


        private void SendSMS(string mobile, int code)
        {
            string message = String.Format("کد فعال سازی شما در اِلِسیم: {0}", code);
            TSMSService.tsmsServiceClient soap = new TSMSService.tsmsServiceClient();
            int[] result = soap.sendSms(Settings.SMS_USERNAME, Settings.SMS_PASSWORD, new string[] { Settings.SMS_NUMBER }, new string[] { mobile }, new string[] { message }, new string[] { }, "");
            var a = result.ToList().First();
        }



        public string GetRegisterationToken(string mobile, int code)
        {
            using (var ctx = new Repository<ClientDataEntity>(UnitOfWork))
            {
                if (String.IsNullOrWhiteSpace(mobile) || code <= 0)
                    throw new InvalidTokenException();

                var exists = ctx.Items.FirstOrDefault(c => c.Mobile == mobile && c.SMSCode == code);
                if (exists == null)
                {
                    throw new InvalidTokenException();
                }
                exists.Token = AcoreX.Security.Token.GenerateKey(DateTime.Now);
                ctx.Save(exists);
                UnitOfWork.SaveChanges();
                return exists.Token;
            }
        }

        public void Register(ClientProfileServiceModel model)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                if (!Validation.IsValidIRNationalCode(model.NationalCode))
                {
                    throw new HandledException("شماره ملی وارد شده صحیح نمی باشد.");
                }
                if(!Validation.IsValidIRMobile(model.Mobile))
                {
                    throw new HandledException("شماره تلفن همراه وارد شده صحیح نمی باشد.");
                }
                if (String.IsNullOrEmpty(model.Firstname) || String.IsNullOrEmpty(model.Lastname))
                {
                    throw new HandledException("نام یا نام خانوادگی وارد شده صحیح نمی باشد.");
                }
                if (repClient.Items.Any(c => c.Mobile == model.Mobile))
                    throw new HandledException("این شماره قبلا در سیستم ثبت شده است.");
                var client = new ClientDataEntity();
                client.Mobile = model.Mobile;
                client.Firstname = model.Firstname;
                client.Lastname = model.Lastname;
                client.NationalCode = model.NationalCode;
                client.SMSCodeCreatedTime = DateTime.Now;
                client.SMSCode = AcoreX.Security.Token.GenerateKeyPass();
                repClient.Save(client);
                UnitOfWork.SaveChanges();
                SendSMS(client.Mobile, client.SMSCode);
            }
        }

        public void SaveProfile(ClientProfileServiceModel model)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                var client = repClient.Items.FirstOrDefault(c => c.Mobile == model.Mobile && c.Token == model.Token);
                if (client == null)
                    throw new NotExistsAccountException();
                if (String.IsNullOrEmpty(model.Firstname) || String.IsNullOrEmpty(model.Lastname))
                {
                    throw new HandledException("نام یا نام خانوادگی وارد شده صحیح نمی باشد.");
                }
                if (!Validation.IsValidIRNationalCode(model.NationalCode))
                {
                    throw new HandledException("شماره ملی وارد شده صحیح نمی باشد.");
                }
                client.Firstname = model.Firstname;
                client.Lastname = model.Lastname;
                client.NationalCode = model.NationalCode;
                client.Phone = model.Phone;
                client.Address = model.Address;
                client.PostalCode = model.PostalCode;
                client.Email = model.Email;
                repClient.Save(client);
                UnitOfWork.SaveChanges();
            }
        }

        public ClientProfileServiceModel GetClientByToken(string token)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new NotExistsAccountException();
                var client = repClient.Items.FirstOrDefault(c => c.Token == token);
                if (client == null)
                    throw new NotExistsAccountException();
                var result = client.MapTo<ClientProfileServiceModel>();
                result.AccountType = (Models.Enum.AccountType)client.AccountType;
                result.Token = token;
                return result;
            }
        }



        #endregion



    }
}
