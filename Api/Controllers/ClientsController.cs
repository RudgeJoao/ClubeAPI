using Api.Interfaces;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            return await _clientRepository.GetClients();
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientRepository.GetClientById(id);

            //return client != null ? Ok(client) : NotFound();
            return client is not null ? Ok(client) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            await _clientRepository.CreateClient(client);
            return CreatedAtAction(nameof(client), client);
        }
    }
}
