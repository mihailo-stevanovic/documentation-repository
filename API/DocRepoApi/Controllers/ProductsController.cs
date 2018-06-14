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
    [Route("api/v1/Products")]
    public class ProductsController : Controller
    {
        #region Initilization
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public ProductsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GET
        #region Product        
        // GET: api/v1/Products
        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <returns>List of products or description of error.</returns>
        /// <response code="200">Products successfully retrieved.</response>
        /// <response code="404">No products were found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetProducts()
        {
            var products = _context.Products.Select(p => _mapper.Map<ProductDto>(p));

            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/v1/Products/5
        /// <summary>
        /// Returns a single product by ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>A single product or description of error.</returns>
        /// <response code="200">Product successfully retrieved.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Products with matching ID not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        #endregion

        #region Product Versions
        // GET: api/v1/Products/5/Versions
        /// <summary>
        /// Returns a list of versions associated to a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <returns>List of product versions.</returns>
        /// <response code="200">Product versions successfully retrieved.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Products versions with matching product ID not found.</response>
        [HttpGet("{productId}/Versions")]
        [ProducesResponseType(typeof(IEnumerable<ProductVersionDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductVersions([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product ID", "The Product ID supplied in the does not match an existing product.");
                return BadRequest(ModelState);
            }

            var productVersions = await _context.ProductVersions
                .Where(pv => pv.ProductId == productId)
                .Include(pv => pv.Product)
                .OrderByDescending(pv => pv.Release)                         
                .ToListAsync();

            if (!productVersions.Any())
            {
                return NotFound();
            }                        

            return Ok(productVersions.Select(pv => _mapper.Map<ProductVersionDto>(pv)));
        }

        // GET: api/v1/Products/5/Versions/5
        /// <summary>
        /// Returns a single version associated to a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <returns>A single product version or description of error.</returns>
        /// <response code="200">Product version successfully retrieved.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Product versions with matching ID not found.</response>
        [HttpGet("{productId}/Versions/{versionId}")]
        [ProducesResponseType(typeof(ProductVersionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductVersion([FromRoute] int productId, [FromRoute] int versionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product ID", "The Product ID supplied in the does not match an existing product.");
                return BadRequest(ModelState);
            }

            var productVersion = await _context.ProductVersions
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.ProductId == productId && m.Id == versionId);

            if (productVersion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductVersionDto>(productVersion));
        }
        #endregion
        #endregion

        #region PUT
        #region Product         
        // PUT: api/v1/Products/5
        /// <summary>
        /// Updates a single product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <param name="product">Product object.</param>
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Update is successuful.</response>
        /// <response code="400">Request is incorrect or ID from the path does not match the ID of the product.</response>
        /// <response code="404">Product does not exist.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                ModelState.AddModelError("Invalid Product ID", "The Product ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }

            var productReversed = _mapper.Map<Product>(product);

            _context.Entry(productReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!ProductExists(id))
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

        #region Product Version
        // PUT: api/v1/products/5/versions/5
        /// <summary>
        /// Updates a version of a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <param name="productVersion">Product version object.</param>
        /// <returns>Empty body or description of error.</returns>
        /// <response code="204">Update is successuful.</response>
        /// <response code="400">Request is incorrect or ID from the path does not match the ID of the product version.</response>
        /// <response code="404">Product version does not exist.</response>
        [HttpPut("{productId}/Versions/{versionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProductVersion([FromRoute] int productId, [FromRoute] int versionId, [FromBody] ProductVersionDto productVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (versionId != productVersion.Id)
            {
                ModelState.AddModelError("Invalid Version ID", "The Version ID supplied in the query and the body of the request do not match.");
                return BadRequest(ModelState);
            }            
            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product", "The product does not exist.");
                return BadRequest(ModelState);
            }

            var productVersionReversed = _mapper.Map<ProductVersion>(productVersion);

            productVersionReversed.ProductId = productId;

            _context.Entry(productVersionReversed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException xcp)
            {
                if (!ProductVersionExists(versionId))
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
        #endregion

        #region POST
        #region Product 
        // POST: api/v1/Products
        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="product">Product object.</param>
        /// <returns>Product object or description of error.</returns>
        /// <response code="201">Returns the newly created product.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(400)]        
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

        // POST: api/v1/Products/Batch
        /// <summary>
        /// Creates multiple products.
        /// </summary>
        /// <param name="ProductList">List of product objects.</param>
        /// <returns>List of product objects or description of error.</returns>
        /// <response code="201">Action is successful.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("Batch")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 201)]
        [ProducesResponseType(400)]
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
        #endregion

        #region Product Version
        // POST: api/v1/Products/5/Versions
        /// <summary>
        /// Creates a new version of a product.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="productVersion">Product version object.</param>
        /// <returns>Product version object or description of error.</returns>
        /// <response code="201">Returns the newly created product version.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("{productId}/Versions")]
        [ProducesResponseType(typeof(ProductVersionDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostProductVersion([FromRoute] int productId, [FromBody] ProductVersionDto productVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product", "The product does not exist.");
                return BadRequest(ModelState);
            }

            // Product product = _context.Products.SingleOrDefault(p => p.Id == productId);

            var productVersionReversed = _mapper.Map<ProductVersion>(productVersion);

            productVersionReversed.ProductId = productId;     

            _context.ProductVersions.Add(productVersionReversed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductVersion", new { productId = productId, versionId = productVersionReversed.Id }, _mapper.Map<ProductVersionDto>(productVersionReversed));
        }

        // POST: api/v1/Products/5/Versions/Batch
        /// <summary>
        /// Creates multiple product versions.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="productVersionList">List of product version objects.</param>
        /// <returns>List of product version objects or description of error.</returns>
        /// <response code="201">Action is successful.</response>
        /// <response code="400">Invalid request.</response>
        [HttpPost("{productId}/Versions/Batch")]
        [ProducesResponseType(typeof(IEnumerable<ProductVersionDto>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostMultipleProductVersions([FromRoute] int productId, [FromBody] IEnumerable<ProductVersionDto> productVersionList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product", "The product does not exist.");
                return BadRequest(ModelState);
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
        #endregion
        #endregion

        #region DELETE
        #region Product 
        // DELETE: api/v1/products/5
        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>Deleted document type object or description of error.</returns>
        /// <response code="200">Product sucessufully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Product with the provided ID could not be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        #endregion

        #region Product Version
        // DELETE: api/v1/products/5/versions/5
        /// <summary>
        /// Deletes a product version.
        /// </summary>
        /// <param name="productId">ID of the product.</param>
        /// <param name="versionId">ID of the version.</param>
        /// <returns>Deleted document type object or description of error.</returns>
        /// <response code="200">Product version sucessufully deleted.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Product version with the provided ID could not be found.</response>
        [HttpDelete("{productId}/Versions/{versionId}")]
        [ProducesResponseType(typeof(ProductVersionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductVersion([FromRoute] int productId, [FromRoute] int versionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(productId))
            {
                ModelState.AddModelError("Invalid Product", "The product does not exist.");
                return BadRequest(ModelState);
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
        #endregion
        #endregion
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