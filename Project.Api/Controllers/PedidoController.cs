using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Communication.Dto.Requests;
using Project.Domain.Entities;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("pedidos")]
        public async Task<IActionResult> GetPedidos()
        {
            var response = await _service.Consultar();

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("pedidos/{id}")]
        public async Task<IActionResult> GetPedidoId(Guid id)
        {
            var response = await _service.ConsultarPorId(id);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("pedidos")]
        public async Task<IActionResult> PostPedidos([FromBody] RequestPedidoRegisterJson pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Cadastrar(pedido);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("pedidos/{id}")]
        public async Task<IActionResult> DeletePedidos(Guid id)
        {
            var response = await _service.Deletar(id);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}