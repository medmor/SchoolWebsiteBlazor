using System.Globalization;

namespace SchoolWebsite.Shared
{
    public partial class MainLayout
    {
        public string dir = "rtl";

        protected override async Task OnInitializedAsync()
        {
            if (CultureInfo.CurrentCulture.Name == "ar-AR")
            {
                dir = "rtl";
            }
            else
            {
                dir = "ltr";
            }
        }
    }
}
