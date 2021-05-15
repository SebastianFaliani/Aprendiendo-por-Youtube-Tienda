using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.BIZ
{
    public abstract class ProductosManager : GenericManager<producto>, IProductosManager
    {
        protected ProductosManager(IGenericRepository<producto> repositorio) : base(repositorio)
        {
        }

        public producto BuscarProductoPorNombreExacto(string nombre)
        {
            return repository.Query(p => p.Nombre == nombre).SingleOrDefault();
        }

        public IEnumerable<producto> BuscarProductosPorNombre(string criterio)
        {
            //ToLower Convierte a minusculas para que al buscar no haga diferencia con las mayusculas
            return repository.Query(p => p.Nombre.ToLower().Contains(criterio.ToLower()));
        }
    }
}
