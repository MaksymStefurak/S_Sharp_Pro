﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRestfullApp.Models.Dto_s
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public string? CategoryName {  get; set; }
    }
}
