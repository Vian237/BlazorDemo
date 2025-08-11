using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Components.Pages
{
    public partial class OrderDetail
    {
        [Parameter]
        public int OrderId { get; set; }
        public OrderWithStatus orderWithStatus;
        public bool invalidOrder = false;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                orderWithStatus = await HttpClient.GetFromJsonAsync<OrderWithStatus>($"{NavigationManager.BaseUri}api/orders/{OrderId}");
            }
            catch (Exception ex)
            {
                invalidOrder = true; // Set invalidOrder to true if an error occurs
                Console.WriteLine($"Error fetching order details: {ex.Message}");
            }
        }
    }
}
