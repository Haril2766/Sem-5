using Hospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAPIController : ControllerBase
    {

        #region Constructor
        private readonly HospitalManagementContext context;

        public DoctorAPIController(HospitalManagementContext context) 
        {
            this.context = context;
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDetailsDoctor()
        {
            var data = await context.Doctors.ToListAsync();
            return Ok(data);
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Doctor>>> GetDetailsByIdDoctors(int id)
        {
            var data = await context.Doctors.FindAsync(id);
            return Ok(data);
        }
        #endregion

        #region Add

        [HttpPost]
        public async Task<ActionResult<List<Doctor>>> AddDoctors(Doctor hp)
        {
            await context.Doctors.AddAsync(hp);
            await context.SaveChangesAsync();

            return Ok(hp);
        }
        #endregion

        #region Edit
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Doctor>>> EditDoctor(int id, Doctor hp)
        {
            if (id != hp.DoctorId)
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
        public async Task<ActionResult<List<Doctor>>> DeleteDoctor(int id)
        {
            var data = await context.Doctors.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            context.Doctors.Remove(data);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
