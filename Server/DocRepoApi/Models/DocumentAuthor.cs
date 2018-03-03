﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// Represent the link between a document and an author.
    /// </summary>
    public class DocumentAuthor
    {
        /// <summary>
        /// ID of the document-author link.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ID of the related document.
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// ID of the related author.
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Link to the related document.
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// Link to the related author.
        /// </summary>
        public Author Author { get; set; }
    }
}
