using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcoreX.DXUtil.Web;
using Esunco.Logics.Contexts;
using Esunco.Models;
using DevExpress.Web;
using AcoreX.DXUtil.GridViewExtender;

public partial class View_Settings_Users : ASPxBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
          
        }
    }


    protected void dsRoles_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
    {
        e.Result = AcoreX.Helper.EnumHelper.GetEnumList<Esunco.Models.Enum.Role>();
    }




    protected void gvx_OnChanged(object sender, ASPxRowUpdateEventArgs e)
    {
        using (var ctx = new AccountContext())
        {
            foreach (var item in e.InsertedItems)
            {
                ctx.SaveUser(item as UserModel);
            }
            foreach (var item in e.UpdatedItems)
            {
                ctx.SaveUser(item as UserModel);
            }
            foreach (var item in e.DeletedKeys)
            {
                ctx.DeleteUser((long)item);
            }
        }
    }

    protected void gvx_DateBind(object sender, ASPxDataBindEventArgs e)
    {
        using (var ctx = new AccountContext())
        {
            e.Data = ctx.GetUserList();
        }
    }
    protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (!ASPxGridView1.IsEditing)
            return;
        if(e.Column.FieldName=="Username" && !ASPxGridView1.IsNewRowEditing)
        {
            e.Editor.ReadOnly = true;
        }
    }

}