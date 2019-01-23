using AcoreX.DXUtil.GridViewExtender;
using AcoreX.DXUtil.Web;
using Esunco.Logics.Contexts;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Sims_Auctions : ASPxBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnCallback(CallbackArgs e)
    {
        using (var ctx = new SimContext())
        {
            switch (e.Action)
            {
               
                default:
                    break;
            }
        }
    }

    protected void gvx_OnChanged(object sender, ASPxRowUpdateEventArgs e)
    {
        using (var ctx = new SimContext())
        {

            foreach (var item in e.DeletedKeys)
            {
                ctx.DeleteAuction((long)item);
            }
        }
    }

    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new SimContext())
        {
            e.Data = ctx.GetAuctionList();
        }
    }
}