using HealthLink.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HealthLink.WEB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Status()
        {
            ViewBag.Username = HttpContext.Request.Cookies["Username"];

            return View();
        }

        public async Task<IActionResult> Attachments()
        {
            Organization organization = new Organization();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync(String.Concat("http://localhost:5004/api/Account/GetAttachment")))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    organization = JsonConvert.DeserializeObject<Organization>(result); 
                }
            }

            return View(organization);
        }
    }
}
