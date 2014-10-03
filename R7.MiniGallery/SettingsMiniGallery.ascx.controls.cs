using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.MiniGallery
{
	public partial class SettingsMiniGallery
	{
		protected LabelControl labelViewerCssClass;
		protected TextBox textViewerCssClass;
		protected LabelControl labelStyleSet;
		protected DnnComboBox comboStyleSet;
		protected TextBox textStyleSet;
		protected LabelControl labelMaxHeight;
		protected TextBox textMaxHeight;
		protected LabelControl labelTarget;
		protected DnnComboBox comboTarget;
		protected TextBox textTarget;
		protected LabelControl labelLightboxType;
		protected DnnComboBox comboLightboxType;
		protected LabelControl labelUseScrollbar;
		protected CheckBox checkUseScrollbar;
		protected LabelControl labelShowTitles;
		protected CheckBox checkShowTitles;
		protected LabelControl labelExpand;
		protected CheckBox checkExpand;
		protected LabelControl labelHeader;
		protected TextEditor editorHeader;
		protected LabelControl labelFooter;
		protected TextEditor editorFooter;
		protected LabelControl labelColumns;
		protected DnnComboBox comboColumns;
		protected LabelControl labelThumbWidth;
		protected TextBox textThumbWidth;
		protected LabelControl labelThumbHeight;
		protected TextBox textThumbHeight;
		protected LabelControl labelImageWidth;
		protected TextBox textImageWidth;
		protected LabelControl labelImageHeight;
		protected TextBox textImageHeight;
		protected CheckBox checkUseImageHandler;
		protected TextBox textImageHandlerParams;
		protected LabelControl labelNumberOfRecords;
		protected TextBox textNumberOfRecords;
		protected LabelControl labelSortOrder;
		protected CheckBox checkSortOrder;
		protected LabelControl labelReplaceHeaderAndFooter;
		protected CheckBox checkReplaceHeaderAndFooter;
	}
}
