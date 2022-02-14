using APIFirstProject.Data.DAL;
using APIFirstProject.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPIFirst.Apps.AdminApi.Dtos;
using ShopAPIFirst.Apps.AdminApi.Dtos.CategoryDtos;
using ShopAPIFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public CategoriesController(ShopDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.Include(c=>c.Products).FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            CategoryGetDto categoryDto = _mapper.Map<CategoryGetDto>(category);

            return Ok(categoryDto);
        }


        [HttpGet]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Categories.Where(x => !x.IsDeleted);

            ListDto<CategoryListItemDto> listDto = new ListDto<CategoryListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 8).Take(8).Select(x => new CategoryListItemDto { Id = x.Id, Name = x.Name }).ToList()
            };

            return Ok(listDto);
        }
        [HttpPost]
        public IActionResult Create([FromForm]CategoryPostDto categoryDto)
        {

            if (_context.Categories.Any(x => x.Name.ToLower() == categoryDto.Name.Trim().ToLower()))
                return StatusCode(409);

            if (!categoryDto.Image.IsSizeOkay(2))
            {
                return NotFound(); 
            }
            if (!categoryDto.Image.IsImage())
            {
                return NotFound();
            }

            Category category = new Category
            {
                Name = categoryDto.Name,
            };
            category.Image = categoryDto.Image.SaveImg(_env.WebRootPath, "assets/uploads");

            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(201, category);
        }
        [HttpPut, Route("{id}")]
        public IActionResult Update(int id,[FromForm]CategoryPostDto categoryDto)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();


            Helpers.Helper.DeleteImg(_env.WebRootPath, "/assets/uploads", category.Image);
            category.Image = categoryDto.Image.SaveImg(_env.WebRootPath, "assets/uploads");
            if (_context.Categories.Any(x => x.Id != id && x.Name.ToUpper() == categoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);

            category.Name = categoryDto.Name;
            category.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            category.IsDeleted = true;
            category.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return NoContent();
        }

    }
}
