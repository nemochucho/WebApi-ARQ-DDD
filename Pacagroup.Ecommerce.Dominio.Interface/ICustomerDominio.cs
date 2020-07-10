using System;
using System.Collections.Generic;
using System.Text;
using Pacagroup.Ecommerce.Dominio.Entity;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Dominio.Interface
{
    public interface ICustomerDominio
    {
        #region Metodos Sincronos
        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        Customer Get(string customerId);
        IEnumerable<Customer> GetAll();
        #endregion

        #region Metodos ASincronos
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        #endregion
    }
}
