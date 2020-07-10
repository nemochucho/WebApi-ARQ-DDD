using System;
using System.Collections.Generic;
using System.Text;
using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Aplicacion.Interface
{
    public interface IUserAplicacion
    {
        Response<UserDTO> Authenticate(string username, string password);
    }
}
