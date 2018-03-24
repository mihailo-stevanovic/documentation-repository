using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a DTO of the Document class used for displaying it internally.
    /// </summary>
    public class DocumentDtoInternal
    {
        #region Properties
        /// <summary>
        /// ID of the document.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Title of the document.
        /// </summary>        
        public string Title { get; set; }
        /// <summary>
        /// Product related to the document.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Product version related to the document.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Relative path to the HTML output of the document.
        /// </summary>
        public string HtmlLink { get; set; }
        /// <summary>
        /// Relative path to the PDF output of the document.
        /// </summary>
        public string PdfLink { get; set; }
        /// <summary>
        /// Relative path to the Word output of the document.
        /// </summary>
        public string WordLink { get; set; }
        /// <summary>
        /// Relative path to a misc output of the document.
        /// </summary>
        public string OtherLink { get; set; }
        /// <summary>
        /// Is document available for clients.
        /// </summary>        
        public bool IsFitForClients { get; set; }                                  
        /// <summary>
        /// Short description of the document.
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Document type.
        /// </summary>
        public string DocumentType { get; set; }
        /// <summary>
        /// Date of the latest update.
        /// </summary>
        public DateTime LatestUpdate { get; set; }
        /// <summary>
        /// List of the latest updated topics.
        /// </summary>
        [DataType(DataType.Html)]
        public string LatestTopicsUpdated { get; set; }        

        #region Internal Portal only
        /// <summary>
        /// Link to the related authors.
        /// </summary>
        public ICollection<AuthorDto> Authors { get; set; }
        /// <summary>
        /// Link to the related client catalogs.
        /// </summary>
        public ICollection<ClientCatalogDto> ClientCatalogs { get; set; }
        #endregion
        

        #endregion
    }
}
