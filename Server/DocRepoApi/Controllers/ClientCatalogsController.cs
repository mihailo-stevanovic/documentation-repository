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
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ClientCatalogDto> GetClientCatalogs()
        {
            return _context.ClientCatalogs.Select(c => _mapper.Map<ClientCatalogDto>(c));
        }

        // GET: api/v1/ClientCatalogs/5
        /// <summary>
        /// Returns a single client catalog.
        /// </summary>
        /// <param name="id">ID of the client catalog.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
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
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientCatalog([FromRoute] int id, [FromBody] ClientCatalogDto clientCatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientCatalog.Id)
            {
                return BadRequest();
            }

            ClientCatalog clientCatalogReversed = _mapper.Map<ClientCatalog>(clientCatalog);

            _context.Entry(clientCatalogReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientCatalogExists(id))
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
        // POST: api/v1/ClientCatalogs
        /// <summary>
        /// Creates a new client catalog.
        /// </summary>
        /// <param name="clientCatalog">Client catalog object.</param>
        /// <returns></returns>
        [HttpPost]
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
        /// <returns></returns>
        [HttpPost("Batch")]
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
        /// <returns></returns>
        [HttpDelete("{id}")]
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