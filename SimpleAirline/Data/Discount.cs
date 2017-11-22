using System;
using System.Text.RegularExpressions;

namespace SimpleAirline
{
    public class Discount
    {
        public Discount(double value, DiscountType discountType)
        {
            Value = value;
            DiscountType = discountType;
        }

        public static explicit operator Discount(string str)
        {
            Regex reg = new Regex(@"^(?<type>-)?(?<value>\d+)(?<type>%)?$");
            Match match = reg.Match(str);

            DiscountType discountType = match.Groups["type"].Value.Equals("-") ? DiscountType.Static :
                match.Groups["type"].Value.Equals("%") ? DiscountType.Procent
                    : throw new FormatException("Неверный тип скидки");
            return new Discount(double.Parse(match.Groups["value"].Value), discountType);
        }

        public static implicit operator string(Discount discount)
        {
            switch (discount.DiscountType)
            {
                case DiscountType.Procent:
                    return discount.Value + "%";
                case DiscountType.Static:
                    return "-" + discount.Value;
                default: throw new InvalidOperationException();
            }
        }

        // скидка задана в процентах, другие имеют фиксированную скидку.
        public double Value { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}