using DentalClinic.Data;
using DentalClinic.Dtos.Requests;
using DentalClinic.Dtos.Responses;
using DentalClinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private DoctorsService _doctorsService;
        public DoctorController(DoctorsService doctorService)
        {
            _doctorsService = doctorService;
        }

        [HttpGet("GetAllDoctors", Name = "GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var allDoctors =  await _doctorsService.GetAllDoctorsAsync();

            var allDoctorsDto = allDoctors.Select(n => new GetDoctorDto()
            {
                Username = n.username,
                name = n.name,
                clinic = n.clinic,
                password = n.password,
            }).ToList();

            return Ok(allDoctorsDto);
        }

        [HttpGet("GetDoctorByUsername", Name = "GetDoctorByUsername")]
        public async Task<IActionResult> GetDoctorByUsername(string doctorUn)
        {
            var doctor = await _doctorsService.GetDoctorByUsernameAsync(doctorUn);
            var allDoctorsDto = new GetDoctorDto()
            {
                Username = doctor.username,
                name = doctor.name,
            };

            return Ok(doctor);
        }

        [HttpGet("GetDoctorByClinic", Name = "GetDoctorByClinic")]
        public async Task<IActionResult> GetDoctorsByClinic(string clinic)
        {
            var doctors = await _doctorsService.GetDoctorsByClinicAsync(clinic);
           
            var allDoctorsDto =doctors.Select(doctors=> new GetDoctorDto
            {
                Username = doctors.username,
                name = doctors.name,
            }).ToList();

            return Ok(doctors);
        }

        [HttpDelete("DeleteDoctorByUsername", Name = "DeleteDoctorByUsername")]
        public async Task<IActionResult> DeleteDoctorByUsername(string doctorUn)
        {
            await _doctorsService.DeleteDoctorByUsernameAsync(doctorUn);
            return Ok();
        }

        [HttpPut("UpdateDoctorByUsername", Name = "UpdateDoctorByUsername")]
        public async Task<IActionResult> UpdateDoctorByUsername(string doctorUn, [FromBody] UpdateDoctorDto payload)
        {
            if (!doctorUn.Equals(payload.username))
                return BadRequest("The doctor username is not valid");

            var updatedDoctor = new Doctors()
            {
                username = payload.username,
                name = payload.name
            };

            await _doctorsService.UpdateDoctorByUsernameAsync(doctorUn, updatedDoctor);

            return Ok(updatedDoctor);
        }

        [HttpPost("CreateDoctor", Name = "CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto payload)
        {

            
            var newDoctor = new Doctors
            {
                name = payload.name
            };

            await _doctorsService.AddNewDoctorAsync(newDoctor);


            return Created();
        }


    }
}
