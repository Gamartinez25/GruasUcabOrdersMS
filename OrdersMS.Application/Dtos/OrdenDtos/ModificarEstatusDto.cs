

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class ModificarEstatusDto
    {
        public ModificarEstatusDto(Guid id, string tipoActualizacion)
        {
            Id = id;
            TipoActualizacion = tipoActualizacion;
        }

        public Guid Id { get; private set; }
        public string TipoActualizacion { get;  private set; }
    }
}
