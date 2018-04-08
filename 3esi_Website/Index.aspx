<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Index.aspx.cs" Inherits="_3esi_Website.Index" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>3esi - Enersight</h1>
        <p class="lead">Import Well and Group Entities into application from comma delimited CSV File</p>
        <table width="50%">
            <tr>
                <td>
                    <asp:Label ID="lblFileUploadTitle" runat="server" Text="Upload CSV File"></asp:Label>
                    <asp:FileUpload ID="CSVFileUpload" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

