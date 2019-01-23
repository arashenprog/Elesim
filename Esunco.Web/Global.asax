<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        //
        Esunco.Logics.Mapper.Configure();
        new Esunco.Logics.Setup().CreateInitialUsers();
    }



    void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.Ignore("{resource}.ashx/{*pathInfo}");

        routes.MapPageRoute("", "", "~/View/Default.aspx");
        routes.MapPageRoute("Home", "Home", "~/View/Default.aspx");
        routes.MapPageRoute("Login", "Login", "~/View/Shared/login.aspx");
        routes.MapPageRoute("route_0", "{*fullpath}", "~/View/{*fullpath}");

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        if (Esunco.Logics.Settings.DEBUG_MODE)
        {
            AcoreX.Security.AccountManager.SignIn("Admin", true);
            return;
        }

        //if (AcoreX.Web.CookieManager.GetValue<int>(Esunco.Web.WebIdentityProvider.STAY_SIGNED_KEY, 0).Equals(1))
        //{
        //    string username = AcoreX.Web.CookieManager.GetValue(Esunco.Web.WebIdentityProvider.USER_NAME_KEY, true);
        //    AcoreX.Security.AccountManager.SignIn(username, true);
        //}
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
