using AcoreX.DXUtil.Web;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Shared_PopupMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void MasterCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
    {
        var current = ASPxBasePage.GetCurrent(this.Page);
        if (current != null)
        {
            current.ProcessPanelCallback(this.MasterCallbackPanel, e);
        }
    }
}
