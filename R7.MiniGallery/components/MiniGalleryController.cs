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

namespace R7.MiniGallery
{
	public partial class MiniGalleryController  /* : IPortable */
	{
        #region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="MiniGallery.MiniGalleryController"/> class.
		/// </summary>
		public MiniGalleryController ()
		{ 

		}

		/// <summary>
		/// Adds a new T object into the database
		/// </summary>
		/// <param name='info'></param>
		public void Add<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Insert (info);
			}
		}

		/// <summary>
		/// Get single object from the database
		/// </summary>
		/// <returns>
		/// The object
		/// </returns>
		/// <param name='itemId'>
		/// Item identifier.
		/// </param>
		public T Get<T> (int itemId) where T: class
		{
			T info;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				info = repo.GetById (itemId);
			}

			return info;
		}
		
		/// <summary>
		/// Get single object from the database
		/// </summary>
		/// <returns>
		/// The object
		/// </returns>
		/// <param name='itemId'>
		/// Item identifier.
		/// </param>
		/// <param name='scopeId'>
		/// Scope identifier (like moduleId)
		/// </param>
		public T Get<T> (int itemId, int scopeId) where T: class
		{
			T info;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				info = repo.GetById (itemId, scopeId);
			}

			return info;
		}
		
		/// <summary>
		/// Updates an object already stored in the database
		/// </summary>
		/// <param name='info'>
		/// Info.
		/// </param>
		public void Update<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Update (info);
			}
		}

		/// <summary>
		/// Gets all objects for items matching scopeId
		/// </summary>
		/// <param name='scopeId'>
		/// Scope identifier (like moduleId)
		/// </param>
		/// <returns></returns>
		public IEnumerable<T> GetObjects<T> (int scopeId) where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.Get (scopeId);
				
				// Without [Scope("ModuleID")] it should be like:
				// infos = repo.Find ("WHERE ModuleID = @0", moduleId);
			}

			return infos;
		}
		
		/// <summary>
		/// Gets all objects of type T from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetObjects<T> () where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.Get ();
			}

			return infos;
		}

		/// <summary>
		/// Gets the all objects of type T from result of a dynamic sql query
		/// </summary>
		/// <returns>Enumerable with objects of type T</returns>
		/// <param name="sqlCondition">SQL command condition.</param>
		/// <param name="args">SQL command arguments.</param>
		/// <typeparam name="T">Type of objects.</typeparam>
		public IEnumerable<T> GetObjects<T> (string sqlConditon, params object [] args) where T: class
		{
			IEnumerable<T> infos = null;
			
			using (var ctx = DataContext.Instance()) 
			{
				var repo = ctx.GetRepository<T> ();
				infos = repo.Find (sqlConditon, args);
			}
			
			return infos;
		}

		/// <summary>
		/// Gets the all objects of type T from result of a dynamic sql query
		/// </summary>
		/// <returns>Enumerable with objects of type T</returns>
		/// <param name="cmdType">Type of an SQL command.</param>
		/// <param name="sql">SQL command.</param>
		/// <param name="args">SQL command arguments.</param>
		/// <typeparam name="T">Type of objects.</typeparam>
		public IEnumerable<T> GetObjects<T> (System.Data.CommandType cmdType, string sql, params object [] args) where T: class
		{
			IEnumerable<T> infos = null;
			
			using (var ctx = DataContext.Instance()) 
			{
				infos = ctx.ExecuteQuery<T>	( cmdType, sql, args);
			}
			
			return infos;
		}

		/// <summary>
		/// Gets one page of objects of type T
		/// </summary>
		/// <param name="scopeId">Scope identifier (like moduleId)</param>
		/// <param name="index">a page index</param>
		/// <param name="size">a page size</param>
		/// <returns>A paged list of T objects</returns>
		public IPagedList<T> GetPage<T> (int scopeId, int index, int size) where T: class
		{
			IPagedList<T> infos;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.GetPage (scopeId, index, size);
			}

			return infos;
		}
		
		/// <summary>
		/// Gets one page of objects of type T
		/// </summary>
		/// <param name="index">a page index</param>
		/// <param name="size">a page size</param>
		/// <returns>A paged list of T objects</returns>
		public IPagedList<T> GetPage<T> (int index, int size) where T: class
		{
			IPagedList<T> infos;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.GetPage (index, size);
			}

			return infos;
		}

		/// <summary>
		/// Delete a given item from the database by instance
		/// </summary>
		/// <param name='info'></param>
		public void Delete<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Delete (info);
		
			}
		}
		
		/// <summary>
		/// Delete a given item from the database by ID
		/// </summary>
		/// <param name='itemId'></param>
		public void Delete<T> (int itemId) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Delete (repo.GetById (itemId));
			}
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

		public IList<ImageInfo> GetImagesTopN (int moduleId, bool showAll, bool sortAscending, int topn = 0)
		{
			var sql = string.Format (
				"SELECT {0} * FROM dbo.MiniGallery_Images WHERE ModuleId={1} {2} ORDER BY SortIndex {3};",
				(topn > 0)? string.Format("TOP({0})", topn) : "", 
				moduleId,
				showAll? "" : "AND IsPublished=1",
				sortAscending ? "ASC" : "DESC"
			);
		
			return GetObjects<ImageInfo> (System.Data.CommandType.Text, sql).ToList(); 
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

