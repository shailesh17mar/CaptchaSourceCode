<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Captcha.ascx.cs" Inherits="CaptchaControl.Captcha" ClassName="Captcha" %>


<style type="text/css" runat="server">
     .captcha {
        background-color: white;
        font-family: "Segoe UI", Frutiger, "Frutiger Linotype", "Dejavu Sans", "Helvetica Neue", Arial, sans-serif;
        font-size: 12px;
        padding: 5px;
        height: 100px;
    }

    .captchaImg {
        float: left;
        padding: 0;
        border: solid 2px #3498db;
        background: url(data:image/gif;base64,R0lGODlhgAAPAPEBAC9rwP///8PU7AAAACH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQECgD/ACwAAAAAgAAPAAACo5QvoYG33NKKUtF3Z8RbN/55CEiNonMaJGp1bfiaMQvBtXzTpZuradUDZmY+opA3DK6KwaQTCbU9pVHc1LrDUrfarq765Ya9u+VRzLyO12lwG10yy39zY11Jz9t/6jf5/HfXB8hGWKaHt6eYyDgo6BaH6CgJ+QhnmWWoiVnI6ddJmbkZGkgKujhplNpYafr5OooqGst66Uq7OpjbKmvbW/p7UAAAIfkEBQoAAQAsAAAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACwLAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALBYAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsIQAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACwsAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALDcAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsQgAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACxNAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALFgAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsYwAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACxuAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALHkAAAAHAA8AAAIIhI+py+0PoywAOw==) no-repeat center;
    }

    .captchaRefresh {
        width: 16px;
        float: left;
        height: 16px;
        color: white;
        display: block;
        text-align: center;
        background:no-repeat center url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAASRJREFUeNq0U8FtwkAQvEP3j3kjFEqAChIqwO4gHcRUAKmAdIDoIHTgDjAdWEL83UEyg+ak1cqO8slKwx3Lzdzt7hDDQMznsxLL0qW/brd7689GR6yx7IAiDEcDbIF34ATBJhnyEctb+D1egYv2VwpORD4Ycg98AFPcEAnsVyzBiT3zI4FM1VrJDliD1NmTrB3neifw6FFSPTkqTwaR/TgMlPcQiDjwbbpcjUyFr1wIL2Y/Teos4zzWOXY7/FewhL2a1/3lJk2s1stiknHyj3kS7Vg/EKWZWKAPvD0L+WDs9oW+nvIYO+f7rfe8RrkzfqEnPrPA1TyLcQRhozzjSR4oDJlm67NAq2RjhEonGkzdlX3hxCTZtPWA54Mu4b9w5cv7EWAAy+JrG0G68doAAAAASUVORK5CYII=);
        cursor: Pointer;
        position: absolute;
        margin: 10px;
        margin-top: -8px;
        top: 50%;

    }

    .refresh-description {
        visibility: hidden;
        position: absolute;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
    }


    .captchaText {
        padding: 0 !important;
        margin: 2px;
        border: 1px solid #bdc3c7;
        float: left;
        display: block;
        color: #34495e;
        font-family: "Lato", Helvetica, Arial, sans-serif;
        font-size: 15px;
        line-height: 1.467;
        height: 25px;
        -webkit-appearance: none;
        border-radius: 2px;
        -webkit-box-shadow: none;
        box-shadow: none;
        -webkit-transition: border .25s linear, color .25s linear, background-color .25s linear;
        transition: border .25s linear, color .25s linear, background-color .25s linear;
    }

        .captcha-text :focus {
            border: 1px solid #3498db;
        }

    .refreshContainer {
        position: relative;
        float: left;
        width: auto;
    }

    
</style>
<asp:UpdatePanel ID="CaptchaPanel" class="CaptchaContainer" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger EventName="Click" ControlID="captchaRefresh" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel Class="captcha" ID="captcha" runat="server">

          <asp:Image ID="captchaImage" runat="server" Class="captchaImg" />
            <asp:Panel ID="refreshContainer" Class="refreshContainer" runat="server">
                <asp:LinkButton ID="captchaRefresh" runat="server" OnClick="refresh_Click" Class="captchaRefresh" ToolTip="Try a new code" />
            </asp:Panel>

            <asp:TextBox ID="txtImgcode" runat="server" Class="captchaText" placeholder="Type here"></asp:TextBox>

            <br />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

