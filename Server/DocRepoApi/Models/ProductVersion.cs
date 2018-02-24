using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class ProductVersion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The Release cannot be longer than 10 characters.")]
        public string Release { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
