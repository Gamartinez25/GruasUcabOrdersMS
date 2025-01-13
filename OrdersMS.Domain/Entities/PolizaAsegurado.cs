using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class PolizaAsegurado
    {
        public PolizaAsegurado()
        {
        }

        public PolizaAsegurado(Guid id,
                               string fechaInicioCobertura,
                               string fechaVencimientoCobertura,
                               string marca,
                               string modelo,
                               string anio,
                               string placa,
                               string tipoVehiculo,
                               string color,
                               string estatus)
        {
            Id = id;
            FechaInicioCobertura = fechaInicioCobertura;
            FechaVencimientoCobertura = fechaVencimientoCobertura;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Placa = placa;
            TipoVehiculo = tipoVehiculo;
            Color = color;
            Estatus = estatus;
        }
        public PolizaAsegurado(Guid id,
                               string fechaInicioCobertura,
                               string fechaVencimientoCobertura,
                               string marca,
                               string modelo,
                               string anio,
                               string placa,
                               string tipoVehiculo,
                               string color,
                               string estatus,
                               Guid polizaId,
                               Guid aseguradoId)
        {
            Id = id;
            FechaInicioCobertura = fechaInicioCobertura;
            FechaVencimientoCobertura = fechaVencimientoCobertura;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Placa = placa;
            TipoVehiculo = tipoVehiculo;
            Color = color;
            Estatus = estatus;
            PolizaId = polizaId;
            AseguradoId=aseguradoId;

        }
        public Guid Id { get; set; }
        public string FechaInicioCobertura { get; set; }
        public string FechaVencimientoCobertura { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Anio { get; private set; }
        public string Placa { get; private set; }
        public string TipoVehiculo { get; private set; }
        public string Color { get; private set; }
        public string Estatus { get; private set; }

        // Relación
        public Guid PolizaId { get; set; }
        public Poliza Poliza { get; private set; }
        public Guid AseguradoId { get;private set; }
        public Asegurado Asegurado { get;private  set; }
        public ICollection<OrdenDeServicio> OrdenesDeServicio { get; private set; }
    }
}
