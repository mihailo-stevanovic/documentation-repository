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
    [Route("api/v1/ClientCatalogs")]
    public class ClientCatalogsController : Controller
    {
        #region Initilization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public ClientCatalogsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GET
        // GET: api/v1/ClientCatalogs
        /// <summary>
        /// Returns all client catalogs.
        /// </summary>
        /// <returns>List of client catalogs or Not Found error.</returns>
        /// <response code="200">Catalogs successfully retrieved.</response>
        /// <response code="404">No catalogs were found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientCatalogDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetClientCatalogs()
        {
            var catalogs = _context.ClientCatalogs.Select(c => _mapper.Map<ClientCatalogDto>(c));

            if (!catalogs.Any())
            {
                return NotFound();
            }

            return Ok(catalogs);
        }

        // GET: api/v1/ClientCatalogs/5
        /// <summary>
        /// Returns a single client catalog.
        /// </summary>
        /// <param name="id">ID of the client catalog.</param>
        /// <returns>Client catalog object or description of error.</returns>
        /// <response code="200">Catalog successfully returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Catalog with a matching ID could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientCatalogDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClientCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientCatalog = await _context.ClientCatalogs.SingleOrDefaultAsync(m => m.Id == id);

            if (clientCatalog == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClientCatalogDto>(clientCatalog));
        }
        #endregion

        #region PUT
        // PUT: api/v1/ClientCatalogs/5
        /// <summary>
        /// Updates an existing client catalog.
        /// </summary>
        /// <param name="id">ID of the client catalog.</param>
        /// <param name="clientCatalog">Client catalog object.</param>
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Update is successuful.</response>
        /// <response code="400">Request is incorrect or ID from the path does not match the ID of the author.</response>
        /// <response code="404">Author does not exist.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutClientCatalog([FromRoute] int id, [FromBody] ClientCatalogDto clientCatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientCatalog.Id)
            {
                ModelState.AddModelError("Invalid Catalog ID", "The Catalog ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            ClientCatalog clientCatalogReversed = _mapper.Map<ClientCatalog>(clientCatalog);

            _context.Entry(clientCatalogReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!ClientCatalogExists(id))
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
        // POST: api/v1/ClientCatalogs
        /// <summary>
        /// Creates a new client catalog.
        /// </summary>
        /// <param name="clientCatalog">Client catalog object.</param>
        /// <returns>Client catalog object or description of error.</returns>
        /// <response code="201">Returns the newly created author.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClientCatalogDto),201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostClientCatalog([FromBody] ClientCatalogDto clientCatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClientCatalog clientCatalogReversed = _mapper.Map<ClientCatalog>(clientCatalog);

            _context.ClientCatalogs.Add(clientCatalogReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCatalog", new { id = clientCatalogReversed.Id }, _mapper.Map<ClientCatalogDto>(clientCatalogReversed));
        }

        // POST: api/v1/ClientCatalogs/Batch
        /// <summary>
        /// Creates multiple client catalogs.
        /// </summary>
        /// <param name="clientCatalogList">List of client catalog objects.</param>
        /// <returns>List of client catalog objects or description of error.</returns>
        /// <response code="204">Action is successful.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("Batch")]
        [ProducesResponseType(typeof(IEnumerable<ClientCatalogDto>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostMultipleClientCatalogs([FromBody] IEnumerable<ClientCatalogDto> clientCatalogList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ClientCatalog> clientCatalogReversedList = clientCatalogList.Select(c => _mapper.Map<ClientCatalog>(c));

            _context.ClientCatalogs.AddRange(clientCatalogReversedList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCatalogs", null, clientCatalogReversedList.Select(c => _mapper.Map<ClientCatalogDto>(c)));
        }
        #endregion

        #region DELETE
        // DELETE: api/v1/ClientCatalogs/5
        /// <summary>
        /// Deletes a client catalog.
        /// </summary>
        /// <param name="id">ID of the client catalog.</param>
        /// <returns>Deleted client catalog object or description of error.</returns>
        /// <response code="200">client catalog sucessufully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Client catalog with the provided ID could not be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClientCatalogDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteClientCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientCatalog = await _context.ClientCatalogs.SingleOrDefaultAsync(m => m.Id == id);
            if (clientCatalog == null)
            {
                return NotFound();
            }

            _context.ClientCatalogs.Remove(clientCatalog);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ClientCatalogDto>(clientCatalog));
        }
        #endregion

        private bool ClientCatalogExists(int id)
        {
            return _context.ClientCatalogs.Any(e => e.Id == id);
        }
    }
}