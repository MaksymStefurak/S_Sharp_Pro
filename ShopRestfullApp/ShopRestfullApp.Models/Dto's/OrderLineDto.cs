using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRestfullApp.Models.Dto_s
{
    public class OrderLineDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
