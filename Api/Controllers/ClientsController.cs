using Api.Interfaces;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        [HttpGet("GetById/{id}")]
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
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest("O ID fornecido no corpo da solicitação não corresponde ao ID na URL.");
            }

            try
            {
                var existingClient = await _clientRepository.GetClientById(id);
                if (existingClient == null)
                {
                    return NotFound($"Cliente com o ID {id} não encontrado.");
                }

                await _clientRepository.EditClient(id, client);
                return Ok("Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar o cliente: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteClient(int id) 
        {
            var existingClient = await _clientRepository.GetClientById(id);
            if (existingClient == null)
            {
                return NotFound();
            }
            await _clientRepository.DeleteClient(id);
            return Ok("Cliente excluído com sucesso");
        }
    }
}
