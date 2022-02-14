using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMvc.DTOs
{
    public class CategoryListDto
    {
        public List<CategoryListItemDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
