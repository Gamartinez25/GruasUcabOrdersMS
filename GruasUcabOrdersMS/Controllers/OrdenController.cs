using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersMS.Application.Commands.AsignarConductorCommand;
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
                return Ok(new { message = "Registro exitoso", status = 200 });

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
                return StatusCode(500, e.Message+"Hubo un error al procesar la busqueda");
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
        [HttpPut("/AsignacionGrua/{idOrden}")]
        public async Task<IActionResult> AsignarGruaAutomaticamente(Guid idOrden)
        {
            try
            {
                var command = new AsignarConductorCommand(idOrden);
                var resultado=await Mediator.Send(command);
                return Ok(resultado);
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
                var vehiculosDisponibles = await Mediator.Send(query);
                return Ok(vehiculosDisponibles);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpGet("/OrdenExpirada")]
        public async Task<IActionResult> GetAllOrdenExpiradas()
        {
            try
            {
                var query = new AsignacionExpiradaCommand();
                 await Mediator.Send(query);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpGet("/OrdenVigente/{idVehiculo}")]
        public async Task<IActionResult> GetOrdenVigente(Guid idVehiculo)
        {
            try
            {
                var query = new ListarOrdenVigentePorGruaQuery(idVehiculo);
                var order= await Mediator.Send(query);
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpGet("/OrdenFinalizada/{idVehiculo}")]
        public async Task<IActionResult> GetOrdenesFinalizadas(Guid idVehiculo)
        {
            try
            {
                var query = new ListarOrdenesFinalizadasQuery(idVehiculo);
                var order = await Mediator.Send(query);
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpGet("/OrdenCancelada/{idVehiculo}")]
        public async Task<IActionResult> GetOrdenesCanceladas(Guid idVehiculo)
        {
            try
            {
                var query = new ListarOrdenesCanceladasQuery(idVehiculo);
                var order = await Mediator.Send(query);
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }
        [HttpGet("/OrdenesResumen/{idVehiculo}")]
        public async Task<IActionResult> GetResumenOrdenes(Guid idVehiculo)
        {
            try
            {
                var query = new ListarResumenOrdenesQuery(idVehiculo);
                var order = await Mediator.Send(query);
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Hubo un error al procesar la busqueda");
            }
        }

    }
}
