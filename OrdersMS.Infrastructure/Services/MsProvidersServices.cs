
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OrdersMS.Core.Services.MsProviders;

namespace OrdersMS.Infrastructure.Services
{
    public class MsProvidersServices : IMsProvidersServices
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;


        public MsProvidersServices(HttpClient httpClient, IConfiguration config)
        {
            HttpClient = httpClient;
            Config = config;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllVehiculosAsync()
        {
            try
            {
                // Realizar solicitud GET
                var response = await HttpClient.GetAsync(Config["ServiosUrl:MsProviders"]);

                // Asegurarse de que la respuesta fue exitosa
                response.EnsureSuccessStatusCode();

                // Leer y deserializar el contenido JSON
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Vehiculo>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al consumir el endpoint: {ex.Message}");
                throw;
            };
        }
    }
}
