using System.Web.UI.WebControls;
using DotNetNuke.UI.UserControls;
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
		protected DnnFilePickerUploader pickerImage;

		protected LabelControl labelLink;
		protected DnnUrlControl urlLink;

		protected LabelControl labelSortIndex;
		protected TextBox textSortIndex;

		protected LabelControl labelIsPublished;
		protected CheckBox checkIsPublished;

		protected ModuleAuditControl ctlAudit;

		protected Label labelTest;
	}
}
