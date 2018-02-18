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
        [Required]
        public bool FitForClients { get; set; }
        public string ClientCatalog { get; set; }
        public string ShortDescription { get; set; }
        public int AitId { get; set; }

        // Foreign Key        
        public ICollection<Author> Authors { get; set; }

        // Foreign Key
        public int LatestUpdateId { get; set; }
        public DocumentUpdate LatestUpdate { get; set; }

        // Foreign Key
        public int ParentDocumentId { get; set; }

        // Foreign Key
        public int ProductVersionId { get; set; }
        public ProductVersion ProductVersion { get; set; }

        // Foreign Key
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
