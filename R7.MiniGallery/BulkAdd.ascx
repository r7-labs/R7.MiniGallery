<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Import.ascx.cs" Inherits="R7.MiniGallery.BulkAdd" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.MiniGallery/R7.MiniGallery/admin.css" Priority="200" />
<div class="dnnForm dnnClear MG_BulkAdd">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelFolder" runat="server" ControlName="ddlFolders" Suffix=":" />
			<dnn:DnnFolderDropDownList id="ddlFolders" runat="server" AutoPostBack="true" />
		</div>
		<asp:Panel id="panelCheck" runat="server" Visible="false" CssClass="dnnFormItem">
			<div class="dnnLabel"></div>
			<asp:LinkButton id="buttonCheckAll" runat="server" CssClass="dnnSecondaryAction" ResourceKey="buttonCheckAll" />
			&#160;
			<asp:LinkButton id="buttonUncheckAll" runat="server" CssClass="dnnSecondaryAction" ResourceKey="buttonUncheckAll" />
			&#160;
			<asp:LinkButton id="buttonInvertSelection" runat="server" CssClass="dnnSecondaryAction" ResourceKey="buttonInvertSelection" />
		</asp:Panel>
		<asp:DataList ID="listImages" runat="server" OnItemDataBound="listImages_ItemDataBound" 
			RepeatLayout="Flow" CssClass="listImages" >
			<ItemTemplate>
				<div class="dnnLabel"></div>
				<div class="divImage">	
					<asp:Image id="imageImage" runat="server" CssClass="imageImage" />
				</div>
				<div class="divMiniForm">
					<asp:CheckBox id="checkIsIncluded" runat="server" Checked="true" />	
					<asp:TextBox id="textSortIndex" runat="server" CssClass="textSortIndex" ToolTip="Sort Index" Text="0" />
					<br />
					<asp:TextBox id="textTitle" runat="server" CssClass="textTitle" ToolTip="Title" MaxLength="255"  />
				</div>		
				<asp:HiddenField id="hiddenImageFileID" runat="server" />
			</ItemTemplate>
			<ItemStyle CssClass="dnnFormItem" />
		</asp:DataList>
	</fieldset>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" OnClick="buttonUpdate_Click" Visible="false" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>
</div>
