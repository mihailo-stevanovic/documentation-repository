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
        #region Initialization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public DocumentsInternalController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IQueryable<Document> BuildDocumentQuery()
        {
            return _context.Documents
                .Where(d => d.Updates.Any(up => up.IsPublished))
                .Include(d => d.DocumentAuthors).ThenInclude(da => da.Author)
                .Include(d => d.DocumentCatalogs).ThenInclude(dc => dc.Catalog)
                .Include(d => d.DocumentType)
                .Include(d => d.Updates)
                .Include(d => d.ProductVersion)
                .Include(d => d.ProductVersion.Product);                       
        }        

        #endregion

        #region GET
        // GET: api/v1/DocumentsInternal
        /// <summary>
        /// Returns a list of published documents.
        /// </summary>
        /// <returns>List of documents or not found error.</returns>
        /// <param name="limit">Number of documents to retreive. Used for pagination. Default is 20.</param>
        /// <param name="page">Index of the page to display starting with 1. Default is 1.</param>
        /// <response code="200">Documents successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No documents found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentDtoInternal>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocuments([FromQuery]int limit = 20, [FromQuery]int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documents = await BuildDocumentQuery()                
                .ToListAsync();

            var documentsDto = documents.Select(d => _mapper.Map<DocumentDtoInternal>(d))
                .OrderByDescending(d => d.LatestUpdate).ThenBy(d => d.Product).ThenByDescending(d => d.Version)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (!documentsDto.Any())
            {
                return NotFound();
            }

            

            return Ok(documentsDto);
        }

        // GET: api/v1/DocumentsInternal/5
        /// <summary>
        /// Returns a single published document.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Document object or description of the error.</returns> 
        /// <response code="200">Document successfully found.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document with the provided ID could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentDtoInternal), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = await BuildDocumentQuery()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentDtoInternal>(document));
        }

        // GET: api/v1/DocumentsInternal/ByDocType/5
        /// <summary>
        /// Returns a list of published documents of a specified document type.
        /// </summary>
        /// <param name="docTypeId">ID of the document type.</param>
        /// <param name="limit">Number of documents to retreive. Used for pagination. Default is 20.</param>
        /// <param name="page">Index of the page to display starting with 1. Default is 1.</param>
        /// <returns>List of document objects or description of the error.</returns>
        /// <response code="200">Documents successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No documents of the specified type could be found.</response>
        [HttpGet("ByDocType/{docTypeId}")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDtoInternal>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentsByType([FromRoute]int docTypeId, [FromQuery]int limit = 20, [FromQuery]int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documents = await BuildDocumentQuery()                
                .Where(d => d.DocumentTypeId == docTypeId)
                .ToListAsync();

            var documentsDto = documents.Select(d => _mapper.Map<DocumentDtoInternal>(d))
                .OrderByDescending(d => d.LatestUpdate).ThenBy(d => d.Product).ThenByDescending(d => d.Version)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (!documentsDto.Any())
            {
                return NotFound();
            }

            return Ok(documentsDto);
        }

        // GET: api/v1/DocumentsInternal/ByProduct/5
        /// <summary>
        /// Returns a list of published documents of a specified product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="limit">Number of documents to retreive. Used for pagination. Default is 20.</param>
        /// <param name="page">Index of the page to display starting with 1. Default is 1.</param>
        /// <returns>List of document objects or description of the error.</returns>
        /// <response code="200">Documents successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No documents could be found for the specified product.</response>
        [HttpGet("ByProduct/{productId}")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDtoInternal>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentsByProduct([FromRoute]int productId, [FromQuery]int limit = 20, [FromQuery]int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documents = await BuildDocumentQuery()                
                .Where(d => d.ProductVersion.ProductId == productId)
                .ToListAsync();

            var documentsDto = documents.Select(d => _mapper.Map<DocumentDtoInternal>(d))
                .OrderByDescending(d => d.LatestUpdate).ThenByDescending(d => d.Version)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (!documentsDto.Any())
            {
                return NotFound();
            }

            return Ok(documentsDto);
        }

        // GET: api/v1/DocumentsInternal/ByProductVersion/5
        /// <summary>
        /// Returns a list of published documents of a specified product.
        /// </summary>
        /// <param name="productVersionId">ID of the product version.</param>
        /// <param name="limit">Number of documents to retreive. Used for pagination. Default is 20.</param>
        /// <param name="page">Index of the page to display starting with 1. Default is 1.</param>
        /// <returns>List of document objects or description of the error.</returns>
        /// <response code="200">Documents successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No documents could be found for the specified product version.</response>
        [HttpGet("ByProductVersion/{productVersionId}")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDtoInternal>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentsByProductVersion([FromRoute]int productVersionId, [FromQuery]int limit = 20, [FromQuery]int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documents = await BuildDocumentQuery()
                .Where(d => d.ProductVersionId == productVersionId)                
                .ToListAsync();

            var documentsDto = documents.Select(d => _mapper.Map<DocumentDtoInternal>(d))
                .OrderByDescending(d => d.LatestUpdate)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (!documentsDto.Any())
            {
                return NotFound();
            }

            return Ok(documentsDto);
        }

        // GET: api/v1/DocumentsInternal/Search?searchTerm=test
        /// <summary>
        /// Returns a list of published documents by searching the title, short description and latest updates.
        /// </summary>
        /// <param name="searchTerm">Terms used to search for the documents.</param>
        /// <param name="limit">Number of documents to retreive. Used for pagination. Default is 20.</param>
        /// <param name="page">Index of the page to display starting with 1. Default is 1.</param>
        /// <param name="exactMatch">When set to false, each word in the search term is searched for separately. Otherwise, the search term is used as a whole. Default is true.</param>
        /// <returns>List of document objects or description of the error.</returns>
        /// <response code="200">Documents successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No documents could be found for the specified product.</response>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDtoInternal>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentsFromSearch([FromQuery]string searchTerm, [FromQuery]int limit = 20, [FromQuery]int page = 1, [FromQuery] bool exactMatch = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            List<Document> documentList;
            if (!exactMatch)
            {
                var multipleTerms = searchTerm.Split(' ');

                documentList = await BuildDocumentQuery()
                .Where(d =>  multipleTerms.Any(term => 
                        (EF.Functions.Like(d.Title, $"%{term}%") ||
                        EF.Functions.Like(d.ShortDescription, $"%{term}%") ||
                        EF.Functions.Like(d.Updates
                                            .Where(up => up.IsPublished)
                                            .OrderByDescending(up => up.Timestamp)
                                            .FirstOrDefault().LatestTopicsUpdated.ToString(), $"%{term}%"))
                )
                )                
                .ToListAsync();

            }

            else
            {
                documentList = await BuildDocumentQuery()
                .Where(d => 
                    (EF.Functions.Like(d.Title, $"%{searchTerm}%") ||
                    EF.Functions.Like(d.ShortDescription, $"%{searchTerm}%") ||
                    EF.Functions.Like(d.Updates
                                        .Where(up => up.IsPublished)
                                        .OrderByDescending(up => up.Timestamp)
                                        .FirstOrDefault().LatestTopicsUpdated.ToString(), $"%{searchTerm}%"))
                )                
                .ToListAsync();
            }

            
            if (!documentList.Any())
            {
                return NotFound();
            }

            var documentsDto = documentList
                .Select(d => _mapper.Map<DocumentDtoInternal>(d))
                .OrderByDescending(d => d.LatestUpdate)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (!documentsDto.Any())
            {
                return NotFound();
            }

            return Ok(documentsDto);
        }
        #endregion

        #region DocumentUpdates
        /// <summary>
        /// Returns all published updates of a specified document.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>List of document updates or description of error.</returns>
        /// <response code="200">Document updates successufully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No updates could be found for the specified document.</response>
        [HttpGet("{id}/Updates")]
        [ProducesResponseType(typeof(IEnumerable<DocumentUpdateDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentUpdates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updates = await _context.DocumentUpdates
                .Where(up => up.DocumentId == id && up.IsPublished)
                .OrderByDescending(up => up.Timestamp).ThenByDescending(up => up.Id)
                .ToListAsync();

            if (!updates.Any())
            {
                return NotFound();
            }

            return Ok(updates.Select(up => _mapper.Map<DocumentUpdateDto>(up)));
        }
        #endregion

    }
}