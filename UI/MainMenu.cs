using System;
using System.Collections.Generic;
using App01.Core.Entities;
using App01.Services;

namespace App01.UI
{
    public class MainMenu
    {
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;

        public MainMenu(ProductService productService, CustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;
        }

        public void Run()
        {
            while (true)
            {
                ConsoleHelper.ShowHeader("SISTEMA DE GESTIÓN NORTHWIND - C# CONSOLE APP");
                Console.WriteLine(" 1. Demostración Interactivas de Fundamentos (Capítulo 1)");
                Console.WriteLine(" 2. Listar Todos los Productos (SELECT)");
                Console.WriteLine(" 3. Buscar Producto por ID (SELECT)");
                Console.WriteLine(" 4. Insertar Nuevo Producto (INSERT)");
                Console.WriteLine(" 5. Actualizar Precio de Producto (UPDATE)");
                Console.WriteLine(" 6. Eliminar Producto (DELETE)");
                Console.WriteLine(" 7. Listar Clientes por País (SELECT)");
                Console.WriteLine(" 8. Registrar Nuevo Cliente (INSERT)");
                Console.WriteLine(" 0. Salir de la Aplicación");
                Console.WriteLine();

                int op = ConsoleHelper.ReadInt("Seleccione una opción del menú", 0, 8);
                if (op == 0)
                {
                    ConsoleHelper.ShowInfo("Gracias por usar el tutorial de C#. ¡Hasta pronto!");
                    break;
                }

                try
                {
                    switch (op)
                    {
                        case 1: FundamentosRunner.RunMenu(); break;
                        case 2: ListarProductos(); break;
                        case 3: BuscarProductoPorId(); break;
                        case 4: InsertarProducto(); break;
                        case 5: ActualizarPrecioProducto(); break;
                        case 6: EliminarProducto(); break;
                        case 7: ListarClientesPorPais(); break;
                        case 8: RegistrarCliente(); break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError($"Excepción capturada en la capa UI: {ex.Message}");
                }

                if (op != 1)
                {
                    ConsoleHelper.Pause();
                }
            }
        }

        private void ListarProductos()
        {
            ConsoleHelper.ShowHeader("PRODUCTOS DE NORTHWIND");
            var productos = _productService.Obtenertodos();

            MostrarTablaProductos(productos);
        }

        private void BuscarProductoPorId()
        {
            ConsoleHelper.ShowHeader("BUSCAR PRODUCTO POR ID");
            int id = ConsoleHelper.ReadInt("Ingrese el ID del producto", 1);

            var prod = _productService.ObtenerPorId(id);
            if (prod == null)
            {
                ConsoleHelper.ShowError($"El producto con ID #{id} no existe.");
                return;
            }

            MostrarTablaProductos(new List<Product> { prod });
        }

        private void InsertarProducto()
        {
            ConsoleHelper.ShowHeader("REGISTRAR NUEVO PRODUCTO");
            string nombre = ConsoleHelper.ReadString("Nombre del Producto");
            decimal precio = ConsoleHelper.ReadDecimal("Precio Unitario", 0.01M);
            int catId = ConsoleHelper.ReadInt("ID Categoría (1:Bebidas, 2:Condimentos, 3:Repostería, 4:Lácteos)", 1, 4);
            int stock = ConsoleHelper.ReadInt("Stock Inicial", 0, 10000);

            var (exito, mensaje, nuevoId) = _productService.CrearProducto(nombre, precio, catId, (short)stock);

            if (exito)
                ConsoleHelper.ShowSuccess(mensaje);
            else
                ConsoleHelper.ShowError(mensaje);
        }

        private void ActualizarPrecioProducto()
        {
            ConsoleHelper.ShowHeader("ACTUALIZAR PRECIO DE PRODUCTO");
            int id = ConsoleHelper.ReadInt("ID del Producto a actualizar", 1);
            decimal nuevoPrecio = ConsoleHelper.ReadDecimal("Nuevo Precio Unitario", 0.01M);

            var (exito, mensaje) = _productService.ActualizarPrecio(id, nuevoPrecio);

            if (exito)
                ConsoleHelper.ShowSuccess(mensaje);
            else
                ConsoleHelper.ShowError(mensaje);
        }

        private void EliminarProducto()
        {
            ConsoleHelper.ShowHeader("ELIMINAR PRODUCTO");
            int id = ConsoleHelper.ReadInt("ID del Producto a eliminar", 1);

            var (exito, mensaje) = _productService.EliminarProducto(id);

            if (exito)
                ConsoleHelper.ShowSuccess(mensaje);
            else
                ConsoleHelper.ShowError(mensaje);
        }

        private void ListarClientesPorPais()
        {
            ConsoleHelper.ShowHeader("BUSCAR CLIENTES POR PAÍS");
            string pais = ConsoleHelper.ReadString("Ingrese el país (Ej: Germany, Mexico, France, UK)");

            var clientes = _customerService.BuscarPorPais(pais);
            if (clientes.Count == 0)
            {
                ConsoleHelper.ShowInfo($"No se encontraron clientes radicados en '{pais}'.");
                return;
            }

            Console.WriteLine($"{"ID",-7} | {"Compañía",-35} | {"Contacto",-22} | {"Ciudad",-15} | {"País",-12}");
            Console.WriteLine(new string('-', 98));

            foreach (var c in clientes)
            {
                Console.WriteLine($"{c.CustomerId,-7} | {c.CompanyName,-35} | {c.ContactName ?? "N/A",-22} | {c.City ?? "N/A",-15} | {c.Country ?? "N/A",-12}");
            }
        }

        private void RegistrarCliente()
        {
            ConsoleHelper.ShowHeader("REGISTRAR NUEVO CLIENTE");
            string id = ConsoleHelper.ReadString("ID Cliente (Exactamente 5 caracteres)", maxLength: 5);
            string company = ConsoleHelper.ReadString("Nombre de la Compañía");
            string contact = ConsoleHelper.ReadString("Nombre del Contacto", permitirVacio: true);
            string city = ConsoleHelper.ReadString("Ciudad", permitirVacio: true);
            string country = ConsoleHelper.ReadString("País", permitirVacio: true);

            var (exito, mensaje) = _customerService.RegistrarCliente(id, company, contact, city, country);

            if (exito)
                ConsoleHelper.ShowSuccess(mensaje);
            else
                ConsoleHelper.ShowError(mensaje);
        }

        private static void MostrarTablaProductos(List<Product> productos)
        {
            if (productos.Count == 0)
            {
                ConsoleHelper.ShowInfo("No hay productos registrados.");
                return;
            }

            Console.WriteLine($"{"ID",-5} | {"Nombre del Producto",-32} | {"Categoría",-18} | {"Precio",-10} | {"Stock",-6}");
            Console.WriteLine(new string('-', 80));

            foreach (var p in productos)
            {
                Console.WriteLine($"{p.ProductId,-5} | {p.ProductName,-32} | {p.CategoryName,-18} | {p.UnitPrice,10:C2} | {p.UnitsInStock,6}");
            }
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Total Registros: {productos.Count}");
        }
    }
}
