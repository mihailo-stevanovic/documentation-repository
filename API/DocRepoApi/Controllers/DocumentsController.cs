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
    [Route("api/v1/Documents")]
    public class DocumentsController : Controller
    {
        #region Initialization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public DocumentsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Create a Document from a DocumentDto.
        /// </summary>
        /// <param name="document">Document DTO to be reversed.</param>
        /// <returns>Reversed document object.</returns>
        private Document ReverseDocumentFromDocumentDto(DocumentDto document)
        {
            Document reversedDocument = _mapper.Map<Document>(document);

            // Handle Authors
            reversedDocument.DocumentAuthors = new List<DocumentAuthor>();
            foreach (int authorId in document.DocumentAuthorIds)
            {
                reversedDocument.DocumentAuthors.Add(
                    new DocumentAuthor
                    {
                        AuthorId = authorId
                    }
                    );
            }

            // Handle Catalogs
            if (document.DocumentCatalogIds != null && document.DocumentCatalogIds.Count > 0)
            {
                reversedDocument.DocumentCatalogs = new List<DocumentCatalog>();
                foreach (int catId in document.DocumentCatalogIds)
                {
                    reversedDocument.DocumentCatalogs.Add(
                        new DocumentCatalog
                        {
                            CatalogId = catId
                        }
                        );
                }
            }

            // Handle Update

            reversedDocument.Updates = new List<DocumentUpdate>
            {
                new DocumentUpdate
                {
                    IsPublished = document.IsPublished,
                    LatestTopicsUpdated = document.LatestTopicsUpdated,
                    Timestamp = DateTime.UtcNow
                }
            };

            return reversedDocument;
        }
        /// <summary>
        /// Checks if document with the provided ID exists in the current context.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>True if the document exists, false if it does not.</returns>
        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
        /// <summary>
        /// Checks if document update with the provided ID exists in the current context.
        /// </summary>
        /// <param name="id">ID of the document update.</param>
        /// <returns>True if the update exists, false if it does not.</returns>
        private bool DocumentUpdateExists(int id)
        {
            return _context.DocumentUpdates.Any(e => e.Id == id);
        }
        #endregion

        #region GET
        // GET: api/v1/Documents
        /// <summary>
        /// Returns all documents.
        /// </summary>
        /// <returns>List of documents or error if not documents are found.</returns>
        /// <response code="200">Documents successfully returned.</response>
        /// <response code="404">No documents found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetDocuments()
        {
            var documents = _context.Documents
                .Include(d => d.DocumentAuthors)
                .Include(d => d.DocumentCatalogs)
                .Select(d => _mapper.Map<DocumentDto>(d));

            if (!documents.Any())
            {
                return NotFound();
            }                     

            return Ok(documents);
        }

        // GET: api/v1/Documents/5
        /// <summary>
        /// Returns a single document.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Single document or description of error.</returns>
        /// <response code="200">Document successfully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document with a matching ID could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = await _context.Documents
                .Include(d => d.DocumentAuthors)
                .Include(d => d.DocumentCatalogs)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentDto>(document));
        }

        // GET: api/v1/Documents/5/Updates
        /// <summary>
        /// Returns all updates related to a document.
        /// </summary>
        /// <param name="documentId">ID of the document.</param>
        /// <returns>List of document updates or description of the error.</returns>
        /// <response code="200">Document updates successfully returned returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Updates for a document with the specified ID could not be found.</response>
        [HttpGet("{documentId}/Updates")]
        [ProducesResponseType(typeof(IEnumerable<DocumentUpdateDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentUpdates([FromRoute] int documentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!DocumentExists(documentId))
            {
                ModelState.AddModelError("Invalid Document ID", "The Document ID supplied in the does not match an existing document.");
                return BadRequest(ModelState);
            }

            var documentUpdates = await _context.DocumentUpdates
                .Where(up => up.DocumentId == documentId)
                .OrderByDescending(up => up.Timestamp).ThenByDescending(up => up.Id)
                .ToListAsync();

            if (!documentUpdates.Any())
            {
                return NotFound();
            }

            return Ok(documentUpdates.Select(up => _mapper.Map<DocumentUpdateDto>(up)));
        }

        // GET: api/v1/Documents/5/Updates/5
        /// <summary>
        /// Returns a single update related to a document.
        /// </summary>
        /// <param name="documentId">ID of the document.</param>
        /// <param name="updateId">ID of the update.</param>
        /// <returns>Single document update or description of error.</returns>
        /// <response code="200">Document update successfully returned returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Update with a matching ID and DocumentID could not be found.</response>
        [HttpGet("{documentId}/Updates/{updateId}")]
        [ProducesResponseType(typeof(DocumentUpdateDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDocumentUpdate([FromRoute] int documentId, [FromRoute] int updateId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var update = await _context.DocumentUpdates.SingleOrDefaultAsync(up => up.DocumentId == documentId && up.Id == updateId);
                

            if (update == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentUpdateDto>(update));
        }

        #endregion

        #region PUT
        // PUT: api/v1/Documents/5
        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <param name="document">Modified document object.</param>
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Document successufully updated.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document with a matching ID could not be found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutDocument([FromRoute] int id, [FromBody] DocumentDto document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.Id)
            {
                ModelState.AddModelError("Invalid ID", "The ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            Document originalDocument = _context.Documents
                .Include(d => d.DocumentAuthors)
                .Include(d => d.DocumentCatalogs)
                .Include(d => d.Updates)
                .SingleOrDefault(d => d.Id == id);

            if (document.RowVersion != originalDocument.RowVersion)
            {
                ModelState.AddModelError("Concurency Issue", "The record has been modified by another user. Please try again.");
                return BadRequest(ModelState);
            }

            originalDocument.Title = document.Title;
            originalDocument.ShortDescription = document.ShortDescription;
            originalDocument.IsFitForClients = document.IsFitForClients;

            originalDocument.HtmlLink = document.HtmlLink;
            originalDocument.PdfLink = document.PdfLink;
            originalDocument.WordLink = document.WordLink;

            originalDocument.AitId = document.AitId;
            originalDocument.ProductVersionId = document.ProductVersionId;
            originalDocument.DocumentTypeId = document.DocumentTypeId;


            // Handle Document Update
            DocumentUpdate update = new DocumentUpdate
            {
                DocumentId = id,
                IsPublished = document.IsPublished,
                LatestTopicsUpdated = document.LatestTopicsUpdated,
                Timestamp = DateTime.UtcNow
            };

            if (originalDocument.Updates == null)
            {
                originalDocument.Updates = new List<DocumentUpdate> { update };
            }
            else
            {
                originalDocument.Updates.Add(update);
            }


            // Handle Document Authors                                 
            // Add new authors
            foreach (int authorId in document.DocumentAuthorIds)
            {
                var daList = originalDocument.DocumentAuthors.Where(da => da.AuthorId == authorId);

                if (daList != null && daList.Count() == 0)
                {
                    originalDocument.DocumentAuthors.Add(new DocumentAuthor
                    {
                        AuthorId = authorId,
                        DocumentId = id
                    });
                }
            }

            // Remove authors                      
            var originalAuthorIds = originalDocument.DocumentAuthors.Select(da => da.AuthorId);

            foreach (int origDAid in originalAuthorIds.ToList())
            {
                if (document.DocumentAuthorIds.Contains(origDAid))
                {
                    continue;
                }

                originalDocument.DocumentAuthors.Remove(originalDocument.DocumentAuthors.Single(da => da.AuthorId == origDAid));
            }

            // Handle Document Catalogs
            // Add new catalogs
            foreach (int catId in document.DocumentCatalogIds)
            {
                var dcList = originalDocument.DocumentCatalogs.Where(dc => dc.CatalogId == catId);

                if (dcList != null && dcList.Count() == 0)
                {
                    originalDocument.DocumentCatalogs.Add(new DocumentCatalog
                    {
                        CatalogId = catId,
                        DocumentId = id
                    });
                }
            }

            // Remove catalogs                      
            var originalCatIds = originalDocument.DocumentCatalogs.Select(dc => dc.CatalogId);

            foreach (int origCatId in originalCatIds.ToList())
            {
                if (document.DocumentCatalogIds.Contains(origCatId))
                {
                    continue;
                }

                originalDocument.DocumentCatalogs.Remove(originalDocument.DocumentCatalogs.Single(dc => dc.CatalogId == origCatId));
            }

            _context.Entry(originalDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!DocumentExists(id))
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

        // PUT: api/v1/Documents/5/Updates/5
        /// <summary>
        /// Updates a document update.
        /// </summary>
        /// <remarks>
        /// Please note that usage of this action is not recommended. It is provided to support specific cases.
        /// </remarks>
        /// <param name="documentId">ID of the document.</param>
        /// <param name="updateId">ID of the document update.</param>
        /// <param name="documentUpdate">Modified document object.</param>
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Document update successufully modified.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document update with a matching ID could not be found.</response>
        [HttpPut("{documentId}/Updates/{updateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutDocumentUpdate([FromRoute] int documentId, [FromRoute] int updateId, [FromBody] DocumentUpdateDto documentUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updateId != documentUpdate.Id)
            {
                ModelState.AddModelError("Invalid Update ID", "The Update ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            if (documentId != documentUpdate.DocumentId)
            {
                ModelState.AddModelError("Invalid Document ID", "The Document ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            if (!DocumentExists(documentId))
            {
                ModelState.AddModelError("Invalid Document ID", "The Document ID supplied in the does not match an existing document.");
                return BadRequest(ModelState);
            }

            var documentUpdateReversed = _mapper.Map<DocumentUpdate>(documentUpdate);            

            _context.Entry(documentUpdateReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!DocumentUpdateExists(updateId))
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

        // POST: api/v1/Documents
        /// <summary>
        /// Creates a document.
        /// </summary>
        /// <param name="document">New document object.</param>
        /// <returns>Newly created Document or error description.</returns>
        /// <response code="201">Document properly created.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DocumentDto), 201)]
        [ProducesResponseType(400)]        
        public async Task<IActionResult> PostDocument([FromBody] DocumentDto document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Document reversedDocument = ReverseDocumentFromDocumentDto(document);

            _context.Documents.Add(reversedDocument);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception xpc)
            {
                ModelState.AddModelError("Error", xpc.ToString());
                return BadRequest(ModelState);
                
            }            

            return CreatedAtAction("GetDocument", new { id = reversedDocument.Id }, _mapper.Map<DocumentDto>(reversedDocument));
        }

        // POST api/v1/Documents/Batch
        /// <summary>
        /// Creates multiple documents.
        /// </summary>
        /// <param name="documentList">List of new document objects.</param>
        /// <returns>Number of created Documents or error description.</returns>
        /// <response code="201">Documents created sucessfully.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("Batch")]
        [ProducesResponseType(typeof(IEnumerable<DocumentDto>), 201)]
        [ProducesResponseType(400)]        
        public async Task<IActionResult> PostDocuments([FromBody] List<DocumentDto> documentList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int count = 0;

            foreach (var document in documentList)
            {
                Document reversedDocument = ReverseDocumentFromDocumentDto(document);

                _context.Documents.Add(reversedDocument);
                
                count++;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception xpc)
            {
                ModelState.AddModelError("Error", xpc.ToString());
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetDocuments", "Sucessfully created " + count + " documents.");
        }

        // POST: api/v1/Documents/5/Updates
        /// <summary>
        /// Creates a document update.
        /// </summary>
        /// <param name="documentId">ID of the document.</param>
        /// <param name="documentUpdate">New document update object.</param>
        /// <returns>Newly created document update or error description.</returns>
        /// <response code="201">Document update properly created.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("{documentId}/Updates")]
        [ProducesResponseType(typeof(DocumentUpdateDto), 201)]
        [ProducesResponseType(400)]        
        public async Task<IActionResult> PostDocumentUpdate([FromRoute] int documentId, [FromBody] DocumentUpdateDto documentUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (documentId != documentUpdate.DocumentId)
            {
                ModelState.AddModelError("Invalid Document ID", "The Document ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            if (!DocumentExists(documentId))
            {
                ModelState.AddModelError("Invalid Document ID", "The Document ID supplied in the does not match an existing document.");
                return BadRequest(ModelState);
            }            

            if (documentUpdate.Timestamp.Year == 1)
            {
                documentUpdate.Timestamp = DateTime.UtcNow;
            }

            DocumentUpdate reversedUpdate = _mapper.Map<DocumentUpdate>(documentUpdate);
                       

            _context.DocumentUpdates.Add(reversedUpdate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception xpc)
            {
                ModelState.AddModelError("Error", xpc.ToString());
                return BadRequest(ModelState);

            }

            return CreatedAtAction("GetDocumentUpdate", new { documentId = reversedUpdate.DocumentId, updateId = reversedUpdate.Id }, _mapper.Map<DocumentUpdateDto>(reversedUpdate));
        }

        #endregion

        #region DELETE
        // DELETE: api/Documents/5
        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns></returns>
        /// <response code="200">Document sucessfully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document with the provided ID could not be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DocumentDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DocumentDto>(document));
        }

        // DELETE: api/Documents/5/Updates/5
        /// <summary>
        /// Deletes a document update.
        /// </summary>
        /// <param name="documentId">ID of the document.</param>
        /// <param name="updateId">ID of the document update.</param>
        /// <returns></returns>
        /// <response code="200">Document update sucessfully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Document update with the provided ID and DocumentID could not be found.</response>
        [HttpDelete("{documentId}/Updates/{updateId}")]
        [ProducesResponseType(typeof(DocumentUpdateDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDocumentUpdate([FromRoute] int documentId, [FromRoute] int updateId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentUpdate = await _context.DocumentUpdates.SingleOrDefaultAsync(m => m.Id == updateId && m.DocumentId == documentId);

            if (documentUpdate == null)
            {
                return NotFound();
            }

            _context.DocumentUpdates.Remove(documentUpdate);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DocumentUpdateDto>(documentUpdate));
        }

        #endregion
        
    }
}