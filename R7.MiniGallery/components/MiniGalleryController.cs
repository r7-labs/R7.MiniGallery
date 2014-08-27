using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using DotNetNuke.Services.Search.Entities;

namespace R7.MiniGallery
{
	public partial class MiniGalleryController : ControllerBase  /* : IPortable */
	{
		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="MiniGallery.MiniGalleryController"/> class.
		/// </summary>
		public MiniGalleryController () : base ()
		{ 

		}

		#endregion

		#region ModuleSearchBase implementaion

		public override IList<SearchDocument> GetModifiedSearchDocuments (ModuleInfo modInfo, DateTime beginDate)
		{
			var searchDocs = new List<SearchDocument> ();

			// TODO: Realize GetModifiedSearchDocuments()

			/* var sd = new SearchDocument();
			searchDocs.Add(searchDoc);
			*/

			return searchDocs;
		}

		#endregion

        
		#region Class-specific controller members (example)

		/*
		public IList<ImageInfo> GetImages (int moduleId, bool showAll, bool sortAscending)
		{
			var sortOrder = sortAscending ? "ASC" : "DESC";

			if (showAll)
				return GetList<ImageInfo> ("WHERE ModuleId = @0 ORDER BY SortIndex " + sortOrder, moduleId);

			return GetList<ImageInfo> ("WHERE ModuleId = @0 AND IsPublished=1 ORDER BY SortIndex " + sortOrder, moduleId);
		}
		*/

		public IEnumerable<ImageInfo> GetImagesTopN (int moduleId, bool showAll, bool sortAscending, int topn = 0)
		{
			var sql = string.Format (
				          "SELECT {0} * FROM dbo.MiniGallery_Images WHERE ModuleId={1} {2} ORDER BY SortIndex {3};",
				          (topn > 0) ? string.Format ("TOP({0})", topn) : string.Empty, 
				          moduleId,
				          !showAll ? "AND IsPublished=1" : string.Empty,
				          sortAscending ? "ASC" : "DESC"
			          );
		
			return GetObjects<ImageInfo> (System.Data.CommandType.Text, sql); 
		}

		#endregion

		/*

        #region IPortable members

		/// <summary>
		/// Exports a module to XML
		/// </summary>
		/// <param name="ModuleID">a module ID</param>
		/// <returns>XML string with module representation</returns>
		public string ExportModule (int moduleId)
		{
			var sb = new StringBuilder ();
			var infos = GetObjects<MiniGalleryInfo> (moduleId);

			if (infos != null) {
				sb.Append ("<MiniGallerys>");
				foreach (var info in infos) {
					sb.Append ("<MiniGallery>");
					sb.Append ("<content>");
					sb.Append (XmlUtils.XMLEncode (info.Content));
					sb.Append ("</content>");
					sb.Append ("</MiniGallery>");
				}
				sb.Append ("</MiniGallerys>");
			}
			
			return sb.ToString ();
		}

		/// <summary>
		/// Imports a module from an XML
		/// </summary>
		/// <param name="ModuleID"></param>
		/// <param name="Content"></param>
		/// <param name="Version"></param>
		/// <param name="UserID"></param>
		public void ImportModule (int ModuleID, string Content, string Version, int UserID)
		{
			var infos = DotNetNuke.Common.Globals.GetContent (Content, "MiniGallerys");
		
			foreach (XmlNode info in infos.SelectNodes("MiniGallery")) {
				var item = new MiniGalleryInfo ();
				item.ModuleID = ModuleID;
				item.Content = info.SelectSingleNode ("content").InnerText;
				item.CreatedByUser = UserID;

				Add<MiniGalleryInfo> (item);
			}
		}
		
	    #endregion
*/
	}
}

