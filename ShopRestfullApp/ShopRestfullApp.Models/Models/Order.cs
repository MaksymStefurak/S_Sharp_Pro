using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRestfullApp.Models.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderData { get; set; }

        public int CustomerId {  get; set; }
        public Customer? Customer { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

    }
}
