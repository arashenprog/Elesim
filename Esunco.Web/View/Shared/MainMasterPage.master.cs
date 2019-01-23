using AcoreX.DXUtil.Web;
using AcoreX.Security;
using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Shared_MainMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {

        if (!IsPostBack)
            UserMenu.Items[0].Text = AcoreX.Security.AccountManager.User.DisplayName;
        //
        var current = ASPxBasePage.GetCurrent(this.Page);
        //MainMenu.Items.FindByName("Configuration").Visible = current.User.HasPermission(Permissions.VIEW_CONFIG);


        //if (!IsPostBack)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var tab = new TabPage();
        //        tab.Name = "Tab" + i;
        //        tab.ClientVisible = false;
        //        tab.ClientEnabled = true;
        //        ASPxPageControl1.TabPages.Add(tab);
        //    }
        //}

    }


    protected void MasterCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
    {
        var current = ASPxBasePage.GetCurrent(this.Page);
        if (current != null)
        {
            current.ProcessPanelCallback(this.MasterCallbackPanel, e);
        }
        if (e.Parameter == "Redirect")
        {

        }
    }
}
