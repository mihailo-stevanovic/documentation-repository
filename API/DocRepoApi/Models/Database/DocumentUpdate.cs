using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a single document update. Used to track changes made to the document.
    /// </summary>
    public class DocumentUpdate : IDocRepoEntity<DocumentUpdate>
    {
        #region Properties
        /// <summary>
        /// ID of the document update.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        #endregion

        #region Methods
        public int CompareTo(DocumentUpdate other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.Timestamp.CompareTo(other.Timestamp) * -1;
                //int comparedTimestamp = this.Timestamp.CompareTo(other.Timestamp) * -1;
                //if (comparedTimestamp == 0)
                //{
                //    return this.Id.CompareTo(other.Id) * -1;
                //}
                //else
                //{
                //    return comparedTimestamp;
                //}
            }

        }

        public bool Equals(DocumentUpdate other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(DocumentUpdate other, bool matchAll)
        {
            if (!matchAll)
            {
                return this.Equals(other);
            }
            if (other == null)
            {
                return false;
            }
            if (!this.Id.Equals(other.Id))
            {
                return false;
            }
            if (!this.Timestamp.Equals(other.Timestamp))
            {
                return false;
            }
            if (!this.IsPublished.Equals(other.IsPublished))
            {
                return false;
            }
            if (!this.DocumentId.Equals(other.DocumentId))
            {
                return false;
            }
            if (!this.LatestTopicsUpdated.Equals(other.LatestTopicsUpdated))
            {
                return false;
            }
            return true;

        }

        #endregion
    }
}
