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
        public string Release { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
