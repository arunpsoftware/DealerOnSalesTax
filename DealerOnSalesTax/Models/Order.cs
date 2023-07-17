using System;
namespace DealerOnSalesTax.Models
{
	public class OrderModel
	{
		private List<CartItem> _cartItems;
		private List<OrderItem> _orderItems;

		public OrderModel(List<CartItem> cartItems)
		{
			_cartItems = cartItems;
			_orderItems = new List<OrderItem>();
		}

		public string GetReceipt()
        {
			createOrderItemsFromCartItems();

            foreach (OrderItem orderItem in _orderItems)
            {

            }

			return "";
        }

		private void createOrderItemsFromCartItems()
        {
            foreach (CartItem cartItem in _cartItems)
            {
                string id = getId(cartItem);

                if (_orderItems.Any(x => x.Id.Equals(id)))
                {
					_orderItems.FirstOrDefault(x => x.Id.Equals(id)).Quantity += cartItem.Quantity;
                }
                else
                {
                    OrderItem newOrderItem = new OrderItem(id, cartItem.Name, cartItem.Category, cartItem.IsImported, cartItem.Price, cartItem.Quantity);
                    _orderItems.Add(newOrderItem);
                }
            }
        }

        private string getId(CartItem cartItem)
        {
            return string.Concat(cartItem.Name, cartItem.Category, cartItem.IsImported, cartItem.Price);
        }
    }
}

