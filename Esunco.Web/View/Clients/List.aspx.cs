using AcoreX.DXUtil.GridViewExtender;
using AcoreX.DXUtil.Web;
using DevExpress.Web;
using Esunco.Logics.Contexts;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcoreX.Helper;

public partial class View_Clients_List : ASPxBasePage
{


    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected override void OnCallback(CallbackArgs e)
    {
        switch (e.Action)
        {
            case "Blocked":
                {
                    using (var ctx = new AccountContext())
                    {
                        ctx.BlockedUsers(grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                }
            case "Unblock":
                {
                    using (var ctx = new AccountContext())
                    {
                        ctx.UnblockBlockedUsers(grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                }
            default:
                break;
        }
    }

    protected void gvx_OnChanged(object sender, ASPxRowUpdateEventArgs e)
    {
        using (var ctx = new AccountContext())
        {
            foreach (var item in e.InsertedItems)
            {
                ctx.SaveClient(item as ClientModel);
            }
            foreach (var item in e.UpdatedItems)
            {
                ctx.SaveClient(item as ClientModel);
            }
            foreach (var item in e.DeletedKeys)
            {
                ctx.DeleteClient((long)item);
            }
        }
    }

    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new AccountContext())
        {

            var selected = (ClientDisplayFilter)hdFilter.Value.DefaultIfNull<int>(0);
            var data = ctx.GetClientList(selected);
            e.Data = data;
        }
    }


    protected void dsClientFilter_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = AcoreX.Helper.EnumHelper.GetEnumList<ClientDisplayFilter>();
    }
    protected void dsGrid_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        using (var ctx = new AccountContext())
        {
            var selected = (ClientDisplayFilter)hdFilter.Value.DefaultIfNull<int>(0);
            var data = ctx.GetClientList(selected);
            e.Result = data;
        }
    }
}