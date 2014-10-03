using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
	public partial class BulkAdd
	{
		protected LinkButton buttonAdd;
		protected HyperLink linkCancel;
		protected LabelControl labelFolder;
		protected DnnFolderDropDownList ddlFolders;
		protected DataList listImages;
		protected Panel panelCheck;
		protected LinkButton buttonCheckAll;
		protected LinkButton buttonUncheckAll;
		protected LinkButton buttonInvertSelection;
	}
}
