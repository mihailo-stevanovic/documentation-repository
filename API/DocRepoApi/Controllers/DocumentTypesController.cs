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
        /// <returns>List of document types or description of error.</returns>
        /// <response code="200">Document types successfully retrieved.</response>
        /// <response code="404">No document types were found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetDocumentTypes()
        {
            var docTypes = _context.DocumentTypes.Select(d => _mapper.Map<DocumentTypeDto>(d));

            if (!docTypes.Any())
            {
                return NotFound();
            }

            return Ok(docTypes);
        }

        // GET: api/v1/DocumentTypes/5
        /// <summary>
        /// Returns a single document type.
        /// </summary>
        /// <param name="id">ID of the document type.</param>
        /// <returns>Document type object or description of error.</returns>
        /// <response code="200">Document type successfully retrieved.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No document types with the matching ID were found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentTypeDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

        // GET: api/v1/DocumentTypes/Categories
        /// <summary>
        /// Returns a list of all document categories as key-value pairs.
        /// </summary>
        /// <remarks>
        /// Provided for informational purposes only.
        /// Key: Category ID
        /// Value: Category Name
        /// </remarks>
        /// <returns>List of document category key-value pairs.</returns>
        /// <reponse code="200">Successfully returned.</reponse>
        [HttpGet("Categories")]
        [Produces(typeof(List<KeyValuePair<int, string>>))]
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
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Update is successuful.</response>
        /// <response code="400">Request is incorrect or ID from the path does not match the ID of the document type.</response>
        /// <response code="404">Document type does not exist.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutDocumentType([FromRoute] int id, [FromBody] DocumentTypeDto documentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentType.Id)
            {
                ModelState.AddModelError("Invalid Document Type ID", "The Document Type ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            DocumentType documentTypeReversed = _mapper.Map<DocumentType>(documentType);

            _context.Entry(documentTypeReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!DocumentTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError("Error", xcp.ToString());
                    return BadRequest(ModelState);
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
        /// <returns>Document Type object or description of error.</returns>
        /// <response code="201">Returns the newly created document type.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DocumentTypeDto), 201)]
        [ProducesResponseType(400)]
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
        /// <returns>Deleted document type object or description of error.</returns>
        /// <response code="200">Document type sucessufully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document type with the provided ID could not be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DocumentTypeDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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