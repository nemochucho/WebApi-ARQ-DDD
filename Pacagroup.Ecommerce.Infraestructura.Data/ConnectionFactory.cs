using System;
using Pacagroup.Ecommerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Pacagroup.Ecommerce.Infraestructura.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration congiguration)
        {
            _configuration = congiguration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlconnection = new SqlConnection();
                if (sqlconnection == null) return null;

                sqlconnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                sqlconnection.Open();
                return sqlconnection; 
            }
        }
    }
}
