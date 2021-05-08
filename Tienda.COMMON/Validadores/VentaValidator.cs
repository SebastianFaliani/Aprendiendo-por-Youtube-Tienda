using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Validadores
{
    public class VentaValidator:AbstractValidator<venta>
    {
        public VentaValidator()
        {
            RuleFor(v => v.NombreDeUsuario).NotNull().NotEmpty().Length(1, 50);
            RuleFor(v => v.FechaHora).NotNull().NotEmpty();
            RuleFor(v => v.Cliente).NotNull().NotEmpty().Length(1, 50);
        }
    }
}
