using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models 
{
    /// <summary>
    /// DTO of the Document class. Used for publishing documents.
    /// </summary>
    public class DocumentDto : IDocRepoEntity<DocumentDto>
    {
        #region Properties
        /// <summary>
        /// ID of the document.
        /// </summary>        
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
        /// Is document available to clients.
        /// </summary>        
        public bool IsFitForClients { get; set; }

        /// <summary>
        /// Short description of the document.
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Author-ID ID of the document.
        /// </summary>
        public int? AitId { get; set; }

        /// <summary>
        /// List of related author IDs.
        /// </summary>
        public ICollection<int> DocumentAuthorIds { get; set; }

        /// <summary>
        /// List of related client catalog IDs.
        /// </summary>
        public ICollection<int> DocumentCatalogIds { get; set; }             

        /// <summary>
        /// List of the latest updated topics.
        /// </summary>
        [DataType(DataType.Html)]
        public string LatestTopicsUpdated { get; set; }       
        /// <summary>
        /// Used to restrict the visibility of the update during publishing.
        /// </summary>
        public bool IsPublished { get; set; }

        // Foreign Key
        /// <summary>
        /// ID of the parent document.
        /// </summary>
        public int? ParentDocumentId { get; set; }        

        // Foreign Key
        /// <summary>
        /// ID of the related product version.
        /// </summary>
        public int ProductVersionId { get; set; }
        
        // Foreign Key
        /// <summary>
        /// ID of the related document type.
        /// </summary>
        public int DocumentTypeId { get; set; }        

        /// <summary>
        /// Used to prevent DB concurrency issues during updates.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        #endregion

        #region Methods

        public int CompareTo(DocumentDto other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int comparedProduct = this.ProductVersionId.CompareTo(other.ProductVersionId);

                if (comparedProduct == 0)
                {
                    return this.Title.CompareTo(other.Title);
                }
                else
                {
                    return comparedProduct * -1;
                }
            }
        }

        public bool Equals(DocumentDto other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(DocumentDto other, bool matchAll)
        {
            if (!matchAll)
            {
                return this.Equals(other);
            }
            if (other == null)
            {
                return false;
            }
            if (!this.Id.Equals(other.Id))
            {
                return false;
            }
            if (this.Title != null && !this.Title.Equals(other.Title))
            {
                return false;
            }
            if (this.AitId != null && !this.AitId.Equals(other.AitId))
            {
                return false;
            }
            // Check Authors
            if (this.DocumentAuthorIds != null && other.DocumentAuthorIds != null && this.DocumentAuthorIds.Count != other.DocumentAuthorIds.Count)
            {
                return false;
            }
            List<int> thisDocAuthList = (List<int>)this.DocumentAuthorIds;
            thisDocAuthList.Sort();
            List<int> otherDocAuthList = (List<int>)other.DocumentAuthorIds;
            otherDocAuthList.Sort();
            for (int i = 0; i < thisDocAuthList.Count; i++)
            {
                if (thisDocAuthList[i] != otherDocAuthList[i])
                {
                    return false;
                }
            }
            // Check Catalogs
            if (this.DocumentCatalogIds != null && other.DocumentCatalogIds != null && this.DocumentCatalogIds.Count != other.DocumentCatalogIds.Count)
            {
                return false;
            }
            List<int> thisDocCatList = (List<int>)this.DocumentCatalogIds;
            thisDocAuthList.Sort();
            List<int> otherDocCatList = (List<int>)other.DocumentCatalogIds;
            otherDocAuthList.Sort();
            for (int i = 0; i < thisDocCatList.Count; i++)
            {
                if (thisDocCatList[i] != otherDocCatList[i])
                {
                    return false;
                }
            }

            if (!this.DocumentTypeId.Equals(other.DocumentTypeId))
            {
                return false;
            }            
            if (this.HtmlLink != null && !this.HtmlLink.Equals(other.HtmlLink))
            {
                return false;
            }
            if (this.PdfLink != null && !this.PdfLink.Equals(other.PdfLink))
            {
                return false;
            }
            if (this.WordLink != null && !this.WordLink.Equals(other.WordLink))
            {
                return false;
            }
            if (this.OtherLink != null && !this.OtherLink.Equals(other.OtherLink))
            {
                return false;
            }
            if (!this.IsFitForClients.Equals(other.IsFitForClients))
            {
                return false;
            }
            if (!this.IsPublished.Equals(other.IsPublished))
            {
                return false;
            }
            if (this.LatestTopicsUpdated != null && !this.LatestTopicsUpdated.Equals(other.LatestTopicsUpdated))
            {
                return false;
            }
            if (this.ParentDocumentId != null && !this.ParentDocumentId.Equals(other.ParentDocumentId))
            {
                return false;
            }
            if (this.ShortDescription != null && !this.ShortDescription.Equals(other.ShortDescription))
            {
                return false;
            }
            //if (this.RowVersion != null && !this.RowVersion.Equals(other.RowVersion))
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
