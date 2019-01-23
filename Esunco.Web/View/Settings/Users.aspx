<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="View_Settings_Users" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {
                //case "New":
                //    grid.AddNewRow();
                //    break;
                //case "Edit":
                //    grid.EditFocusedRow();
                //    break;
                //case "Delete":
                //    Master.DXUtil.GridDeleteSelected(grid, function () {
                //        grid.DeleteSelectedRows();
                //    });
                //    break;
                //case "Refresh":
                //    grid.Refresh();
                //    break;
                //default:

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <dx:ASPxMenu runat="server" ID="menu">
        <ClientSideEvents ItemClick="OnItemClick" />
        <Items>
            <dx:MenuItem Text="جدید" Name="New">
                <Image IconID="actions_additem_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="ویرایش" Name="Edit">
                <Image IconID="edit_edit_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="حذف" Name="Delete">
                <Image IconID="actions_deletelist_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="بروزرسانی" Name="Refresh" BeginGroup="true">
                <Image IconID="actions_refresh_16x16office2013">
                </Image>
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
    <ax:ASPxGridViewExtender runat="server" GridViewID="ASPxGridView1" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnChanged="gvx_OnChanged" OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.UserModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid" OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0"  />
            <dx:GridViewDataTextColumn FieldName="Username"  VisibleIndex="1" Width="200">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-Display="Dynamic"/>
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Role"  VisibleIndex="2" Width="200" EnumTypeName="Esunco.Models.Enum.Role, Esunco.Models">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-Display="Dynamic"/>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Password"  VisibleIndex="2" PropertiesTextEdit-Password="true" Width="200">
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Status" EnumTypeName="Esunco.Models.Enum.UserStatus, Esunco.Models" VisibleIndex="3" Width="100">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true"  ValidationSettings-Display="Dynamic"/>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" ShowUpdateButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />

    </dx:ASPxGridView>


</asp:Content>

