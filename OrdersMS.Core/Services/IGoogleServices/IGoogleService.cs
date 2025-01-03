using OrdersMS.Infrastructure.Mappers;

namespace OrdersMS.Core.Services.IGoogleServices
{
    public interface IGoogleService
    {
        Task<List<VehiculoDto>> GetDistanceAvailableVehiclesToOrigin( double origenLatitud, double origenLongitud);
        Task<RutaDto> GetDistanceToOriginAccidentDestination(double origenLatitud, double origenLongitud, double destinoLatitud, double destinoLongitud);
   
        Task<string> GetDirecction(double latitud, double longitud);
    }
}
