using System;
using System.Collections.Generic;
using System.Text;
using Pacagroup.Ecommerce.Dominio.Entity;
using Pacagroup.Ecommerce.Dominio.Interface;
using Pacagroup.Ecommerce.Infraestructura.Interface;

namespace Pacagroup.Ecommerce.Dominio.Core
{
    public class UserDominio : IUserDominio
    {
        private readonly IUserRepository _userRepository;

        public UserDominio(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username, password);
            
            throw new NotImplementedException();
        }
    }
}
