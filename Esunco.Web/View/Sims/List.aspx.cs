using AcoreX.DXUtil.GridViewExtender;
using AcoreX.DXUtil.Web;
using Esunco.Logics.Contexts;
using Esunco.Models;
using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcoreX.Helper;
using AcoreX.Utility.Persian;

public partial class View_Sims_List : ASPxBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbxAuctionStartTime.Text = PersianDate.Now.ToDateTimeString();
            tbxAuctionFinishTime.Text = PersianDate.Now.AddDays(1).ToDateTimeString();
            using (var ctx = new SimContext())
            {
                cbxPacks.DataSource = ctx.GetNewPackList();
                cbxPacks.DataBind();
            }
        }
    }

    protected override void OnCallback(CallbackArgs e)
    {
        using (var ctx = new SimContext())
        {
            switch (e.Action)
            {
                case "Pack":
                    {

                        var pack = ctx.AddNewPack(tbxTitle.Text, cbxPackType.GetValue<PackType>(), tbxPackCode.Text, grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        RedirectCallback("PackEdit.aspx?PackID=" + pack.ID);
                        break;
                    }

                case "AddToPack":
                    {
                        var pack = ctx.AddToPack(cbxPacks.GetValue<long>(), grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        RedirectCallback("PackEdit.aspx?PackID=" + pack.ID);
                        break;
                    }

                case "Rond":
                    {
                        ctx.MarkAsRondSims((long)tbxRondPrice.Number, grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                case "Price":
                    {
                        ctx.SetPrice((long)tbxPrice.Number, grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                case "Auction":
                    {
                        var auction = ctx.AddNewAuction(
                            tbxAuctionTitle.Text,
                            (long)tbxAuctionPrice.Number,
                            PersianDate.Parse(tbxAuctionStartTime.Text),
                            PersianDate.Parse(tbxAuctionFinishTime.Text),
                            grid.GetSelectedKeyFieldValues<long>(-1).ToArray()
                        );
                        RedirectCallback("AuctionEdit.aspx?ID=" + auction.ID);
                        break;
                    }
                case "Normal":
                    {
                        ctx.UndoRondSims(grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                case "Import":
                    {
                        var a = uplFile.UploadedFiles;
                        break;
                    }
                case "Published":
                    {
                        ctx.MarkAsPublished(grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                case "Unpublished":
                    {
                        ctx.MarkAsUnpublished(grid.GetSelectedKeyFieldValues<long>(-1).ToArray());
                        grid.DataBind();
                        break;
                    }
                default:
                    break;
            }
        }
    }

    protected void gvx_OnChanged(object sender, ASPxRowUpdateEventArgs e)
    {
        using (var ctx = new SimContext())
        {
            foreach (var item in e.InsertedItems)
            {
                ctx.SaveSim(item as SimModel);
            }
            foreach (var item in e.UpdatedItems)
            {
                ctx.SaveSim(item as SimModel);
            }
            foreach (var item in e.DeletedKeys)
            {
                ctx.DeleteSim((long)item);
            }
        }
    }

    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new SimContext())
        {
            var numberType = (NumberType?)rblNumberType.Value.DefaultIfNull<byte?>(null);
            var simType = (SimType?)rblSimType.Value.DefaultIfNull<byte?>(null);
            var status = (SimPackStatus?)rblVisible.Value.DefaultIfNull<byte?>(null);
            var data = ctx.GetSimList(new SimDisplayFilter { SimType = simType, NumberType = numberType, Status = status, query = tbxSearch.Text.Trim() });
            e.Data = data;
        }
    }
    protected void dsSimFilter_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = AcoreX.Helper.EnumHelper.GetEnumList<NumberType>();
    }
    protected void uplFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        e.CallbackData = "true";
        using (var ctx = new SimContext())
        {
            var price = hdSimPrice.Value.DefaultIfNull(0);
            var buyPrice = hdSimBuyPrice.Value.DefaultIfNull(0);
            ctx.Import(tbxCode.Text, buyPrice, price, e.UploadedFile.FileBytes, (hdImportType.Value == "0" ? SimType.PrePaid : SimType.PostPaid));
        }
    }

    protected void dsPackType_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = EnumHelper.GetEnumList<PackType>();
    }
}