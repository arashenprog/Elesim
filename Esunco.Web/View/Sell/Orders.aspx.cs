﻿using AcoreX.DXUtil.GridViewExtender;
using AcoreX.DXUtil.Web;
using Esunco.Logics.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcoreX.Helper;
using DevExpress.Web;
using AcoreX.Utility.Persian;

public partial class View_Sell_Orders : ASPxBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var menuFilter = menu.Items.FindByName("Filter");
            var tbxStartDate = (ASPxTextBox)menuFilter.FindControl("tbxStartDate");
            var tbxFinishDate = (ASPxTextBox)menuFilter.FindControl("tbxFinishDate");
            hdStartDate.Value = tbxStartDate.Text = PersianDate.Now.FirstDayOfTheMonth.ToDateString();
            hdFinishDate.Value = tbxFinishDate.Text = PersianDate.Now.ToDateString();
        }
    }

    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new OrderContext())
        {
            var selected = (OrderDisplayFilter)hdFilter.Value.DefaultIfNull<int>(1);
            var startDate = (DateTime)PersianDate.Parse(hdStartDate.Value);
            var finishDate = (DateTime)PersianDate.Parse(hdFinishDate.Value);
            var data = ctx.GetOrderList(startDate, finishDate, selected);
            e.Data = data;
        }
    }



    protected void dsFilter_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = AcoreX.Helper.EnumHelper.GetEnumList<OrderDisplayFilter>();
    }
}