using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;

namespace Tienda.COMMON.Validadores
{
    public class UsuarioValidator:AbstractValidator<usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.NombreDeUsuario).NotNull().NotEmpty().Length(1, 50);
            RuleFor(u => u.Apellidos).NotNull().NotEmpty().Length(1, 50);
            RuleFor(u => u.Nombres).NotNull().NotEmpty().Length(1, 50);
            RuleFor(u => u.Password).NotNull().NotEmpty().Length(1, 50);
        }
    }
}
