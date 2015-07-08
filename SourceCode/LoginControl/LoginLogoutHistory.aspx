<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true" CodeBehind="LoginLogoutHistory.aspx.cs" Inherits="LoginControl.LoginLogoutHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<h2 class="page-title">
        Login History</h2>
    <div class="user-profile wrapper">
        <asp:Label ID="lblError" runat="server" CssClass="redcolor"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    User Name :</label>
                <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" MaxLength="50" ToolTip="Enter User Name"
                    CssClass="inputfield" Style="margin-left: -30px;"></asp:TextBox>
                <asp:Button ID="btnLoginHistory" runat="server" Text="Search" CssClass="buttonsmallred"
                    Width="13%" OnClick="btnLoginHistory_Click" Style="margin-left: 10px;" />
            </p>
        </div>
    </div>
    <div style="margin-top: 10px;">
        <asp:GridView ID="grdLoginHistory" runat="server" CellPadding="4" CssClass="SearchBlock"
            DataKeyNames="Id" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
            Width="100%" AllowPaging="True" EmptyDataText="No results found." OnPageIndexChanging="grdLoginHistory_PageIndexChanging">
            <RowStyle BackColor="WhiteSmoke" />
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                <asp:BoundField DataField="LoginTime" HeaderText="Login Time" SortExpression="LoginTime" />
                <asp:BoundField DataField="LogoutTime" HeaderText="Logout Time" SortExpression="LogoutTime" />
                <asp:BoundField DataField="CreatedIp" HeaderText="Login Ip" SortExpression="CreatedIp" />
                <asp:BoundField DataField="UpdatedIp" HeaderText="Logout Ip" SortExpression="UpdatedIp" />
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
