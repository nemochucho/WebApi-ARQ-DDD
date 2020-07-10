using Pacagroup.Ecommerce.Dominio.Entity;

namespace Pacagroup.Ecommerce.Infraestructura.Interface
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
    }
}
