using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a person who authored a document. It can be a tech writer or another person.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// ID of the author.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// First name of the author.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The First name cannot be longer than 50 characters.")] 
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the author.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        /// <summary>
        /// Email address of the author.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// Active directory alias of the author.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(7, ErrorMessage = "The Alias cannot be longer than 7 characters.")]
        public string Alias { get; set; }
        /// <summary>
        /// Is former employee.
        /// </summary>
        [Display(Name = "Former Author")]
        public bool IsFormerAuthor { get; set; }
        /// <summary>
        /// Author-IT name of the author.
        /// </summary>
        [StringLength(10, ErrorMessage = "AIT Name cannot be longer than 10 characters.")]
        public string AitName { get; set; }
        /// <summary>
        /// Documents linked to the author.
        /// </summary>
        public ICollection<DocumentAuthor> DocumentsAuthored { get; set; }
        
    }
}
