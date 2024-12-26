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
                        string tipo,
                        string marca,
                        string modelo,
                        string placa,
                        string color,
                        double latitud,
                        double longitud
                        )
        {
            Id = id;
            Tipo = tipo;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Color = color;
            Latitud = latitud;
            Longitud = longitud;
            
        }

        public Guid Id { get; private  set; }
        public string Tipo { get; private set; }
        public string  Marca {  get; private set; }
        public string Modelo { get; private set; }
        public string Anio { get; private set; }
        public string Placa { get; private set; }
        public  string Color {  get; private set; }
        public string Estatus{ get; private set;}
        public double Latitud {  get; private set; }
        public double Longitud { get; private set; }
        public DateTime UltimaActualizacion { get; private set; }

    }
}
