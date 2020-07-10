using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pacagroup.Ecommerce.Dominio.Entity;

namespace Pacagroup.Ecommerce.Infraestructura.Interface
{
    public interface ICustomerRepository
    {
        #region Metodos Síncronos
        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        Customer Get(string customerId);
        IEnumerable<Customer> GetAll();
        #endregion

        #region Metodos Asíncronos
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        #endregion
    }
}
