using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /// <summary>
    /// Proporciona metodos estandarizado para el acceso a tablas; cada manager creado debe implementar de esta interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public interface IGenericManager<T> where T:BaseDTO
    {
        /// <summary>
        /// Proporciona el error relacionado despues de alguna operacion
        /// </summary>
        string Error { get; }
        
        /// <summary>
        /// Inserta una entidad en la tabla
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Confirmaion o no de la Inserción</returns>
        bool Insertar(T entidad);
        
        /// <summary>
        ///Obtiene todos los registros de la tabla 
        /// </summary>
        IEnumerable<T>ObtenerTodos { get; }

        /// <summary>
        /// Actualiza un registro de la tabla en base a su propiedad Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Confirmacion o no de la actualizacion</returns>
        bool Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad en base al Id proporcionado
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar</param>
        /// <returns>Confirmacion o no de la Eliminacion</returns>
        bool Eliminar(string id);

        /// <summary>
        /// Obtiene un elemento de acuerdo a si Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entidad completa correspondiente al ID proporcionado</returns>
        T BuscarPorId(string id);
  
    }
}
