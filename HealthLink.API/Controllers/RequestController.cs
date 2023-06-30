using HealthLink.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : Controller
    {
        private readonly DatabaseContext context;

        public RequestController(DatabaseContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Request> Get()
        {
            return context.Requests;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Request> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed to the request");
            return Ok(context.Requests.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult<Request> Post([FromBody] Request request)
        {
            try
            {
                context.Requests.Add(request);
                context.SaveChanges();
                return Ok(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Request> Put([FromForm] Request request)
        {
            var temp = context.Requests.Find(request.Id);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.UserId = request.UserId;
                    temp.OrganizationId = request.OrganizationId;
                    temp.StatusId = request.StatusId;
                    temp.DateCreated = request.DateCreated;
                    temp.DateProcessed = request.DateProcessed;
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
        public ActionResult<Request> Delete(int id)
        {
            try
            {
                var temp = context.Requests.Find(id);
                _ = context.Requests.Remove(temp); //?
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
