using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a specific document type.
    /// </summary>
    public class DocumentType : IDocRepoEntity<DocumentType>
    {
        #region Properties
        /// <summary>
        /// ID of the document type.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Full name of the document type.
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Short name (code) of the document type.
        /// </summary>
        [Required]
        [StringLength(5, ErrorMessage = "The Short name cannot be longer than 5 characters.")]
        public string ShortName { get; set; }        
        /// <summary>
        /// Category of the document type.
        /// </summary>
        public DocumentCategory DocumentCategory { get; set; }
        #endregion

        #region Methods
        public int CompareTo(DocumentType other)
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

        public bool Equals(DocumentType other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(DocumentType other, bool matchAll)
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
