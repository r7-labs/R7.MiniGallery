<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditMiniGallery.ascx.cs" Inherits="R7.MiniGallery.EditMiniGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %> 
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>

<div class="dnnForm dnnClear">
	<fieldset>	
		<div class="dnnFormItem">
			<dnn:Label id="labelImage" runat="server" ControlName="urlImage" Suffix=":" />
			<dnn:Picker id="pickerImage" runat="server" Required="true" />
		</div>
		<%-- <div class="dnnFormItem dnnFormRequired"> --%>
		<div class="dnnFormItem">
			<dnn:Label id="labelAlt" runat="server" ControlName="textAlt" Suffix=":" />
			<asp:TextBox id="textAlt" runat="server" />
			<%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="textAlt" 
				CssClass="dnnFormMessage dnnFormError" resourcekey="Alt.Required" /> --%>
		</div>
		<div class="dnnFormItem">
		    <dnn:Label id="labelTitle" runat="server" ControlName="textTitle" Suffix=":" />
			<asp:TextBox id="textTitle" runat="server" TextMode="Multiline" Rows="2" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelLink" runat="server" ControlName="urlLink" Suffix=":" />
			<dnn:Url id="urlLink" runat="server" UrlType="N" Style="float:left"
			        ShowFiles="true" ShowTabs="true"
			        ShowUrls="true" ShowUsers="true"
					ShowLog="false" ShowTrack="false"
					ShowNone="true" ShowNewWindow="false" />                
		</div> 
		<div class="dnnFormItem">
		    <dnn:Label id="labelSortIndex" runat="server" ControlName="textSortIndex" Suffix=":" />
			<asp:TextBox id="textSortIndex" runat="server" Text="0" />
		</div>
		<div class="dnnFormItem">
		    <dnn:Label id="labelIsPublished" runat="server" ControlName="checkIsPublished" Suffix="?" />
			<asp:CheckBox id="checkIsPublished" runat="server" Checked="true" />
		</div>
	</fieldset>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" /></li>
		<li><asp:LinkButton id="buttonDelete" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdDelete" CausesValidation="false" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>
	<hr />
	<dnn:Audit id="ctlAudit" runat="server" />
</div>