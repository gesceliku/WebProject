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
    public class AppointmentController : ControllerBase
    {
        private AppointmentService _appointmentService;
        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("GetAppointmentByClient", Name = "GetAppoinmentByClient")]
        public async Task<IActionResult> GetAppointmentByClient(string clientUn)
        {
            var appointments = await _appointmentService.GetAppointmentByClientAsync(clientUn);

            var allAppointmentsDto = appointments.Select(appointment => new GetAppointmentDto
            {
                ClientName = appointment.clients.Fullname,
                DoctorName = appointment.doctors.name,
                time = appointment.time.ToString("HH:mm"),
                date = appointment.date.ToString("yyyy-MM-dd"),
                clinic = appointment.clinic,


            }).ToList();

            return Ok(allAppointmentsDto);
        }

        [HttpGet("GetAppointmentByDoctor", Name = "GetAppoinmentByDoctor")]
        public async Task<IActionResult> GetAppointmentByDoctor(string doctorUn)
        {
            var appointments = await _appointmentService.GetAppointmentByDoctorAsync(doctorUn);

            var allAppointmentsDto = appointments.Select(appointment => new GetAppointmentDto
            {
                ClientName = appointment.clients.Fullname,
                DoctorName = appointment.doctors.name,
                time = appointment.time.ToString("HH:mm"),
                date = appointment.date.ToString("yyyy-MM-dd"),
                clinic = appointment.clinic,


            }).ToList();

            return Ok(allAppointmentsDto);
        }

        [HttpPost("CreateAppointment", Name = "CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto payload)
        {

            
            var newAppointment = new Appointments
            {
                
                ClientUsername = payload.ClientUsername,
                DoctorUsername= payload.DoctorUsername,
                date= payload.date,
                time= payload.time,
                clinic=payload.clinic,
                
            };

           await _appointmentService.CreateAppointmentAsync(newAppointment);


            return Created();
        }
        [HttpDelete("DeleteAppointmentById", Name = "DeleteAppointmentById")]
        public async Task<IActionResult> DeleteAppointmentById(int appointmentId)
        {
            await _appointmentService.DeleteAppointmentByIdAsync(appointmentId);
            return Ok();
        }

    }
}
