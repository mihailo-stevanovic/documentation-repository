using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class Author : IEquatable<Author>, IComparable<Author>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The First name cannot be longer than 50 characters.")] 
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(7, ErrorMessage = "The Alias cannot be longer than 7 characters.")]
        public string Alias { get; set; }
        [Display(Name = "Former Author")]
        public bool IsFormerAuthor { get; set; }
        [StringLength(10, ErrorMessage = "AIT Name cannot be longer than 10 characters.")]
        public string AitName { get; set; }

        public ICollection<DocumentAuthor> DocumentsAuthored { get; set; }

        public int CompareTo(Author other)
        {

            if(other == null)
            {
                return 1;
            }
            else
            {
                return this.Alias.CompareTo(other.Alias);
            }

        }

        public bool Equals(Author other)
        {            
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(Author other, bool matchAll)
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
            if (!this.IsFormerAuthor.Equals(other.IsFormerAuthor))
            {
                return false;
            }
            if (!this.FirstName.Equals(other.FirstName))
            {
                return false;
            }
            if (!this.LastName.Equals(other.LastName))
            {
                return false;
            }
            if (!this.Alias.Equals(other.Alias))
            {
                return false;
            }

            return true;
        }
    }
}
