<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Import.ascx.cs" Inherits="R7.MiniGallery.BulkAdd" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<div class="dnnForm dnnClear">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelFolder" runat="server" ControlName="ddlFolders" Suffix=":" />
			<dnn:DnnFolderDropDownList id="ddlFolders" runat="server" AutoPostBack="true" />
			<%-- <asp:DropDownList id="ddlFolder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFolder_SelectedIndexChanged" /> --%>
		</div>
		<%--
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
		--%>
		<div class="dnnFormItem">
			<div class="dnnLabel"></div>
		    <asp:DataList ID="listImages" runat="server" OnItemDataBound="listImages_ItemDataBound" 
				RepeatLayout="Flow" Style="width:47%">
				<ItemTemplate>
					<asp:Image id="imageImage" runat="server" Style="float:left;width:120px" />
					<div style="float:left;padding-left:10px">
						<asp:CheckBox id="checkIsIncluded" runat="server" />
						<asp:TextBox id="textAlt" runat="server" Style="float:left;display:inline-block" />
						<asp:TextBox id="textSortIndex" runat="server" Style="float:left" />
					</div>
				</ItemTemplate>
				<ItemStyle CssClass="MG_BulkAddImagesList_Item" />
			</asp:DataList>
		</div>
	</fieldset>
	
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdUpdate" CausesValidation="true" OnClick="buttonUpdate_Click" Visible="false" /></li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
	</ul>

	
</div>