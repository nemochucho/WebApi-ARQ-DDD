using AutoMapper;
using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Aplicacion.Interface;
using Pacagroup.Ecommerce.Dominio.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System;

namespace Pacagroup.Ecommerce.Aplicacion.Main
{
    public class UserAplicacion : IUserAplicacion
    {
        private readonly IUserDominio _userDominio;
        private readonly IMapper _mapper;

        public UserAplicacion(IUserDominio userDominio, IMapper mapper)
        {
            _userDominio = userDominio;
            _mapper = mapper;
        }

        public Response<UserDTO> Authenticate(string username, string password)
        {
            var response = new Response<UserDTO>();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Parámetros no pueden ser vacíos.";
                return response;
            }
            try
            {
                var user = _userDominio.Authenticate(username, password);
                response.Data = _mapper.Map<UserDTO>(user);
                response.IsSuccess = true;
                response.Message = "Autenticación Exitosa.";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe.";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message.ToString();  
            }
            return response;
        }
    }
}
