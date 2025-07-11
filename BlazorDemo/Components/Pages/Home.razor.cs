using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        List<PizzaSpecial> specials = new();

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
    }
}
