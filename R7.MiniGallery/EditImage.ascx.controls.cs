using System.Web.UI.WebControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
    public partial class EditImage
	{
		protected LinkButton buttonUpdate;
		protected LinkButton buttonDelete;
        protected LinkButton btnDeleteWithFile;
		protected HyperLink linkCancel;
		protected TextBox textAlt;
		protected TextBox textTitle;
		protected DnnFilePickerUploader pickerImage;
		protected DnnUrlControl urlLink;
        protected CheckBox chkOpenInLightbox;
        protected TextBox txtCssClass;
		protected TextBox textSortIndex;
        protected DnnDateTimePicker datetimeStartDate;
        protected DnnDateTimePicker datetimeEndDate;
		protected ModuleAuditControl ctlAudit;
	}
}
