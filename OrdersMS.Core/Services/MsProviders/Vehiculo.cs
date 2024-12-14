using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Services.MsProviders
{
    public class Vehiculo
    {
        public Vehiculo(Guid id,
                        string marca,
                        string modelo,
                        string anio,
                        string placa,
                        string color,
                        string estatus,
                        string latitud,
                        string longitud,
                        DateTime ultimaActualizacion)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Placa = placa;
            Color = color;
            Estatus = estatus;
            Latitud = latitud;
            Longitud = longitud;
            UltimaActualizacion = ultimaActualizacion;
        }

        public Guid Id { get; private  set; }
        public string  Marca {  get; private set; }
        public string Modelo { get; private set; }
        public string Anio { get; private set; }
        public string Placa { get; private set; }
        public  string Color {  get; private set; }
        public string Estatus{ get; private set;}
        public string Latitud {  get; private set; }
        public string Longitud { get; private set; }
        public DateTime UltimaActualizacion { get; private set; }

    }
}
