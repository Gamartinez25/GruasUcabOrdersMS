using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.TarifaDtos;

namespace GruasUcabOrdersMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController:ControllerBase
    {
        private readonly IMediator Mediator;

        public TarifaController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTarifa([FromBody] CrearTarifaDto tarifaDto)
        {

            try
            {
                var command = new CrearTarifaCommand(tarifaDto);
                await Mediator.Send(command);
                return Ok("Registro Exitoso");
            }
            catch (Exception e)
            {
                return StatusCode(500,"Hubo un error al procesar el registro");
            }
        }
    }
   
}
