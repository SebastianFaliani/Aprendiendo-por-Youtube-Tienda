using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;

namespace Tienda.DAL.MSSqlServer
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseDTO
    {
        DBMSSqlServer db;
        private bool idEsAutonumerico;
        private AbstractValidator<T> validator;
        

        public GenericRepository(AbstractValidator<T> validator,bool idEsAutonumerico)
        {
            this.validator = validator;
            this.idEsAutonumerico = idEsAutonumerico;
            db = new DBMSSqlServer();
        }
        public string Error { get; private set; }

        public IEnumerable<T> Read
        {
            get
            {
                try
                {
                    string sql = string.Format("SELECT * FROM {0}", typeof(T).Name);
                    SqlDataReader r = (SqlDataReader)db.Consulta(sql);
                    List<T> datos = new List<T>();
                    var campos = typeof(T).GetProperties();
                    T dato;
                    Type tipo = typeof(T);
                    while (r.Read())
                    {
                        dato = (T)Activator.CreateInstance(typeof(T));
                        for (int i = 0; i < campos.Length; i++)
                        {
                            PropertyInfo prop = tipo.GetProperty(campos[i].Name);
                            prop.SetValue(dato, r[i]);
                        }
                        datos.Add(dato);
                    }
                    r.Close();
                    Error = "";
                    return datos;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }
            }
        }

        public bool Create(T entidad)
        {
            try
            {
                ValidationResult resultadoDeValidacion = validator.Validate(entidad);
                if (resultadoDeValidacion.IsValid)
                {
                    string sql1 = ("INSERT INTO " + typeof(T).Name + " (");
                    string sql2 = ") VALUES (";
                    var campos = typeof(T).GetProperties();
                    Type tipo = typeof(T);
                    for(int i = 0; i < campos.Length; i++)
                    {
                        if (idEsAutonumerico && i == 0)
                        {
                            continue;
                        }
                        sql1 += " " + campos[i].Name;
                        var propiedad = tipo.GetProperty(campos[i].Name);
                        var valor = propiedad.GetValue(entidad);
                        switch (propiedad.PropertyType.Name)
                        {
                            case "String":
                                sql2 += "'" + valor + "'";
                                break;
                            case "DateTime":
                                DateTime v = (DateTime)valor;
                                sql2 += string.Format($"convert(datetime,'{v.Day}-{v.Month}-{v.Year.ToString().Substring(2,2)} {v.Hour}:{v.Minute}:{v.Second}',5)");
                                break;
                            default:
                                sql2 += " " + valor;
                                break;
                        }
                        if (i != campos.Length - 1)
                        {
                            sql1 += " ,";
                            sql2 += " ,";
                        }
                    }
                    return EjecutarComando(sql1 + sql2 + ");");
                }
                Error = "Error de Validacion";
                foreach (var item in resultadoDeValidacion.Errors)
                {
                    Error += item.ErrorMessage + ".\n ";
                }
                return false;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false; 
            }
        }

        private bool EjecutarComando(string sql)
        {
            if (db.Comando(sql))
            {
                Error = "";
                return true;
            }
            else
            {
                Error = db.Error;
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var campos = typeof(T).GetProperties();
                Type tipo = typeof(T);
                string sql = "DELETE FROM " + typeof(T).Name + " WHERE " + campos[0].Name + "=";
                switch (tipo.GetProperty(campos[0].Name).PropertyType.Name)
                {
                    case "String":
                        sql += "'" + id + "'";
                        break;
                    default:
                        sql += " " + id;
                        break;
                }
                if (db.Comando(sql + ";"))
                {
                    Error = "";
                    return true;
                }
                else
                {
                    Error = db.Error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public IEnumerable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicado)
        {
            return Read.Where(predicado.Compile());
        }

        public T SearchById(string id)
        {
            try
            {
                var campos = typeof(T).GetProperties();
                Type tipo = typeof(T);
                string sql = "SELECT * FROM " + typeof(T).Name + " WHERE " + campos[0].Name + "=";
                switch (tipo.GetProperty(campos[0].Name).PropertyType.Name)
                {
                    case "String":
                        sql += "'" + id + "'";
                        break;
                    default:
                        sql += " " + id;
                        break;
                }
                SqlDataReader r = (SqlDataReader)db.Consulta(sql + ";");
                T dato = (T)Activator.CreateInstance(typeof(T)); ;
                int j = 0;
                while (r.Read())
                {
                    dato = (T)Activator.CreateInstance(typeof(T));
                    for (int i = 0; i < campos.Length; i++)
                    {
                        PropertyInfo prop = tipo.GetProperty(campos[i].Name);
                        prop.SetValue(dato, r[i]);
                    }
                    j++;
                }
                r.Close();
                if (j > 0)
                {
                    Error = "";
                    return dato;
                }
                else
                {
                    Error = "Id No existente...";
                    return null;
                }
            }
            catch (Exception ex)

            {
                Error = ex.Message;
                return null;
            }
        }

        public bool Update(T entidad)
        {
            try
            {
                ValidationResult resultadoDeValidacion = validator.Validate(entidad);
                if (resultadoDeValidacion.IsValid)
                {
                    string sql1 = "UPDATE " + typeof(T).Name + " SET ";
                    string sql2 = " WHERE ";
                    string sql = "";
                    var campos = typeof(T).GetProperties();
                    T dato = (T)Activator.CreateInstance(typeof(T));
                    Type tipo = typeof(T);
                    for (int i = 0; i < campos.Length; i++)
                    {
                        var propiedad = tipo.GetProperty(campos[i].Name);
                        var valor = propiedad.GetValue(entidad);
                        sql += propiedad.Name + "=";
                        switch (propiedad.PropertyType.Name)
                        {
                            case "String":
                                sql += "'" + valor + "'";
                                break;
                            case "DateTime":
                                DateTime v = (DateTime)valor;
                                sql += string.Format($"convert(datetime,'{v.Day}-{v.Month}-{v.Year.ToString().Substring(2, 2)} {v.Hour}:{v.Minute}:{v.Second}',5)");
                                break;
                            default:
                                sql += " " + valor;
                                break;
                        }
                        if (i == 0)
                        {
                            sql2 += sql;
                        }
                        if (i != campos.Length - 1)
                        {
                            sql += " ,";
                        }
                        if (!idEsAutonumerico || !(i == 0))
                        {
                            sql1 += sql;
                        }
                        sql = "";
                    }
                    if (db.Comando(sql1 + sql2))
                    {
                        Error = "";
                        return true;
                    }
                    else
                    {
                        Error = db.Error;
                        return false;
                    }
                }
                Error = "Error de Validacion";
                foreach (var item in resultadoDeValidacion.Errors)
                {
                    Error += item.ErrorMessage + ".\n ";
                }
                return false;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
    }
}
