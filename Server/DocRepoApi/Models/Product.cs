using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents a single product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of the product.
        /// </summary>
        [Key]
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
    }
}
