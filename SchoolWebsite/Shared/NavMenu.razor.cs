using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
namespace SchoolWebsite.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public void SetCulture(string culture)
        {
            if (culture != CultureInfo.CurrentCulture.Name)
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                var js = (IJSInProcessRuntime)JSRuntime;
                js.InvokeVoid("blazorCulture.set", culture);
                NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
            }
        }
    }
}
