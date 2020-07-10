using System;
using AutoMapper;
using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Aplicacion.Interface;
using Pacagroup.Ecommerce.Dominio.Entity;
using Pacagroup.Ecommerce.Dominio.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pacagroup.Ecommerce.Aplicacion.Main
{
    public class CustomerAplicacion : ICustomerAplicacion
    {
        private readonly ICustomerDominio _customerDominio;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomerAplicacion> _logger;

        public CustomerAplicacion(ICustomerDominio customerDominio, IMapper mapper, IAppLogger<CustomerAplicacion> logger)
        {
            _customerDominio = customerDominio;
            _mapper = mapper;
            _logger = logger;
        }

        #region Métodos Síncronos
        public Response<bool> Insert(CustomerDTO customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = _customerDominio.Insert(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso.";
                }
            }
            catch (Exception e){
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;            
        }
        public Response<bool> Update(CustomerDTO customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = _customerDominio.Update(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitoso.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = _customerDominio.Delete(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public Response<CustomerDTO> Get(string customerId)
        {
            var response = new Response<CustomerDTO>();
            try
            {
                var customer = _customerDominio.Get(customerId);
                response.Data = _mapper.Map<CustomerDTO>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public Response<IEnumerable<CustomerDTO>> GetAll()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            try
            {
                var customer = _customerDominio.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa.";
                    _logger.LogInformation("Consulta Exitosa.");
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                _logger.LogError(e.Message); 
                throw new NotImplementedException();
            }
            return response;
        }
        #endregion

        #region Métodos Asíncronos
        public async Task<Response<bool>> InsertAsync(CustomerDTO customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = await _customerDominio.InsertAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = await _customerDominio.UpdateAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitoso.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _customerDominio.DeleteAsync(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public async Task<Response<CustomerDTO>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDTO>();
            try
            {
                var customer = await _customerDominio.GetAsync(customerId);
                response.Data = _mapper.Map<CustomerDTO>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            try
            {
                var customer = await _customerDominio.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa.";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                throw new NotImplementedException();
            }
            return response;
        }
        #endregion
    }
}
