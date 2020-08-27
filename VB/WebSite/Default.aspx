<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


	<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
		DataFile="~/App_Data/nwind.mdb" 
		SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
	</asp:AccessDataSource>
	<dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
		Text="Export to PDF">
	</dx:ASPxButton>
	<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
		GridViewID="ASPxGridView1">
	</dx:ASPxGridViewExporter>
	<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
		DataSourceID="AccessDataSource1" KeyFieldName="CategoryID">
		<Columns>
			<dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" 
				VisibleIndex="0">
				<EditFormSettings Visible="False" />
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
			</dx:GridViewDataTextColumn>
		</Columns>
	</dx:ASPxGridView>


</asp:Content>