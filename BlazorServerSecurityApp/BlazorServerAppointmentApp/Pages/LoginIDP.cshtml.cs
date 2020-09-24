using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerAppointmentApp.Pages
{
    public class LoginIDPModel : PageModel
    {
        public async Task OnGetAsync()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
            }
            else
            {
                // redirect to the root
                Response.Redirect(Url.Content("~/").ToString());
            }
        }
    }
}