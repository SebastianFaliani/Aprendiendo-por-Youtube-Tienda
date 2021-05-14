using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tienda.COMMON.Entidades;
using Tienda.COMMON.Intefaces;
using Tienda.COMMON.Validadores;
//using Tienda.DAL.MySQL;
using Tienda.DAL.MSSqlServer;

namespace Tienda.Test
{
    [TestClass]
    public class UnitTestDAL
    {
        IGenericRepository<producto> productosRepository;
        IGenericRepository<usuario> usuarioRepository;
        IGenericRepository<venta> ventaRepository;
        IGenericRepository<productovendido> productoVendidoRepository;
        Random r;
        
        public UnitTestDAL()
        {
            r = new Random(DateTime.Now.Second);
            productosRepository = new GenericRepository<producto>(new ProductoValidator(),true);
            usuarioRepository = new GenericRepository<usuario>(new UsuarioValidator(), false);
            ventaRepository = new GenericRepository<venta>(new VentaValidator(), true);
            productoVendidoRepository = new GenericRepository<productovendido>(new ProductoVendidoValidator(), true);
        }

        [TestMethod]
        public void TestProductos()
        {
            //Creamos un producto de prueba
            producto nuevoProducto = CrearProductoDePrueba();
            //Obtenemos la cantidad de productos actuales en la base de datos
            int cantidadProductos = productosRepository.Read.Count();
            //Creamos el producto
            Assert.IsTrue(productosRepository.Create(nuevoProducto), productosRepository.Error);
            //Verificamos que la cantidad de productos se incremento en uno despues de ingresar el nuevo producto
            Assert.AreEqual(cantidadProductos + 1, productosRepository.Read.Count(), "No se inserto el registro");
            //Obtenemos el Id del ultimo producto ingresado
            int ultimoId = productosRepository.Read.Max(j=>j.IdProducto);
            //Con ese Id Obtengo el registro por medio del metodo SearchById
            producto aModificar = productosRepository.SearchById(ultimoId.ToString()); 
            //Lo Modifico
            aModificar.Nombre = "Modificado";
            //Ahora lo actualizo atraves del metodo Update
            Assert.IsTrue(productosRepository.Update(aModificar), productosRepository.Error);
            //Con el Id Obtengo el registro modificado por medio del metodo SearchById
            producto modificado = productosRepository.SearchById(ultimoId.ToString());
            //Verifico que el producto de alla modificado correctamente
            Assert.AreEqual("Modificado", modificado.Nombre, "No se Modifico");
            //Elimino el producto por en Id atraves del metodo Delete
            Assert.IsTrue(productosRepository.Delete(ultimoId.ToString()), productosRepository.Error);
            //Verifico que de elimino comparando la cantidad de productos
            Assert.AreEqual(cantidadProductos, productosRepository.Read.Count(), "No se elimino");
        }

        [TestMethod]
        public void TestUsuarios()
        {
            //Creamos un usuario de prueba
            usuario nuevoUsuario = CrearUsuarioDePrueba();
            //Obtenemos la cantidad de usuario actuales en la base de datos
            int cantidadUsuarios = usuarioRepository.Read.Count();
            //Creamos el usuario
            Assert.IsTrue(usuarioRepository.Create(nuevoUsuario), usuarioRepository.Error);
            //Verificamos que la cantidad de usuario se incremento en uno despues de ingresar el nuevo usuario
            Assert.AreEqual(cantidadUsuarios + 1, usuarioRepository.Read.Count(), "No se inserto el registro");
            //Con ese Id Obtengo el registro por medio del metodo SearchById
            usuario aModificar = usuarioRepository.Read.Last();
            //Lo Modifico
            aModificar.Nombres = "Modificado";
            //Ahora lo actualizo atraves del metodo Update
            Assert.IsTrue(usuarioRepository.Update(aModificar), usuarioRepository.Error);
            //Con el Id Obtengo el registro modificado por medio del metodo SearchById
            usuario modificado = usuarioRepository.SearchById(aModificar.NombreDeUsuario);
            //Verifico que el usuario de alla modificado correctamente
            Assert.AreEqual("Modificado", modificado.Nombres, "No se Modifico");
            //Elimino el usuario por en Id atraves del metodo Delete
            Assert.IsTrue(usuarioRepository.Delete(aModificar.NombreDeUsuario), usuarioRepository.Error);
            //Verifico que de elimino comparando la cantidad de usuarios
            Assert.AreEqual(cantidadUsuarios, usuarioRepository.Read.Count(), "No se elimino");

        }

        [TestMethod]
        public void TestVentas()
        {
            usuario nuevoUsuario = CrearUsuarioDePrueba();
            usuarioRepository.Create(nuevoUsuario);
            venta nuevaVenta = CrearVentaDePrueba(nuevoUsuario.NombreDeUsuario);
            Assert.IsTrue (ventaRepository.Create(nuevaVenta),ventaRepository.Error);
            int idUltimaVenta = ventaRepository.Read.Max(v => v.IdVenta);
            nuevaVenta = ventaRepository.Read.Last();
            List<producto> ProductosDePrueba = new List<producto>();
            int cantidadProductosVendidos = productoVendidoRepository.Read.Count();
            for (int i = 0; i < 10; i++)
            {
                producto nuevoProducto = CrearProductoDePrueba();
                Assert.IsTrue(productosRepository.Create(nuevoProducto), productosRepository.Error);
                int ultimoId = productosRepository.Read.Max(p => p.IdProducto);
                producto UltimoProducto = productosRepository.SearchById(ultimoId.ToString());
                ProductosDePrueba.Add(UltimoProducto);
                productovendido vendido = new productovendido()
                {
                    IdVenta = idUltimaVenta,
                    Costo = UltimoProducto.Costo,
                    Cantidad =r.Next(1,10),
                    IdProducto=UltimoProducto.IdProducto
                };
                Assert.IsTrue(productoVendidoRepository.Create(vendido), productoVendidoRepository.Error);

            }
            Assert.AreEqual(cantidadProductosVendidos + 10, productoVendidoRepository.Read.Count(), "No se Insertaron los 10 Productos Vendidos");
            nuevaVenta.Cliente = "Modificado";
            Assert.IsTrue(ventaRepository.Update(nuevaVenta), ventaRepository.Error);
            List<productovendido> vendidos = new List<productovendido>(productoVendidoRepository.Query
                (p => p.IdVenta == idUltimaVenta).ToList());
            foreach (var item in vendidos)
            {
                Assert.IsTrue(productoVendidoRepository.Delete(item.IdProductoVendido.ToString()), 
                    productoVendidoRepository.Error);
            }
            Assert.AreEqual(cantidadProductosVendidos, productoVendidoRepository.Read.Count(), 
                "No se eliminaron los productos vendidos");
            Assert.IsTrue(ventaRepository.Delete(idUltimaVenta.ToString()), ventaRepository.Error);
            foreach (var item in ProductosDePrueba)
            {
                Assert.IsTrue(productosRepository.Delete(item.IdProducto.ToString()),
                    productoVendidoRepository.Error);
            }
            Assert.IsTrue(usuarioRepository.Delete(nuevoUsuario.NombreDeUsuario), usuarioRepository.Error);
        }

        private producto CrearProductoDePrueba()
        {
            //decimal p = (25 * 1.3M);
            return new producto()
            {
                Nombre = "Producto de Prueba " + r.Next(),
                Costo = (r.Next(1, 100)/3M)
            };
        }

        private usuario CrearUsuarioDePrueba()
        {
            return new usuario()
            {
                NombreDeUsuario = "PruebaUser" + r.Next(),
                Apellidos = "User",
                Nombres = "Prueba",
                Password = "123456"
            };
        }

        private venta CrearVentaDePrueba(string vendedor)
        {
            return new venta
            {
                FechaHora = DateTime.Now,
                NombreDeUsuario =vendedor,
                Cliente="Cliente de Prueba",
            };
        }
    }
}
