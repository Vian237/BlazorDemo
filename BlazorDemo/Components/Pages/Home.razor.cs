using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        public List<PizzaSpecial> specials = new();
        public Pizza configuringPizza;
        public bool showConfigurePizzaDialog = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>($"{NavigationManager.BaseUri}api/specials");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching specials: {ex.Message}");
            }
        }

        public void ShowConfigurePizza(PizzaSpecial special)
        {
            Console.WriteLine($"{special.Name}");
            configuringPizza = new Pizza
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize
            };
            showConfigurePizzaDialog = true;
            StateHasChanged();
        }
    }
}
