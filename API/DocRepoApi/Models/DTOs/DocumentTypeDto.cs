using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a DTO of a DocumentType.
    /// </summary>
    public class DocumentTypeDto : IDocRepoEntity<DocumentTypeDto>
    {
        #region Properties
        /// <summary>
        /// ID of the document type.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Full name of the document type.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }
        /// <summary>
        /// Short name (code) of the document type.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(5, ErrorMessage = "The Short name cannot be longer than 5 characters.")]
        public string ShortName { get; set; }
        /// <summary>
        /// Category of the document type.
        /// </summary>
        public DocumentCategory DocumentCategory { get; set; }
        #endregion

        #region Methods
        public int CompareTo(DocumentTypeDto other)
        {
            if (other == null)
            {
                return 1;
            }

            int comparedCategory = this.DocumentCategory.CompareTo(other.DocumentCategory);

            if (comparedCategory == 0)
            {
                return this.FullName.CompareTo(other.FullName);
            }

            return comparedCategory;
        }

        public bool Equals(DocumentTypeDto other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(DocumentTypeDto other, bool matchAll)
        {
            if (!matchAll)
            {
                return this.Equals(other);
            }
            if (!this.Equals(other))
            {
                return false;
            }
            if (!this.FullName.Equals(other.FullName))
            {
                return false;
            }
            if (!this.ShortName.Equals(other.ShortName))
            {
                return false;
            }
            if (!this.DocumentCategory.Equals(other.DocumentCategory))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
