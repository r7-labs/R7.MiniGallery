<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewMiniGallery.ascx.cs" Inherits="R7.MiniGallery.ViewMiniGallery" %>
<asp:DataList id="listImages" DataKeyField="ImageID" runat="server" CssClass="MG_List"
	RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="listImages_ItemDataBound">
	<ItemTemplate>
		<div style="display:inline-block">
			<asp:HyperLink id="linkEdit" runat="server" Visible="<%# IsEditable %>">
				<asp:Image id="imageEdit" runat="server" ImageUrl="<%# EditIconUrl %>" Visible="<%# IsEditable %>" ResourceKey="Edit" CssClass="MG_Edit" />
			</asp:HyperLink>
			<asp:HyperLink id="linkImage" runat="server">
				<asp:Image id="imageImage" runat="server" CssClass="MG_Image" />

			</asp:HyperLink>
			<asp:Label id="labelTitle" runat="server" CssClass="MG_Title" />
			<asp:Label id="labelInfo" runat="server" />
		</div>
	</ItemTemplate>
	<ItemStyle CssClass="MG_Item" />
	<AlternatingItemStyle CssClass="MG_AltItem" />
</asp:DataList>

	<!--- <script language="javascript" type="text/javascript" src="/js/lightbox2/js/lightbox-2.6.min.js"></script>
<link href="/js/lightbox2/js/css/lightbox.css" rel="stylesheet" /> -->
