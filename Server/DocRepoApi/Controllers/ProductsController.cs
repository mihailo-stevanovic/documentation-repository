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
    [Route("api/v1/products")]
    public class ProductsController : Controller
    {
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public ProductsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
         * GET METHODS
         */

        // GET: api/v1/products
        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <returns>List of all products.</returns>
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            return _context.Products.Select(p => _mapper.Map<ProductDto>(p));
        }

        // GET: api/v1/products/5
        /// <summary>
        /// Returns a single product by ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>A single product.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // GET: api/v1/products/5/versions
        /// <summary>
        /// Returns a list of versions associated to a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <returns>List of product versions.</returns>
        [HttpGet("{productId}/versions")]
        public async Task<IActionResult> GetProductVersions([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var productVersions = _context.ProductVersions.Where(pv => pv.ProductId == product.Id).Select(pv => _mapper.Map<ProductVersionDto>(pv)).ToList();

            foreach (ProductVersionDto pv in productVersions)
            {
                pv.Product = product.FullName;
            }

            productVersions.Sort();

            return Ok(productVersions);
        }

        // GET: api/products/5/versions/5
        /// <summary>
        /// Returns a single version associated to a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <returns></returns>
        [HttpGet("{productId}/versions/{versionId}")]
        public async Task<IActionResult> GetProductVersion([FromRoute] int productId, [FromRoute] int versionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productVersion = await _context.ProductVersions.Include(p => p.Product).SingleOrDefaultAsync(m => m.ProductId == productId && m.Id == versionId);

            if (productVersion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductVersionDto>(productVersion));
        }

        /*
         * PUT METHODS
         */

        // PUT: api/v1/products/5
        /// <summary>
        /// Updates a single product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <param name="product">Product object.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            var productReversed = _mapper.Map<Product>(product);

            _context.Entry(productReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // PUT: api/v1/products/5/versions/5
        /// <summary>
        /// Updates a version of a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <param name="productVersion">Product version object.</param>
        /// <returns></returns>
        [HttpPut("{productId}/versions/{versionId}")]
        public async Task<IActionResult> PutProductVersion([FromRoute] int productId, [FromRoute] int versionId, [FromBody] ProductVersionDto productVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (versionId != productVersion.Id)
            {
                return BadRequest("Version ID from the route does not match the ID from the version object.");
            }
            if (!ProductExists(productId))
            {
                return BadRequest("Invalid product.");
            }

            var productVersionReversed = _mapper.Map<ProductVersion>(productVersion);

            productVersionReversed.ProductId = productId;

            _context.Entry(productVersionReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVersionExists(versionId))
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


        /*
         * POST METHODS
         */
        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="product">Product object.</param>
        /// <returns></returns>
        // POST: api/v1/products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productReversed = _mapper.Map<Product>(product);

            _context.Products.Add(productReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = productReversed.Id }, _mapper.Map<ProductDto>(productReversed));
        }

        // POST: api/v1/products/batch
        /// <summary>
        /// Creates multiple products.
        /// </summary>
        /// <param name="ProductList">List of product objects.</param>
        /// <returns></returns>
        [HttpPost("batch")]
        public async Task<IActionResult> PostMultipleProducts([FromBody] IEnumerable<ProductDto> ProductList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Product> ProductReversedList = ProductList.Select(a => _mapper.Map<Product>(a)).ToList();

            _context.Products.AddRange(ProductReversedList);

            await _context.SaveChangesAsync();


            return CreatedAtAction("GetProducts", null, ProductReversedList.Select(p => _mapper.Map<ProductDto>(p)));
        }
        // POST: api/v1/products/5/versions/
        /// <summary>
        /// Creates a new version of a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="productVersion">Product version object.</param>
        /// <returns></returns>
        [HttpPost("{productId}/versions")]
        public async Task<IActionResult> PostProductVersion([FromRoute] int productId, [FromBody] ProductVersionDto productVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!ProductExists(productId))
            {
                return BadRequest("Invalid product.");
            }

            Product product = _context.Products.SingleOrDefault(p => p.Id == productId);

            var productVersionReversed = _mapper.Map<ProductVersion>(productVersion);
            productVersionReversed.Product = product;     

            _context.ProductVersions.Add(productVersionReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductVersion", new { productId = product.Id, versionId = productVersionReversed.Id }, _mapper.Map<ProductVersionDto>(productVersionReversed));
        }

        // POST: api/v1/products/5/versions/batch
        /// <summary>
        /// Creates multiple product versions.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="productVersionList">List of product version objects.</param>
        /// <returns></returns>
        [HttpPost("{productId}/versions/batch")]
        public async Task<IActionResult> PostMultipleProductVersions([FromRoute] int productId, [FromBody] IEnumerable<ProductVersionDto> productVersionList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                return BadRequest("Invalid product.");
            }

            Product product = _context.Products.SingleOrDefault(p => p.Id == productId);

            List<ProductVersion> productVersionListReversed = productVersionList.Select(p => _mapper.Map<ProductVersion>(p)).ToList();

            foreach (ProductVersion pv in productVersionListReversed)
            {
                pv.Product = product;                
                
            }

            _context.ProductVersions.AddRange(productVersionListReversed);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductVersions", new { productId = product.Id }, productVersionListReversed.Select(p => _mapper.Map<ProductVersionDto>(p)));
        }


        /*
         * DELETE METHODS
         */

        // DELETE: api/v1/products/5
        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // DELETE: api/v1/products/5/versions/5
        /// <summary>
        /// Deletes a product version.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <returns></returns>
        [HttpDelete("{productId}/versions/{versionId}")]
        public async Task<IActionResult> DeleteProductVersion([FromRoute] int productId, [FromRoute] int versionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                return BadRequest("Invalid product.");
            }

            var productVersion = await _context.ProductVersions.Include(p => p.Product).SingleOrDefaultAsync(m => m.Id == versionId);
            if (productVersion == null)
            {
                return NotFound();
            }

            _context.ProductVersions.Remove(productVersion);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ProductVersionDto>(productVersion));
        }
        
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private bool ProductVersionExists(int id)
        {
            return _context.ProductVersions.Any(e => e.Id == id);
        }
    }
}