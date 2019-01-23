using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcoreX.Security;
using System.Web.SessionState;
using AcoreX.Web;
using Esunco.Logics.Contexts;

namespace Esunco.Web
{
    public class WebIdentityProvider : IdentityProvider
    {
        const string USER_SESSION_KEY = "8A06F24E481465321547BG6FU5465AD";

        public const string USER_NAME_KEY = "451EssadsdG32130";
        public const string STAY_SIGNED_KEY = "8731JHKggbJK454654";

        public WebIdentityProvider()
        {
        }

        public override Identity User
        {
            get
            {
                Identity user = HttpContext.Current.Session[USER_SESSION_KEY] as Identity;
                if (user == null)
                    user = new Identity(this);
                return user;
            }
            set
            {
                HttpContext.Current.Session[USER_SESSION_KEY] = value;
            }
        }


        public override bool Authenticate(string username, string password)
        {
            using (AccountContext ctx = new AccountContext())
            {
                return ctx.Authenticat(username, password);
            }
        }

        public override void SignIn(string username, bool staySignedIn)
        {
            using (AccountContext ctx = new AccountContext())
            {
                var user = ctx.FindUser(username);
                if (user != null)
                {
                    this.User = new Identity(this) { Name = username, UserID = user.ID.ToString(), DisplayName = user.DisplayName, UserSourceObject = user };
                }
            }

        }

        public override void SignOut()
        {
            this.User = null;
            var response = HttpContext.Current.Response;
            response.Buffer = true;
            response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            response.Expires = -1500;
            response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.Cache.SetNoStore();
            response.CacheControl = "no-cache";
            response.Redirect("~/Login");
        }

        public override bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.Session[USER_SESSION_KEY] != null;
            }
        }


        public override bool IsUserInRoles(params IRole[] roles)
        {
            //return roles.Any(r => r.Id == (User.UserSourceObject as UserDataEntity).RoleID);
            return true;
        }

        public override IRole[] GetRoles()
        {
            throw new NotImplementedException();
        }

        public override bool HasPermission(string permission)
        {
            //if (!IsAuthenticated)
            //    return false;

            //var user = (User.UserSourceObject as UserDataEntity);
            //var val = user.RoleDataEntity.GetType().GetProperties().FirstOrDefault(c => c.Name == permission);
            //return val != null && (bool)val.GetValue(user.RoleDataEntity, null) == true;

            return true;
        }

        public override bool HasExternalLogin(string externalProvider = null)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRoles(params string[] roleNames)
        {
            throw new NotImplementedException();
        }
    }
}