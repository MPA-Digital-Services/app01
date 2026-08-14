# Capítulo 5: Solucionario de Ejercicios Prácticos

En este capítulo se incluyen las soluciones completas, explicadas y funcionales para todos los ejercicios propuestos a lo largo del tutorial.

---

## 5.1 Soluciones del Capítulo 1

### Ejercicio 1.1: Tipos de Datos y Operaciones Numéricas
```csharp
using System;

class Ejercicio1_1
{
    static void Main()
    {
        decimal p1 = 120.50M;
        decimal p2 = 45.00M;
        decimal p3 = 89.99M;

        decimal subtotal = p1 + p2 + p3;
        decimal totalConDescuento = subtotal * 0.85M;
        int totalEntero = (int)totalConDescuento;

        Console.WriteLine($"Subtotal: ${subtotal:F2}");
        Console.WriteLine($"Total con 15% Desc: ${totalConDescuento:F2}");
        Console.WriteLine($"Total Convertido a int: {totalEntero}");
    }
}
```

### Ejercicio 1.2: Filtrado y Promedio de Arreglos
```csharp
using System;
using System.Linq;

class Ejercicio1_2
{
    static void Main()
    {
        int[] notas = { 85, 92, 60, 45, 78, 95, 30 };

        Array.Sort(notas);
        double promedio = notas.Average();
        int[] aprobadas = notas.Where(n => n >= 60).ToArray();

        Console.WriteLine($"Notas ordenadas: {string.Join(", ", notas)}");
        Console.WriteLine($"Promedio: {promedio:F2}");
        Console.WriteLine($"Aprobadas: {string.Join(", ", aprobadas)}");
    }
}
```

### Ejercicio 1.3: Limpieza y Extracción de Cadenas
```csharp
using System;

class Ejercicio1_3
{
    static void Main()
    {
        string entrada = "Juan Perez, 30 años, Desarrollador C#";
        string[] partes = entrada.Split(',');

        string nombre = partes[0].Trim();
        string edad = partes[1].Trim();
        string rol = partes[2].Trim();

        Console.WriteLine($"Nombre: {nombre} | Edad: {edad} | Rol: {rol}");
    }
}
```

### Ejercicio 1.4: Pirámide con StringBuilder
```csharp
using System;
using System.Text;

class Ejercicio1_4
{
    public static string GenerarPiramide(int n)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= n; i++)
        {
            sb.AppendLine(new string('*', i));
        }
        return sb.ToString();
    }

    static void Main()
    {
        Console.WriteLine(GenerarPiramide(5));
    }
}
```

### Ejercicio 1.5: Cálculo de Próximo Cumpleaños
```csharp
using System;

class Ejercicio1_5
{
    static void Main()
    {
        DateTime nacimiento = new DateTime(1998, 10, 15);
        DateTime hoy = DateTime.Today;
        DateTime proximoCumple = new DateTime(hoy.Year, nacimiento.Month, nacimiento.Day);

        if (proximoCumple < hoy)
            proximoCumple = proximoCumple.AddYears(1);

        int diasFaltantes = (proximoCumple - hoy).Days;
        bool esBisiesto = DateTime.IsLeapYear(hoy.Year);

        Console.WriteLine($"Días para próximo cumpleaños: {diasFaltantes}");
        Console.WriteLine($"¿Año {hoy.Year} es bisiesto?: {esBisiesto}");
    }
}
```

---

## 5.2 Soluciones del Capítulo 3 (SQL / ADO.NET)

### Ejercicio 3.1: Consultar Productos por Categoría
```csharp
using System;
using Microsoft.Data.Sqlite;

class Ejercicio3_1
{
    public static void ListarProductosPorCategoria(string connectionString, int categoryId)
    {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        string sql = "SELECT ProductID, ProductName, UnitPrice FROM Products WHERE CategoryID = @catId";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@catId", categoryId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader.GetInt32(0)} | Nombre: {reader.GetString(1)} | Precio: ${reader.GetDecimal(2):F2}");
        }
    }
}
```
