<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Payments.aspx.cs" Inherits="View_Reports_Payments" %>

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
    <asp:HiddenField runat="server" ID="hdStartDate" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdFinishDate" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdFilter" ClientIDMode="Static" />
    <telerik:OpenAccessLinqDataSource runat="server" ID="dsFilter" OnSelecting="dsFilter_Selecting" />
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.PaymentReportModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFooter="True" />
        <Columns>
            <dx:GridViewBandColumn Caption="خریدار" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ClientName" VisibleIndex="3" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientMobile" VisibleIndex="4" Width="120">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="پرداخت" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <ax:ASPxGridViewPersianDateColumn FieldName="DateTime" VisibleIndex="6" Width="100">
                    </ax:ASPxGridViewPersianDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Type" VisibleIndex="7" Width="100">
                    </dx:GridViewDataTextColumn>
                    <ax:ASPxGridViewEnumComboBoxColumn FieldName="PaymentStatus" VisibleIndex="8" EnumTypeName="Esunco.Models.Enum.PaymentStatus, Esunco.Models" Width="100" CellStyle-HorizontalAlign="Center">
                    </ax:ASPxGridViewEnumComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="SaleOrderID" VisibleIndex="9" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="RefID" VisibleIndex="10" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SaleRefID" VisibleIndex="11" Width="200">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Price" VisibleIndex="12" Width="200" PropertiesTextEdit-DisplayFormatString="#,###">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
        <SettingsDetail ShowDetailButtons="true" />
        <TotalSummary>
            <dx:ASPxSummaryItem DisplayFormat="#,###" FieldName="Price" SummaryType="Sum" />
        </TotalSummary>
    </dx:ASPxGridView>
</asp:Content>

