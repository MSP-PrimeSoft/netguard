<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true" CodeBehind="LockedUsers.aspx.cs" Inherits="LoginControl.LockedUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
  <h2 class="page-title">
        Locked Users</h2>
    <div class="user-profile wrapper">
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    User Name :</label>
                <asp:TextBox CssClass="inputfield" ID="txtUserName" runat="server" TabIndex="1" MaxLength="50"
                    ToolTip="Enter User Name" Style="margin-left: -30px;"></asp:TextBox>
                <asp:Button ID="btnLockedUsers" runat="server" Text="Search" CssClass="buttonsmallred"
                    Width="20%" OnClick="btnLockedUsers_Click" Style="margin-left: 10px;" />
            </p>
        </div>
    </div>
    <div style="margin-top: 10px;">
        <asp:GridView ID="grdLockedUsers" runat="server" CellPadding="4" CssClass="SearchBlock"
            DataKeyNames="Id" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
            Width="100%" AllowPaging="True" EmptyDataText="No results found." OnRowCommand="grdLockedUsers_RowCommand"
            OnRowDataBound="grdLockedUsers_RowDataBound" OnPageIndexChanging="grdLockedUsers_PageIndexChanging">
            <RowStyle BackColor="WhiteSmoke" />
            <Columns>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <asp:Label Width="100" ID="lblFirstname" CssClass="lblbrakword" Text='<%# Bind("FirstName")%>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label Width="100" ID="lblLastname" CssClass="lblbrakword" Text='<%# Bind("LastName")%>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile">
                    <ItemTemplate>
                        <asp:Label Width="100" ID="lblMobile" Text='<%# Bind("Mobile")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label Width="100" ID="lblEmail" CssClass="lblbrakword" Text='<%# Bind("Email")%>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <%# Eval("UserLogin.UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password Wrong Attempts" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("UserLogin.PasswordWrongAttempts")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Password Wrong" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("UserLogin.LastPasswordWrong")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton ID="lnkLock" runat="server" CommandArgument='<%# Eval("UserLogin.AccountLocked")%>'
                            CommandName="Locked" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="gridViewItem" BackColor="WhiteSmoke" />
            <FooterStyle CssClass="gridViewHeader" />
            <PagerStyle CssClass="pagerDetails" BackColor="#169CE3" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E0E0E0" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
