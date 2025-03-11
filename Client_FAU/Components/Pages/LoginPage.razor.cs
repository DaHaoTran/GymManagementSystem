using Microsoft.AspNetCore.Components;

namespace Client_FAU.Components.Pages
{
    public partial class LoginPage
    {
        [Inject]
        private IHttpContextAccessor? HttpContextAccessor {  get; set; }
    }
}
