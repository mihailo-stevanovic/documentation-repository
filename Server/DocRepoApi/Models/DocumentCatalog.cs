using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represents the relationship between documents and client catalogs.
    /// </summary>
    public class DocumentCatalog
    {
        #region Properties
        /// <summary>
        /// ID of the relationship.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// ID of the related document.
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// ID of the related client catalog.
        /// </summary>
        public int CatalogId { get; set; }
        /// <summary>
        /// Link to the related document.
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// Link to the related client catalog.
        /// </summary>
        public ClientCatalog Catalog { get; set; }
        #endregion
    }
}
