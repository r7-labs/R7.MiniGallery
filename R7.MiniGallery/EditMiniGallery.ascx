<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditMiniGallery.ascx.cs" Inherits="R7.MiniGallery.EditMiniGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/DnnUrlControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %> 
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.MiniGallery/css/admin.css" Priority="200" />
<div class="dnnForm dnnClear">
	<fieldset>	
		<div class="dnnFormItem">
			<dnn:Label id="labelImage" runat="server" ControlName="urlImage" Suffix=":" />
			<dnn:Picker id="pickerImage" runat="server" Required="true" />
		</div>
		<div class="dnnFormItem">
            <dnn:Label id="labelTitle" runat="server" ControlName="textTitle" Suffix=":" />
            <asp:TextBox id="textTitle" runat="server" TextMode="Multiline" Rows="2" />
        </div>
		<div class="dnnFormItem">
			<dnn:Label id="labelAlt" runat="server" ControlName="textAlt" Suffix=":" />
			<asp:TextBox id="textAlt" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelLink" runat="server" ControlName="urlLink" Suffix=":" />
			<dnn:Url id="urlLink" runat="server" UrlType="N" CssClass="dnnLeft"
			        ShowFiles="true" ShowTabs="true"
			        ShowUrls="true" ShowUsers="true"
					ShowLog="false" ShowTrack="false"
					ShowNone="true" ShowNewWindow="false"
					IncludeActiveTab="true" />                
		</div> 
		<div class="dnnFormItem">
		    <dnn:Label id="labelSortIndex" runat="server" ControlName="textSortIndex" Suffix=":" />
			<asp:TextBox id="textSortIndex" runat="server" />
		</div>
        <div class="dnnFormItem">
            <dnn:Label ID="labelStartDate" runat="server" ControlName="datetimeStartDate" />
            <dnn:DnnDateTimePicker id="datetimeStartDate" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="labelEndDate" runat="server" ControlName="datetimeEndDate" />
            <dnn:DnnDateTimePicker id="datetimeEndDate" runat="server" />
        </div>
	</fieldset>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" /></li>
		<li>&nbsp;</li>
		<li><asp:LinkButton id="buttonDelete" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdDelete" CausesValidation="false" /></li>
		<li><asp:LinkButton id="btnDeleteWithFile" runat="server" CssClass="dnnSecondaryAction" ResourceKey="btnDeleteWithFile.Text" CausesValidation="false" /></li>
		<li>&nbsp;</li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>
	<hr />
	<dnn:Audit id="ctlAudit" runat="server" />
</div>