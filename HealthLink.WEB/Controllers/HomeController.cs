using HealthLink.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace HealthLink.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            Response response = new Response();
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.GetAsync(String.Concat("http://localhost:5004/token?username=", login, "&password=", password)))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    response = JsonConvert.DeserializeObject<Response>(result);
                }
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(45);

            HttpContext.Response.Cookies.Append("AccessToken", response.AccessToken, option);
            HttpContext.Response.Cookies.Append("Role", response.Role, option);
            HttpContext.Response.Cookies.Append("Username", response.Username, option);

            ViewBag.Username = HttpContext.Request.Cookies["Username"];

            if (response != null)
            {
                if(response.Role == "Admin")
                    return View("../Admin/Status");
                else if(response.Role == "Manager")
                    return View("../Manager/Status");
                else if(response.Role == "User")
                    return View("../User/Status");
                else
                    throw new Exception("Role not defined");
            }
            else
            {
                throw new Exception("Response is null");
            }
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string login, string password, string name, string gender, int age, string phone, string address)
        {
            User user = new User();
            user.Login = login;
            user.Password = password;
            user.Name = name;
            user.Gender = gender;
            user.Age = age;
            user.Phone = phone;
            user.Address = address;
            user.RoleId = 3;
            user.DateCreated = DateTime.Now;
            user.IsBlocked = false;  

            string document = JsonConvert.SerializeObject(user);

            HttpContent content = new StringContent(document);

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var request = client.PostAsync("http://localhost:5004/api/Account/PostUser", content))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();
                }
            }

            return View("Index");
        }
    }
}