using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocRepoApi.Data;
using DocRepoApi.Models;

namespace DocRepoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Authors")]
    public class AuthorsController : Controller
    {
        private readonly DocRepoContext _context;

        public AuthorsController(DocRepoContext context)
        {
            _context = context;
        }

        // GET: api/v1/Authors
        /// <summary>
        /// Returns all authors.
        /// </summary>
        /// <param name="includeDocuments">Optionally include documents in the response.</param>
        /// <returns>A list of authors.</returns>
        [HttpGet]
        public IEnumerable<Author> GetAuthors(bool includeDocuments = true)
        {
            if (includeDocuments)
            {
                return _context.Authors.Include(a => a.DocumentsAuthored);
            }

            return _context.Authors;


        }

        // GET: api/v1/Authors/Active
        /// <summary>
        /// Returns all active authors. Former authors are excluded.
        /// </summary>
        /// <param name="includeDocuments">Optionally include documents in the response.</param>
        /// <returns></returns>
        [HttpGet("Active")]
        public IEnumerable<Author> GetActiveAuthors(bool includeDocuments = true)
        {
            if (includeDocuments)
            {
                return _context.Authors.Include(a => a.DocumentsAuthored).Where(a => !a.IsFormerAuthor);
            }

            return _context.Authors.Where(a => !a.IsFormerAuthor);

        }

        // GET: api/v1/Authors/5
        /// <summary>
        /// Get a single author.
        /// </summary>
        /// <param name="id">ID of the author.</param>
        /// <param name="includeDocuments">Optionally include documents in the response.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id, bool includeDocuments = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author author;

            if (includeDocuments)
            {
                author = await _context.Authors.Include(a => a.DocumentsAuthored).SingleOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            }
            

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        /// <summary>
        /// Updates an author.
        /// </summary>
        /// <param name="id">ID of the author.</param>
        /// <param name="author">Updated author.</param>
        /// <returns></returns>
        /// <response code="204">Update is sucessuful.</response>
        /// <response code="400">Request is incorrect or id from the path does not match the id of the author.</response>
        /// <response code="404">Author does not exist.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/v1/Authors
        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created author.</response>
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // POST: api/v1/Authors/Batch
        /// <summary>
        /// Creates multiple authors.
        /// </summary>
        /// <param name="AuthorList">List of authors.</param>
        /// <returns></returns>
        /// <response code="201">Returns the list of newly created authors.</response>
        [HttpPost("Batch")]
        public async Task<IActionResult> PostMultipleAuthors([FromBody] IEnumerable<Author> AuthorList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Authors.AddRange(AuthorList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthors",AuthorList);
        }

        // DELETE: api/Authors/5
        /// <summary>
        /// Deletes an author.
        /// </summary>
        /// <param name="id">ID of the author to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}