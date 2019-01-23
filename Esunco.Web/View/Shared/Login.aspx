<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="View_Shared_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Esunco</title>
    <meta http-equiv="Expires" content="0"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            text-align: center;
            vertical-align: middle;
        }

        * {
            font-size: 11px;
            font-family: Tahoma;
        }

        .container {
            width: 400px;
            top: 50%;
            left: 50%;
            margin-top: -100px;
            margin-left: -200px;
            position: fixed;
        }

        .login {
            width: 100%;
            border-collapse: collapse;
        }

            .login tr td:first-child {
                width: 100px;
                text-align: right;
                padding: 5px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="ورود به سیستم" ShowCollapseButton="false" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table class="login">
                            <tr>
                                <td>نام کاربری:
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="tbxUsername" runat="server" Width="100%">
                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="Login">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>کلمه عبور:
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="tbxPassword" runat="server" Width="100%" Password="true">
                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="Login">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <dx:ASPxButton ID="btnLogin" runat="server" Width="100%" Text="ورود" UseSubmitBehavior="true" OnClick="btnLogin_Click" ValidationGroup="Login">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <dx:ASPxLabel runat="server" ID="lblMessage" Style="color: red;" />
                                </td>
                            </tr>

                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
