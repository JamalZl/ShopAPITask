using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFirstProject.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SalePrice{ get; set; }
        public double CostPrice{ get; set; }
        public bool DisplayStatus{ get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
