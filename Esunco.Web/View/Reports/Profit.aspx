<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="Profit.aspx.cs" Inherits="View_Reports_Profit" %>

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
                            <td>ازتاریخ:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxStartDate" ClientInstanceName="tbxStartDate" Width="100px" MaskSettings-Mask="<1396..1400>/<01..12>/<01..31>" />
                            </td>
                            <td>تا تاریخ:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxFinishDate" ClientInstanceName="tbxFinishDate" Width="100px" MaskSettings-Mask="<1396..1400>/<01..12>/<01..31>" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton runat="server" Text="نمایش" Width="80px" ClientSideEvents-Click="Show" />
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
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.ProfitViewModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="Profit" ClientInstanceName="grid">
        <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFooter="True" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="TotalBuy" VisibleIndex="5" Width="200" PropertiesTextEdit-DisplayFormatString="#,###">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TotalSell" VisibleIndex="6" Width="200" PropertiesTextEdit-DisplayFormatString="#,###">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Profit" VisibleIndex="8" Width="200" PropertiesTextEdit-DisplayFormatString="#,###">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="10" ShowCancelButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
    </dx:ASPxGridView>
</asp:Content>

