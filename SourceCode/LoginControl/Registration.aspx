<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="LoginControl.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<script type="text/javascript">

    function RefreshScreen() {
        location.reload(true);
        return false;
    }

    function RedirectHome() {
        location.href = "Login.aspx";
        return false;
    }

    </script>
    <h2 class="page-title">
        Registration Form</h2>
    <div class="user-profile wrapper" >
        <asp:Label runat="server" Text="" ID="lblMsg"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    First Name<span class="red-star" >*</span> </label> 
                <asp:TextBox runat="server" Width="200px" ID="txtFirstName" MaxLength="30" CssClass="inputfield txtmarginleft"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="First name required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Last Name<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtLastName" MaxLength="20"
                        CssClass="inputfield"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="Last name required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    User Name<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtUserName" autocomplete="off"
                        MaxLength="50" CssClass="inputfield"></asp:TextBox>
                <asp:CustomValidator ID="cvUserName" Display="Dynamic" ErrorMessage="Already user exists with this username."
                    OnServerValidate="ValidateUserName" runat="server" ControlToValidate="txtUserName"
                    CssClass="redcolor" />
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"  
                    ErrorMessage="User name required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Password<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtPassword" autocomplete="off"
                        TextMode="Password" MaxLength="15" CssClass="inputfield"></asp:TextBox>
                <asp:CustomValidator ID="cvPassword" Display="Dynamic" ErrorMessage="" OnServerValidate="ValidatePassword"
                    runat="server" ControlToValidate="txtPassword" CssClass="redcolor" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" Display="Dynamic"
                    ErrorMessage="Password required" CssClass="redcolor" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Confirm Password<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtConfirmPassword"
                        autocomplete="off" TextMode="Password" MaxLength="15" CssClass="inputfield"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic"
                    ErrorMessage="Confirm password required" CssClass="redcolor" ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="cmpConfirmPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic"
                    CssClass="redcolor" ControlToCompare="txtPassword" Operator="Equal" Type="String"
                    ErrorMessage="Password and confirm password does not match." ValidationGroup="UserValidation" /></p>
            <p>
                <label for="userName">
                    Mobile Number<span class="red-star" >*</span></label>
                <asp:TextBox Width="200px" runat="server" ID="txtMobile" MaxLength="10" CssClass="inputfield txtmarginleft"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile"
                    ErrorMessage="Mobile number required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <b>
                    <label for="userName">
                        Email<span class="red-star" >*</span></label></b>
                <asp:TextBox runat="server" Width="200px" ID="txtEmail" MaxLength="50" CssClass="inputfield txtmarginleft"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexEmail" runat="server" Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                    CssClass="redcolor" ErrorMessage="Please enter valid email address." ValidationGroup="UserValidation" /></p>
            <div class="row">
                <div class="col-sm-12">
                    <label class="email-label">
                        <asp:Label ID="Label9" CssClass="small-label" runat="server" Text="Gender" />
                        <span class="red-star" >*</span>
                    </label>
                    <div class="email-radio">
                        <asp:RadioButtonList runat="server" ID="rbnLstSex" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Male" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <p>
                <label for="userName">
                    Address<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" Width="200px" ID="txtAddress" MaxLength="200" TextMode="MultiLine"
                    CssClass="inputfield" Style="margin-left: -4px;"></asp:TextBox></p>
            <p>
                <label for="userName">
                    Country<span class="red-star" >*</span></label><asp:DropDownList Width="200px" runat="server" ID="ddlCountry" CssClass="dropDownClass">
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                    ErrorMessage="Country required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"
                    InitialValue="0"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    City<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" Width="200px" ID="txtCity" MaxLength="30" CssClass="inputfield txtmarginleft"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                    ErrorMessage="City required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    ZipCode<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtZipCode" MaxLength="6"
                        CssClass="inputfield"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                    ErrorMessage="ZipCode required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Role<span class="red-star" >*</span></label><asp:DropDownList runat="server" Width="200px" ID="ddlRole" CssClass="dropDownClass">
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                    ErrorMessage="Role required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"
                    InitialValue="0"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Security Question<span class="red-star" >*</span></label>
                <asp:DropDownList runat="server" Width="200px" ID="ddlSecurityQuestion" CssClass="dropDownClass txtmarginleft">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" ControlToValidate="ddlSecurityQuestion"
                    ErrorMessage="Security question required" CssClass="redcolor" Display="Dynamic"
                    ValidationGroup="UserValidation" InitialValue="0"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Answer<span class="red-star" >*</span></label><asp:TextBox runat="server" Width="200px" ID="txtAnswer" MaxLength="50"
                        CssClass="inputfield"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ControlToValidate="txtAnswer"
                    ErrorMessage="Answer required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Enter Text<span class="red-star" >*</span></label><asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/CaptchaImage.ashx"
                        Width="170px" />
                <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="~/Style/Images/refresh.png"
                    Width="30px" OnClick="imgRefresh_Click" /></p>
            <p>
                <label for="userName">
                </label>
                <asp:TextBox runat="server" Width="200px" ID="txtCaptcha" MaxLength="50" CssClass="inputfield"></asp:TextBox>
                <asp:CustomValidator ID="cvCaptcha" Display="Dynamic" ErrorMessage="Invalid Captcha. Please try again."
                    OnServerValidate="ValidateCaptcha" runat="server" CssClass="redcolor" ControlToValidate="txtCaptcha" />
                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ControlToValidate="txtCaptcha"
                    ErrorMessage="Captcha required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
            </p>
            <p>
                <label for="userName">
                </label>
                <asp:Button runat="server" Width="12%" ID="btnRegistration" Text="Register" OnClick="btnRegistration_Click"
                    ValidationGroup="UserValidation" />
                <asp:Button runat="server" Width="12%" ID="btnRefresh" Text="Reset" OnClientClick="return RefreshScreen();" />
                <asp:Button runat="server" Width="12%" ID="btnHome" Text="Home" OnClientClick="return RedirectHome();" /></p>
        </div>
    </div>
</asp:Content>
