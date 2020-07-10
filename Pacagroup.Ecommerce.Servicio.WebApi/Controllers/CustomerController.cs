using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Aplicacion.Interface;

namespace Pacagroup.Ecommerce.Servicio.WebApi.Controllers
{
    /// <summary>
    /// Controller Customer.
    /// </summary>
    [Authorize] // Con este filtro le indico que todos los metodos de customer estan protegidos, pero tambien lo puedo asignar por metodos específicos
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAplicacion _customerAplicacion;

        /// <summary>
        /// Constructor Controller Customer.
        /// </summary>
        public CustomerController(ICustomerAplicacion customerAplicacion)
        {
            _customerAplicacion = customerAplicacion;
        }

        #region Métodos Síncronos
        /// <summary>
        /// Syncronic Method Insert Customer.
        /// </summary>
        [HttpPost]
        public IActionResult Insert([FromBody]CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerAplicacion.Insert(customerDto);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Syncronic Method Update Customer.
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody]CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerAplicacion.Update(customerDto);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Syncronic Method Delete Customer.
        /// </summary>
        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerAplicacion.Delete(customerId);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Syncronic Method Delete Customer.
        /// </summary>
        [HttpGet("{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerAplicacion.Get(customerId);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Syncronic Method Get All Customer.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _customerAplicacion.GetAll();
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion

        #region Métodos Asíncronos
        /// <summary>
        /// Asyncronic Method Insert Customer.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody]CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerAplicacion.InsertAsync(customerDto);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asyncronic Method Update Customer.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerAplicacion.UpdateAsync(customerDto);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asyncronic Method Delete Customer.
        /// </summary>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = await _customerAplicacion.DeleteAsync(customerId);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asyncronic Method Get Customer.
        /// </summary>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = await _customerAplicacion.GetAsync(customerId);
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asyncronic Method Get All Customer.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customerAplicacion.GetAllAsync();
            if (response.IsSuccess == true)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion
    }
}