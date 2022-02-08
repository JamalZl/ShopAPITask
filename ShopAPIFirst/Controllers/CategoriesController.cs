using APIFirstProject.Data.DAL;
using APIFirstProject.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return StatusCode(201, category);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(201, _context.Categories.ToList());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return BadRequest();
            List<Category> categories = _context.Categories.Where(c => c.Name == category.Name).ToList();
            foreach (Category item in categories)
            {
                if (item.Name.ToLower().Trim() == category.Name.ToLower().Trim())
                {
                    return StatusCode(403);
                }
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(201, category);
        }
        [HttpPut]
        public IActionResult Update(Category category, int id)
        {
            if (category.Name == null)
            {
                NotFound();
            }
            Category nameControl = _context.Categories.FirstOrDefault(t => t.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            Category existCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existCategory == null) return NotFound();
            if (nameControl != null && nameControl.Id != id)
            {
                return StatusCode(405);
            }
            existCategory.Name = category.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
