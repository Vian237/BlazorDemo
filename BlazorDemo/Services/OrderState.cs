using BlazorDemo.Model;

namespace BlazorDemo.Services
{
    public class OrderState
    {
        public bool ShowingConfigDialog { get; set; }
        public Pizza? ConfiguringPizza { get; set; }
        public Order CurrentOrder { get; set; } = new Order();

        public void ShowConfigPizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowingConfigDialog = true;
        }

        public void CancelConfigPizzaDialog()
        {
            ShowingConfigDialog = false;
            ConfiguringPizza = null;
        }

        public void ConfirmConfigPizzaDialog()
        {
            if (ConfiguringPizza != null) // Ensure ConfiguringPizza is not null
            {
                CurrentOrder.Pizzas.Add(ConfiguringPizza);
                ShowingConfigDialog = false;
                ConfiguringPizza = null;
            }
        }

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            if (CurrentOrder.Pizzas.Contains(pizza))
            {
                CurrentOrder.Pizzas.Remove(pizza);
            }
        }

        public void ResetOrder()
        {
            CurrentOrder = new Order();
        }
    }
}
