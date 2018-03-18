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
    public class DocumentType
    {
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
    }
}
