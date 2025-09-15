using BlazorDemo.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Shared
{
    public partial class AddressEditor
    {
        [Parameter]
        public Address? Address { get; set; }

        private ElementReference startName;

        /// <summary>
        /// Set focus to the first input element when the component is rendered
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await startName.FocusAsync();
            }
        }
    }
}
