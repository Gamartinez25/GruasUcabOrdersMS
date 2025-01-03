

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class OrdenVigentePorGruaDto
    {
        public OrdenVigentePorGruaDto(Guid id,
                                             string detallesIncidente,
                                             string direccionOrigen,
                                             string direcionDestino,
                                             string nombreDenunciante,
                                             string estatus
                                             )
        {
            Id = id;
            DetallesIncidente = detallesIncidente;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direcionDestino;
            NombreDenunciante= nombreDenunciante;
            Estatus = estatus;


        }
        public OrdenVigentePorGruaDto() { }

        public Guid Id { get;  private set; }
        public string DetallesIncidente { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public string NombreDenunciante { get; private set; }
      
        public string Estatus {  get; private set; }



    }
}
