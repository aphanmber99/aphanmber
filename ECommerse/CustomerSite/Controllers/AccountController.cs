using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn(){
            return Challenge(new AuthenticationProperties  { RedirectUri = "/" }, "oidc");
        }
        public IActionResult SignOut(){
            return Challenge(new AuthenticationProperties  { RedirectUri = "/" }, "Cookies","oidc");
        }
        [Authorize]
        public ActionResult MyProfile(){
            return View();
        }
    }
}