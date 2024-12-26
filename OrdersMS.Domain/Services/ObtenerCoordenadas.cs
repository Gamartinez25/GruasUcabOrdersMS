using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Services
{
    public static class ObtenerCoordenadas
    {
        public static (double Latitud, double Longitud) SepararCoordenadas(string direccion)
        {
            
            var parts = direccion.Split(',');

            if (parts.Length != 2)
            {
                throw new ArgumentException("La cadena de coordenadas debe contener exactamente una latitud y una longitud separadas por coma.");
            }
          
            if (!double.TryParse(parts[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double latitud) ||
                !double.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double longitud))
            {
                throw new ArgumentException("No se pudo convertir las coordenadas a valores numéricos.");
            }

           
            return (latitud, longitud);
        }
    }
}
