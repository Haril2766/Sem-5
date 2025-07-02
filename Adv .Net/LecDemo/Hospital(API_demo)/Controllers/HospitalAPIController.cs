using Hospital.Models;
using System.Text.Json;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAPIController : ControllerBase
    {
        private readonly HospitalManagementContext context;

        public HospitalAPIController(HospitalManagementContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<HospitalMaster>>> GetDetails()
        {
            var data = await context.HospitalMasters.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<HospitalMaster>>> GetDetailsById(int id)
        {
            var data = await context.HospitalMasters.FindAsync(id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<List<HospitalMaster>>> AddDetails(HospitalMaster hp)
        {
            await context.HospitalMasters.AddAsync(hp);
            await context.SaveChangesAsync();

            return Ok(hp);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<HospitalMaster>>> EditDetails(int id, HospitalMaster hp)
        {
            if (id != hp.HospitalId)
            {
                return BadRequest();
            }
            context.Entry(hp).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(hp);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<HospitalMaster>>> DeleteDetails(int id)
        {
            var data = await context.HospitalMasters.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            context.HospitalMasters.Remove(data);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Serialition")]
        public IActionResult SerData([FromBody] HospitalMaster hp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jsonOutput = JsonConvert.SerializeObject(hp, Newtonsoft.Json.Formatting.Indented);
            return Ok(new
            {
                message = "data received",
                jsonData = jsonOutput
            });
        }

        [HttpPost("Deserialize")]
        public IActionResult deserData([FromBody] string xp)
        {
            try
            {
                var Data = JsonConvert.DeserializeObject<HospitalMaster>(xp);
                return Ok(new
                {
                    message = "Json Convert",
                    data = Data
                });
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                });
            }
        }

    }
}

