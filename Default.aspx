<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebSiteStatusChecker.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <br />
        <br />
        <asp:TextBox ID="updateText" Height="20px" BorderColor="Black" runat="server" Columns="2" BorderStyle="Solid" Width="650px" BackColor="#CEF3FF" Font-Italic="True" ForeColor="#666666">Buraya Site Adresini yazıp Kayıt Et&#39;e Tıklayınız Örnek Adres &quot;https://keos.alanya.bel.tr/keos&quot;</asp:TextBox>
        <br />
        <asp:RadioButton ID="isSendRequest" runat="server" Text="Siteye İstek Gönder"/>
        <asp:RadioButton ID="isSendMail" runat="server" Text="Mail Gönder"  />
        <br />
        <br />
        <asp:Button runat="server" ID="insertButton2" Text="Kayıt ET" OnClick="insertButton2_Click" />
        <asp:Button runat="server" ID="insertButton" BorderColor="YellowGreen" OnClick="Page_Load" Text="Yenile" />
         <br />
        <br />
        <asp:Button runat="server" ID="sendRequest" OnClick="sendRequest_Click" Text="İstek Gönder" />
        <br />
        <br />
        <br />
        <asp:GridView ID="gridView" runat="server" BorderColor="Black" CellPadding="5" EmptyDataText="BOŞ" BackColor="Black" BorderStyle="Dashed" BorderWidth="1px" style="margin-right: 0px" CellSpacing="1">
            <AlternatingRowStyle BackColor="#C7F1F1" BorderColor="Black" />
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#E1FFF0" BorderColor="Black" />
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="#FFFFCC" BorderColor="Black" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
    </form>
</body>
</html>
