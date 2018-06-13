using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent a DTO of the Document class used for displaying it internally.
    /// </summary>
    public class DocumentDtoInternal : IDocRepoEntity<DocumentDtoInternal>
    {
        #region Properties
        /// <summary>
        /// ID of the document.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Title of the document.
        /// </summary>        
        public string Title { get; set; }
        /// <summary>
        /// Product related to the document.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Product version related to the document.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Relative path to the HTML output of the document.
        /// </summary>
        public string HtmlLink { get; set; }
        /// <summary>
        /// Relative path to the PDF output of the document.
        /// </summary>
        public string PdfLink { get; set; }
        /// <summary>
        /// Relative path to the Word output of the document.
        /// </summary>
        public string WordLink { get; set; }
        /// <summary>
        /// Relative path to a misc output of the document.
        /// </summary>
        public string OtherLink { get; set; }
        /// <summary>
        /// Is document available for clients.
        /// </summary>        
        public bool IsFitForClients { get; set; }                                  
        /// <summary>
        /// Short description of the document.
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Document type.
        /// </summary>
        public string DocumentType { get; set; }
        /// <summary>
        /// Date of the latest update.
        /// </summary>
        public DateTime LatestUpdate { get; set; }
        /// <summary>
        /// List of the latest updated topics.
        /// </summary>
        [DataType(DataType.Html)]
        public string LatestTopicsUpdated { get; set; }        

        #region Internal Portal only
        /// <summary>
        /// Link to the related authors.
        /// </summary>
        public ICollection<AuthorDto> Authors { get; set; }
        /// <summary>
        /// Link to the related client catalogs.
        /// </summary>
        public ICollection<ClientCatalogDto> ClientCatalogs { get; set; }
        #endregion
        #endregion

        #region Methods
        public int CompareTo(DocumentDtoInternal other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int comparedUpdate = this.LatestUpdate.CompareTo(other.LatestUpdate);

                if (comparedUpdate == 0)
                {
                    int comparedProduct = this.Product.CompareTo(other.Product);

                    if (comparedProduct == 0)
                    {
                        int comparedVersion = this.Version.CompareTo(other.Version);
                        if (comparedVersion == 0)
                        {
                            int comparedDocType = this.DocumentType.CompareTo(other.DocumentType);
                            if (comparedDocType == 0)
                            {
                                return this.Title.CompareTo(other.Title);                                
                            }
                            else
                            {
                                return comparedDocType;
                            }
                        }                        

                        else
                        {
                            return comparedVersion * -1;
                        }
                    }

                    else
                    {
                        return comparedProduct;
                    }
                    
                    
                }
                else
                {
                    return comparedUpdate * -1;
                }
            }
        }

        public bool Equals(DocumentDtoInternal other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public bool Equals(DocumentDtoInternal other, bool matchAll)
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

            if (!this.Title.Equals(other.Title))
            {
                return false;
            }

            if (!this.ShortDescription.Equals(other.ShortDescription))
            {
                return false;
            }

            if (!this.LatestTopicsUpdated.Equals(other.LatestTopicsUpdated))
            {
                return false;
            }

            if (!this.LatestUpdate.Equals(other.LatestUpdate))
            {
                return false;
            }

            if (!this.Version.Equals(other.Version))
            {
                return false;
            }

            if (!this.Product.Equals(other.Product))
            {
                return false;
            }

            if (!this.DocumentType.Equals(other.DocumentType))
            {
                return false;
            }

            if (!this.IsFitForClients.Equals(other.IsFitForClients))
            {
                return false;
            }

            if (this.HtmlLink != null && !this.HtmlLink.Equals(other.HtmlLink))
            {
                return false;
            }
            if (this.PdfLink != null && !this.PdfLink.Equals(other.PdfLink))
            {
                return false;
            }
            if (this.WordLink != null && !this.WordLink.Equals(other.WordLink))
            {
                return false;
            }
            if (this.OtherLink != null && !this.OtherLink.Equals(other.OtherLink))
            {
                return false;
            }

            
            // Check Authors
            if (this.Authors != null && other.Authors != null)
            {
                if (this.Authors.Count != other.Authors.Count)
                {
                    return false;
                }

                List<AuthorDto> thisAuthorList = (List<AuthorDto>)this.Authors;
                thisAuthorList.Sort();
                List<AuthorDto> otherAuthorList = (List<AuthorDto>)other.Authors;
                otherAuthorList.Sort();

                for (int i = 0; i < thisAuthorList.Count; i++)
                {
                    if (!thisAuthorList[i].Equals(otherAuthorList[i]))
                    {
                        return false;
                    }
                }
            }

            // Check Catalogs
            if (this.ClientCatalogs != null && other.ClientCatalogs != null)
            {
                if (this.ClientCatalogs.Count != other.ClientCatalogs.Count)
                {
                    return false;
                }

                List<ClientCatalogDto> thisClientCatalogList = (List<ClientCatalogDto>)this.ClientCatalogs;
                thisClientCatalogList.Sort();
                List<ClientCatalogDto> otherClientCatalogList = (List<ClientCatalogDto>)other.ClientCatalogs;
                otherClientCatalogList.Sort();

                for (int i = 0; i < thisClientCatalogList.Count; i++)
                {
                    if (!thisClientCatalogList[i].Equals(otherClientCatalogList[i]))
                    {
                        return false;
                    }
                }
            }


            return true;
        }
        #endregion        
    }
}
