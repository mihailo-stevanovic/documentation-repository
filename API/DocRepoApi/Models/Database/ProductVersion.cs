using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a release of a product.
    /// </summary>
    public class ProductVersion : IDocRepoEntity<ProductVersion>
    {
        #region Properties
        /// <summary>
        /// ID of the product version.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Release version of the product.
        /// </summary>
        [Required]
        [StringLength(10, ErrorMessage = "The Release cannot be longer than 10 characters.")]
        public string Release { get; set; }
        /// <summary>
        /// End of support date.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime EndOfSupport { get; set; }     
        // Foreign Key
        /// <summary>
        /// Id of the related product.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Link to the related product.
        /// </summary>
        public Product Product { get; set; }
        #endregion

        #region Methods
        public int CompareTo(ProductVersion other)
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

        public bool Equals(ProductVersion other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(ProductVersion other, bool matchAll)
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
