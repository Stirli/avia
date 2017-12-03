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

        /*
         * Явное преобразование из строки в Discount
         */
        public static explicit operator Discount(string str)
        {
            // Создаем регулярное выражение
            // ^(?<type>-)? указывает, что если в начале строки стоит '-' - помещаем его в группу type
            // (?<type>%)?$ - в конце строки символ %
            // на границы строки здесь указывают символы ^ $
            // число будет помещено в группу value
            Regex reg = new Regex(@"^(?<type>-)?(?<value>\d+)(?<type>%)?$");
            // парсим строку регулярным выражением
            Match match = reg.Match(str);

            DiscountType discountType;
            // если в группу type попал -
            if (match.Groups["type"].Value.Equals("-"))
            {
                // скидка статическая
                discountType = DiscountType.Static;
            }
            // если %
            else if (match.Groups["type"].Value.Equals("%"))
            {
                // процентная
                discountType = DiscountType.Procent;
            }
            // если что-то не так
            else throw new FormatException("Неверный тип скидки");
            // Извлекаем значение в виде строки и парсим его
            double value = double.Parse(match.Groups["value"].Value);
            // Возвращаем скидку
            return new Discount(value, discountType);
        }

        /*
         * Преобразовываем в строку
         * InvalidOperationException
         */
        public static implicit operator string(Discount discount)
        {
            // В зависимости от типа скидки делаем разные строки
            switch (discount.DiscountType)
            {
                case DiscountType.Procent:
                    return discount.Value + "%";
                case DiscountType.Static:
                    return "-" + discount.Value;
                    // на случай, если будет добавлено
                default: throw new InvalidOperationException();
            }
        }
        
        public override string ToString()
        {
            return this;
        }

        // скидка задана в процентах, другие имеют фиксированную скидку.
        public double Value { get; set; }
        // Тип скидки
        public DiscountType DiscountType { get; set; }

    }
    // тип скидки
    public enum DiscountType
    {
        Static,
        Procent
    }
}