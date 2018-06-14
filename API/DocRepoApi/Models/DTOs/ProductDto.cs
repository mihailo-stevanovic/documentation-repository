using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a DTO class of the Product class.
    /// </summary>
    public class ProductDto : IDocRepoEntity<ProductDto>
    {
        #region Properties
        /// <summary>
        /// Id of the product.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Marketing name of the product.
        /// </summary>        
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }
        /// <summary>
        /// Short name of the product
        /// </summary>        
        [Required(AllowEmptyStrings = false)]
        [StringLength(7, ErrorMessage = "The Short name cannot be longer than 7 characters.")]
        public string ShortName { get; set; }
        /// <summary>
        /// Used for old product names.
        /// </summary>        
        [MinLength(4)]
        public string Alias { get; set; }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            var other = obj as ProductDto;
            if (other == null)
            {
                return false;
            }
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(ProductDto other)
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

        public bool Equals(ProductDto other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(ProductDto other, bool matchAll)
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
