using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.COMMON.Entidades
{
    public class usuario:BaseDTO
    {
        public string NombreDeUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Password { get; set; }
    }
}
