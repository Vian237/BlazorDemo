using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Checkout
    {
        public Order Order => OrderState.CurrentOrder;
        public bool IsSubmitting { get; set; } = false;

        public async Task SubmitOrder()
        {
            try
            {
                IsSubmitting = true; // Set the submitting state to true

                var response = await HttpClient.PostAsJsonAsync($"{NavigationManager.BaseUri}api/orders", Order);
                var newOrderId = await response.Content.ReadFromJsonAsync<int>();

                //Reset the current order after submission
                OrderState.ResetOrder();

                //NavigationManager.NavigateTo($"myorders/{newOrderId}");
                NavigationManager.NavigateTo($"/myorders");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting order: {ex.Message}");
            }
            finally
            {
                IsSubmitting = false; // Reset the submitting state
            }
        }
    }
}
