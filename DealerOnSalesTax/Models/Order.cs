using System;
using System.Text;

namespace DealerOnSalesTax.Models
{
	public class Order
	{
        private const string _noItemsMessage = "No Items in Cart";
        private const double _basicSalesTaxMultiplier = 0.10;
        private const double _importTaxMultiplier = 0.05;
        private const double _roundingMultiplier = 0.05;

		private List<CartItem> _cartItems;
		private List<OrderItem> _orderItems;
        private double _totalSalesTax;
        private double _totalOrderPrice;

		public Order(List<CartItem> cartItems)
		{
			_cartItems = cartItems;
			_orderItems = new List<OrderItem>();
		}

		public string GetReceipt()
        {
            if (_cartItems.Count == 0)
            {
                return _noItemsMessage;
            }

            StringBuilder receipt = new StringBuilder();

			createOrderItemsFromCartItems();

            foreach (OrderItem orderItem in _orderItems)
            {
                string name = orderItem.Name;
                double individualItemPrice = orderItem.Price;
                int quantity = orderItem.Quantity;
                double taxAmount = 0.0;

                if (orderItem.HasBasicSalesTax)
                {
                    taxAmount += getTaxAmount(individualItemPrice, _basicSalesTaxMultiplier, _roundingMultiplier);
                }

                if (orderItem.IsImported)
                {
                    taxAmount += getTaxAmount(individualItemPrice, _importTaxMultiplier, _roundingMultiplier);
                }

                individualItemPrice += taxAmount;
                double priceForAllItems = individualItemPrice * quantity;

                _totalSalesTax += taxAmount * quantity;
                _totalOrderPrice += priceForAllItems;

                receipt.AppendLine($"{name}: {priceForAllItems}");

                if (quantity > 1)
                {
                    receipt.Append($" ({quantity} @ {individualItemPrice})");
                }
            }

            receipt.AppendLine($"Sales Taxes: {_totalSalesTax}");
            receipt.AppendLine($"Total: {_totalOrderPrice}");

            return receipt.ToString();
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

        private double getTaxAmount(double price, double taxMultiplier, double roundingMultiplier)
        {
            double taxAmount = price * taxMultiplier;
            return Math.Ceiling(taxAmount / roundingMultiplier) * roundingMultiplier;
        }
    }
}

