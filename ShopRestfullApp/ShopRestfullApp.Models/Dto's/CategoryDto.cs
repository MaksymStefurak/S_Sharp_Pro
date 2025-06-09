using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRestfullApp.Models.Dto_s
{
    public class CategoryDto
    {
        public string? Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
