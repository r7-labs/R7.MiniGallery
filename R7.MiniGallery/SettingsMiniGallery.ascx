<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsMiniGallery.ascx.cs" Inherits="R7.MiniGallery.SettingsMiniGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.MiniGallery/R7.MiniGallery/admin.css" Priority="200" />
<div class="dnnForm dnnClear MG_Settings">
	<h2 class="dnnFormSectionHead"><a href="" ><asp:Label runat="server" ResourceKey="sectionBaseSettings.Text" /></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelStyleSet" runat="server" ControlName="textStyleSet" Suffix=":" />
			<dnn:DnnComboBox id="comboStyleSet" runat="server" CssClass="comboStyleSet" /> 
			<asp:TextBox id="textStyleSet" runat="server" CssClass="textStyleSet" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelMaxHeight" runat="server" controlname="textMaxHeight" suffix=":" />
			<asp:TextBox id="textMaxHeight" runat="server" CssClass="NormalTextBox" />
		<div class="dnnFormItem">
			<dnn:Label id="labelImageWidth" runat="server" controlname="textImageWidth" suffix=":" />
			<asp:TextBox id="textImageWidth" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelImageHeight" runat="server" controlname="textImageHeight" suffix=":" />
			<asp:TextBox id="textImageHeight" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelTarget" runat="server" controlname="comboTarget" suffix=":" />
			<dnn:DnnComboBox id="comboTarget" runat="server" CssClass="comboTarget" />
			<asp:TextBox id="textTarget" runat="server" CssClass="textTarget" />
	   	</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelLightboxType" runat="server" controlname="checkLightboxType" suffix=":" />
			<dnn:DnnComboBox id="comboLightboxType" runat="server" />
		</div>
		<!--
		<div class="dnnFormItem">
			<dnn:Label id="labelUseScrollbar" runat="server" controlname="checkUseScrollbar" suffix="?" />
			<asp:CheckBox id="checkUseScrollbar" runat="server" Enabled="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelShowTitles" runat="server" controlname="checkShowTitles" suffix="?" />
			<asp:CheckBox id="checkShowTitles" runat="server" />
		</div>
		-->
		<div class="dnnFormItem">
			<dnn:Label id="labelColumns" runat="server" controlname="comboColumns" suffix=":" />
			<dnn:DnnComboBox id="comboColumns" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelExpand" runat="server" controlname="checkExpand" suffix="?" />
			<asp:CheckBox id="checkExpand" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelNumberOfRecords" runat="server" controlname="textNumberOfRecords" suffix=":" />
			<asp:TextBox id="textNumberOfRecords" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelSortOrder" runat="server" controlname="checkSortOrder" suffix=":" />
			<asp:CheckBox id="checkSortOrder" runat="server" />
		</div>
	</fieldset>
	<h2 class="dnnFormSectionHead"><a href="" ><asp:Label runat="server" ResourceKey="ImageHandler.Section" /></a></h2>
	<fieldset>
		<div class="dnnFormItem divUseImageHandler">
			<dnn:Label id="labelUseImageHandler" runat="server" controlname="checkUseImageHandler" suffix="?" />
			<asp:CheckBox id="checkUseImageHandler" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelImageHandlerParams" runat="server" controlname="textImageHandlerParams" suffix=":" />
			<asp:TextBox id="textImageHandlerParams" runat="server" CssClass="textImageHandlerParams" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelThumbWidth" runat="server" controlname="textThumbWidth" suffix=":" />
			<asp:TextBox id="textThumbWidth" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelThumbHeight" runat="server" controlname="textThumbHeight" suffix=":" />
			<asp:TextBox id="textThumbHeight" runat="server" CssClass="NormalTextBox" />
		</div>
	</fieldset>
	<h2 class="dnnFormSectionHead"><a href="#"><asp:Label runat="server" ResourceKey="HeaderAndFooter.Section" /></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelHeader" runat="server" ControlName="editorHeader" Suffix=":" />
			<dnn:TextEditor id="editorHeader" runat="server" HtmlEncode="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelFooter" runat="server" ControlName="editorFooter" Suffix=":" />
			<dnn:TextEditor id="editorFooter" runat="server" HtmlEncode="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelReplaceHeaderAndFooter" runat="server" ControlName="checkReplaceHeaderAndFooter" Suffix=":" />
			<asp:CheckBox id="checkReplaceHeaderAndFooter" runat="server" Checked="false" />
		</div>
	</fieldset>
</div>