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
using AcoreX.Helper;
using AcoreX.Utility;
using Esunco.Models.Enum;

public partial class View_Sims_PackEdit : ASPxBasePage
{
    PackModel pack;
    protected void Page_Init(object sender, EventArgs e)
    {
        using (var ctx = new SimContext())
        {
            pack = ctx.FindPack(Request["PackID"].DefaultIfNull<long>(-1));
            if (pack == null)
            {
                throw new HttpNotFoundException();
            }
            if(!IsPostBack)
            {
                tbxTitle.Text = pack.Title;
                tbxPrice.Number = pack.Price;
                tbxCode.Text = pack.Code;
                cbxPackType.DataBind();
                cbxPackType.Value = (int)pack.Type;
                tbxStatus.Value = EnumHelper.GetEnumDescription(pack.Status);
            }
        }
    }

    protected override void OnCallback(CallbackArgs e)
    {
        switch (e.Action)
        {
            case "Delete":
                {
                    using (var ctx = new SimContext())
                    {
                        ctx.DeletePack(pack.ID);
                    }
                    break;
                }
            case "Publish":
                {
                    using (var ctx = new SimContext())
                    {
                        ctx.PublishPack(pack.ID);
                        tbxStatus.Value = EnumHelper.GetEnumDescription(SimPackStatus.Published);
                    }
                    break;
                }
            case "Save":
                {
                    using (var ctx = new SimContext())
                    {
                        pack.Price = (long)tbxPrice.Number;
                        pack.Title = tbxTitle.Text.Trim();
                        pack.Code = tbxCode.Text;
                        pack.Type = cbxPackType.GetValue<PackType>();
                        ctx.SavePack(pack, grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                }
            default:
                break;
        }

    }



    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new SimContext())
        {
            e.Data = ctx.GetPackItemsList(pack.ID);
        }
    }

    protected void dsPackType_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = EnumHelper.GetEnumList<PackType>();
    }
}