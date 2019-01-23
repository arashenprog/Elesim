<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="AuctionEdit.aspx.cs" Inherits="View_Sims_AuctionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {

                case "Delete":
                    Master.DXUtil.ConfirmDelete(function () {
                        Master.PerformCallback("Delete", null, function () {
                            window.location = "Auctions.aspx";
                        });
                    });
                    break;
                case "Save":
                    Master.PerformCallback("Save", null, function () {
                    });
                    break;
                case "Publish":
                    Master.PerformCallback("Publish", null, function () {
                    });
                    break;
                default:

            }
        }

        function AjustGridSize(s, e) {
            var height = $(".main-content").height() - 210;
            s.SetHeight(height);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <dx:ASPxMenu runat="server" ID="menu">
        <ClientSideEvents ItemClick="OnItemClick" />
        <Items>
            <dx:MenuItem Text="حذف" Name="Delete">
                <Image IconID="actions_deletelist_16x16">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="ذخیره تغییرات" Name="Save">
                <Image IconID="actions_apply_16x16">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="انتشار" Name="Publish">
                <Image IconID="actions_apply_16x16">
                </Image>
            </dx:MenuItem>

        </Items>
    </dx:ASPxMenu>
    <div class="margin-5">
        <dx:ASPxRoundPanel runat="server" Width="100%" HeaderText="مشخصات پک">
            <PanelCollection>
                <dx:PanelContent>
                    <table class="full-width">
                        <tr>
                            <td style="width: 100px;">عنوان:
                            </td>
                            <td colspan="3">
                                <dx:ASPxTextBox runat="server" ID="tbxTitle" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">شماره:
                            </td>
                            <td colspan="3">
                                <dx:ASPxTextBox runat="server" ID="tbxNumbers" ReadOnly="true" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">قیمت:
                            </td>
                            <td style="width: 200px;">
                                <dx:ASPxSpinEdit runat="server" ID="tbxPrice" MinValue="0" MaxValue="9999999999" NumberType="Integer" Width="150px" DisplayFormatString="#,###" />
                            </td>
                            <td style="width: 100px;">وضعیت انتشار:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxStatus" ReadOnly="true" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">زمان شروع:
                            </td>
                            <td style="width: 200px;">
                                <dx:ASPxTextBox runat="server" ID="tbxStartTime" ClientInstanceName="tbxStartTime" ValidationGroup="FormAuction" Width="150px">
                                    <MaskSettings Mask="<1395..1499>/<01..12>/<01..31> <00..23>:<00..59> " />
                                    <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 100px;">زمان پایان:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxFinishTime" ClientInstanceName="tbxFinishTime" ValidationGroup="FormAuction" Width="150px">
                                    <MaskSettings Mask="<1395..1499>/<01..12>/<01..31> <00..23>:<00..59> " />
                                    <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <DataSource OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.BidModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared">
        <SettingsDataSecurity AllowDelete="false" />
        <SettingsSearchPanel Visible="false" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFooter="true" ShowStatusBar="Hidden" VerticalScrollBarMode="Auto" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0" />
            <dx:GridViewBandColumn Caption="خریدار">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Client.Fullname" Width="200" />
                    <dx:GridViewDataTextColumn FieldName="Client.Mobile" Width="200" />
                    <dx:GridViewDataTextColumn FieldName="Client.NationalCode" Width="200" />
                    <dx:GridViewDataTextColumn FieldName="Client.OfficeCode" Width="200" />
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="پیشنهاد">
                <Columns>
                    <dx:GridViewDataSpinEditColumn FieldName="Price" Width="150" SortIndex="0" SortOrder="Descending">
                        <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <ax:ASPxGridViewPersianDateColumn FieldName="Time" Width="150">
                        <PropertiesTextEdit DisplayFormatString="HH:mm YYYY/MM/dd"></PropertiesTextEdit>
                        <Settings SortMode="Value"></Settings>
                    </ax:ASPxGridViewPersianDateColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" ShowUpdateButton="true" />
        </Columns>
        <SettingsPager Mode="ShowAllRecords" Visible="false" />
        <ClientSideEvents Init="AjustGridSize" />

    </dx:ASPxGridView>
</asp:Content>

