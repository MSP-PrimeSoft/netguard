<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="LoginControl.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
 <h2 class="page-title">
        Change Password</h2>
    <div class="user-profile wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    Password<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtPassword" autocomplete="off" TextMode="Password"
                    MaxLength="50"></asp:TextBox>
                <asp:CustomValidator ID="cvPassword" ValidationGroup="UserValidation" Display="Dynamic"
                    ErrorMessage="" OnServerValidate="ValidatePassword" runat="server" ControlToValidate="txtPassword"
                    CssClass="redcolor" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
            </p>
            <p>
                <label for="userName">
                    Confirm Password<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtConfirmPassword" autocomplete="off" TextMode="Password"  CssClass="inputfield"
                    MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                    Display="Dynamic" ErrorMessage="Confirm password required" CssClass="redcolor"
                    ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="cvConfirmPassword" ControlToValidate="txtConfirmPassword"
                    CssClass="redcolor" Display="Dynamic" ControlToCompare="txtPassword" Operator="Equal"
                    Type="String" ErrorMessage="Password and confirm password does not match." ValidationGroup="UserValidation" />
            </p>
            <p>
                <label for="userName">
                </label>
                <asp:Button runat="server" ID="btnChangePassword" Text="Change Password" Style="width: 20% !important;"
                    ValidationGroup="UserValidation" OnClick="btnChangePassword_Click" />
            </p>
        </div>
    </div>
</asp:Content>
