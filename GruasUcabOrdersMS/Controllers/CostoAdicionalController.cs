using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Querys;

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
                return StatusCode(500, "Ha ocurrido un error al procesar el registro");
            }
        }
        [HttpGet("{idOrden}")]
        public async Task<IActionResult> GetAllCostoAdicionales(Guid idOrden)
        {
            try
            {
                var query = new ListarCostoAdicionalPorOrdenQuery(idOrden);
                var costosAdicionales = await Mediator.Send(query);
                return Ok(costosAdicionales);
            }
            catch (Exception e)
            {
                return StatusCode(500,"Hubo un error al procesar la busqueda");
            }
        }
    }
}
