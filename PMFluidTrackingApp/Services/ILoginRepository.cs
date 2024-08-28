using PMFluidTrackingApp.Models;

namespace PMFluidTrackingApp.Services;

public interface ILoginRepository
{
    Task<User> Login(string email, string password);
    Task<User> Register(User user);
}
