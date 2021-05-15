using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.BIZ
{
    public class VentaManager : GenericManager<venta>, IVentaManager
    {
        public VentaManager(IGenericRepository<venta> repositorio) : base(repositorio)
        {
        }

        public IEnumerable<venta> VentasDeClienteEnIntervalo(string nombreCliente, DateTime inicio, DateTime fin)
        {
            DateTime rInicio = new DateTime(inicio.Year, inicio.Month, inicio.Day, 0, 0, 0);
            DateTime rFin = new DateTime(fin.Year, fin.Month, fin.Day, 23, 59, 59);
            return repository.Query(v => v.Cliente == nombreCliente && v.FechaHora >= rInicio && v.FechaHora < rFin);
        }

        public IEnumerable<venta> VentasEnInterbalo(DateTime inicio, DateTime fin)
        {
            DateTime rInicio = new DateTime(inicio.Year, inicio.Month, inicio.Day,0,0,0);
            DateTime rFin = new DateTime(fin.Year, fin.Month, fin.Day, 23, 59, 59);
            return repository.Query(v => v.FechaHora >= rInicio && v.FechaHora < rFin);
        }
    }
}
