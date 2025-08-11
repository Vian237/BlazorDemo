using BlazorDemo.Model;
using System.Threading.Tasks;

namespace BlazorDemo.Components.Pages
{
    public partial class MyOrders
    {
        public List<OrderWithStatus> OrdersWithStatus = new();

        protected override async Task OnParametersSetAsync()
        {
            OrdersWithStatus = await HttpClient.GetFromJsonAsync<List<OrderWithStatus>>($"{NavigationManager.BaseUri}api/orders");
        }
    }
}
