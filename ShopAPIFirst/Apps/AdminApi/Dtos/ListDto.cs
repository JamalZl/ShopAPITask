using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Apps.AdminApi.Dtos
{
    public class ListDto<TItem>
    {

        public List<TItem> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
