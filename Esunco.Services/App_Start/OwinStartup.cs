﻿using System;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OAuth20;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth20
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAuthorizationServerProvider()
                {
                    OnValidateClientAuthentication = async (context) =>
                    {
                        context.Validated();
                    },
                    OnGrantResourceOwnerCredentials = async (context) =>
                    {
                        if (context.UserName == "test.test@mail.com" && context.Password == "test123")
                        {
                            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                            context.Validated(oAuthIdentity);
                        }
                    }
                },
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
        }
    }
}