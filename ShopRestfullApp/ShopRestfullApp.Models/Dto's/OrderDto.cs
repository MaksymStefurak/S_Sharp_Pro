using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRestfullApp.Models.Dto_s
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
    }
}
