using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.BIZ
{
    public class UsuarioManager : GenericManager<usuario>, IUsuarioManager
    {
        public UsuarioManager(IGenericRepository<usuario> repositorio) : base(repositorio)
        {

        }

        public usuario Login(string nombreUsuario, string password)
        {
            return repository.Query(u => u.NombreDeUsuario == nombreUsuario && u.Password == password).SingleOrDefault();
        }
    }
}
