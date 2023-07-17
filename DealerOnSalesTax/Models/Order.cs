using System.Text;

namespace DealerOnSalesTax.Models
{
    public class Order
	{
        // Constants (which could reside in a database in more complicated apps).
        private const string _noItemsMessage = "No Items in Cart";
        private const double _basicSalesTaxMultiplier = 0.10;
        private const double _importTaxMultiplier = 0.05;
        private const double _roundingMultiplier = 0.05;

		private List<CartItem> _cartItems;
		private List<OrderItem> _orderItems;
        private double _totalSalesTax;
        private double _totalOrderPrice;

        /// <summary>
        /// Initialize order. In a real-world app this order may be given an ID
        /// and saved to a database.
        /// </summary>
        /// <param name="cartItems">Cart items from the frontend.</param>
		public Order(List<CartItem> cartItems)
		{
			_cartItems = cartItems;
			_orderItems = new List<OrderItem>();
		}

        /// <summary>
        /// Get the receipt for this order.
        /// </summary>
        /// <returns>Receipt text, with line items separated by newline characters.</returns>
		public string GetReceipt()
        {
            if (_cartItems.Count == 0)
            {
                return _noItemsMessage;
            }

            StringBuilder receipt = new StringBuilder();

            /* Convert the frontend cart items to objects more suitable for processing.
             * These would be useful if other calculations were also needed on this order.
             */
			createOrderItemsFromCartItems();

            foreach (OrderItem orderItem in _orderItems)
            {
                string name = orderItem.Name;
                double individualItemPrice = orderItem.Price;
                int quantity = orderItem.Quantity;
                double taxAmount = 0.0;

                // Calculate applicable taxes.
                if (orderItem.HasBasicSalesTax)
                {
                    taxAmount += getTaxAmount(individualItemPrice, _basicSalesTaxMultiplier, _roundingMultiplier);
                }

                if (orderItem.IsImported)
                {
                    taxAmount += getTaxAmount(individualItemPrice, _importTaxMultiplier, _roundingMultiplier);
                }

                // Add tax to individual item price and calculate price for all items.
                individualItemPrice = Math.Round(individualItemPrice += taxAmount, 2);
                double priceForAllItems = individualItemPrice * quantity;

                // Update running totals.
                _totalSalesTax += taxAmount * quantity;
                _totalOrderPrice += priceForAllItems;

                // Add line to receipt.
                string receiptLine = $"{name}: {priceForAllItems.ToString("F2")}";

                if (quantity > 1)
                {
                    receiptLine += $" ({quantity} @ {individualItemPrice.ToString("F2")})";
                }

                receipt.AppendLine(receiptLine);
            }

            receipt.AppendLine($"Sales Taxes: {_totalSalesTax.ToString("F2")}");
            receipt.AppendLine($"Total: {_totalOrderPrice.ToString("F2")}");

            return receipt.ToString();
        }

        /// <summary>
        /// Convert the frontend cart items to objects more suitable for processing.
        /// </summary>
		private void createOrderItemsFromCartItems()
        {
            foreach (CartItem cartItem in _cartItems)
            {
                string id = getId(cartItem);

                // If ID already exists in order, add to quantity.
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

        /// <summary>
        /// Since we don't have a database with items that have unique ID's, we can create
        /// a unique ID using the item properties.
        /// </summary>
        /// <param name="cartItem">The frontend item to create an ID for.</param>
        /// <returns>Newly created ID.</returns>
        private string getId(CartItem cartItem)
        {
            return string.Concat(cartItem.Name, cartItem.Category, cartItem.IsImported, cartItem.Price);
        }

        /// <summary>
        /// Get the tax amount for an item.
        /// </summary>
        /// <param name="price">Item price (before tax).</param>
        /// <param name="taxMultiplier">Tax multiplier: 15% tax = multiplier of 0.05</param>
        /// <param name="roundingMultiplier">Round up to nearest multiple of this number. Ex: 0.05</param>
        /// <returns>Tax amount.</returns>
        private double getTaxAmount(double price, double taxMultiplier, double roundingMultiplier)
        {
            double taxAmount = price * taxMultiplier;
            return Math.Ceiling(taxAmount / roundingMultiplier) * roundingMultiplier;
        }
    }
}

