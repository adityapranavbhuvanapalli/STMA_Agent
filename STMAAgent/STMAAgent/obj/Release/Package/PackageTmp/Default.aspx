<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="STMAAgent._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <form id="form1" method="post" runat="server">

       <div id="services">
           <br /><br />
           <asp:DropDownList ID="ddlServices" runat="server">

           </asp:DropDownList>
           <p><asp:Label ID="lblNoUser" runat="server"></asp:Label>&nbsp;</p>
           <table style="width:100%;">
               <tr><td><asp:TextBox ID="txtMobile" runat="server" placeholder="Mobile"></asp:TextBox>
                   <br />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Enter a registered mobile number." ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                   </td><td>&nbsp;</td><td>&nbsp;</td></tr>
               <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
               <tr><td><asp:Button ID="btnShow" runat="server" Text="Show User" OnClick="btnShow_Click"></asp:Button></td><td>&nbsp;</td><td>&nbsp;</td></tr>

           </table>
<asp:Panel ID="pnlShowUser" runat="server">
<table style="width:100%;">
    <tr><td><b>User Information</b></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>

     
    <tr><td>User ID: </td><td><asp:Label ID="lblUserID" runat="server" ></asp:Label> </td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>User Name: </td><td><asp:Label ID="lblUserName" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Date of birth: </td><td><asp:Label ID="lblDob" runat="server"></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Gender: </td><td><asp:Label ID="lblGender" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Address: </td><td><asp:Label ID="lblAddress" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Mobile: </td><td><asp:Label ID="lblMobile" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td><asp:HiddenField ID="hdnUserID" runat="server"></asp:HiddenField></td><td>&nbsp;</td><td>&nbsp;</td></tr>
</table>
<asp:Button ID="btnGenToken" runat="server" Text="Generate Token" OnClick="btnGenToken_Click"></asp:Button>
    </asp:Panel>




       </div>
<asp:Panel ID="pnlToken" runat="server">

<table style="width:100%;">
    <tr><td><b>Token Information</b></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>

     
    <tr><td>Service Name: </td><td><asp:Label ID="lblSname" runat="server" ></asp:Label> </td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Service Token No: </td><td><asp:Label ID="lblTokenNo" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Queue No: </td><td><asp:Label ID="lblQNo" runat="server"></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Rooms: </td><td><asp:Label ID="lblRooms" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Bay Area: </td><td><asp:Label ID="lblBay" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td>Expected Waiting Time: </td><td><asp:Label ID="lblEWT" runat="server" ></asp:Label></td><td>&nbsp;</td><td>&nbsp;</td></tr>
    
</table>


</asp:Panel>
       <asp:Label ID="lblTokenMsg" runat="server"></asp:Label>
   </form>

</asp:Content>
