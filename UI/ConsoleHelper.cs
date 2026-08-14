using System;

namespace App01.UI
{
    public static class ConsoleHelper
    {
        public static void ShowHeader(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========================================================================");
            Console.WriteLine($"   {titulo.ToUpper()}");
            Console.WriteLine("==========================================================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowSuccess(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[ÉXITO] {mensaje}");
            Console.ResetColor();
        }

        public static void ShowError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR] {mensaje}");
            Console.ResetColor();
        }

        public static void ShowInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[INFO] {mensaje}");
            Console.ResetColor();
        }

        public static void Pause()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey(true);
        }

        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int val;
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out val) && val >= min && val <= max)
                    return val;

                ShowError($"Entrada inválida. Ingrese un entero entre {min} y {max}.");
            }
        }

        public static decimal ReadDecimal(string prompt, decimal min = 0M)
        {
            decimal val;
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();
                if (decimal.TryParse(input, out val) && val >= min)
                    return val;

                ShowError($"Entrada inválida. Ingrese un valor decimal >= {min}.");
            }
        }

        public static string ReadString(string prompt, bool permitirVacio = false, int maxLength = 100)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!permitirVacio && string.IsNullOrWhiteSpace(input))
                {
                    ShowError("Este campo es obligatorio.");
                    continue;
                }

                if (input.Length > maxLength)
                {
                    ShowError($"La longitud máxima permitida es de {maxLength} caracteres.");
                    continue;
                }

                return input;
            }
        }
    }
}
