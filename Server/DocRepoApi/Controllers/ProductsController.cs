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
        private readonly DocRepoContext _context;
        private readonly IMapper _mapper;

        public ProductsController(DocRepoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/Products
        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <returns>List of all products.</returns>
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            return _context.Products.Select(p => _mapper.Map<ProductDto>(p));
        }

        // GET: api/v1/Products/5
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

        // PUT: api/v1/Products/5
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

        // POST: api/Products
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

            return CreatedAtAction("GetProduct", new { id = productReversed.Id }, _mapper.Map<ProductDto>(product));
        }

        [HttpPost("Batch")]
        public async Task<IActionResult> PostMultipleProducts([FromBody] IEnumerable<ProductDto> ProductList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Product> ProductReversedList = ProductList.Select(a => _mapper.Map<Product>(a));

            _context.Products.AddRange(ProductReversedList);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        // DELETE: api/Products/5
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}