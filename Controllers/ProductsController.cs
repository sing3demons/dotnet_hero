using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dotnet_hero.Data;
using dotnet_hero.DTOs.Product;
using dotnet_hero.Entities;
using dotnet_hero.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_hero.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductsController(IProductService productService) => this.productService = productService;


        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            //return Ok(databaseContext.Products.Include(p => p.Category).OrderByDescending(p => p.ProductId).Select(ProductResponse.FromProduct).ToList());
            var products = (await productService.FindAll()).Select(ProductResponse.FromProduct).ToList();
            return Ok(new { products = products });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            //var result = databaseContext.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
            var product = await productService.FindOne(id);
            if (product == null) return NotFound();
            //return Ok(ProductResponse.FromProduct(product));
            return product.Adapt<ProductResponse>();
        }

        // GET api/values/search?name=$name
        [HttpGet("search")]
        public async Task<ActionResult> SearchProducts([FromQuery] string name = "")
        {
            //var result = databaseContext.Products.Include(p => p.Category).Where(p => p.Name.ToLower().Contains(name.ToLower()))
            //    .Select(ProductResponse.FromProduct).ToList();
            List<ProductResponse> result = (await productService.Search(name))
                .Select(ProductResponse.FromProduct).ToList();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult> AddProduct([FromForm] ProductRequest productRequest)
        {
            (string errorMessage, string imageName) = await productService.UploadImage(productRequest.FormFile);
            if (!string.IsNullOrEmpty(errorMessage)) return BadRequest();

            //var product = new Product
            //{
            //    Name = productRequest.Name
            //};

            //mapster
            var product = productRequest.Adapt<Product>();

            product.Image = imageName;

            await productService.Create(product);

            return StatusCode((int)HttpStatusCode.Created);

        }




        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] ProductRequest productRequest)
        {

            //if (id != productRequest.ProductId) return BadRequest();

            //var result = databaseContext.Products.Find(id);
            var product = await productService.FindOne(id);

            if (product == null) return NotFound();

            (string errorMessage, string imageName) = await productService.UploadImage(productRequest.FormFile);
            if (!string.IsNullOrEmpty(errorMessage)) return BadRequest();

            if (!string.IsNullOrEmpty(imageName))
            {
                product.Image = imageName;
            }

            //mapser
            productRequest.Adapt(product);

            await productService.Update(product);
            //databaseContext.Products.Update(result);
            //databaseContext.SaveChanges();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            //var result = databaseContext.Products.Find(id);
            var product = await productService.FindOne(id);

            if (product == null) return NotFound();
            await productService.Delete(product);

            return NoContent();
        }
    }
}
