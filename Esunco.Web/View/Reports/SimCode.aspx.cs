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
using AcoreX.Utility.Persian;
using DevExpress.Web;

public partial class View_Reports_SimCode : ASPxBasePage
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
        using (var ctx = new ReportContext())
        {
            var startDate = (DateTime)PersianDate.Parse(hdStartDate.Value);
            var finishDate = (DateTime)PersianDate.Parse(hdFinishDate.Value);
            var data = ctx.GetSimCodeList(startDate, finishDate);
            e.Data = data;
        }
    }


}