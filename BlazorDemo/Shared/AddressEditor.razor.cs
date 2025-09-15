using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Shared
{
    public partial class AddressEditor
    {
        [Parameter]
        public Address? Address { get; set; }
    }
}
