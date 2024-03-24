using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtRepository _courtRepository;

        public CourtController(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Court>>> GetCourts()
        {
            return await _courtRepository.GetCourts();
        }


        [HttpGet("GetCourt/{id}")]
        public async Task<ActionResult<Court>> GetCourt(int id)
        {
            var court = await _courtRepository.GetCourtById(id);
            if (court == null)
            {
                NotFound();
            }
            return Ok(court);
        }

        [HttpPost]
        public async Task<ActionResult<Court>> CreateCourt(Court court)
        {
            if (court == null)
            {
                return BadRequest();
            }
            await _courtRepository.CreateCourt(court);
            return CreatedAtAction(nameof(GetCourt), new { id = court.Id }, court);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Court>> PutCourt(int id, Court court)
        {
            if (id != court.Id)
            {
                return BadRequest("O ID fornecido no corpo da solicitação não corresponde ao ID na URL.");
            }

            try
            {
                var existingCourt = _courtRepository.GetCourtById(id);
                if (existingCourt == null)
                {
                    return NotFound($"Quadra com o ID {id} não encontrado.");
                }

                await _courtRepository.EditCourt(id, court);
                return Ok($"{court.Name} atualizado com sucesso.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar o cliente: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCourt(int id)
        {
            var existingCourt = _courtRepository.GetCourtById(id);
            if (existingCourt == null)
                return BadRequest("Quadra nao encontrada");

            await _courtRepository.DeleteCourt(id);
            return Ok("Quadra deletada com sucesso");
        }

    }
}
