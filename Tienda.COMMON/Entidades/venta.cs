using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.COMMON.Entidades
{
    public class venta:BaseDTO
    {
        public int IdVenta { get; set; }
        public DateTime FechaHora { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Cliente { get; set; }
        
    }
}
