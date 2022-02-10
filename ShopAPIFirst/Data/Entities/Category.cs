using ShopAPIFirst.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFirstProject.Data.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
        public bool IsDeleted { get; set; }


    }
}
