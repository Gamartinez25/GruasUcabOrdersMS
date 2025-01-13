

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public  class OrdenByIdDto
    {
        public OrdenByIdDto()
        {
        }
        public OrdenByIdDto(Guid id,
                                            string numeroFactura,
                                            string denunciante,
                                            string fecha,
                                            string direccionOrigen,
                                            string direccionDestino,
                                            string datosVehiculo,
                                            string detallesIncidente,
                                            double total, 
                                            double totalCostoAdicional,
                                            string estatus)
        {
            Id=id;
            NumeroFactura=numeroFactura;
            Denunciante=denunciante;
            Fecha=fecha;
            DireccionOrigen=direccionOrigen;
            DireccionDestino=direccionDestino;
            DatosVehiculo=datosVehiculo;
            DetallesIncidente=detallesIncidente;
            Total=total;    
            TotalCostoAdicional=totalCostoAdicional;
            Estatus=estatus;
        }

        public Guid Id { get; private set; }
        public string NumeroFactura { get; private set; }
        public string Denunciante { get; private set; }
        public string Fecha { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public string DatosVehiculo { get; private set; }
        public string DetallesIncidente { get; private set; }
        public double Total {  get; private set; }
        public double TotalCostoAdicional { get; private set; }
        public string Estatus { get; private set; }
    }
}
