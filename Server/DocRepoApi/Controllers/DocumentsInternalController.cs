using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocRepoApi.Data;
using DocRepoApi.Models;
using AutoMapper;

namespace DocRepoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/DocumentsInternal")]
    public class DocumentsInternalController : Controller
    {
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public DocumentsInternalController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DocumentsInternal
        /// <summary>
        /// Returns a list of published documents.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DocumentDtoInternal> GetDocuments(int number = 20)
        {
            var documents =  _context.Documents
                .Include(d => d.DocumentAuthors).ThenInclude(da => da.Author)
                .Include(d => d.DocumentCatalogs).ThenInclude(dc => dc.Catalog)
                .Include(d => d.DocumentType)
                .Include(d => d.Updates)
                .Include(d => d.ProductVersion)
                .Include(d => d.ProductVersion.Product)
                .Select(d => _mapper.Map<DocumentDtoInternal>(d));

            return documents.OrderByDescending(d => d.LatestUpdate).Take(number);
        }

        // GET: api/DocumentsInternal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = await _context.Documents
                .Include(d => d.DocumentAuthors).ThenInclude(da => da.Author)
                .Include(d => d.DocumentCatalogs).ThenInclude(dc => dc.Catalog)
                .Include(d => d.DocumentType)
                .Include(d => d.Updates)
                .Include(d => d.ProductVersion)
                .Include(d => d.ProductVersion.Product)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentDtoInternal>(document));
        }
              

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}