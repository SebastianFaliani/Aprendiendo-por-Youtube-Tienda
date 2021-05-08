using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.COMMON.Entidades
{
    public class productovendido:BaseDTO
    {
        public int IdProductoVendido { get; set; }
        public int IdVenta { get; set; }
        public decimal Costo { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
    }
}
