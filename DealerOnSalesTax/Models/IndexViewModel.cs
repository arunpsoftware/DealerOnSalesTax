using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DealerOnSalesTax.Models
{
	public class IndexViewModel
	{
		public List<OrderItem> OrderItems { get; set; }
		public List<SelectListItem> Categories { get; set; }
		public string Receipt { get; set; }
	}

	public class OrderItem
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public bool IsImported { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
    }
}

