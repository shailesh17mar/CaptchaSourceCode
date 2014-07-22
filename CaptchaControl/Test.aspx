<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="CaptchaControl.Test" %>

<%@ Register Assembly="CaptchaControl" Namespace="CaptchaControl" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="captcha.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox runat="server"/>
        <asp:TextBox ID="TextBox1" runat="server"/>
        <asp:TextBox ID="TextBox2" runat="server"/>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <cc1:CaptchaControl ID="CaptchaControl1" runat="server" Width="100" Height="50" Length="9" Enabled="True" Level="3" GraphicLevel="3" />
            <cc1:CaptchaControl ID="CaptchaControl2" runat="server" Width="200" Height="50"  Enabled="True"  TabIndex="1" />
        </div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
    </form>
</body>
</html>
