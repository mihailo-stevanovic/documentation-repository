using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a single product.
    /// </summary>
    public class Product : IDocRepoEntity<Product>
    {
        #region Properties
        /// <summary>
        /// Id of the product.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Marketing name of the product.
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Short name of the product (e.g. FPM, FIA, etc.)
        /// </summary>
        [Required]
        [StringLength(7, ErrorMessage = "The Short name cannot be longer than 7 characters.")]
        public string ShortName { get; set; }
        /// <summary>
        /// Used for old product names.
        /// </summary>
        public string Alias { get; set; }
        #endregion

        #region Methods
        public int CompareTo(Product other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.FullName.CompareTo(other.FullName);
            }
        }

        public bool Equals(Product other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(Product other, bool matchAll)
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
            if (!this.Alias.Equals(other.Alias))
            {
                return false;
            }

            return true;

        }
        #endregion
    }
}
