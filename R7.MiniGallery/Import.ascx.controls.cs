using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
	public partial class Import
	{
		protected LinkButton buttonUpdate;
		protected HyperLink linkCancel;
	
		protected LabelControl labelFolder;
		protected DropDownList ddlFolder;

		//protected LabelControl labelFiles;
		protected LabelControl labelThumbFilter;
		protected DropDownList ddlThumbFilter;
		protected TextBox textCustomThumbFilter;
		protected HtmlControl divCustomThumbFilter;
		protected Button buttonApplyFilter;

		//protected CheckBoxList listFiles;
		//protected DnnListBox listFiles;
		//protected Button buttonSelectAll;
		//protected Button buttonUnselectAll;

		protected DataList listPairs;
	
		//protected TextBox textTest;

	}
}
