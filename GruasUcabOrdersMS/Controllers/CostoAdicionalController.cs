using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;

namespace GruasUcabOrdersMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostoAdicionalController : ControllerBase
    {
        private readonly IMediator Mediator;
        public CostoAdicionalController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCostoAdicional([FromBody] RegistrarCostoAdicionalDto costoAdicionalDto)
        {
            try
            {
                var command = new RegistrarCostoAdicionalCommand(costoAdicionalDto);
                await Mediator.Send(command);
                return Ok("Registro Exitoso");
            }
            catch (Exception e)
            {
                return StatusCode(500,"Ha ocurrido un error al procesar el registro");
            }
        }
    }
}
