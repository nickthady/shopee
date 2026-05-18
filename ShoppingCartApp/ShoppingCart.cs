namespace ShoppingCartApp
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items;

        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }

        public void AddItem(string name, double unitPrice, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("A név nem lehet null vagy üres.");
            if (unitPrice <= 0)
                throw new ArgumentException("Az egységár csak pozitív szám lehet.");
            if (quantity < 1)
                throw new ArgumentException("A mennyiség legalább 1 kell legyen.");

            CartItem existing = _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                _items.Add(new CartItem(name, unitPrice, quantity));
            }
        }

        public bool RemoveItem(string name)
        {
            CartItem existing = _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (existing == null)
                return false;

            _items.Remove(existing);
            return true;
        }

        public int GetItemCount()
        {
            return _items.Count;
        }

        public decimal GetTotal()
        {
            return _items.Sum(i => (decimal)i.GetLineTotal());
        }

        public IReadOnlyList<CartItem> GetItems()
        {
            return _items.AsReadOnly();
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}