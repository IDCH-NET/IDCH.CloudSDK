using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IDCH.StorageBrowser.Data;
using IDCH.StorageBrowser.Model;

namespace IDCH.StorageBrowser.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
      
        public string ReturnUrl { get; set; }
        public async Task<IActionResult>
            OnGetAsync(string access, string secret, string bucket)
        {
            var MemorySvc = AppConstants.DataSession;
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            bool isAuthenticate = false;
            var setting = new StorageSetting() { };
            setting.Bucket = bucket;
            setting.SecretKey = secret;
            setting.AccessKey = access;
            var uid = $"user-{Guid.NewGuid().ToString()}"; 
            if(StorageObjectService.TestSetting(setting))
            {
                MemorySvc.SetItem<StorageSetting>(uid, setting);
                isAuthenticate = true;
            }
            // In this example we just log the user in
            // (Always log the user in for this demo)
            if (isAuthenticate)
            {
                // *** !!! This is where you would validate the user !!! ***
                // In this example we just log the user in
                // (Always log the user in for this demo)
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, uid),
                new Claim(ClaimTypes.Role, "User"),
            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            if (!isAuthenticate) returnUrl = "/auth/login?result=false";
            return LocalRedirect(returnUrl);
        }
    }
}
