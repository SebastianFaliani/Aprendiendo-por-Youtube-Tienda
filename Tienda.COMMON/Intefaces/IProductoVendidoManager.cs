using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /// <summary>
    /// proporciona todos los metodos relacionados a productos vendidos
    /// </summary>
    public interface IProductoVendidoManager:IGenericManager<productovendido>
    {
        /// <summary>
        /// Obtiene los productos contenidos en una venta
        /// </summary>
        /// <param name="idVenta"></param>
        /// <returns>Conjunto de productos contenidos en la venta</returns>
        IEnumerable<productovendido> ProductosDeUnaVenta(int idVenta);
    }
}
