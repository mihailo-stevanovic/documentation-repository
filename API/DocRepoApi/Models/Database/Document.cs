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
    public class Document : IDocRepoEntity<Document>
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

        // Necessary in order to retrieve all the related updates.
        /// <summary>
        /// Link to all the updates made to the document.
        /// </summary>
        public ICollection<DocumentUpdate> Updates { get; set; }

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
        [Required]
        public int ProductVersionId { get; set; }
        /// <summary>
        /// Link to the related product version.
        /// </summary>
        public ProductVersion ProductVersion { get; set; }

        // Foreign Key
        /// <summary>
        /// ID of the related document type.
        /// </summary>
        [Required]
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

        public int CompareTo(Document other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int comparedProductVersion = this.ProductVersion.CompareTo(other.ProductVersion);

                if (comparedProductVersion == 0)
                {
                    int comparedDocumentType = this.DocumentType.CompareTo(other.DocumentType);

                    if (comparedDocumentType == 0)
                    {
                        return this.Title.CompareTo(other.Title);
                    }
                    else
                    {
                        return comparedDocumentType;
                    }
                }
                else
                {
                    return comparedProductVersion;
                }
            }
        }

        public bool Equals(Document other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(Document other, bool matchAll)
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

            //// Check Authors only if full data is available
            //if ((this.DocumentAuthors != null && other.DocumentAuthors == null) || (this.DocumentAuthors == null && other.DocumentAuthors != null) || (this.DocumentAuthors.Count != other.DocumentAuthors.Count))
            //{
            //    return false;
            //}
            
            //List<Author> thisDocAuthList = (List<Author>)this.DocumentAuthors.Select(da => da.Author);
            //thisDocAuthList.Sort();
            //List<Author> otherDocAuthList = (List<Author>)other.DocumentAuthors.Select(da => da.Author);
            //otherDocAuthList.Sort();
            //for (int i = 0; i < thisDocAuthList.Count; i++)
            //{
            //    if (thisDocAuthList[i].Equals(otherDocAuthList[i], true))
            //    {
            //        return false;
            //    }
            //}

            //// Check Catalog
            //if (this.DocumentCatalogs != null && other.DocumentCatalogs != null && this.DocumentCatalogs.Count != other.DocumentCatalogs.Count)
            //{
            //    return false;
            //}
            //List<DocumentCatalog> thisDocCatList = (List<DocumentCatalog>)this.DocumentCatalogs;
            //thisDocCatList.Sort();
            //List<DocumentCatalog> otherDocCatList = (List<DocumentCatalog>)other.DocumentCatalogs;
            //otherDocAuthList.Sort();
            //for (int i = 0; i < thisDocCatList.Count; i++)
            //{
            //    if (thisDocCatList[i].Catalog.Equals(otherDocCatList[i].Catalog, true))
            //    {
            //        return false;
            //    }
            //}

            return true;
        }
        #endregion
    }
}
