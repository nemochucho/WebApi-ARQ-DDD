using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Servicio.WebApi.Helpers
{
    /// <summary>
    /// Controller Customer.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// OriginCors.
        /// </summary>
        public string OriginCors { get; set; }

        /// <summary>
        /// Secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience.
        /// </summary>
        public string Audience { get; set; }
    }
}
