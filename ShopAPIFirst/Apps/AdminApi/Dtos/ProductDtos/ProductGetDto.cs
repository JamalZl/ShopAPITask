using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Apps.AdminApi.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Profit { get; set; }
        public bool DisplayStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public CategoryInProductDto Category { get; set; }

    }
    public class CategoryInProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
    }
}
