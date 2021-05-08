using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.COMMON.Entidades
{
    public abstract class BaseDTO:IDisposable
    {
        private bool _isDisposed;

        /* esto va a preguntar a la entidad si ya se dejo de usar
         si es asi la elimina para liberar memoria*/
        public void Dispose()
        {
            if (!_isDisposed)
            {
                this._isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
