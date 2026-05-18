using ShoppingCartApp;

namespace ShoppingCartAppTests
{
    [TestClass]
    public class CartItemTests
    {
        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var item = new CartItem("Apple", 1.50, 3);
            Assert.AreEqual("Apple", item.Name);
            Assert.AreEqual(1.50, item.UnitPrice);
            Assert.AreEqual(3, item.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullName_ThrowsArgumentException()
        {
            var item = new CartItem(null, 1.50, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            var item = new CartItem("", 1.50, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ZeroUnitPrice_ThrowsArgumentException()
        {
            var item = new CartItem("Apple", 0, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NegativeUnitPrice_ThrowsArgumentException()
        {
            var item = new CartItem("Apple", -1.00, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ZeroQuantity_ThrowsArgumentException()
        {
            var item = new CartItem("Apple", 1.50, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NegativeQuantity_ThrowsArgumentException()
        {
            var item = new CartItem("Apple", 1.50, -1);
        }

        [TestMethod]
        public void GetTotal_MultipleQuantity()
        {
            var item = new CartItem("Banana", 0.75, 4);
            Assert.AreEqual(3.00, item.GetLineTotal());
        }

        [TestMethod]
        public void UpdateQuantity_ValidValue()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(5);
            Assert.AreEqual(5, item.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateQuantity_ZeroValue_ThrowsArgumentException()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateQuantity_NegativeValue_ThrowsArgumentException()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(-3);
        }
    }

    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCart CreateCartWithItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);
            cart.AddItem("Bread", 2.50, 1);
            return cart;
        }

        [TestMethod]
        public void AddItem_NewItem()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_DuplicateName_IncreasesQuantity()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            cart.AddItem("Apple", 1.00, 3);
            Assert.AreEqual(1, cart.GetItemCount());
            Assert.AreEqual(5, cart.GetItems()[0].Quantity);
        }

        [TestMethod]
        public void AddItem_DuplicateName_CaseInsensitive()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            cart.AddItem("apple", 1.00, 1);
            Assert.AreEqual(1, cart.GetItemCount());
            Assert.AreEqual(3, cart.GetItems()[0].Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItem_InvalidName_ThrowsArgumentException()
        {
            var cart = new ShoppingCart();
            cart.AddItem("", 1.00, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItem_InvalidPrice_ThrowsArgumentException()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", -1.00, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItem_InvalidQuantity_ThrowsArgumentException()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 0);
        }

        [TestMethod]
        public void RemoveItem_ExistingItem()
        {
            var cart = CreateCartWithItems();
            bool result = cart.RemoveItem("Apple");
            Assert.IsTrue(result);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void RemoveItem_NonExistingItem_ReturnsFalse()
        {
            var cart = CreateCartWithItems();
            bool result = cart.RemoveItem("Milk");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveItem_CaseInsensitive()
        {
            var cart = CreateCartWithItems();
            bool result = cart.RemoveItem("apple");
            Assert.IsTrue(result);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void GetTotal_MultipleItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);
            cart.AddItem("Bread", 2.50, 2);
            Assert.AreEqual(8.00m, cart.GetTotal());
        }

        [TestMethod]
        public void GetTotal_EmptyCart_ReturnsZero()
        {
            var cart = new ShoppingCart();
            Assert.AreEqual(0m, cart.GetTotal());
        }

        [TestMethod]
        public void GetTotal_AfterRemove_IsCorrect()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);
            cart.AddItem("Bread", 2.50, 2);
            cart.RemoveItem("Apple");
            Assert.AreEqual(5.00m, cart.GetTotal());
        }

        [TestMethod]
        public void Clear_CartWithItems()
        {
            var cart = CreateCartWithItems();
            cart.Clear();
            Assert.AreEqual(0, cart.GetItemCount());
            Assert.AreEqual(0m, cart.GetTotal());
        }

        [TestMethod]
        public void Clear_EmptyCart_DoesNotThrow()
        {
            var cart = new ShoppingCart();
            cart.Clear();
            Assert.AreEqual(0, cart.GetItemCount());
        }
    }

    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void ApplyPercentage_TenPercent()
        {
            var discount = new Discount();
            Assert.AreEqual(180, discount.ApplyPercentage(200, 10));
        }

        [TestMethod]
        public void ApplyPercentage_ZeroPercent_ReturnsUnchanged()
        {
            var discount = new Discount();
            Assert.AreEqual(200, discount.ApplyPercentage(200, 0));
        }

        [TestMethod]
        public void ApplyPercentage_HundredPercent_ReturnsZero()
        {
            var discount = new Discount();
            Assert.AreEqual(0, discount.ApplyPercentage(200, 100));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApplyPercentage_OverHundred_ThrowsArgumentException()
        {
            var discount = new Discount();
            discount.ApplyPercentage(200, 110);
        }

        [TestMethod]
        public void ApplyFixed_AmountLessThanTotal()
        {
            var discount = new Discount();
            Assert.AreEqual(75, discount.ApplyFixed(100, 25));
        }

        [TestMethod]
        public void ApplyFixed_DiscountGreaterThanTotal_ReturnsZero()
        {
            var discount = new Discount();
            Assert.AreEqual(0, discount.ApplyFixed(50, 100));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApplyFixed_NegativeDiscount_ThrowsArgumentException()
        {
            var discount = new Discount();
            discount.ApplyFixed(100, -10);
        }

        [TestMethod]
        public void IsValid_PositiveValue()
        {
            var discount = new Discount();
            Assert.IsTrue(discount.IsValid(15));
        }

        [TestMethod]
        public void IsValid_Zero_ReturnsFalse()
        {
            var discount = new Discount();
            Assert.IsFalse(discount.IsValid(0));
        }

        [TestMethod]
        public void IsValid_Negative_ReturnsFalse()
        {
            var discount = new Discount();
            Assert.IsFalse(discount.IsValid(-5));
        }
    }
}