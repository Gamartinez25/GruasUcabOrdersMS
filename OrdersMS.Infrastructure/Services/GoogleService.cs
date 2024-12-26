
using System.Globalization;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Core.Services.MsProviders;
using OrdersMS.Infrastructure.Mappers;

namespace OrdersMS.Infrastructure.Services
{
    public class GoogleService:IGoogleService
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;
        private readonly IMsProvidersServices MsProvidersServices;
        private readonly IMapper Mapper;
        public GoogleService(HttpClient httpClient, IConfiguration config, IMsProvidersServices msProvidersServices,IMapper mapper)
        {
            HttpClient= httpClient;
            Config= config;
            MsProvidersServices= msProvidersServices;
            Mapper= mapper;
        }

        public async Task<List<VehiculoDto>> GetDistanceAvailableVehiclesToOrigin( double origenLatitud, double origenLongitud)
        {
            var listaVehiculos = await MsProvidersServices.GetAllVehiculosAsync();
            var vehiculoDto= Mapper.Map<List<VehiculoDto>>(listaVehiculos);
            var vehiclesLocationBuilder = new StringBuilder();
            foreach (var item in listaVehiculos)
            {
                if (vehiclesLocationBuilder.Length > 0)
                {
                    vehiclesLocationBuilder.Append('|');
                }
                vehiclesLocationBuilder.AppendFormat(CultureInfo.InvariantCulture,"{0},{1}", item.Latitud.ToString(CultureInfo.InvariantCulture), item.Longitud.ToString(CultureInfo.InvariantCulture));
            }

            var vehiclesLocation = vehiclesLocationBuilder.ToString();
            var orgLocation = $"{origenLatitud.ToString(CultureInfo.InvariantCulture)},{origenLongitud.ToString(CultureInfo.InvariantCulture)}";
            var response = await HttpClient.GetAsync( $"{Config["ServiosUrl:Google"]}distancematrix/json?origins={vehiclesLocation}&destinations={orgLocation}&key={Config["ApiKey:Google"]}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Root>(content);

               
            for (var i = 0; i < listaVehiculos.Count(); i++)
            {
                var row = result.Rows[i];
                if (row.Elements[0].Status == "OK")
                {
                    
                    vehiculoDto[i].DistanciaTexto=( row.Elements[0].Distance.Text);
                    vehiculoDto[i].DistanciaValor=(row.Elements[0].Distance.Value);
                    vehiculoDto[i].DuracionTexto=(row.Elements[0].Duration.Text);
                    vehiculoDto[i].DuracionValor=(row.Elements[0].Duration.Value);
                }
            }
            
            

            return  vehiculoDto.OrderBy(x => x.DistanciaValor).OrderBy(x => x.DuracionValor).ToList();
        }

        public  async Task<RutaDto> GetDistanceToOriginAccidentDestination(double origenLatitud, double origenLongitud, double destinoLatitud, double destinoLongitud)
        {
            var orgLocation = $"{origenLatitud.ToString(CultureInfo.InvariantCulture)},{origenLongitud.ToString(CultureInfo.InvariantCulture)}";
            var desLocation = $"{destinoLatitud.ToString(CultureInfo.InvariantCulture)},{destinoLongitud.ToString(CultureInfo.InvariantCulture)}";
            var response = await HttpClient.GetAsync($"{Config["ServiosUrl:Google"]}distancematrix/json?origins={orgLocation}&destinations={desLocation}&key={Config["ApiKey:Google"]}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Root>(content);
            var rutaDto= new RutaDto();

                if (result.Rows[0].Elements[0].Status == "OK")
                {
                    rutaDto.DistanciaTexto = (result.Rows[0].Elements[0].Distance.Text);
                    rutaDto.DistanciaValor = (result.Rows[0].Elements[0].Distance.Value);
                    rutaDto.DuracionTexto = (result.Rows[0].Elements[0].Duration.Text);
                    rutaDto.DuracionValor = (result.Rows[0].Elements[0].Duration.Value);
                }
            return rutaDto;

        }
    }
}
