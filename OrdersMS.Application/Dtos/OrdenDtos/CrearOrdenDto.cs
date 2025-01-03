
namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class CrearOrdenDto
    {
        public string DetallesIncidente { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public string NombreDenunciante { get; private set; }
        public string TipoDocumentoDenunciante { get; private set; }
        public string NumeroDocumentoDenunciante { get; private set; }
       
        // Relaciones
        public Guid PolizaAseguradoId { get; set; }
        public Guid? Administrador { get; private set; }
        public Guid? Operador { get; private set; }
        public CrearOrdenDto(string detallesIncidente,
                             string direccionOrigen,
                             string direccionDestino,
                             string nombreDenunciante,
                             string tipoDocumentoDenunciante,
                             string numeroDocumentoDenunciante,
                             Guid polizaAseguradoId,
                             Guid? administrador=null,
                             Guid? operador=null
                             )
        {
            DetallesIncidente = detallesIncidente;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            NombreDenunciante = nombreDenunciante;
            TipoDocumentoDenunciante = tipoDocumentoDenunciante;
            NumeroDocumentoDenunciante = numeroDocumentoDenunciante;
            PolizaAseguradoId= polizaAseguradoId;
            Administrador= administrador;
            Operador= operador;
           
        }
    }

}
