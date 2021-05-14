using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /*
     * Solamente voy a poder usar esta interface en los objetos que hereden de BaseDTO
     */
    
    /// <summary>
    /// Proporciona los metodos básicos de acceso a una BD (CRUD)
    /// </summary>
    /// <typeparam name="T">T es una entidad (Clase) a la que se refiere la tabla</typeparam>
    public interface IGenericRepository<T> where T:BaseDTO
    {
        /// <summary>
        /// Proporciona información sobre algun error ocurrido en alguna de las operaciones
        /// </summary>
        string Error { get; }

        /*
         * CRUD=> Create (Insert), Read (Select), Update (Update), Delete (Delete)
         */

        /// <summary>
        /// Inserta una entidad en la tabla
        /// </summary>
        /// <param name="entidad">Entidad a Insertar</param>
        /// <returns>Retorna la confirmacion o no de la Insercion</returns>
        bool Create(T entidad);
        
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        IEnumerable<T> Read { get; }

        /// <summary>
        /// Actualiza un registro en la tabla en base a la propiedad ID
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna la confirmacion o no de la modificación</returns>
        bool Update(T entidad);

        /// <summary>
        /// Elimina una entidad en la base de datos de acurdo al ID relacionado
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <returns>Retorna la confirmacion o no de la Eliminación</returns>
        bool Delete(string id);

        //Query=> Realizara consultas en la tabla, mediante instrucciones lambda

        /// <summary>
        /// Realiza una consulta personalizada a la tabla
        /// </summary>
        /// <param name="predicado">Exprecion lambda que define la consulta</param>
        /// <returns>Retorna el conjunto de entidades que cumplen con la consulta</returns>
        IEnumerable<T> Query(Expression<Func<T, bool>> predicado);

        /// <summary>
        /// Obtiene una entidad en base a su id 
        /// </summary>
        /// <param name="id">Id de la entidad a obtener</param>
        /// <returns>Retorna la entidad que corresponde al Id proporcionado</returns>
        T SearchById(string id);

    }
}
