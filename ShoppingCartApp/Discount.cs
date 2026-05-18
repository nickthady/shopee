namespace ShoppingCartApp
{
    public class Discount
    {
        public double ApplyPercentage(double total, double percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException("A százalék csak 0 és 100 közé eshet.");

            return total * (1 - percent / 100);
        }

        public double ApplyFixed(double total, double discountAmount)
        {
            if (discountAmount < 0)
                throw new ArgumentException("A kedvezmény nem lehet negatív.");

            double result = total - discountAmount;
            return result < 0 ? 0 : result;
        }

        public bool IsValid(double amount)
        {
            return amount > 0;
        }
    }
}