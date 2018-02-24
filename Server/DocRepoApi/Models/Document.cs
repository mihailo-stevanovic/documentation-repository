using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string HtmlLink { get; set; }
        public string PdfLink { get; set; }
        public string WordLink { get; set; }
        public string OtherLink { get; set; }
        [Display(Name = "Fit for clients")]
        public bool IsFitForClients { get; set; }
        [DisplayFormat(NullDisplayText = "N/A")]
        public string ClientCatalog { get; set; }
        public string ShortDescription { get; set; }
        public int? AitId { get; set; }
                   
        
        public ICollection<DocumentAuthor> DocumentAuthors { get; set; }

        // Should be handleded differently
        //// Foreign Key
        //public int LatestUpdateId { get; set; }
        //public DocumentUpdate LatestUpdate { get; set; }

        // Foreign Key
        public int? ParentDocumentId { get; set; }
        public virtual Document ParentDocument { get; set; }
        public virtual ICollection<Document> ChildDocuments { get; set; }

        // Foreign Key
        public int ProductVersionId { get; set; }
        public ProductVersion ProductVersion { get; set; }

        // Foreign Key
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
