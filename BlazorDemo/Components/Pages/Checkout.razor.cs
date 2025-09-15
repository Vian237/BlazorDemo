using BlazorDemo.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorDemo.Components.Pages
{
    public partial class Checkout
    {
        public Order Order => OrderState.CurrentOrder;
        public bool IsSubmitting { get; set; } = false;
        public bool isError = false;

        private async Task CheckSubmission(EditContext editContext)
        {
            IsSubmitting = true;
            var model = editContext.Model as Address;
            isError = string.IsNullOrWhiteSpace(model?.Name) ||
                      string.IsNullOrWhiteSpace(model?.Street) ||
                      string.IsNullOrWhiteSpace(model?.ZipCode);
            if (!isError)
            {
                await SubmitOrder();
            }
            IsSubmitting = false;
        }

        protected void ShowError()
        {
            isError = true;
        }

        public async Task SubmitOrder()
        {
            try
            {
                isError = false;
                IsSubmitting = true;

                var response = await HttpClient.PostAsJsonAsync($"{NavigationManager.BaseUri}api/orders", Order);
                var newOrderId = await response.Content.ReadFromJsonAsync<int>();

                //Reset the current order after submission
                OrderState.ResetOrder();

                NavigationManager.NavigateTo($"myorders/{newOrderId}");
                //NavigationManager.NavigateTo($"/myorders");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting order: {ex.Message}");
            }
        }
    }
}
