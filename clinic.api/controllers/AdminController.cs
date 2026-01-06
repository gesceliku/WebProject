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
    public class AdminController : ControllerBase
    {
        private AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("GetAdminByUserName", Name = "GetAdminByUsername")]
        public async Task<IActionResult> GetAdminByUsername(string adminUn)
        {
            var admin = await _adminService.GetAdminByUsernameAsync(adminUn);
            var allAdminDto = new GetAdminDto()
            {
                username = admin.username,
                password = admin.password,
            };

            return Ok(admin);
        }

        [HttpPost("CreateAdmin", Name = "CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto payload)
        {

            
            var newAdmin = new Admin
            {
                username = payload.username
            };

            await _adminService.AddNewAdminAsync(newAdmin);


            return Created();
        }
    }
}
