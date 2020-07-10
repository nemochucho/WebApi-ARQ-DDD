using System;
using System.Collections.Generic;
using System.Text;

namespace Pacagroup.Ecommerce.Transversal.Common
{
    public class Response <T>
    {
        /* Atributo Data (T Generico) almacenará la respuesta de los metodos de la capa de dominio */
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
