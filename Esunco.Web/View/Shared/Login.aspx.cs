using AcoreX.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcoreX.Helper;

public partial class View_Shared_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMessage.Text = "";
            if (Request["do"].DefaultIfNull("").Equals("signout", StringComparison.OrdinalIgnoreCase))
            {
                AccountManager.SignOut();
            }

            if (AccountManager.IsAuthenticated)
            {
                Response.Redirect("~/#Home");
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (AccountManager.Authenticate(tbxUsername.Text.Trim(), tbxPassword.Text.Trim()))
            {
                AccountManager.SignIn(tbxUsername.Text.Trim(), false);
                Response.Redirect("~/#Home");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}