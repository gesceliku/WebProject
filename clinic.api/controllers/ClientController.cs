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
    public class ClientController : ControllerBase
    {
        private ClientsService _clientsService;
        public ClientController(ClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet("GetAllClients", Name = "GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var allClients = await _clientsService.GetAllClientsAsync();

            var allClientsDto = allClients.Select(n => new GetClientDto()
            {
                Username = n.username,
                Fullname = n.Fullname,
                Email=n.Email,
                Password=n.password,
            }).ToList();

            return Ok(allClientsDto);
        }

        [HttpGet("GetClientByUserName", Name = "GetClientByUsername")]
        public async Task<IActionResult> GetClientByUsername(string clientUn)
        {
            var client = await _clientsService.GetClientByUsernameAsync(clientUn);
            var allClientsDto = new GetClientDto()
            {
                Username = client.username,
                Fullname = client.Fullname,
                Email= client.Email,
                Password=client.password,
            };

            return Ok(client);
        }

        [HttpDelete("DeleteClientByUsername", Name = "DeleteClientByUsername")]
        public async Task<IActionResult> DeleteClientById(string clientUn)
        {
            await _clientsService.DeleteClientByUsernameAsync(clientUn);
            return Ok();
        }

        [HttpPut("UpdateClientByUsername", Name = "UpdateAirlineByUsername")]
        public async Task<IActionResult> UpdateClientByUsername(string clientUn, [FromBody] UpdateClientDto payload)
        {
            if (!clientUn.Equals(payload.Username))
                return BadRequest("The client username is not valid");

            var updatedClient = new Clients()
            {
                username = payload.Username,
                Fullname = payload.Fullname
            };

            await _clientsService.UpdateClientByUsernameAsync(clientUn, updatedClient);

            return Ok(updatedClient);
        }

        [HttpPost("CreateClient", Name = "CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto payload)
        {

            
            var newClient = new Clients
            {
                Fullname = payload.Fullname,
                username=payload.Username,
                Email=payload.Email,
                password=payload.Password,
            };

            await _clientsService.AddNewClientAsync(newClient);


            return Created();
        }


    }
}
