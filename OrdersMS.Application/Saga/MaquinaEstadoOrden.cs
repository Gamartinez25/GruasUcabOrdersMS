using MassTransit;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Saga
{
    public class MaquinaEstadoOrden: MassTransitStateMachine<EstadoOrden>
    {
        public State PorAsignar { get; set; }
        public State PorAceptar { get; set; }
        public State Aceptado { get; set; }
        public State Localizado { get; set; }
        public State Cancelado { get; set; }
        public State EnProceso { get; set; }
        public State Finalizado { get; set; }
        public State Pagado { get; set; }
      
        public Event<OrdenCreadaEvent> OrdenCreada{ get; set; }
        public Event<OrdenCanceladaEvent> OrdenCancelada { get; set; }
        public Event<ActualizarOrdenEvent> ActualizarOrden {  get; set; }
        public Event<ReasignarOrdenEvent> ReasignarOrden { get; set; }

        private readonly IVehiculosAsignadosRepository VehiculosAsignadosRepository;
        public MaquinaEstadoOrden(IVehiculosAsignadosRepository vehiculosAsignadosRepository)
        {
            VehiculosAsignadosRepository= vehiculosAsignadosRepository;

            InstanceState(x => x.EstadoActual);
            Event(() => OrdenCreada, e => e.CorrelateById(m => m.Message.OrdenId));
            Event(() => ActualizarOrden, e => e.CorrelateById(m => m.Message.OrdenId));
            Event(() => OrdenCancelada, e => e.CorrelateById(m => m.Message.OrdenId));
            Event(() => ReasignarOrden, e => e.CorrelateById(m => m.Message.OrdenId));


            Initially(
            When(OrdenCreada)
                   .Then(context => {
                       context.Saga.CorrelationId = context.Message.OrdenId;
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;

                   })
                .TransitionTo(PorAsignar));

            During(PorAsignar,
                When(ActualizarOrden)
                    .Then(context =>
                    {
                        context.Saga.UltimaActualizacion = DateTime.UtcNow;
                    })
                   .TransitionTo(PorAceptar),

                When(OrdenCancelada)
                    .Then(context =>
                    {
                        context.Saga.UltimaActualizacion = DateTime.UtcNow;
                    })
                   .TransitionTo(Cancelado));

            During(PorAceptar,
                When(ActualizarOrden)
                    .Then(context =>
                    {
                        context.Saga.UltimaActualizacion = DateTime.UtcNow;
                        VehiculosAsignadosRepository.DeleteAsignacionGrua(context.Message.OrdenId);    
                    }
                    )
                    .TransitionTo(Aceptado),
                When(OrdenCancelada)
                    .Then(context =>
                    {
                        context.Saga.UltimaActualizacion = DateTime.UtcNow;
                    })
                    .TransitionTo(Cancelado),
            When(ReasignarOrden)
                .Then(context =>
                {
                    context.Saga.UltimaActualizacion = DateTime.UtcNow;
                })
                .TransitionTo(PorAsignar));

            During(Aceptado,
               When(ActualizarOrden)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                   .TransitionTo(Localizado),
               When(OrdenCancelada)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                   .TransitionTo(Cancelado));

            During(Localizado,
               When(ActualizarOrden)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                   .TransitionTo(EnProceso),
               When(OrdenCancelada)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                   .TransitionTo(Cancelado));

            During(EnProceso,
               When(ActualizarOrden)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                   .TransitionTo(Finalizado),
                When(OrdenCancelada)
                    .Then(context =>
                    {
                    Console.WriteLine("No se puede cancelar una orden ya finalizada.");
                    }));
            During(Finalizado,
               When(ActualizarOrden)
                   .Then(context =>
                   {
                       context.Saga.UltimaActualizacion = DateTime.UtcNow;
                   })
                    .TransitionTo(Pagado), When(OrdenCancelada)
                    .Then(context =>
                    {
                    Console.WriteLine("No se puede cancelar una orden ya finalizada.");
                    }));
        }
    }
}
