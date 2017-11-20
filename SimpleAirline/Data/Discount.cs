namespace SimpleAirline
{
    public class Discount
    {
        public Discount(double value, DiscountType discountType)
        {
            Value = value;
            DiscountType = discountType;
        }

        // скидка задана в процентах, другие имеют фиксированную скидку.
        public double Value { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}