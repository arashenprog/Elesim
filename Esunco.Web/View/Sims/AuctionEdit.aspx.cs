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
using AcoreX.Utility.Persian;
using AcoreX.DateTimeCalendar;
using Esunco.Models.Enum;

public partial class View_Sims_AuctionEdit : ASPxBasePage
{
    AuctionModel auction;
    protected void Page_Init(object sender, EventArgs e)
    {
        using (var ctx = new SimContext())
        {
            auction = ctx.FindAuction(Request["ID"].DefaultIfNull<long>(-1));
            if (auction == null)
            {
                throw new HttpNotFoundException();
            }
            if (!IsPostBack)
            {
                tbxNumbers.Text = auction.Numbers;
                tbxTitle.Text = auction.Title;
                tbxPrice.Number = auction.BasePrice;
                tbxStartTime.Text = auction.StartTime.ToPersian().ToDateTimeString();
                tbxStatus.Value = EnumHelper.GetEnumDescription(auction.Status);
                if (DateTime.Now > auction.StartTime)
                {
                    tbxStartTime.ReadOnly = true;
                }
                tbxFinishTime.Text = auction.FinishTime.ToPersian().ToDateTimeString();
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
                        ctx.DeleteAuction(auction.ID);
                    }
                    break;
                }
            case "Publish":
                {
                    using (var ctx = new SimContext())
                    {
                        ctx.PublishAuction(auction.ID);
                        tbxStatus.Value = EnumHelper.GetEnumDescription(AuctionStatus.Published);
                    }
                    break;
                }
            case "Save":
                {
                    using (var ctx = new SimContext())
                    {
                        auction.BasePrice = (int)tbxPrice.Number;
                        auction.StartTime = PersianDate.Parse(tbxStartTime.Text);
                        auction.FinishTime = PersianDate.Parse(tbxFinishTime.Text);
                        auction.Title = tbxTitle.Text.Trim();
                        ctx.SaveAuction(auction);
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
            e.Data = ctx.GetAuctionBidList(auction.ID);
        }
    }
    protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.CellValue.DefaultIfNull<long>(-1) == auction.MaxPrice)
        {
            e.Cell.ForeColor = System.Drawing.Color.Red;
        }
    }
}