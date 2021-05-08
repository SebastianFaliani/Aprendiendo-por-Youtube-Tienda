using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Validadores
{
    public class ProductoVendidoValidator:AbstractValidator<productovendido>
    {
        public ProductoVendidoValidator()
        {
            RuleFor(p => p.IdVenta).NotNull().NotEmpty();
            RuleFor(p => p.Costo).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(p => p.Cantidad).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(p => p.IdProducto).NotNull().NotEmpty();
        }
    }
}
