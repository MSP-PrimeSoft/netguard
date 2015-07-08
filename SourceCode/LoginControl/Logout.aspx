<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="LoginControl.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
 <script type="text/javascript">
     history.go(-1);
    </script>
  
    <table style="border-right: #003366 thin solid; border-top: #003366 thin solid; border-left: #003366 thin solid;
        border-bottom: #003366 thin solid;">
        <tr>
            <td>
                Thankyou for using our services
            </td>
        </tr>
    </table>
</asp:Content>
