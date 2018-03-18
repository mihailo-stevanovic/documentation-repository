using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a single document on the repository.
    /// </summary>
    public class Document
    {
        #region Properties
        /// <summary>
        /// ID of the document.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Title of the document.
        /// </summary>
        [Required]
        public string Title { get; set; }
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
        [Display(Name = "Fit for clients")]
        public bool IsFitForClients { get; set; }

        /* 
         * To be used in the DTO class
        /// <summary>
        /// Name of the client catalog where the document is available on.
        /// </summary>        
        DisplayFormat(NullDisplayText = "N/A")]
        public string ClientCatalog { get; set; }
            */

        /// <summary>
        /// Short description of the document.
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Author-ID ID of the document.
        /// </summary>
        public int? AitId { get; set; }
                   
        /// <summary>
        /// Link to the related authors.
        /// </summary>
        public ICollection<DocumentAuthor> DocumentAuthors { get; set; }

        /// <summary>
        /// Link to the related client catalogs.
        /// </summary>
        public ICollection<DocumentCatalog> DocumentCatalogs { get; set; }

        // Should be handleded differently
        //// Foreign Key
        //public int LatestUpdateId { get; set; }
        //public DocumentUpdate LatestUpdate { get; set; }

        // Foreign Key
        /// <summary>
        /// ID of the parent document.
        /// </summary>
        public int? ParentDocumentId { get; set; }
        /// <summary>
        /// Link to the parent document.
        /// </summary>
        public virtual Document ParentDocument { get; set; }
        /// <summary>
        /// Link to the child documents.
        /// </summary>
        public virtual ICollection<Document> ChildDocuments { get; set; }

        // Foreign Key
        /// <summary>
        /// ID of the related product version.
        /// </summary>
        public int ProductVersionId { get; set; }
        /// <summary>
        /// Link to the related product version.
        /// </summary>
        public ProductVersion ProductVersion { get; set; }

        // Foreign Key
        /// <summary>
        /// ID of the related document type.
        /// </summary>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// Link to the related document type.
        /// </summary>
        public DocumentType DocumentType { get; set; }              

        /// <summary>
        /// Used to prevent DB concurrency issues during updates.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }
        #endregion
    }
}
