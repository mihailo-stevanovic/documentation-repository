using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "The Short name cannot be longer than 5 characters.")]
        public string ShortName { get; set; }        
        public DocumentCategory DocumentCategory { get; set; }
    }
}
