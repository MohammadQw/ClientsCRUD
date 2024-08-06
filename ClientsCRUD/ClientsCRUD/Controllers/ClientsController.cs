using Microsoft.AspNetCore.Mvc;

using ClientsCRUD.DTOs;
using ClientsCRUD.Services;
namespace ClientsCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Retrieve Client By Search and filter
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients([FromQuery] ClientQueryParameters queryParameters)
        {
            var clients = await _clientService.GetClientsAsync(queryParameters);
            return Ok(clients);
        }

        /// <summary>
        ///  Create Client 
        /// </summary>
        /// <param name="createClientDto"></param>
        /// <returns></returns>
        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _clientService.CreateClientAsync(createClientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Retrieve  Client By ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetClientById/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Update Client details by id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateClientDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateClient/{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientDto updateClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _clientService.UpdateClientAsync(id, updateClientDto);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete Client by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
