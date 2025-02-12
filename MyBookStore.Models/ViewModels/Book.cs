﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MyBookStore.Models.ViewModels
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Base64image { get; set; }
        public int Categoryid { get; set; }
        public int Publisherid { get; set; }
        public int? Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
