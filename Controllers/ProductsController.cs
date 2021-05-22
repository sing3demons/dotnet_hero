using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dotnet_hero.Data;
using dotnet_hero.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_hero.Controllers
{
    //[ApiController]
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
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(databaseContext.Products.OrderByDescending(p => p.ProductId).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            var result = databaseContext.Products.Find(id);
            if (result == null) return NotFound();
            return Ok(new { product = result });
        }

        [HttpGet("search")]
        public ActionResult SearchProducts([FromQuery] string name = "")
        {
            var result = databaseContext.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult AddProduct([FromForm] Product model)
        {
            databaseContext.Add(model);
            databaseContext.SaveChanges();
            return StatusCode((int) HttpStatusCode.Created);
        }




        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromForm] Product model)
        {
            //if (id != model.ProductId) return BadRequest();

            var result = databaseContext.Products.Find(id);

            if (result == null) return NotFound();

            //mapser
            result.Name = model.Name;
            result.Price = model.Price;
            result.Stock = model.Stock;
            result.CategoryId = model.CategoryId;

            databaseContext.Products.Update(result);
            
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
