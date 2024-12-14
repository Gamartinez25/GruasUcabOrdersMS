
using MassTransit;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace OrdersMS.Domain.Entities
{
    public  class EstadoOrden: SagaStateMachineInstance
    {
        public string EstadoActual { get; private set; } 
        public DateTime? UltimaActualizacion { get; set; } 
        public Guid CorrelationId { get ; set; }
 
        public EstadoOrden(Guid correlacionId, string estadoActual, DateTime ultimaActualizacion)
        {
            CorrelationId = correlacionId;
            EstadoActual = estadoActual;
            UltimaActualizacion = ultimaActualizacion;

        }

        public EstadoOrden()
        {
        }
    }
}
