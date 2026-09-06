using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Services.Funcionario;
using Project.Communication.Dto.Requests;
using Project.Domain.Entities;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _service;

        public FuncionarioController(IFuncionarioService service)
        {
            _service = service;
        }

        [HttpGet("funcionarios")]
        public async Task<IActionResult> GetFuncionarios()
        {
            var response = await _service.Consultar();

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("funcionarios/tarefas")]
        public async Task<IActionResult> GetTarefas(Guid id)
        {
            var response = await _service.ConsultarTarefas(id);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("funcionarios/{id}")]
        public async Task<IActionResult> GetFuncionarioId(Guid id)
        {
            var response = await _service.ConsultarPorId(id);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("funcionarios")]
        public async Task<IActionResult> PostFuncionario([FromBody] RequestFuncionarioRegisterJson funcionario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Cadastrar(funcionario);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("funcionarios/{id}")]
        public async Task<IActionResult> DeleteFuncionario(Guid id)
        {
            var response = await _service.Deletar(id);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}