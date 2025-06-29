﻿namespace Restourant.Models
{
    public class CartViewModel
    {
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
