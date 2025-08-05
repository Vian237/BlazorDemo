using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Checkout
    {
        public Order Order => OrderState.CurrentOrder;
    }
}
