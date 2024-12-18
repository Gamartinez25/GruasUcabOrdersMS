namespace OrdersMS.Application.Saga.Events
{
    public class OrdenCreadaEvent
    {
        public Guid OrdenId { get; set; }
    }
    public class ActualizarOrdenEvent
    {
        public Guid OrdenId { get; set; } 
        public ActualizarOrdenEvent(Guid ordenId)
        {
            OrdenId = ordenId;
        }
    }
    public class OrdenCanceladaEvent 
    {
        public Guid OrdenId { get; set; }
        public OrdenCanceladaEvent(Guid ordenId)
        {
            OrdenId = ordenId;
        }
    }
    public class ReasignarOrdenEvent
    {
        public Guid OrdenId { get; set; }
        public ReasignarOrdenEvent(Guid ordenId)
        {
            OrdenId = ordenId;
        }
    }



}
