
namespace OrdersMS.Application.Dtos.CostoAdicionalDtos
{
    public class ListarCostosAdicionalesPorOrdenDto
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public  double Monto { get;  private set; }
        public string Descripcion { get; private set; }
        public  string Estatus {  get; private set; }
        public ListarCostosAdicionalesPorOrdenDto(Guid id, string nombre, double monto, string descripcion, string estatus)
        {
            Id = id;
            Nombre = nombre;
            Monto = monto;
            Descripcion = descripcion;
            Estatus = estatus;
        }

        public ListarCostosAdicionalesPorOrdenDto()
        {
        }
    }
}
