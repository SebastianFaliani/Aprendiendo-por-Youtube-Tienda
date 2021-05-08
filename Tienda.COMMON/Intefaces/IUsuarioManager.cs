using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /// <summary>
    /// Proporciona los metodos relacionados a los usuarios
    /// </summary>
    public interface IUsuarioManager:IGenericManager<usuario>
    {
        /// <summary>
        /// Verifica si las credenciales son validad para el usuario
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="password"></param>
        /// <returns>Si son correctas me regresa la entidad del usuario, sino regresa Null</returns>
        usuario Login(string nombreUsuario, string password);
    }
}
