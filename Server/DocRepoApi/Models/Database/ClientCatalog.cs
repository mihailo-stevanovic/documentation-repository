using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a client catalog. Used for restricting document visibility to client users.
    /// </summary>
    public class ClientCatalog : IDocRepoEntity<ClientCatalog>
    {
        #region Properties
        /// <summary>
        /// ID of the client catalog.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Name of the client catalog.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The catalog name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        /// <summary>
        /// ID used for integration with other internal systems.
        /// </summary>
        public string InternalId { get; set; }
        #endregion

        #region Methods
        public int CompareTo(ClientCatalog other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.Name.CompareTo(other.Name);
            }
        }

        public bool Equals(ClientCatalog other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(ClientCatalog other, bool matchAll)
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
            if (!this.Name.Equals(other.Name))
            {
                return false;
            }
            if (!this.InternalId.Equals(other.InternalId))
            {
                return false;
            }            

            return true;
        }
        #endregion
    }
}
