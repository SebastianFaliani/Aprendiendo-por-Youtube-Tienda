using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.COMMON.Entidades
{
    public class producto:BaseDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
    }
}
