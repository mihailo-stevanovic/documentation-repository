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
    [Route("api/v1/DocumentTypes")]
    public class DocumentTypesController : Controller
    {
        #region Initizalization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public DocumentTypesController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GET
        // GET: api/v1/DocumentTypes
        /// <summary>
        /// Returns all document types.
        /// </summary>
        /// <returns>List of document types.</returns>
        [HttpGet]        
        public IEnumerable<DocumentTypeDto> GetDocumentTypes()
        {
            return _context.DocumentTypes.Select(d => _mapper.Map<DocumentTypeDto>(d));
        }

        // GET: api/v1/DocumentTypes/5
        /// <summary>
        /// Returns a single document type.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentType = await _context.DocumentTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (documentType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentTypeDto>(documentType));
        }

        // GET: api/v1/DocumentTypes/categories
        /// <summary>
        /// Returns a list of all document categories as key-value pairs.
        /// Provided for informational purposes only.
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        public List<KeyValuePair<int, string>> GetDocumentCategories()
        {
            var list = new List<KeyValuePair<int, string>>();

            foreach (DocumentCategory cat in Enum.GetValues(typeof(DocumentCategory)))
            {
                list.Add(new KeyValuePair<int, string>((int)cat, cat.ToString()));
            }

            return list;
        }
        #endregion

        #region PUT
        // PUT: api/v1/DocumentTypes/5
        /// <summary>
        /// Updates an existing Document Type.
        /// </summary>
        /// <param name="id">ID of the Document Type.</param>
        /// <param name="documentType">Document Type object.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentType([FromRoute] int id, [FromBody] DocumentTypeDto documentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentType.Id)
            {
                return BadRequest();
            }

            DocumentType documentTypeReversed = _mapper.Map<DocumentType>(documentType);

            _context.Entry(documentTypeReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region POST
        // POST: api/v1/DocumentTypes
        /// <summary>
        /// Creates a Document Type.
        /// </summary>
        /// <param name="documentType">Document Type object.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostDocumentType([FromBody] DocumentTypeDto documentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentType documentTypeReversed = _mapper.Map<DocumentType>(documentType);

            _context.DocumentTypes.Add(documentTypeReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentType", new { id = documentTypeReversed.Id }, _mapper.Map<DocumentTypeDto>(documentTypeReversed));
        }
        #endregion

        #region DELETE
        // DELETE: api/v1/DocumentTypes/5
        /// <summary>
        /// Deletes a Document Type.
        /// </summary>
        /// <param name="id">ID of the document type object.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentType = await _context.DocumentTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DocumentTypeDto>(documentType));
        }
        #endregion

        private bool DocumentTypeExists(int id)
        {
            return _context.DocumentTypes.Any(e => e.Id == id);
        }
    }
}