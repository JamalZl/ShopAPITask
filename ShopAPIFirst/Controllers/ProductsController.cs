using APIFirstProject.Data.DAL;
using APIFirstProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIFirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return StatusCode(201, product);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(201, _context.Products.Where(p=>p.DisplayStatus).ToList());
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }
        [HttpPut, Route("")]
        public IActionResult Update(Product product)
        {
            Product existProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                if(existProduct == null) return NotFound();

            existProduct.CostPrice = product.CostPrice;
            existProduct.SalePrice = product.SalePrice;
            existProduct.Name = product.Name;
            existProduct.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return NoContent();

        }
        [HttpDelete,Route("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch,Route("{id}")]
        public IActionResult ChangeStatus(int id,bool status)
        {
            Product product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();

            product.DisplayStatus = status;
            _context.SaveChanges();
            return NoContent();


        }
    }
}
