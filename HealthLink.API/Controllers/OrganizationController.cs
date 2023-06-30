using HealthLink.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly DatabaseContext context;

        public OrganizationController(DatabaseContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            return context.Organizations;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Organization> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed to the request");
            return Ok(context.Organizations.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult<Organization> Post([FromBody] Organization organization)
        {
            try
            {
                context.Organizations.Add(organization);
                context.SaveChanges();
                return Ok(organization);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Organization> Put([FromForm] Organization organization)
        {
            var temp = context.Organizations.Find(organization.Id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.Name = organization.Name;
                    temp.Address = organization.Address;
                    temp.Information = organization.Information;
                    temp.UserId = organization.UserId;
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
        public ActionResult<Organization> Delete(int id)
        {
            try
            {
                var temp = context.Organizations.Find(id);
                _ = context.Organizations.Remove(temp); //?
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
