using Hospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAPIController : ControllerBase
    {


        #region Constructor
        private readonly HospitalManagementContext context;

        public PatientAPIController(HospitalManagementContext context)
        {
            this.context = context;
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetDetailsPatient()
        {
            var data = await context.Patients.ToListAsync();
            return Ok(data);
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Patient>>> GetDetailsByIdPatients(int id)
        {
            var data = await context.Patients.FindAsync(id);
            return Ok(data);
        }
        #endregion

        #region Add

        [HttpPost]
        public async Task<ActionResult<List<Patient>>> AddPatients(Patient hp)
        {
            await context.Patients.AddAsync(hp);
            await context.SaveChangesAsync();

            return Ok(hp);
        }
        #endregion

        #region Edit
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Patient>>> EditPatient(int id, Patient hp)
        {
            if (id != hp.PatientId)
            {
                return BadRequest();
            }
            context.Entry(hp).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(hp);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Patient>>> DeletePatient(int id)
        {
            var data = await context.Patients.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            context.Patients.Remove(data);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Success
        [HttpGet("success")]
        public IActionResult GetSuccess()
        {

            return Ok(new { Message = "Api is working fine!" });
        
        }
        #endregion

        #region Failed

        [HttpGet("Fail")]
        public IActionResult GetFailure()
        {
            throw new Exception("This is a test exception.");
        }
        #endregion
    }
}
