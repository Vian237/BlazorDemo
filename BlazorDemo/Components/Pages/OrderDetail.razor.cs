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
        public bool IsOrderIncomplete => orderWithStatus is null || orderWithStatus.IsDelivered == false;
        public PeriodicTimer timer = new(TimeSpan.FromSeconds(3));

        protected override async Task OnParametersSetAsync()
        {
            await GetLastestOrderStatusUpdatesAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _ = StartPollingTimerAsync(); // Start the polling timer without awaiting it
            }
        }

        private async Task GetLastestOrderStatusUpdatesAsync()
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

        private async Task StartPollingTimerAsync()
        {
            while (IsOrderIncomplete && await timer.WaitForNextTickAsync())
            {
                await GetLastestOrderStatusUpdatesAsync();
                StateHasChanged(); // Trigger UI update
            }
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
