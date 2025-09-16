using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Shared
{
    public partial class ConfigurePizzaDialog
    {
        [Parameter]
        public Pizza Pizza { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }

        public int minSize = Pizza.MinSize;
        public int maxSize = Pizza.MaxSize;

        public bool supportSizing = true;

        protected override void OnInitialized()
        {
            if (Pizza is { Special.FixedSize: not null })
            {
                supportSizing = false;
                Pizza.Size = Pizza.Special.FixedSize.Value;
            }
            else if (Pizza.Size == 0)
            {
                Pizza.Size = Pizza.DefaultSize;
            }
        }
    }
}
