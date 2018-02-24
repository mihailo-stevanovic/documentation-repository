using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class DocumentUpdate
    {
        [Key]
        public int Id { get; set; }        
        public DateTime Timestamp { get; set; }
        [DataType(DataType.Html)]
        public string LatestTopicsUpdated { get; set; }
        // Is visible
        public bool IsPublished { get; set; }

        // Foreign Key
        public int DocumentId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
