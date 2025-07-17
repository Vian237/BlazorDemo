using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        public List<PizzaSpecial> specials = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>($"{NavigationManager.BaseUri}api/specials");
                specials = result ?? new List<PizzaSpecial>(); // Handle possible null assignment
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching specials: {ex.Message}");
            }
        }
    }
}
