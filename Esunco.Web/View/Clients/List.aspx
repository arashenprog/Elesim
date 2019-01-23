<%@ Page Title="" Language="C#" MasterPageFile="~/View/Shared/MainMasterPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="View_Clients_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <script type="text/javascript">

        function OnItemClick(s, e) {
            switch (e.item.name) {
                case "Blocked":
                    Master.DXUtil.GridOprationItems(grid, function () {
                        Master.PerformCallback("Blocked");
                    });
                    break;
                case "Unblock":
                    Master.DXUtil.GridOprationItems(grid, function () {
                        Master.PerformCallback("Unblock");
                    });
                    break;
                case "FilterAll":
                    {
                        $("#hdFilter").val(0);
                        grid.Refresh();
                        break;
                    }
                case "BlackList":
                    {
                        $("#hdFilter").val(2);
                        grid.Refresh();
                        break;
                    }
                default:

            }
        }

        function rblFilter_SelectedIndexChanged(s, e) {
            $("#hdFilter").val(s.GetValue());
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
            <dx:MenuItem Text="لیست سیاه ">
                <Image IconID="actions_cancel_16x16office2013">
                </Image>
                <Items>
                    <dx:MenuItem Text="اضافه به لیست سیاه" Name="Blocked" />
                    <dx:MenuItem Text="حذف از لیست سیاه" Name="Unblock" />
                </Items>
            </dx:MenuItem>
            <dx:MenuItem Text="بروزرسانی" Name="Refresh" BeginGroup="true">
                <Image IconID="actions_refresh_16x16office2013">
                </Image>
            </dx:MenuItem>
            <dx:MenuItem Text="نمایش">
                <Items>
                    <dx:MenuItem Text="(همه)" Name="FilterAll" GroupName="Filter" Checked="true" />
                    <dx:MenuItem Text="لیست سیاه" Name="BlackList" GroupName="Filter" />
                </Items>
                <Image IconID="filter_masterfilter_16x16office2013">
                </Image>
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
    <asp:HiddenField runat="server" ID="hdFilter" ClientIDMode="Static" />
    <telerik:OpenAccessLinqDataSource runat="server" ID="dsClientFilter" OnSelecting="dsClientFilter_Selecting" />
    <ax:ASPxGridViewExtender runat="server" GridViewID="grid" ID="gvx">
        <MenuIntegration MenuID="menu" />
        <DataSource OnChanged="gvx_OnChanged" OnDataBind="gvx_DateBind" ModelTypeName="Esunco.Models.ClientModel, Esunco.Models" />
    </ax:ASPxGridViewExtender>
    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="50" VisibleIndex="0" />
            <dx:GridViewDataTextColumn FieldName="Firstname" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Lastname" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NationalCode" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Email" Width="200">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Mobile" Width="100" PropertiesTextEdit-DisplayFormatString="{0:#### ### ####}">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Phone" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Address" Width="300">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PostalCode" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Credit" Width="100" PropertiesTextEdit-DisplayFormatString="{0:#,###}">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="OfficeCode" Width="100">
            </dx:GridViewDataTextColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="AccountType" EnumTypeName="Esunco.Models.Enum.AccountType, Esunco.Models" Width="100" CellStyle-HorizontalAlign="Center">
            </ax:ASPxGridViewEnumComboBoxColumn>
            <ax:ASPxGridViewEnumComboBoxColumn FieldName="BlackList" EnumTypeName="Esunco.Models.Enum.YesNo, Esunco.Models" Width="100" CellStyle-HorizontalAlign="Center">
            </ax:ASPxGridViewEnumComboBoxColumn>


            <dx:GridViewCommandColumn Width="100%" VisibleIndex="99" ShowCancelButton="true" ShowUpdateButton="true" />
        </Columns>
        <ClientSideEvents Init="Master.DXUtil.AjustGridSize" />
    </dx:ASPxGridView>
</asp:Content>

