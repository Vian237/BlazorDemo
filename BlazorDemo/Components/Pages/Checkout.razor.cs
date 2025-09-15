using BlazorDemo.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorDemo.Components.Pages
{
    public partial class Checkout
    {
        public Order Order => OrderState.CurrentOrder;
        public bool isError = true;

        private EditContext editContext;

        protected override void OnInitialized()
        {
            editContext = new EditContext(Order.DeliveryAddress);
            editContext.OnFieldChanged += HandleFieldChanged;
        }

        private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            isError = !editContext.Validate();
            StateHasChanged(); // Trigger UI update
        }

        protected void ShowError()
        {
            isError = true;
        }

        public async Task SubmitOrder()
        {
            try
            {
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

        public void Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
        }

        private async Task CheckSubmission(EditContext editContext)
        {
            var model = editContext.Model as Address;
            isError = string.IsNullOrWhiteSpace(model?.Name) ||
                      string.IsNullOrWhiteSpace(model?.Street) ||
                      string.IsNullOrWhiteSpace(model?.ZipCode);
            if (!isError)
            {
                await SubmitOrder();
            }
        }
    }
}
