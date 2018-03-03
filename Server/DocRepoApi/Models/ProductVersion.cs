using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a release of a product.
    /// </summary>
    public class ProductVersion
    {
        /// <summary>
        /// ID of the product version.
        /// </summary>
        [Key]
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
        /// <summary>
        /// True if the version is still supported.
        /// </summary>
        public bool IsSupported
        {
            get { return EndOfSupport > DateTime.Today; }            
        }
        // Foreign Key
        /// <summary>
        /// Id of the related product.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Link to the related product.
        /// </summary>
        public Product Product { get; set; }

    }
}
