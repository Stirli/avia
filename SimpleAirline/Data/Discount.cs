using System;
using System.Text.RegularExpressions;

namespace SimpleAirline
{
    /*
     * Представляет 
     */
    public class Discount
    {
        public Discount(double value, DiscountType discountType)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", value, "Значение скидки не может быть меньше 0");
            }

            if (discountType == DiscountType.Procent && value > 100)
            {
                throw new ArgumentOutOfRangeException("value", value, "Значение скидки не может быть больше 100");
            }
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

        public override string ToString()
        {
            return this;
        }

        // скидка задана в процентах, другие имеют фиксированную скидку.
        public double Value { get; set; }
        public DiscountType DiscountType { get; set; }

    }
    public enum DiscountType
    {
        Static,
        Procent
    }
}