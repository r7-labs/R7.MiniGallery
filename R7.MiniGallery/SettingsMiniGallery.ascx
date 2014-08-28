<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsMiniGallery.ascx.cs" Inherits="R7.MiniGallery.SettingsMiniGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="dnnForm dnnClear">
	
	<h2 class="dnnFormSectionHead"><a href="" ><asp:Label runat="server" ResourceKey="sectionBaseSettings.Text" /></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelStyleSet" runat="server" controlname="textStyleSet" suffix=":" />
			<asp:TextBox id="textStyleSet" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelMaxHeight" runat="server" controlname="textMaxHeight" suffix=":" />
			<asp:TextBox id="textMaxHeight" runat="server" CssClass="NormalTextBox" />
			<asp:RangeValidator id="valMaxHeight" ControlToValidate="textMaxHeight" MinimumValue="-1" /> 
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelImageWidth" runat="server" controlname="textImageWidth" suffix=":" />
			<asp:TextBox id="textImageWidth" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelImageHeight" runat="server" controlname="textImageHeight" suffix=":" />
			<asp:TextBox id="textImageHeight" runat="server" CssClass="NormalTextBox" />
		</div>
	<div class="dnnFormItem">
			<dnn:Label id="labelFrameWidth" runat="server" controlname="textFrameWidth" suffix=":" />
			<asp:TextBox id="textFrameWidth" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelFrameHeight" runat="server" controlname="textFrameHeight" suffix=":" />
			<asp:TextBox id="textFrameHeight" runat="server" CssClass="NormalTextBox" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelTarget" runat="server" controlname="ddlTarget" suffix=":" />
				<asp:DropDownList ID="ddlTarget" runat="server" Style="width:100px" >
				    <asp:ListItem Value="none" ResourceKey="ddlTargetItemNone.Text" />
			        <asp:ListItem Value="other" ResourceKey="ddlTargetItemOther.Text" />
			        <asp:ListItem Value="_blank" Selected="True" Text="_blank" />
	                <asp:ListItem Value="_top" Text="_top" />
	                <asp:ListItem Value="_parent" Text="_parent" />
	                <asp:ListItem Value="_self" Text="_self" />
	          </asp:DropDownList>
			  &#160;
			  <asp:TextBox id="textTarget" runat="server" CssClass="NormalTextBox" Style="width:310px" />
	   	</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelUseViewer" runat="server" controlname="checkUseViewer" suffix="?" />
			<asp:CheckBox id="checkUseViewer" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelUseScrollbar" runat="server" controlname="checkUseScrollbar" suffix="?" />
			<asp:CheckBox id="checkUseScrollbar" runat="server" Enabled="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelShowTitles" runat="server" controlname="checkShowTitles" suffix="?" />
			<asp:CheckBox id="checkShowTitles" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelColumns" runat="server" controlname="ddlColumns" suffix=":" />
			<asp:DropDownList ID="ddlColumns" runat="server" Style="width:100px" />
			<%-- <b> x </b> 
			<asp:DropDownList ID="ddlRows" runat="server" Style="width:100px" /> --%>
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelExpand" runat="server" controlname="checkExpand" suffix="?" />
			<asp:CheckBox id="checkExpand" runat="server" />
		</div>
		<%--
		<div class="dnnFormItem">
			<dnn:Label id="labelRows" runat="server" controlname="ddlRows" suffix=":" />
			
	   	</div>--%>
		
	</fieldset>
	
	<h2 class="dnnFormSectionHead"><a href="" ><asp:Label runat="server" ResourceKey="sectionHeader.Text" /></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<div class="dnnLabel"></div>
			<dnn:TextEditor id="editorHeader" runat="server" height="300" HtmlEncode="false" />
		</div>
	</fieldset>
	
	<h2 class="dnnFormSectionHead"><a href="" ><asp:Label runat="server" ResourceKey="sectionFooter.Text" /></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<div class="dnnLabel"></div>
			<dnn:TextEditor id="editorFooter" runat="server" height="300" HtmlEncode="false" />
		</div>
	</fieldset>
	
	<div class="dnnFormItem"></div>
	
</div>