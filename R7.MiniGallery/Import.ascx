<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Import.ascx.cs" Inherits="R7.MiniGallery.Import" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<div class="dnnForm dnnClear">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelFolder" runat="server" ControlName="ddlFolder" Suffix=":" />
			<asp:DropDownList id="ddlFolder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFolder_SelectedIndexChanged" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelThumbFilter" runat="server" ControlName="ddlThumbFilter" Suffix=":" />
			<asp:DropDownList id="ddlThumbFilter" runat="server" CssClass="dnnButtonDropdown" OnSelectedIndexChanged="ddlThumbFilter_SelectedIndexChanged" AutoPostBack="true">
				<asp:ListItem Value=".+[_-]+(small|prev|preview|thumb|thumbnail|tmb)" Selected="true" ResourceKey="ddlThumbFilterItem1.Text" />
		        <asp:ListItem Value="(small|prev|preview|thumb|thumbnail|tmb)[_-]+.+" ResourceKey="ddlThumbFilterItem2.Text" />
		        <asp:ListItem Value="custom" ResourceKey="ddlThumbFilterItemCustom.Text" />
			</asp:DropDownList>
		</div>
		<div class="dnnFormItem" id="divCustomThumbFilter" runat="server" Visible="false">
			<dnn:Label id="labelCustomThumbFilter" runat="server" ControlName="textCustomThumbFilter" Suffix=":" />
			<asp:TextBox id="textCustomThumbFilter" runat="server" CssClass="MG_Import_CustomFilter" />
			<asp:Button id="buttonApplyFilter" runat="server" ResourceKey="buttonApplyFilter" OnClick="buttonApplyFilter_Click" />
		</div>
		<div class="dnnFormItem">
		    <div class="dnnLabel"></div>
		    <div class="MG_Import_PairsContainer">
			   	<asp:DataList ID="listPairs" runat="server" OnItemDataBound="listPairs_ItemDataBound" RepeatLayout="Flow">
			  		<ItemTemplate>
						<asp:HiddenField id="hiddenThumb" runat="server" />
						<asp:CheckBox id="checkThumb" runat="server" />
						<br />
						<asp:DropDownList id="ddlFiles" runat="server" Width="300px" />
					</ItemTemplate>
				</asp:DataList>
			<div>
		</div>
	</fieldset>
	
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" OnClick="buttonUpdate_Click" Visible="false" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>

	
</div>