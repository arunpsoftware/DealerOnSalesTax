using Microsoft.AspNetCore.Mvc.Rendering;

/* Simple viewmodel to handle data the frontend needs
 * to read and write.
 */
namespace DealerOnSalesTax.Models
{
	public class IndexViewModel
	{
		public List<CartItem> CartItems { get; set; }
		public SelectList Categories { get; set; }
		public string Receipt { get; set; }
	}

	public class CartItem
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public bool IsImported { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
    }
}

