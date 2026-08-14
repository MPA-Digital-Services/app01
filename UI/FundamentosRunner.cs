using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App01.UI
{
    public static class FundamentosRunner
    {
        public static void RunMenu()
        {
            while (true)
            {
                ConsoleHelper.ShowHeader("DEMOSTRACIÓN INTERACTIVA - FUNDAMENTOS DE C#");
                Console.WriteLine(" 1. Tipos Primitivos y Nullables");
                Console.WriteLine(" 2. Arrays y LINQ");
                Console.WriteLine(" 3. Operaciones con Cadenas (String)");
                Console.WriteLine(" 4. StringBuilder y Rendimiento");
                Console.WriteLine(" 5. DateTime y TimeSpan");
                Console.WriteLine(" 6. Formateo Numérico y Culturas");
                Console.WriteLine(" 7. Expresiones Regulares (Regex)");
                Console.WriteLine(" 8. Control de Flujo y Switch Expressions");
                Console.WriteLine(" 9. Clases, Propiedades y Métodos");
                Console.WriteLine("10. Parámetros ref y out");
                Console.WriteLine("11. Delegados y Eventos");
                Console.WriteLine("12. struct y enum");
                Console.WriteLine(" 0. Volver al Menú Principal");
                Console.WriteLine();

                int op = ConsoleHelper.ReadInt("Seleccione una opción", 0, 12);
                if (op == 0) break;

                Console.Clear();
                switch (op)
                {
                    case 1: DemoPrimitivos(); break;
                    case 2: DemoArrays(); break;
                    case 3: DemoStrings(); break;
                    case 4: DemoStringBuilder(); break;
                    case 5: DemoDateTime(); break;
                    case 6: DemoFormateo(); break;
                    case 7: DemoRegex(); break;
                    case 8: DemoControlFlujo(); break;
                    case 9: DemoClases(); break;
                    case 10: DemoRefOut(); break;
                    case 11: DemoDelegadosEventos(); break;
                    case 12: DemoStructEnum(); break;
                }
                ConsoleHelper.Pause();
            }
        }

        private static void DemoPrimitivos()
        {
            ConsoleHelper.ShowHeader("1. Tipos Primitivos y Nullables");
            int entero = 100;
            decimal salario = 2500.50M;
            double temp = 36.6;
            bool activo = true;
            int? edadAnulable = null;

            Console.WriteLine($"int: {entero}, decimal: {salario:C2}, double: {temp}, bool: {activo}");
            Console.WriteLine($"Nullable int HasValue?: {edadAnulable.HasValue} (Valor por defecto: {edadAnulable.GetValueOrDefault(-1)})");
        }

        private static void DemoArrays()
        {
            ConsoleHelper.ShowHeader("2. Arrays y Operaciones LINQ");
            int[] valores = { 89, 12, 45, 67, 3, 99, 23 };
            Array.Sort(valores);
            Console.WriteLine($"Array Ordenado: {string.Join(", ", valores)}");
            Console.WriteLine($"Valor Máximo: {valores.Max()}, Promedio: {valores.Average():F2}");
        }

        private static void DemoStrings()
        {
            ConsoleHelper.ShowHeader("3. Operaciones con String");
            string frase = "  Aprender C# con .NET 8 es Genial  ";
            Console.WriteLine($"Original: '{frase}'");
            Console.WriteLine($"Trim & Substring: '{frase.Trim().Substring(0, 11)}'");
            Console.WriteLine($"Replace: '{frase.Replace("Genial", "Excelente")}'");
        }

        private static void DemoStringBuilder()
        {
            ConsoleHelper.ShowHeader("4. Uso de StringBuilder");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== REPORTE GENERADO CON STRINGBUILDER ===");
            for (int i = 1; i <= 3; i++)
            {
                sb.AppendLine($"Fila #{i}: Procesado a las {DateTime.Now:HH:mm:ss.fff}");
            }
            Console.WriteLine(sb.ToString());
        }

        private static void DemoDateTime()
        {
            ConsoleHelper.ShowHeader("5. DateTime y TimeSpan");
            DateTime ahora = DateTime.Now;
            DateTime finDeAño = new DateTime(ahora.Year, 12, 31);
            TimeSpan faltante = finDeAño - ahora;
            Console.WriteLine($"Fecha Actual: {ahora:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Días faltantes para fin de año: {faltante.Days} días y {faltante.Hours} horas.");
        }

        private static void DemoFormateo()
        {
            ConsoleHelper.ShowHeader("6. Formateo Numérico");
            decimal monto = 1250500.75M;
            Console.WriteLine($"Moneda (C): {monto:C}");
            Console.WriteLine($"Número con separador (N): {monto:N2}");
            Console.WriteLine($"Porcentaje (P): {(0.155M):P2}");
        }

        private static void DemoRegex()
        {
            ConsoleHelper.ShowHeader("7. Expresiones Regulares");
            string email = "estudiante.csharp@dominio.com";
            string patron = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            bool esValido = Regex.IsMatch(email, patron);
            Console.WriteLine($"Evaluando Email '{email}': {(esValido ? "VÁLIDO" : "INVÁLIDO")}");
        }

        private static void DemoControlFlujo()
        {
            ConsoleHelper.ShowHeader("8. Control de Flujo - Switch Expression");
            int dia = (int)DateTime.Now.DayOfWeek;
            string tipoDia = dia switch
            {
                0 or 6 => "Fin de Semana",
                _ => "Día Laboral"
            };
            Console.WriteLine($"Hoy es día #{dia} del ciclo semanal: {tipoDia}.");
        }

        private static void DemoClases()
        {
            ConsoleHelper.ShowHeader("9. Clases y Encapsulamiento");
            var p = new App01.Core.Entities.Product { ProductId = 1, ProductName = "Monitor 4K", UnitPrice = 450.00M, UnitsInStock = 10 };
            Console.WriteLine(p.ToString());
        }

        private static void DemoRefOut()
        {
            ConsoleHelper.ShowHeader("10. Parámetros ref y out");
            int val = 20;
            DuplicarRef(ref val);
            ObtenerDimensionesOut(out int ancho, out int alto);
            Console.WriteLine($"Valor ref duplicado: {val}");
            Console.WriteLine($"Valores out recibidos: Ancho={ancho}, Alto={alto}");
        }

        private static void DuplicarRef(ref int n) => n *= 2;
        private static void ObtenerDimensionesOut(out int width, out int height)
        {
            width = 1920;
            height = 1080;
        }

        private static void DemoDelegadosEventos()
        {
            ConsoleHelper.ShowHeader("11. Delegados y Eventos");
            Action<string> notificar = msg => Console.WriteLine($"[DELEGADO NOTIFICA] {msg}");
            notificar("Operación asíncrona completada.");
        }

        private static void DemoStructEnum()
        {
            ConsoleHelper.ShowHeader("12. struct y enum");
            Console.WriteLine($"Enumeración Categoría: Bebidas (Valor: 1)");
            var punto = new Punto(10, 20);
            Console.WriteLine($"Estructura Punto: X={punto.X}, Y={punto.Y}");
        }

        private readonly struct Punto
        {
            public int X { get; }
            public int Y { get; }
            public Punto(int x, int y) { X = x; Y = y; }
        }
    }
}
