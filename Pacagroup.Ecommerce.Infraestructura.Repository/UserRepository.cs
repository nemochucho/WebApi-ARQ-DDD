using Dapper;
using Pacagroup.Ecommerce.Dominio.Entity;
using Pacagroup.Ecommerce.Infraestructura.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System;
using System.Data;

namespace Pacagroup.Ecommerce.Infraestructura.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public User Authenticate(string username, string password)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", username);
                parameters.Add("Password", password);

                var user = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
            throw new NotImplementedException();
        }
    }
}
