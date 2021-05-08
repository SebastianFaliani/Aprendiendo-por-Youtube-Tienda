using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Intefaces
{
    /// <summary>
    /// Proporciona los metodos relacionados a las ventas
    /// </summary>
    public interface IVentaManager:IGenericManager<venta>
    {
        /// <summary>
        /// Obtiene todas las ventas en el intervalo especificado de tiempo
        /// </summary>
        /// <param name="Inicio">fecha inicio</param>
        /// <param name="fin">fecha fin</param>
        /// <returns>Conjunto de ventas efectuadas en el intervalo proporcionado</returns>
        IEnumerable<venta> VentasEnInterbalo(DateTime Inicio, DateTime fin);

        /// <summary>
        /// Obtiene las ventas a un cliente en un intervalos de tiempo
        /// </summary>
        /// <param name="nombreCliente"></param>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        /// <returns>Conjunto de ventas realizada a un cliente en un intervalo e tiempo espesificado</returns>
        IEnumerable<venta> VentasDeClienteEnIntervalo(string nombreCliente, DateTime inicio, DateTime fin);
    }
}
