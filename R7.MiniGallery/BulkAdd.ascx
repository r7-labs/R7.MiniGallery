<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Import.ascx.cs" Inherits="R7.MiniGallery.BulkAdd" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.MiniGallery/css/admin.css" Priority="200" />
<div class="dnnForm dnnClear r7-mg-bulk-add">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelFolder" runat="server" ControlName="ddlFolders" Suffix=":" />
			<dnn:DnnFolderDropDownList id="ddlFolders" runat="server" AutoPostBack="true" />
		</div>
		<asp:Panel id="panelCheck" runat="server" Visible="false" CssClass="dnnFormItem mb-3">
			<div class="dnnLabel"></div>
			<asp:LinkButton id="buttonCheckAll" runat="server" CssClass="btn btn-sm btn-outline-secondary" ResourceKey="buttonCheckAll" />
			&#160;
			<asp:LinkButton id="buttonUncheckAll" runat="server" CssClass="btn btn-sm btn-outline-secondary" ResourceKey="buttonUncheckAll" />
			&#160;
			<asp:LinkButton id="buttonInvertSelection" runat="server" CssClass="btn btn-sm btn-outline-secondary" ResourceKey="buttonInvertSelection" />
		</asp:Panel>
		<asp:DataList ID="listImages" runat="server" OnItemDataBound="listImages_ItemDataBound"
			RepeatLayout="Flow" CssClass="listImages" >
			<ItemTemplate>
				<div class="dnnLabel"></div>
				<div class="divImage">
					<asp:Image id="imageImage" runat="server" CssClass="imageImage" />
				</div>
				<div class="divMiniForm">
					<asp:CheckBox id="checkIsIncluded" runat="server" Checked="false" />
					<asp:TextBox id="textOrder" runat="server" CssClass="textOrder" />
					<br />
					<asp:TextBox id="textTitle" runat="server" CssClass="textTitle" MaxLength="255"  />
				</div>
				<asp:HiddenField id="hiddenImageFileID" runat="server" />
			</ItemTemplate>
			<ItemStyle CssClass="dnnFormItem" />
		</asp:DataList>
	</fieldset>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonAdd" runat="server" CssClass="btn btn-primary mr-3" resourcekey="buttonAdd.Text" CausesValidation="true" OnClick="buttonAdd_Click" Visible="false" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="btn btn-outline-secondary" resourcekey="cmdCancel" /></li>
	</ul>
</div>
