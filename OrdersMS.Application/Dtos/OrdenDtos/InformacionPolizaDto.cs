using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class InformacionPolizaDto
    {
        public InformacionPolizaDto(Guid polizaAseguradoId,
                                    string nombreAsegurado,
                                    string documentoIdentidad,
                                    double coberturaBasePoliza,
                                    double distanciaCoberturaPoliza,
                                    string tipoPoliza,
                                    string placa,
                                    string informacionVehiculo)
        {
            PolizaAseguradoId = polizaAseguradoId;
            NombreAsegurado = nombreAsegurado;
            DocumentoIdentidad = documentoIdentidad;
            CoberturaBasePoliza = coberturaBasePoliza;
            DistanciaCoberturaPoliza = distanciaCoberturaPoliza;
            TipoPoliza = tipoPoliza;
            Placa = placa;
            InformacionVehiculo = informacionVehiculo;
        }

        public Guid PolizaAseguradoId { get; set; }
        public string NombreAsegurado { get; private set; }
        public string DocumentoIdentidad { get; private set; }
        public double CoberturaBasePoliza { get; private set; }
        public double DistanciaCoberturaPoliza { get; private set; }
        public string TipoPoliza { get; private set; }
        public string Placa {  get; private set; }
        public string InformacionVehiculo { get; private set; }
    }
}
