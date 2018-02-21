using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public class DocumentAuthor
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int AuthorId { get; set; }

        public Document Document { get; set; }
        public Author Author { get; set; }
    }
}
