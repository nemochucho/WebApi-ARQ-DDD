using System;
using System.Collections.Generic;
using System.Text;
using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Aplicacion.Interface
{
    public interface ICustomerAplicacion
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomerDTO customerDto);
        Response<bool> Update(CustomerDTO customerDto);
        Response<bool> Delete(string customerId);
        Response<CustomerDTO> Get(string customerId);
        Response<IEnumerable<CustomerDTO>> GetAll();
        #endregion

        #region Metodos ASincronos
        Task<Response<bool>> InsertAsync(CustomerDTO customerDto);
        Task<Response<bool>> UpdateAsync(CustomerDTO customerDto);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
        #endregion
    }
}
