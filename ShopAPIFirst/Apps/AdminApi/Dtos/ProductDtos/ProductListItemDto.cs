using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Apps.AdminApi.Dtos.ProductDtos
{
    public class ProductListItemDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool DisplayStatus { get; set; }
        public CategoryInProductListItemDto Category { get; set; }
    }

    public class CategoryInProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
