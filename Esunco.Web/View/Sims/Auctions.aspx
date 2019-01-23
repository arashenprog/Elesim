<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Auctions.aspx.cs" Inherits="View_Sims_Auctions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {
                case "EditPage":
                    {
                        window.location = "AuctionEdit.aspx?ID=" + grid.GetFocusedRowKey();
                    }
                default:
            }
        }
        function OnRowDbClick(s,e) {
            window.location = "AuctionEdit.aspx?ID=" + grid.GetFocusedRowKey();
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
        <DataSource OnChanged="gvx_OnChanged" OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.AuctionModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowEdit="false" />
        <SettingsEditing Mode="Inline" />
        <ClientSideEvents RowDblClick="OnRowDbClick" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0" />
            <dx:GridViewDataTextColumn FieldName="Title" Width="200px" VisibleIndex="1" />
            <dx:GridViewDataTextColumn FieldName="Numbers" Width="100%" VisibleIndex="1" />
            <dx:GridViewDataSpinEditColumn FieldName="BasePrice" Width="120" >
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000"></PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="StartTime" Width="120">
                <PropertiesTextEdit DisplayFormatString="HH:mm yyyy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="FinishTime" Width="120">
                <PropertiesTextEdit DisplayFormatString="HH:mm yyyy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
             <dx:GridViewDataSpinEditColumn FieldName="MaxPrice" Width="120" >
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000"></PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
             <ax:ASPxGridViewEnumComboBoxColumn FieldName="Status" Width="100" EnumTypeName="Esunco.Models.Enum.AuctionStatus, Esunco.Models" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
    </dx:ASPxGridView>



</asp:Content>

