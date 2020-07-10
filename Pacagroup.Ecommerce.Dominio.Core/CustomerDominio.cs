using Pacagroup.Ecommerce.Dominio.Entity;
using Pacagroup.Ecommerce.Dominio.Interface;
using Pacagroup.Ecommerce.Infraestructura.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;  

namespace Pacagroup.Ecommerce.Dominio.Core
{
    public class CustomerDominio : ICustomerDominio
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerDominio(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /* Pacagroup.Ecommerce.Dominio.Core es la implementación de las interfaces */
        /* En esta clase es donde se implementa la lógica del negocio */

        #region Metodos Síncronos
        public bool Insert(Customer customer)
        {
            return _customerRepository.Insert(customer);
        }
        public bool Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }
        public bool Delete(string customerId)
        {
            return _customerRepository.Delete(customerId);
        }
        public Customer Get(string customerId)
        {
            return _customerRepository.Get(customerId);
        }
        public IEnumerable <Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }
        #endregion

        #region Metodos Asíncronos
        public async Task<bool> InsertAsync(Customer customer)
        {
            return await _customerRepository.InsertAsync(customer);
        }
        public async Task<bool> UpdateAsync(Customer customer)
        {
            return await _customerRepository.UpdateAsync(customer);
        }
        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _customerRepository.DeleteAsync(customerId);
        }
        public async Task<Customer> GetAsync(string customerId)
        {
            return await _customerRepository.GetAsync(customerId);
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
        #endregion
    }
}
