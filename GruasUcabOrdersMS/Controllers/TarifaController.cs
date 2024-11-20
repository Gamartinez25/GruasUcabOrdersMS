using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Querys;
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
        [HttpGet]
        public async Task<IActionResult> GetAllTarifas()
        {
            try
            {
                var query = new ListarTarifasQuery();
                var tarifas = await Mediator.Send(query);
                return Ok(tarifas);
            }
            catch (Exception e)
            {
                return StatusCode(500,"Hubo un error al procesar la busqueda");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartamento(Guid id, [FromBody] ListarTarifaDto tarifaDto)
        {
            try
            {
                var command = new ModificarTarifaCommand(tarifaDto,id);
                await Mediator.Send(command);
                return Ok("Modificación Exitosa");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ha ocurrido un error al realizar al realizar la modificación");
            }
        }

    }

}
