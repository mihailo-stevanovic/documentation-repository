using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a DTO class of the ProductVersion class.
    /// </summary>
    public class ProductVersionDto : IDocRepoEntity<ProductVersionDto>
    {
        #region Properties
        /// <summary>
        /// ID of the product version.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Marketing name of the product.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Release version of the product.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "The Release cannot be longer than 10 characters.")]
        public string Release { get; set; }
        /// <summary>
        /// End of support date.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime EndOfSupport { get; set; }
        /// <summary>
        /// True if the version is still supported.
        /// </summary>
        public bool IsSupported
        {
            get { return EndOfSupport > DateTime.Today; }
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            var other = obj as ProductVersionDto;
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

        public int CompareTo(ProductVersionDto other)
        {
            if (other == null)
            {
                return 1;
            }
            else 
            {
                int comparedFullName = this.Product.CompareTo(other.Product);

                if (comparedFullName == 0)
                {
                    return this.Release.CompareTo(other.Release) * -1;
                }
                else
                {
                    return comparedFullName;
                }
            }
            
        }

        public bool Equals(ProductVersionDto other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(ProductVersionDto other, bool matchAll)
        {
            if (!matchAll)
            {
                return this.Equals(other);
            }
            if (!this.Equals(other))
            {
                return false;
            }
            if (!this.Product.Equals(other.Product))
            {
                return false;
            }
            if (!this.Release.Equals(other.Release))
            {
                return false;
            }
            if (!this.EndOfSupport.Equals(other.EndOfSupport))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
