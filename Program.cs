using System;
using App01.Data;
using App01.Services;
using App01.UI;

namespace App01
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Configurar título y codificación de la consola
                Console.Title = "Tutorial C# desde Cero - Sistema de Gestión Northwind";
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                // 1. Inicializar la base de datos (SQLite Northwind)
                DatabaseInitializer.InitializeDatabase();

                // 2. Inyección de Dependencias Manual (Inyección por Constructor)
                var productRepository = new SqliteProductRepository();
                var customerRepository = new SqliteCustomerRepository();

                var productService = new ProductService(productRepository);
                var customerService = new CustomerService(customerRepository);

                // 3. Ejecutar Interfaz de Usuario
                var menu = new MainMenu(productService, customerService);
                menu.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR CRÍTICO NO CONTROLADO] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
                Console.WriteLine("\nPresione cualquier tecla para cerrar...");
                Console.ReadKey();
            }
        }
    }
}
