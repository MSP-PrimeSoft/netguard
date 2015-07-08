<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true" CodeBehind="ConfiguringLoginAttempts.aspx.cs" Inherits="LoginControl.ConfiguringLoginAttempts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
 <script type="text/javascript">

     function IsNumeric(e) {
         var keyCode = e.which ? e.which : e.keyCode
         var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
         document.getElementById("error").style.display = ret ? "none" : "inline";
         return ret;
     }
    </script>
    <h2 class="page-title">
        Configuring Login Attempts</h2>
    <div class="user-profile wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName" style="width: 200px;">
                    Configuring Login Attempts<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtLoginAttempts" MaxLength="2" CssClass="inputfield"
                    Style="margin-left: -7px;" onkeypress="return IsNumeric(event);"></asp:TextBox>
                <asp:Button runat="server" ID="btnSave" Text="Save" ValidationGroup="UserValidation"
                    OnClick="btnSave_Click" Width="75px" Style="margin-left: 10px;" />
                <asp:RequiredFieldValidator ID="rfvLoginAttempts" runat="server" ControlToValidate="txtLoginAttempts"
                    ErrorMessage="Configuring Login Attempts required" CssClass="redcolor" Display="Dynamic"
                    ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
            </p>
        </div>
</asp:Content>
