using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dotnet_hero.Data;
using dotnet_hero.DTOs.Product;
using dotnet_hero.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_hero.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public ProductsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> GetProducts()
        {
            return Ok(databaseContext.Products.Include(p => p.Category).OrderByDescending(p => p.ProductId).Select(ProductResponse.FromProduct).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetProductById(int id)
        {
            var result = databaseContext.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
            if (result == null) return NotFound();
            return Ok(ProductResponse.FromProduct(result));
        }

        // GET api/values/search?name=$name
        [HttpGet("search")]
        public ActionResult SearchProducts([FromQuery] string name = "")
        {
            var result = databaseContext.Products.Include(p => p.Category).Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(ProductResponse.FromProduct).ToList();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult AddProduct([FromForm] ProductRequest productRequest)
        {

            //var product = new Product
            //{
            //    Name = productRequest.Name
            //};

            //mapster
            var product = productRequest.Adapt<Product>();

            databaseContext.Add(product);
            databaseContext.SaveChanges();
            return StatusCode((int)HttpStatusCode.Created);
        }




        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromForm] ProductRequest productRequest)
        {
            if (id != productRequest.ProductId) return BadRequest();

            var result = databaseContext.Products.Find(id);

            if (result == null) return NotFound();

            //mapser
            productRequest.Adapt(result);

            databaseContext.Products.Update(result);
            databaseContext.SaveChanges();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {


            var result = databaseContext.Products.Find(id);

            if (result == null) return NotFound();
            databaseContext.Products.Remove(result);
            databaseContext.SaveChanges();

            return NoContent();
        }
    }
}
