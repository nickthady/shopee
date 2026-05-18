namespace ShoppingCartApp
{
    public class CartItem
    {
        public string Name { get; }
        public double UnitPrice { get; }
        public int Quantity { get; private set; }

        public CartItem(string name, double unitPrice, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("A név nem lehet null vagy üres.");
            if (unitPrice <= 0)
                throw new ArgumentException("Az egységár csak pozitív szám lehet.");
            if (quantity < 1)
                throw new ArgumentException("A mennyiség legalább 1 kell legyen.");

            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public double GetLineTotal()
        {
            return UnitPrice * Quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("A mennyiség legalább 1 kell legyen.");
            Quantity = quantity;
        }
    }
}