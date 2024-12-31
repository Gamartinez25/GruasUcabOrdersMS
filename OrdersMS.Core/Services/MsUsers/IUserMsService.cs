
namespace OrdersMS.Core.Services.MsUsers
{
    public interface IUserMsService
    {
        Task SendNotification(Guid idVehiculo);
    }
}
