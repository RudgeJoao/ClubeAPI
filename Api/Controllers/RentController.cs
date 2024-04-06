using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController : ControllerBase
{
    private readonly IRentRepository _rentRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICourtRepository _courtRepository;


    public RentController(IRentRepository rentRepository, IClientRepository clientRepository, ICourtRepository courtRepository)
    {
        _rentRepository = rentRepository;
        _clientRepository = clientRepository;
        _courtRepository = courtRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rent>>> GetRents()
    {
        var rents = await _rentRepository.GetRents();
        return Ok(rents);
    }
    [HttpGet("GetRent/{id}")]
    public async Task<ActionResult<Rent>> GetRentById(int id)
    {
        var rent = await _rentRepository.GetRentById(id);
        return Ok(rent);
    }
    [HttpPost]
    public async Task<ActionResult<Rent>> CreateRent(Rent rent, int courtId, int clientId) 
    {
        rent.Client = await _clientRepository.GetClientById(clientId);
        rent.Court = await _courtRepository.GetCourtById(courtId);

        if (rent.Client == null)
        {
            return NotFound($"Cliente com o ID {clientId} não encontrado.");
        }

        if (rent.Court == null)
        {
            return NotFound($"Quadra com o ID {courtId} não encontrada.");
        }
        
        await _rentRepository.CreateRent(rent);
        return CreatedAtAction(nameof(GetRentById), new { id = rent.Id }, rent);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Rent>> PutRent(int id, Rent rent)
    {
        if (id != rent.Id)
        {
            return BadRequest("O ID fornecido no corpo da solicitação não corresponde ao ID na URL.");
        }

        try
        {
            var existingRent = _rentRepository.GetRentById(id);
            if (existingRent == null)
            {
                return BadRequest($"Locação com o {id} não encontrado");
            }

            await _rentRepository.EditRent(id, rent);
            return Ok($"{rent.Id} Aluguel alterado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar a locação: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteRent(int id)
    {
        var existingRent = _rentRepository.GetRentById(id);
        if (existingRent == null)
        {
            return BadRequest($"Locação com o {id} não encontrado");
        }

        await _rentRepository.DeleteRent(id);
        return Ok("Locação excluída");
    }

}