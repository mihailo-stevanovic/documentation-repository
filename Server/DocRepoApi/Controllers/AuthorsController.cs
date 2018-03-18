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
    [Route("api/v1/Authors")]
    public class AuthorsController : Controller
    {
        #region Initialization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GET
        // GET: api/v1/Authors
        /// <summary>
        /// Returns all authors.
        /// </summary>
        /// <returns>A list of authors.</returns>
        [HttpGet]
        public IEnumerable<AuthorDto> GetAuthors()
        {           

            return _context.Authors.Select(a => _mapper.Map<AuthorDto>(a));

        }

        // GET: api/v1/Authors/Active
        /// <summary>
        /// Returns all active authors. Former authors are excluded.
        /// </summary>        
        /// <returns></returns>
        [HttpGet("Active")]
        public IEnumerable<AuthorDto> GetActiveAuthors()
        {         
            return _context.Authors.Where(a => !a.IsFormerAuthor).Select(a => _mapper.Map<AuthorDto>(a));
        }

        // GET: api/v1/Authors/5
        /// <summary>
        /// Returns a single author.
        /// </summary>
        /// <param name="id">ID of the author.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);                      

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(author));
        }
        #endregion

        #region PUT
        // PUT: api/Authors/5
        /// <summary>
        /// Updates an author.
        /// </summary>
        /// <param name="id">ID of the author.</param>
        /// <param name="author">Updated author.</param>
        /// <returns></returns>
        /// <response code="204">Update is successuful.</response>
        /// <response code="400">Request is incorrect or id from the path does not match the id of the author.</response>
        /// <response code="404">Author does not exist.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] AuthorDto author)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            var authorReversed = _mapper.Map<Author>(author);

            _context.Entry(authorReversed).State = EntityState.Modified;
            

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
        #endregion

        #region POST
        // POST: api/v1/Authors
        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created author.</response>
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] AuthorDto author)
        {            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author authorReversed = _mapper.Map<Author>(author);

            _context.Authors.Add(authorReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = authorReversed.Id }, _mapper.Map<AuthorDto>(authorReversed));
        }

        // POST: api/v1/Authors/Batch
        /// <summary>
        /// Creates multiple authors.
        /// </summary>
        /// <param name="AuthorList">List of authors.</param>
        /// <returns></returns>
        /// <response code="204">Action is successful.</response>
        [HttpPost("Batch")]
        public async Task<IActionResult> PostMultipleAuthors([FromBody] IEnumerable<AuthorDto> AuthorList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Author> authorReversedList = AuthorList.Select(a => _mapper.Map<Author>(a));

            _context.Authors.AddRange(authorReversedList);
            await _context.SaveChangesAsync();
                        
            return CreatedAtAction("GetAuthors", null, authorReversedList.Select(a => _mapper.Map<AuthorDto>(a)));
        }
        #endregion

        #region DELETE
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

            return Ok(_mapper.Map<AuthorDto>(author));
        }
        #endregion

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}