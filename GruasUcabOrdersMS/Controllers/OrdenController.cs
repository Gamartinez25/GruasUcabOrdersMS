using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Dtos.TarifaDtos;

namespace GruasUcabOrdersMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController:ControllerBase
    {

        private readonly IMediator Mediator;

        public OrdenController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrden([FromBody] CrearOrdenDto ordenDto)
        {

            try
            {
                var command = new CrearOrdenCommand(ordenDto);
                await Mediator.Send(command);
                return Ok("Registro Exitoso");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ha ocurrido un error al procesar el registro");
            }
        }
    }
}
