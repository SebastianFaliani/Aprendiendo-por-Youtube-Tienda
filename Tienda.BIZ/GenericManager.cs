using System;
using System.Collections.Generic;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.BIZ
{
    public abstract class GenericManager<T> : IGenericManager<T> where T : BaseDTO
    {
        protected IGenericRepository<T> repository;
        public GenericManager(IGenericRepository<T> repositorio)
        {
            repository = repositorio;
        }
        public string Error
        { 
            get { return repository.Error; }
        }

        public IEnumerable<T> ObtenerTodos 
        {
            get {return repository.Read;}
        }

        public bool Actualizar(T entidad)
        {
            return repository.Update(entidad);

        }

        public T BuscarPorId(string id)
        {
            return repository.SearchById(id);
        }

        public bool Eliminar(string id)
        {
            return repository.Delete(id);
        }

        public bool Insertar(T entidad)
        {
            return repository.Create(entidad);
        }
    }
}
