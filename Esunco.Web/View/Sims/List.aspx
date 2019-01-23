<%@ Page Title="مدیریت سیم کارت ها" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="View_Sims_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {
                case "Normal":
                case "Published":
                case "Unpublished":
                    Master.PerformCallback(e.item.name);
                    break;
                case "NewPack":
                    {
                        Master.DXUtil.GridOprationItems(grid, function () {
                            popPack.Show();
                        });
                        break;
                    }
                case "AddToPack":
                    {
                        Master.DXUtil.GridOprationItems(grid, function () {
                            popAddToPack.Show();
                        });
                        break;
                    }
                case "Auction":
                    {
                        Master.DXUtil.GridOprationItems(grid, function () {
                            popAuction.Show();
                        });
                        break;
                    }
                case "Rond":
                    Master.DXUtil.GridOprationItems(grid, function () {
                        popRond.Show();
                    });
                    break;
                case "Price":
                    Master.DXUtil.GridOprationItems(grid, function () {
                        popPrice.Show();
                    });
                    break;
                case "ImportPrePaid":
                    {
                        $("#hdImportType").val(0);
                        popImport.Show();
                        break;
                    }
                case "ImportPostPaid":
                    {
                        $("#hdImportType").val(1);
                        popImport.Show();
                        break;
                    }



            }
        }

        function SavePack() {
            Master.PerformCallback("Pack");
        }

        function SaveAddToPack() {
            Master.PerformCallback("AddToPack");
        }

        function SaveAuction() {
            Master.PerformCallback("Auction");
        }

        function SaveRond() {
            Master.PerformCallback("Rond", null, function () {
                tbxRondPrice.SetValue(0);
                popRond.Hide();
            });
        }

        function SavePrice() {
            Master.PerformCallback("Price", null, function () {
                tbxPrice.SetValue(0);
                popPrice.Hide();
            });
        }


        function OnFileUploadComplete(s, e) {
            if (e.isValid) {
                tbxSimPrice.SetValue(0);
                tbxSimBuyPrice.SetValue(0);
                popImport.Hide();
                grid.Refresh();
            }
        }

        function SaveSim(s, e) {
            $("#hdSimPrice").val(tbxSimPrice.GetValue());
            $("#hdSimBuyPrice").val(tbxSimBuyPrice.GetValue());
            uplFile.Upload();
        }

        function OnAjustGridSize(s, e) {
            var height = $(".main-content").height() - 80;
            s.SetHeight(height);
        }

        function rbFilterSelectedIndexChanged(s, e) {
            grid.Refresh();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <dx:ASPxMenu runat="server" ID="menu">
        <ClientSideEvents ItemClick="OnItemClick" />
        <Items>
            <dx:MenuItem Text="بارگزاری">
                <Image IconID="actions_addfile_16x16office2013">
                </Image>
                <Items>
                    <dx:MenuItem Text="دائمی" Name="ImportPostPaid" />
                    <dx:MenuItem Text="اعتباری" Name="ImportPrePaid" />
                </Items>
            </dx:MenuItem>
            <dx:MenuItem Text="ویرایش" Name="Edit">
                <Image IconID="edit_edit_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="عملیات گروهی">
                <Items>
                    <dx:MenuItem Text="حذف" Name="Delete">
                        <Image IconID="actions_deletelist_16x16office2013">
                        </Image>
                    </dx:MenuItem>
                    <dx:MenuItem Text="پک" BeginGroup="true">
                        <Image IconID="support_packageproduct_16x16office2013">
                        </Image>
                        <Items>
                            <dx:MenuItem Text="پک جدید" Name="NewPack" />
                            <dx:MenuItem Text="پک موجود" Name="AddToPack" />
                        </Items>
                    </dx:MenuItem>
                    <dx:MenuItem Text="مزایده" Name="Auction" BeginGroup="true">
                        <Image IconID="scheduling_time_16x16office2013">
                        </Image>
                    </dx:MenuItem>
                    <dx:MenuItem Text="قیمت" Name="Price" BeginGroup="true">
                    </dx:MenuItem>
                    <dx:MenuItem Text="وضعیت انتشار" BeginGroup="true">

                        <Items>
                            <dx:MenuItem Text="منتشر شده" Name="Published" />
                            <dx:MenuItem Text="منتشر نشده" Name="Unpublished" />
                        </Items>
                    </dx:MenuItem>

                    <dx:MenuItem Text="رند/ معمولی" BeginGroup="true">
                        <Image IconID="support_feature_16x16office2013">
                        </Image>
                        <Items>
                            <dx:MenuItem Text="رند" Name="Rond" />
                            <dx:MenuItem Text="معمولی" Name="Normal" />
                        </Items>
                    </dx:MenuItem>
                </Items>
                <Image IconID="programming_technology_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="بروزرسانی" Name="Refresh" BeginGroup="true">
                <Image IconID="scheduling_recurrence_16x16office2013">
                </Image>
            </dx:MenuItem>

        </Items>
    </dx:ASPxMenu>
    <div>
        <table class="full-width">
            <tr>
                <td style="width: 200px;">
                    <dx:ASPxRadioButtonList runat="server" ID="rblSimType" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="rbFilterSelectedIndexChanged" />
                        <Items>
                            <dx:ListEditItem Text="دائمی" Selected="true" Value="1" />
                            <dx:ListEditItem Text="اعتباری" Value="0" />
                            <dx:ListEditItem Text="همه" Value="-1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td style="width: 50px;">&nbsp;
                </td>
                <td style="width: 200px;">
                    <dx:ASPxRadioButtonList runat="server" ID="rblNumberType" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="rbFilterSelectedIndexChanged" />
                        <Items>
                            <dx:ListEditItem Text="عادی" Value="0" Selected="true" />
                            <dx:ListEditItem Text="رند" Value="1" />
                            <dx:ListEditItem Text="همه" Value="-1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td style="width: 50px;">&nbsp;
                </td>
                <td style="width: 350px;">
                    <dx:ASPxRadioButtonList runat="server" ID="rblVisible" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="rbFilterSelectedIndexChanged" />
                        <Items>
                            <dx:ListEditItem Text="منتشر نشده" Value="0" Selected="true" />
                            <dx:ListEditItem Text="منتشر شده" Value="1" />
                            <dx:ListEditItem Text="همه" Value="-1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td style="width: 50px;">&nbsp;
                </td>
                <td>
                    <dx:ASPxButtonEdit runat="server" ID="tbxSearch" Width="400px" NullText="جستجو..." ClearButton-DisplayMode="OnHover">
                        <Buttons>
                            <dx:EditButton Image-IconID="search_16x16office2013">
                                <Image IconID="zoom_zoom_16x16office2013"></Image>
                            </dx:EditButton>
                        </Buttons>

                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </dx:ASPxButtonEdit>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hdState" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdSimBuyPrice" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdSimPrice" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdImportType" ClientIDMode="Static" />
    <telerik:OpenAccessLinqDataSource runat="server" ID="dsSimFilter" OnSelecting="dsSimFilter_Selecting" />
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnChanged="gvx_OnChanged" OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.SimModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <SettingsDataSecurity AllowInsert="False" AllowEdit="true" />
        <SettingsEditing Mode="PopupEditForm" />
        <SettingsSearchPanel CustomEditorID="tbxSearch" Visible="false" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0" />
            <dx:GridViewDataTextColumn FieldName="Number" Width="150" PropertiesTextEdit-DisplayFormatString="{0:0### ### ####}" CellStyle-CssClass="ltr" ReadOnly="true">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Display" Width="150" CellStyle-CssClass="ltr">
                <PropertiesTextEdit Style-CssClass="ltr" />
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Type" Width="120" EnumTypeName="Esunco.Models.Enum.SimType, Esunco.Models" ReadOnly="true">
                <PropertiesComboBox TextField="Value" ValueField="Key"></PropertiesComboBox>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Code" Width="100">
                <PropertiesTextEdit Style-CssClass="ltr" />
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="Status" Width="120" EnumTypeName="Esunco.Models.Enum.SimPackStatus, Esunco.Models" ReadOnly="true">
                <PropertiesComboBox TextField="Value" ValueField="Key"></PropertiesComboBox>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="NumberType" Width="120" EnumTypeName="Esunco.Models.Enum.NumberType, Esunco.Models" ReadOnly="true" EditFormSettings-Visible="False">
                <PropertiesComboBox TextField="Value" ValueField="Key"></PropertiesComboBox>
            </ax:ASPxGridViewEnumComboBoxColumn>
            <dx:GridViewDataSpinEditColumn FieldName="BuyPrice" Width="120" PropertiesSpinEdit-MinValue="10000" PropertiesSpinEdit-MaxValue="9999999999" PropertiesSpinEdit-DecimalPlaces="0" PropertiesSpinEdit-DisplayFormatString="#,###">
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn FieldName="Price" Width="120" PropertiesSpinEdit-MinValue="10000" PropertiesSpinEdit-MaxValue="9999999999" PropertiesSpinEdit-DecimalPlaces="0" PropertiesSpinEdit-DisplayFormatString="#,###">
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn FieldName="RondPrice" Width="120" PropertiesSpinEdit-MinValue="10000" PropertiesSpinEdit-MaxValue="9999999999" PropertiesSpinEdit-DecimalPlaces="0" PropertiesSpinEdit-DisplayFormatString="#,###">
                <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom" MaxValue="9999999999" MinValue="10000" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataTextColumn FieldName="ActivationCode" Width="150" ReadOnly="true" EditFormSettings-Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ReceiptCode" Width="150" ReadOnly="true" EditFormSettings-Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PaymentCode" Width="150" ReadOnly="true" EditFormSettings-Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TraceCode" Width="150" ReadOnly="true" EditFormSettings-Visible="False">
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="CreateDate" Width="120" ReadOnly="true" EditFormSettings-Visible="False">
                <PropertiesTextEdit DisplayFormatString="yy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="ExpireDate" Width="120" ReadOnly="true" EditFormSettings-Visible="False">
                <PropertiesTextEdit DisplayFormatString="yy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
            <ax:ASPxGridViewPersianDateColumn FieldName="RegisterTime" Width="120" ReadOnly="true" EditFormSettings-Visible="False">
                <PropertiesTextEdit DisplayFormatString="HH:mm yy/MM/dd"></PropertiesTextEdit>
                <Settings SortMode="Value"></Settings>
            </ax:ASPxGridViewPersianDateColumn>
            <dx:GridViewCommandColumn Width="100%" VisibleIndex="99" />
        </Columns>
        <ClientSideEvents Init="OnAjustGridSize" />
    </dx:ASPxGridView>


    <dx:ASPxPopupControl runat="server" ID="popPack" ClientInstanceName="popPack" HeaderText="بسته جدید" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <label>عنوان</label>
                <dx:ASPxTextBox runat="server" ID="tbxTitle" ClientInstanceName="tbxTitle" Width="100%" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup="FormPack" />
                <br />
                <label>نوع بسته</label>
                <dx:ASPxComboBox runat="server" ID="cbxPackType" ClientInstanceName="cbxPackType" Width="100%" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup="FormPack" DataSourceID="dsPackType" ValueField="Key" TextField="Value" />
                <telerik:OpenAccessLinqDataSource runat="server" ID="dsPackType" OnSelecting="dsPackType_Selecting" />
                <br />
                <label>پیش شماره</label>
                <dx:ASPxTextBox runat="server" ID="tbxPackCode" ClientInstanceName="tbxPackCode" AllowNull="false" Width="100%" ValidationSettings-ValidationGroup="FormSim" NullText="913,916,912,..." />
                <br />
                <dx:ASPxButton runat="server" ID="btnSave" Text="ذخیره" Width="100%" ClientSideEvents-Click="SavePack" ValidationGroup="FormPack" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <dx:ASPxPopupControl runat="server" ID="popAddToPack" ClientInstanceName="popAddToPack" HeaderText="اضاقه به ..." Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxComboBox runat="server" ID="cbxPacks" Width="100%" AllowNull="false" SelectedIndex="0" TextField="Title" ValueField="ID" />
                <br />
                <dx:ASPxButton runat="server" ID="ASPxButton4" Text="ذخیره" Width="100%" ClientSideEvents-Click="SaveAddToPack" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <dx:ASPxPopupControl runat="server" ID="popRond" ClientInstanceName="popRond" HeaderText="قیمت رند" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <label>قیمت</label>
                <dx:ASPxSpinEdit runat="server" ID="tbxRondPrice" ClientInstanceName="tbxRondPrice" MinValue="0" AllowNull="false" Number="0" MaxValue="999999999999" Width="100%" ValidationSettings-ValidationGroup="FormRond" DisplayFormatString="#,###" />
                <br />
                <dx:ASPxButton runat="server" ID="ASPxButton1" Text="ذخیره" Width="100%" ClientSideEvents-Click="SaveRond" ValidationGroup="FormRond" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl runat="server" ID="popPrice" ClientInstanceName="popPrice" HeaderText="قیمت" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <label>قیمت</label>
                <dx:ASPxSpinEdit runat="server" ID="tbxPrice" ClientInstanceName="tbxPrice" MinValue="0" AllowNull="false" Number="0" MaxValue="999999999999" Width="100%" ValidationSettings-ValidationGroup="FormPrice" DisplayFormatString="#,###" />
                <br />
                <dx:ASPxButton runat="server" ID="ASPxButton3" Text="ذخیره" Width="100%" ClientSideEvents-Click="SavePrice" ValidationGroup="FormPrice" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <dx:ASPxPopupControl runat="server" ID="popAuction" ClientInstanceName="popAuction" HeaderText="مزایده" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <label>عنوان</label>
                <dx:ASPxTextBox runat="server" ID="tbxAuctionTitle" ClientInstanceName="tbxAuctionTitle" Width="100%" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup="FormAuction" />
                <br />
                <label>قیمت پایه</label>
                <dx:ASPxSpinEdit runat="server" ID="tbxAuctionPrice" ClientInstanceName="tbxAuctionPrice" MinValue="0" AllowNull="false" Number="0" MaxValue="999999999999" Width="100%" ValidationSettings-ValidationGroup="FormAuction" DisplayFormatString="#,###" ValidationSettings-RequiredField-IsRequired="true" />
                <br />
                <label>زمان شروع</label>
                <dx:ASPxTextBox runat="server" ID="tbxAuctionStartTime" ClientInstanceName="tbxAuctionStartTime" ValidationGroup="FormAuction" Width="100%">
                    <MaskSettings Mask="<1395..1499>/<01..12>/<01..31> <00..23>:<00..59> " />
                    <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                </dx:ASPxTextBox>
                <br />
                <label>زمان پایان</label>
                <dx:ASPxTextBox runat="server" ID="tbxAuctionFinishTime" ClientInstanceName="tbxAuctionFinishTime" ValidationGroup="FormAuction" Width="100%">
                    <MaskSettings Mask="<1395..1499>/<01..12>/<01..31> <00..23>:<00..59> " />
                    <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                </dx:ASPxTextBox>
                <br />
                <dx:ASPxButton runat="server" ID="ASPxButton2" Text="ذخیره" Width="100%" ClientSideEvents-Click="SaveAuction" ValidationGroup="FormAuction" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl runat="server" ID="popImport" ClientInstanceName="popImport" HeaderText="دریافت فایل" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <label>پیش شماره</label>
                <dx:ASPxTextBox runat="server" ID="tbxCode" ClientInstanceName="tbxCode" Width="100%" ValidationSettings-ValidationGroup="FormSim" />
                <br />
                <label>فایل</label>
                <dx:ASPxUploadControl FileUploadMode="OnPageLoad" AutoStartUpload="false" ShowProgressPanel="true" ShowUploadButton="false" runat="server" ID="uplFile" ClientInstanceName="uplFile"
                    NullText="فابل را انتخاب کنید" Width="100%"
                    ValidationSettings-AllowedFileExtensions=".csv"
                    OnFileUploadComplete="uplFile_FileUploadComplete">
                    <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                    <BrowseButton Image-IconID="actions_addfile_16x16" />
                </dx:ASPxUploadControl>
                <br />
                <label>قیمت خرید</label>
                <dx:ASPxSpinEdit runat="server" ID="tbxSimBuyPrice" ClientInstanceName="tbxSimBuyPrice" MinValue="0" AllowNull="false" Number="0" MaxValue="999999999999" Width="100%" ValidationSettings-ValidationGroup="FormSim" DisplayFormatString="#,###" />
                <br />
                <label>قیمت فروش</label>
                <dx:ASPxSpinEdit runat="server" ID="tbxSimPrice" ClientInstanceName="tbxSimPrice" MinValue="0" AllowNull="false" Number="0" MaxValue="999999999999" Width="100%" ValidationSettings-ValidationGroup="FormSim" DisplayFormatString="#,###" />
                <br />
                <dx:ASPxButton runat="server" ID="btnSaveSim" Text="ذخیره" Width="100%" ClientSideEvents-Click="SaveSim" ValidationGroup="FormSim" UseSubmitBehavior="false" AutoPostBack="false" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>

