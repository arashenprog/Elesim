<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="View_Sell_Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function rblFilter_SelectedIndexChanged(s, e) {
            $("#hdFilter").val(s.GetValue());
        }

        function Show(s, e) {
            $("#hdStartDate").val(tbxStartDate.GetText());
            $("#hdFinishDate").val(tbxFinishDate.GetText());
            grid.Refresh();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <dx:ASPxMenu runat="server" ID="menu">
         <Items>
            <dx:MenuItem Name="Filter">
                <Template>
                    <table>
                        <tr>
                            <td>
                                ازتاریخ:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxStartDate" ClientInstanceName="tbxStartDate" Width="100px" MaskSettings-Mask="<1396..1400>/<01..12>/<01..31>" />
                            </td>
                            <td>
                                تا تاریخ:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxFinishDate" ClientInstanceName="tbxFinishDate" Width="100px" MaskSettings-Mask="<1396..1400>/<01..12>/<01..31>" />
                            </td>
                            <td>
                                <dx:ASPxRadioButtonList runat="server" ID="rblFilter" DataSourceID="dsFilter" ValueField="Key" TextField="Value"
                                    RepeatDirection="Horizontal" SelectedIndex="1"
                                    ClientSideEvents-SelectedIndexChanged="rblFilter_SelectedIndexChanged">
                                </dx:ASPxRadioButtonList>
                            </td>
                            <td>
                                <dx:ASPxButton runat="server"  Text="نمایش"  Width="80px" ClientSideEvents-Click="Show"/>
                            </td>
                        </tr>
                    </table>
                </Template>
                <Items>
                    <dx:MenuItem />
                </Items>
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
    <asp:HiddenField runat="server" ID="hdFilter" ClientIDMode="Static" />
     <asp:HiddenField runat="server" ID="hdStartDate" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdFinishDate" ClientIDMode="Static" />
    <telerik:OpenAccessLinqDataSource runat="server" ID="dsFilter" OnSelecting="dsFilter_Selecting" />
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.OrderReportModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
        <SettingsEditing Mode="Inline" />
        <Columns>
            <dx:GridViewBandColumn Caption="سفارش" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1" Width="120">
                        <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="2" Width="250">
                    </dx:GridViewDataTextColumn>
                    <ax:ASPxGridViewPersianDateColumn FieldName="OrderTime" VisibleIndex="3" Width="150" PropertiesTextEdit-DisplayFormatString="HH:mm yyyy/MM/dd">
                    </ax:ASPxGridViewPersianDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Price" VisibleIndex="6" Width="120" PropertiesTextEdit-DisplayFormatString="#,###">
                    </dx:GridViewDataTextColumn>
                    <ax:ASPxGridViewEnumComboBoxColumn FieldName="OrderStatus" EnumTypeName="Esunco.Models.Enum.OrderStatus, Esunco.Models" Width="100" CellStyle-HorizontalAlign="Center">
                    </ax:ASPxGridViewEnumComboBoxColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="خریدار" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ClientFullname" VisibleIndex="3" Width="250">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientMobile" VisibleIndex="4" Width="120" PropertiesTextEdit-DisplayFormatString="#### ### ####">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientNationalCode" VisibleIndex="5" Width="120">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientOfficeCode" VisibleIndex="6" Width="120">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="پرداخت" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="SaleOrderID" VisibleIndex="9" Width="200">
                    </dx:GridViewDataTextColumn>
                    <ax:ASPxGridViewEnumComboBoxColumn FieldName="PaymentStatus" EnumTypeName="Esunco.Models.Enum.PaymentStatus, Esunco.Models" Width="100" CellStyle-HorizontalAlign="Center">
                    </ax:ASPxGridViewEnumComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="RefID" VisibleIndex="9" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SaleRefID" VisibleIndex="10" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ResultCode" VisibleIndex="11" Width="200">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>

            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
        <SettingsDetail ShowDetailButtons="true" />
     
    </dx:ASPxGridView>
</asp:Content>

