using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a DTO class for the Author class.
    /// </summary>
    public class AuthorDto : IDocRepoEntity<AuthorDto>
    {
        /// <summary>
        /// ID of the author.
        /// </summary>
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
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(7, ErrorMessage = "The Alias cannot be longer than 7 characters.")]        
        public string Alias { get; set; }
        /// <summary>
        /// Is former employee.
        /// </summary>        
        public bool IsFormerAuthor { get; set; }
        /// <summary>
        /// Author-IT name of the author.
        /// </summary>        
        [StringLength(10, ErrorMessage = "AIT Name cannot be longer than 10 characters.")]
        [MinLength(4)]
        public string AitName { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as AuthorDto;
            if (other == null)
            {
                return false;
            }
            return this.Equals(other);
        }

        public int CompareTo(AuthorDto other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.Alias.CompareTo(other.Alias);
            }
        }

        public bool Equals(AuthorDto other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(AuthorDto other, bool matchAll)
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
