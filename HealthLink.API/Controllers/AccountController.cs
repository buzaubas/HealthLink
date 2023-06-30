using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using HealthLink.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;

namespace HealthLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly DatabaseContext context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(DatabaseContext _context, ILogger<AccountController> logger)
        {
            context = _context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        [HttpGet("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                AccessToken = encodedJwt,
                Username = identity.Name,
                Role = identity.RoleClaimType.ToString(),
            };
            return Json(response);
        }

        [HttpGet]
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            _logger.LogInformation("Username: {username}", username);

            User person = context.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            string role = "";

            if (person != null)
            {
                if (person.RoleId == 1)
                    role = "Admin";
                else if (person.RoleId == 2)
                    role = "Manager";
                else if (person.RoleId == 3)
                    role = "User";

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, role);
                return claimsIdentity;
            }

            return null;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            _logger.LogInformation("Username: {id}", id);
            if (id == 0)
                return BadRequest("Value must be passed to the request");
            return Ok(context.Users.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<User> Put([FromForm] User user)
        {
            var temp = context.Users.Find(user.Id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.Login = user.Login;
                    temp.Password = user.Password;
                    temp.Name = user.Name;
                    temp.Gender = user.Gender;
                    temp.Age = user.Age;
                    temp.Phone = user.Phone;
                    temp.Address = user.Address;
                    temp.RoleId = user.RoleId;
                    temp.DateCreated = user.DateCreated;
                    temp.IsBlocked = user.IsBlocked;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                var temp = context.Users.Find(id);
                _ = context.Users.Remove(temp); //?
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Specific Methods

        [Authorize]
        [HttpGet]
        [Route("GetAdmins")]
        public IEnumerable<User> GetAdmins()
        {
            return context.Users.Where(x => x.RoleId == 1);
        }

        [Authorize]
        [HttpGet]
        [Route("GetManagers")]
        public IEnumerable<User> GetManagers()
        {
            return context.Users.Where(x => x.RoleId == 2);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return context.Users.Where(x => x.RoleId == 3);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsersAndManagers")]
        public IEnumerable<User> GetUsersAndManagers()
        {
            return context.Users.Where(x => x.RoleId == 3 || x.RoleId == 2);
        }

        [Authorize]
        [HttpGet]
        [Route("UnmakeAdmin/{id}")]
        public ActionResult<User> UnmakeAdmin(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.RoleId = 3;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("MakeAdmin/{id}")]
        public ActionResult<User> MakeAdmin(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.RoleId = 1;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("UnmakeManager/{id}")]
        public ActionResult<User> UnmakeManager(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.RoleId = 3;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("MakeManager/{id}")]
        public ActionResult<User> MakeManager(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.RoleId = 2;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Block/{id}")]
        public ActionResult<User> Block(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.IsBlocked = true;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Unblock/{id}")]
        public ActionResult<User> Unblock(int id)
        {
            var temp = context.Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.IsBlocked = false;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetBlocked")]
        public IEnumerable<User> GetBlocked()
        {
            return context.Users.Where(x => x.IsBlocked == true);
        }

        [Authorize]
        [HttpGet]
        [Route("GetNotBlocked")]
        public IEnumerable<User> GetNotBlocked()
        {
            return context.Users.Where(x => x.IsBlocked == false);
        }

        [HttpPost]
        [Route("PostUser")]
        public ActionResult<User> PostUser(string json)
        {
            var user = JsonConvert.DeserializeObject<User>(json);
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAttachment")]
        public ActionResult<Organization> GetAttachment()
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Login == User.Identity.Name);    
                var attachment = context.Requests.FirstOrDefault(x => x.UserId == user.Id && x.StatusId == 2);
                Organization organization = context.Organizations.FirstOrDefault(x => x.Id == attachment.OrganizationId);
                if (attachment == null)
                {
                    return null;    
                }
                else
                {
                    return Ok(organization);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
