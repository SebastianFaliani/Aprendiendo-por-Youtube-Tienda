using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /// <summary>
    /// Proporciona los metodos relacionado a los productos
    /// </summary>
    public interface IProductosManager:IGenericManager<producto>
    {
        /// <summary>
        /// Obtiene todos los productos que cumplan con el criterio de busqueda
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns>Conjunto de productos que cumplen con el criterio</returns>
        IEnumerable<producto> BuscarProductosPorNombre(string criterio);

        /// <summary>
        /// Obtiene
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        producto BuscarProductoPorNombreExacto(string nombre);
    }
}
