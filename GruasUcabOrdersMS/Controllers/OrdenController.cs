using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Querys;

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
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrdenes()
        {
            try
            {
                var query = new ListarOrdenesQuery();
                var ordenes = await Mediator.Send(query);
                return Ok(ordenes);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrden(Guid id, [FromBody] ModificarOrdenDto ordenDto)
        {
            try
            {
                var command = new ModificarOrdenCommand(ordenDto, id);
                await Mediator.Send(command);
                return Ok("Modificación Exitosa");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ha ocurrido un error  al realizar la modificación");
            }
        }
        [HttpPut("/status/{idOrden}")]
        public async Task<IActionResult> UpdateStatus(Guid idOrden, [FromBody] string TipoActualizacion)
        {
            try
            {
                var command = new ModificarEstatusOrdenCommand(new ModificarEstatusDto(idOrden,TipoActualizacion));
                await Mediator.Send(command);
                return Ok("Modificación Exitosa");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ha ocurrido un error  al realizar la modificación");
            }
        }
        [HttpGet("/information/{idPolizaAsegurado}")]
        public async Task<IActionResult> GetInformationPolizaAsegurado(Guid idPolizaAsegurado)
        {
            try
            {
                var query = new InformacionPolizaQuery(idPolizaAsegurado);
                var informacionPoliza = await Mediator.Send(query);
                return Ok(informacionPoliza);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
    }
}
