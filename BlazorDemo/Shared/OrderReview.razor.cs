using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Shared
{
    public partial class OrderReview
    {
        [Parameter]
        public Order Order { get; set; }
    }
}
