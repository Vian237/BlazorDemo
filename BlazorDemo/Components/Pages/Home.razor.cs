using BlazorDemo.Model;
using Microsoft.JSInterop;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        public List<PizzaSpecial> specials = new();
        public Order Order => OrderState.CurrentOrder;

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

        public async Task RemovePizzaConfirmation (Pizza removePizza)
        {
            var messageParams = new
            {
                title = "Remove Pizza",
                text = $"Are you sure you want to remove {removePizza.Special!.Name} from your order?",
                icon = "warning",
                buttons = new
                {
                    abort = new { text = "No, leave it in my order", value = false },
                    confirm = new { text = "Yes, remove pizza", value = true }
                },
                dangerMode = true
            };

            if (await JavaScript.InvokeAsync<bool>("swal", messageParams))
            {
                OrderState.RemoveConfiguredPizza(removePizza);
            }
        }
    }
}
