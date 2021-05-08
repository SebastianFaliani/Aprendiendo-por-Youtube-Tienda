using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Validadores
{
    public class ProductoValidator:AbstractValidator<producto>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.Nombre).NotNull().NotEmpty().Length(1, 50);
            RuleFor(p => p.Costo).NotNull().GreaterThan(0);
        }
    }
}
