using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OrdersMS.Core.Services.MsUsers;

namespace OrdersMS.Infrastructure.Services
{
    public class UserMsService : IUserMsService
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;


        public UserMsService(HttpClient httpClient, IConfiguration config)
        {
            HttpClient = httpClient;
            Config = config;
        }
        public async Task SendNotification(Guid idVehiculo)
        {
            var data = new
            {
                IdVehiculo = idVehiculo,
                Titulo = "Nueva Orden de Servicio",
                Mensaje = "Se te ha asignado una nueva orden de servicio. Revisa los detalles y confirma si puedes atenderla."
            };
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonContent = JsonContent.Create(data, options: jsonOptions);
            var response = await HttpClient.PostAsync(Config["ServiosUrl:MsUsers"], jsonContent);
            

        }
    }
}
