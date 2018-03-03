using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a single document update. Used to track changes made to the document.
    /// </summary>
    public class DocumentUpdate
    {
        /// <summary>
        /// ID of the document update.
        /// </summary>
        [Key]
        public int Id { get; set; }        
        /// <summary>
        /// Date and time of the update.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of the latest updated topics.
        /// </summary>
        [DataType(DataType.Html)]
        public string LatestTopicsUpdated { get; set; }
        /// <summary>
        /// Used to restrict the visibility of the update during publishing.
        /// </summary>
        public bool IsPublished { get; set; }

        // Foreign Key
        /// <summary>
        /// Link to the related document.
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// Used to prevent DB concurrency issues during updates.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
