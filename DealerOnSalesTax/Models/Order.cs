using System;
namespace DealerOnSalesTax.Models
{
	public class OrderModel
	{
		private List<OrderItem> _orderItems;

		public OrderModel(List<CartItem> cartItems)
		{
			foreach (CartItem ci in cartItems)
			{
			}
		}

		public string GetReceipt()
		{
			return "";
		}
	}
}

