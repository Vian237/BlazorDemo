using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Shared
{
    public partial class ConfigurePizzaDialog
    {
        [Parameter]
        public Pizza Pizza { get; set; }

        public int minSize = Pizza.MinSize;
        public int maxSize = Pizza.MaxSize;
    }
}
