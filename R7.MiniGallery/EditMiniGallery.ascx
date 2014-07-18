<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditMiniGallery.ascx.cs" Inherits="R7.MiniGallery.EditMiniGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %> 
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>

<div class="dnnForm dnnClear">
	
	<asp:Label id="labelTest" runat="server" />
	<asp:ValidationSummary id="summaryTop" ValidationGroup="ValGroup1" />
	
	<fieldset>	
		<div class="dnnFormItem">
			<dnn:Label id="labelImage" runat="server" ControlName="urlImage" Suffix=":" />
			<dnn:Picker id="pickerImage" runat="server" Required="true" />
			<%--
			<dnn:Url id="urlImage" runat="server"  
			        ShowFiles="true" ShowTabs="false" 
			        ShowUrls="false" ShowUsers="false"
					ShowLog="false" ShowTrack="false"  
					ShowNone="false" ShowNewWindow="false" /> --%>
		</div>
			
			<%--
		<div class="dnnFormItem">
			<dnn:Label id="labelPreview" runat="server" ControlName="imagePreview" Suffix=":" />
			<asp:Image id="imagePreview" runat="server" CssClass="MG_Preview" />
			<asp:Button id="buttonUpdatePreview" runat="server" CssClass="MG_PreviewButton" ResourceKey="buttonUpdatePreview.Text" OnClick="buttonUpdatePreview_Click" />
		</div>--%>
			
		<div class="dnnFormItem">
			<dnn:Label id="labelAlt" runat="server" ControlName="textAlt" Suffix=":" />
			<asp:TextBox id="textAlt" runat="server" />
			<asp:RequiredFieldValidator id="valAlt" ControlToValidate="textAlt" ValidationGroup="ValGroup1" />
		</div>

		<div class="dnnFormItem">
		    <dnn:Label id="labelTitle" runat="server" ControlName="textTitle" Suffix=":" />
			<asp:TextBox id="textTitle" runat="server" TextMode="Multiline" Rows="2" />
		</div>
			
		<div class="dnnFormItem">
			<dnn:Label id="labelLink" runat="server" ControlName="urlLink" Suffix=":" />
			<dnn:Url id="urlLink" runat="server" UrlType="F" 
			        ShowFiles="true" ShowTabs="true"
			        ShowUrls="true" ShowUsers="true"
					ShowLog="true" ShowTrack="true"
					ShowNone="true" ShowNewWindow="false" />                
		</div> 
		
		<div class="dnnFormItem">
		    <dnn:Label id="labelSortIndex" runat="server" ControlName="textSortIndex" Suffix=":" />
			<asp:TextBox id="textSortIndex" runat="server">10</asp:TextBox>
		</div>
		
		<div class="dnnFormItem">
		    <dnn:Label id="labelIsPublished" runat="server" ControlName="checkIsPublished" Suffix=":" />
			<asp:CheckBox id="checkIsPublished" runat="server" Checked="true" />
		</div>
		               
	</fieldset>
		
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" /></li>
		<li><asp:LinkButton id="buttonDelete" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdDelete" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>

	<dnn:Audit id="ctlAudit" runat="server" />
	
</div>
<script language="javascript" type="text/javascript">
	$('#<%= buttonDelete.ClientID %>').dnnConfirm({
	  text: '<%= LocalizeString("ConfirmDelete.Text") %>',
	  yesText: '<%= Localization.GetString("Yes.Text", Localization.SharedResourceFile) %>',
	  noText: '<%= Localization.GetString("No.Text", Localization.SharedResourceFile) %>',
	  title: '<%= Localization.GetString("Confirm.Text", Localization.SharedResourceFile) %>'
	});
</script>