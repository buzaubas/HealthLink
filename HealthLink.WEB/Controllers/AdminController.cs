using HealthLink.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace HealthLink.WEB.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Status()
        {
            return View();
        }

        public async Task<IActionResult> UnmakeAdmins()
        {
            List<User> admins = new List<User>();
            //List<User> users = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetAdmins"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    admins = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> UnmakeAdmins(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/UnmakeAdmin/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> admins = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetAdmins"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    admins = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(admins);
        }

        public async Task<IActionResult> MakeAdmins()
        {
            List<User> users = new List<User>();
            //List<User> users = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetUsersAndManagers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmins(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/MakeAdmin/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetUsersAndManagers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        public async Task<IActionResult> UnmakeManagers()
        {
            List<User> managers = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetManagers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    managers = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(managers);
        }

        [HttpPost]
        public async Task<IActionResult> UnmakeManagers(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/UnmakeManager/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> managers = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetManagers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    managers = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(managers);
        }

        public async Task<IActionResult> MakeManagers()
        {
            List<User> users = new List<User>();
            //List<User> users = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetUsers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> MakeManagers(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/MakeManager/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetUsers"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        public async Task<IActionResult> Block()
        {
            List<User> users = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetNotBlocked"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/Block/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetNotBlocked"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        public async Task<IActionResult> Unblock()
        {
            List<User> users = new List<User>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetBlocked"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int id)
        {
            var token = HttpContext.Request.Cookies["AccessToken"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/Unblock/" + id))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Account/GetBlocked"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
            }
            return View(users);
        }

        public async Task<IActionResult> Organizations()
        {
            List<Organization> users = new List<Organization>();
            var token = HttpContext.Request.Cookies["AccessToken"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync("http://localhost:5004/api/Organization"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<Organization>>(result);
                }
            }
            return View(users);
        }
    }
}
