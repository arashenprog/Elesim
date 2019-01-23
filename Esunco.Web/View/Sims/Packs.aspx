<%@ Page Title="مدیریت پک ها" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Packs.aspx.cs" Inherits="View_Sims_Packs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {
                case "EditPage":
                    {
                        window.location = "PackEdit.aspx?packID=" + grid.GetFocusedRowKey();
                    }
                default:
            }
        }
        function OnRowDbClick(s, e) {
            window.location = "PackEdit.aspx?packID=" + grid.GetFocusedRowKey();
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <dx:ASPxMenu runat="server" ID="menu">
        <ClientSideEvents ItemClick="OnItemClick" />
        <Items>
            <dx:MenuItem Text="ویرایش" Name="EditPage">
                <Image IconID="edit_edit_16x16" />
            </dx:MenuItem>
            <dx:MenuItem Text="حذف" Name="Delete">
                <Image IconID="actions_deletelist_16x16">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="بروزرسانی" Name="Refresh" BeginGroup="true">
                <Image IconID="actions_refresh_16x16">
                </Image>
            </dx:MenuItem>

        </Items>
    </dx:ASPxMenu>
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnChanged="gvx_OnChanged" OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.PackModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowEdit="false" />
        <SettingsEditing Mode="Inline" />
        <ClientSideEvents RowDblClick="OnRowDbClick" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0" />
            <dx:GridViewDataTextColumn FieldName="Title" Width="300" VisibleIndex="1" />
            <dx:GridViewDataSpinEditColumn FieldName="Price" Width="200" PropertiesSpinEdit-MinValue="10000" PropertiesSpinEdit-MaxValue="9999999999" PropertiesSpinEdit-DecimalPlaces="0" PropertiesSpinEdit-DisplayFormatString="#,###">
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000"></PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Type" Width="120" EnumTypeName="Esunco.Models.Enum.PackType, Esunco.Models" ReadOnly="true">
                <PropertiesComboBox TextField="Value" ValueField="Key"></PropertiesComboBox>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Code" Width="200">
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="CreateTime" Width="120">
                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Status" Width="100" EnumTypeName="Esunco.Models.Enum.SimPackStatus, Esunco.Models" />
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" ShowUpdateButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
    </dx:ASPxGridView>



</asp:Content>

