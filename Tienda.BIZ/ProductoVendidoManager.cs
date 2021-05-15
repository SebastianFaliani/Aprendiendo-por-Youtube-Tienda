using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.BIZ
{
    public class ProductoVendidoManager : GenericManager<productovendido>, IProductoVendidoManager
    {
        public ProductoVendidoManager(IGenericRepository<productovendido> repositorio) : base(repositorio)
        {
        }

        public IEnumerable<productovendido> ProductosDeUnaVenta(int idVenta)
        {
            return repository.Query(p => p.IdVenta == idVenta);
        }
    }
}
