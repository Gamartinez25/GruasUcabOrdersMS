﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class PolizaAsegurado
    {
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
