using APIFirstProject.Data.DAL;
using APIFirstProject.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPIFirst.Apps.AdminApi.Dtos;
using ShopAPIFirst.Apps.AdminApi.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Linq;

namespace APIFirstProject.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ShopDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(p=>p.Category).ThenInclude(c => c.Products).FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null)
            {
                return NotFound();
            }

            ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);
            return StatusCode(201, productDto);
        }


        [HttpGet]
        public IActionResult GetAll(int page=1,string search=null)
        {

            var query = _context.Products.Include(p=>p.Category).Where(p => !p.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query=query.Where(q => q.Name.ToLower().Contains(search.ToLower().Trim()));
            }
            ListDto<ProductListItemDto> productListDto = new ListDto<ProductListItemDto>
            {
                Items = query.Skip((page - 1) * 4).Take(4).Select(p => new ProductListItemDto 
                { Name = p.Name, 
                    SalePrice = p.SalePrice,
                    CostPrice = p.CostPrice,
                    DisplayStatus = p.DisplayStatus,
                    Category=new CategoryInProductListItemDto
                    {
                        Id=p.CategoryId,
                        Name=p.Category.Name
                    }
                
                }).ToList(),
                TotalCount = query.Count()
            };
            return StatusCode(201, productListDto);
        }


        [HttpPost]
        public IActionResult Create(ProductPostDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                SalePrice = productDto.SalePrice,
                CostPrice = productDto.CostPrice,
                DisplayStatus = productDto.DisplayStatus,
                CategoryId=productDto.CategoryId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }
        [HttpPut, Route("{id}")]
        public IActionResult Update(int id,ProductPostDto productDto)
        {
            Product existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
                if(existProduct == null) return NotFound();

            if (existProduct.CategoryId != productDto.CategoryId && !_context.Products.Any(p => p.Id == productDto.CategoryId && !p.IsDeleted))
                return NotFound();
            existProduct.CostPrice = productDto.CostPrice;
            existProduct.SalePrice = productDto.SalePrice;
            existProduct.Name = productDto.Name;
            existProduct.DisplayStatus = productDto.DisplayStatus;
            existProduct.CategoryId = productDto.CategoryId;
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
