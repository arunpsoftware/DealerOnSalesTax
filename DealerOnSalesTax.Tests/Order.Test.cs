using DealerOnSalesTax.Models;

namespace DealerOnSalesTax.Tests;

public class Tests
{
    private const string _noItemsMessage = "No Items in Cart";

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ReturnsNoItemsInCartMessage_WhenNoItemsInCart()
    {
        List<CartItem> cartItems = new List<CartItem>();
        Order order = new Order(cartItems);
        string receipt = order.GetReceipt();

        Assert.AreEqual(receipt, _noItemsMessage);
    }

    [Test]
    public void ReturnsExpectedReceipt_WhenExampleInput1()
    {
        CartItem ci1 = getCartItem("Book", "Book", false, 12.49, 1);
        CartItem ci2 = getCartItem("Book", "Book", false, 12.49, 1);
        CartItem ci3 = getCartItem("Music CD", "Uncategorized", false, 14.99, 1);
        CartItem ci4 = getCartItem("Chocolate bar", "Food", false, 0.85, 1);

        List<CartItem> cartItems = new List<CartItem>()
        {
            ci1,
            ci2,
            ci3,
            ci4
        };

        Order order = new Order(cartItems);
        string receipt = order.GetReceipt();

        string expectedReceipt = $"Book: 24.98 (2 @ 12.49){Environment.NewLine}Music CD: 16.49{Environment.NewLine}Chocolate bar: 0.85{Environment.NewLine}Sales Taxes: 1.50{Environment.NewLine}Total: 42.32{Environment.NewLine}";

        Assert.AreEqual(receipt, expectedReceipt);
    }

    [Test]
    public void ReturnsExpectedReceipt_WhenExampleInput2()
    {
        CartItem ci1 = getCartItem("Imported box of chocolates", "Food", true, 10.00, 1);
        CartItem ci2 = getCartItem("Imported bottle of perfume", "Uncategorized", true, 47.50, 1);

        List<CartItem> cartItems = new List<CartItem>()
        {
            ci1,
            ci2
        };

        Order order = new Order(cartItems);
        string receipt = order.GetReceipt();

        string expectedReceipt = $"Imported box of chocolates: 10.50{Environment.NewLine}Imported bottle of perfume: 54.65{Environment.NewLine}Sales Taxes: 7.65{Environment.NewLine}Total: 65.15{Environment.NewLine}";

        Assert.AreEqual(receipt, expectedReceipt);
    }

    [Test]
    public void ReturnsExpectedReceipt_WhenExampleInput3()
    {
        CartItem ci1 = getCartItem("Imported bottle of perfume", "Uncategorized", true, 27.99, 1);
        CartItem ci2 = getCartItem("Bottle of perfume", "Uncategorized", false, 18.99, 1);
        CartItem ci3 = getCartItem("Packet of headache pills", "Medical", false, 9.75, 1);
        CartItem ci4 = getCartItem("Imported box of chocolates", "Food", true, 11.25, 1);
        CartItem ci5 = getCartItem("Imported box of chocolates", "Food", true, 11.25, 1);

        List<CartItem> cartItems = new List<CartItem>()
        {
            ci1,
            ci2,
            ci3,
            ci4,
            ci5
        };

        Order order = new Order(cartItems);
        string receipt = order.GetReceipt();

        string expectedReceipt = $"Imported bottle of perfume: 32.19{Environment.NewLine}Bottle of perfume: 20.89{Environment.NewLine}Packet of headache pills: 9.75{Environment.NewLine}Imported box of chocolates: 23.70 (2 @ 11.85){Environment.NewLine}Sales Taxes: 7.30{Environment.NewLine}Total: 86.53{Environment.NewLine}";

        Assert.AreEqual(receipt, expectedReceipt);
    }

    private CartItem getCartItem(string name, string category, bool isImported, double price, int quantity)
    {
        return new CartItem()
        {
            Name = name,
            Category = category,
            IsImported = isImported,
            Price = price,
            Quantity = quantity
        };
    }
}
