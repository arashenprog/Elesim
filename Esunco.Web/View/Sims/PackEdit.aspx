<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="PackEdit.aspx.cs" Inherits="View_Sims_PackEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {

                case "Delete":
                    Master.DXUtil.ConfirmDelete(function () {
                        Master.PerformCallback("Delete", null, function () {
                            window.location = "Packs.aspx";
                        });
                    });
                    break;
                case "Save":
                    Master.PerformCallback("Save");
                    break;
                case "Publish":
                    Master.PerformCallback("Publish", null, function () {
                    });
                    break;
                default:

            }
        }

        function AjustGridSize(s, e) {
            var height = $(".main-content").height() - 185;
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
                    <table class="edit-from">
                        <tr>
                            <td style="width: 100px;">عنوان بسته:
                            </td>
                            <td colspan="6">
                                <dx:ASPxTextBox runat="server" ID="tbxTitle" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>قیمت:
                            </td>
                            <td style="width: 100px;">
                                <dx:ASPxSpinEdit runat="server" ID="tbxPrice" MinValue="10000" MaxValue="99999999999" DecimalPlaces="0" DisplayFormatString="#,###" Width="150px"  />
                            </td>
                            <td style="width: 100px;">نوع بسته:
                            </td>
                            <td style="width: 200px;">
                                <dx:ASPxComboBox runat="server" ID="cbxPackType" ClientInstanceName="cbxPackType" Width="100%" ValueType="System.Int32" DataSourceID="dsPackType" ValueField="Key" TextField="Value" />
                                <telerik:OpenAccessLinqDataSource runat="server" ID="dsPackType" OnSelecting="dsPackType_Selecting" />
                            </td>
                            <td style="width: 100px;">پیش شماره:
                            </td>
                            <td style="width: 200px;">
                                <dx:ASPxTextBox runat="server" ID="tbxCode" CssClass="ltr" Width="100%" NullText="913,916,912,..." />
                            </td>
                            <td style="width: 100px;">وضعیت انتشار:
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="tbxStatus" ReadOnly="true" Width="150px" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <DataSource OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.SimModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowDelete="false" />
        <SettingsSearchPanel Visible="false" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFooter="true" ShowStatusBar="Hidden" VerticalScrollBarMode="Auto" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="100" Caption="حذف شود" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn FieldName="Number" Width="150" CellStyle-CssClass="ltr">
                <PropertiesTextEdit DisplayFormatString="{0:0### ### ####}">
                    <MaskSettings Mask="999 999 9999"></MaskSettings>
                </PropertiesTextEdit>
                <CellStyle CssClass="ltr"></CellStyle>
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Type" VisibleIndex="2" Width="100" EnumTypeName="Esunco.Models.Enum.SimType, Esunco.Models">
                <PropertiesComboBox TextField="Value" ValueField="Key"></PropertiesComboBox>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewDataSpinEditColumn FieldName="Price" Width="120">
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000"></PropertiesSpinEdit>

            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="9" ShowCancelButton="true" />
        </Columns>
        <SettingsPager Mode="ShowAllRecords" Visible="false" />
        <ClientSideEvents Init="AjustGridSize" />
    </dx:ASPxGridView>
</asp:Content>

