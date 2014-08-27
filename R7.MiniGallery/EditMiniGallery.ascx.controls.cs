using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
	public partial class EditMiniGallery
	{
		protected LinkButton buttonUpdate;
		protected LinkButton buttonDelete;
		protected HyperLink linkCancel;
	
		protected LabelControl labelAlt;
		protected TextBox textAlt;

		protected LabelControl labelTitle;
		protected TextBox textTitle;

		protected LabelControl labelImage;
		//protected UrlControl urlImage;
		protected DnnFilePickerUploader pickerImage;

		protected LabelControl labelLink;
		protected UrlControl urlLink;

		protected LabelControl labelSortIndex;
		protected TextBox textSortIndex;

		protected LabelControl labelIsPublished;
		protected CheckBox checkIsPublished;

		//protected HtmlControl divPreview;
		/*
		protected LabelControl labelPreview;
		protected Image imagePreview;
		protected Button buttonUpdatePreview;*/

		protected ModuleAuditControl ctlAudit;

		protected Label labelTest;
	}
}
