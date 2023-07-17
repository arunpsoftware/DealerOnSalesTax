﻿using System;
namespace DealerOnSalesTax.Models
{
	public class OrderItem
    {
        // Readonly.
        public string Id { get; }
        public string Name { get; }
        public string Category { get; }
        public bool IsImported { get; }
        public double Price { get; }
        public bool HasBasicSalesTax { get; }

        // Properties which can be changed.
        public int Quantity { get; set; }

        public OrderItem(string id, string name, string category, bool isImported, double price, int quantity)
		{
            Id = id;
            Name = name;
            Category = category;
            IsImported = isImported;
            Price = price;
            HasBasicSalesTax = DoesCategoryHaveBasicSalesTax();
            Quantity = quantity;
        }

        private bool DoesCategoryHaveBasicSalesTax()
        {
            return !Category.Equals("Uncategorized");
        }
	}
}

