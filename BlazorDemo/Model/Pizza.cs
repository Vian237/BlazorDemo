namespace BlazorDemo.Model
{
    public class Pizza
    {
        public const int DefaultSize = 12;
        public const int MinSize = 9;
        public const int MaxSize = 17;

        public int Id { get; set; }
        public int OrderId { get; set; }
        public PizzaSpecial Special { get; set; }
        public int SpecialId { get; set; }
        public int Size { get; set; }
        public List<PizzaTopping> Toppings { get; set; }

        public decimal GetBasePrice()
        {
            return (decimal)Size / DefaultSize * Special.BasePrice;
        }

        public decimal GetTotalPrice()
        {
            return GetBasePrice();
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
