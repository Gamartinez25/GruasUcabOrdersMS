using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class Asegurado
    {
        public Guid Id { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string FechaNacimiento { get; private set; }
        public string TipoDocumento { get; private set; }
        public string NumeroDocumento { get; private set; }
        public string Estatus { get; private set; }
        public Asegurado(Guid id, string nombres, string apellidos, string fechaNacimiento, string tipoDocumento, string numeroDocumento, string estatus)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Estatus = estatus;
        }

        // Relación
        public ICollection<PolizaAsegurado> PolizasAsegurados { get; private set; }
    }
}
