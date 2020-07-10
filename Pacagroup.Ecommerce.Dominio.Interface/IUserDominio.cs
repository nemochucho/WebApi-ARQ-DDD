using System;
using System.Collections.Generic;
using System.Text;
using Pacagroup.Ecommerce.Dominio.Entity;

namespace Pacagroup.Ecommerce.Dominio.Interface
{
    public interface IUserDominio
    {
        User Authenticate(string username, string password);
    }
}
