# Capítulo 1: Fundamentos del Lenguaje C# y .NET

Bienvenido al tutorial de C# desde cero. Este capítulo cubre los conceptos fundamentales del lenguaje basados en la especificación *Core C# and .NET Quick Reference*. Cada tema se presenta con su explicación teórica, un ejemplo mínimo funcional, un ejercicio práctico y sus mejores prácticas.

---

## 1.1 Tipos de Datos Primitivos

### Explicación Conceptual
En C#, todos los tipos de datos se dividen en dos categorías principales: **Tipos por Valor** (Value Types) y **Tipos por Referencia** (Reference Types). 
Los tipos primitivos por valor se almacenan en el *Stack* (memoria de acceso rápido) e incluyen:
- **Enteros**: `byte` (8 bits), `short` (16 bits), `int` (32 bits), `long` (64 bits).
- **Punto Flotante**: `float` (32 bits, precisión de 7 dígitos), `double` (64 bits, precisión de 15-17 dígitos).
- **Moneda / Alta Precisión**: `decimal` (128 bits, precisión de 28-29 dígitos, ideal para cálculos financieros).
- **Booleanos y Caracteres**: `bool` (`true`/`false`), `char` (un carácter Unicode de 16 bits).

C# es un lenguaje de **tipado fuerte y estático**. Admite conversión implícita (sin pérdida de datos) y explícita (mediante *casting* `(tipo)`). Además, permite representar ausencia de valor mediante nulos en tipos por valor usando `Nullable<T>` o su sintaxis corta `T?`.

### Ejemplo Mínimo Funcional
```csharp
using System;

class Program
{
    static void Main()
    {
        // Declaraciones explícitas e inferidas (var)
        int edad = 25;
        long poblacionMundial = 8_000_000_000L; // Sufijo L para long y separador '_'
        double temperatura = 36.6;
        decimal salario = 1550.75M; // Sufijo M para decimal
        bool esActivo = true;
        char genero = 'M';
        
        // Conversión explícita (Casting)
        int salarioEntero = (int)salario;

        // Tipo Anulable (Nullable)
        int? edadOpcional = null;

        Console.WriteLine($"Edad: {edad}, Salario entero: {salarioEntero}");
        Console.WriteLine($"Tiene edad?: {edadOpcional.HasValue}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Crea un programa que declare el precio de tres productos en tipo `decimal`, calcule el total con un descuento del 15% (multiplicando por `0.85M`), convierta el resultado final a `int` mediante casting explícito y muestre en consola tanto el precio exacto como el precio redondeado/convertido.

### Buenas Prácticas
- Usa `decimal` para cálculos financieros o monetarios. Nunca utilices `float` o `double` para dinero debido a los errores de redondeo binario.
- Usa el separador de dígitos `_` en números grandes para mejorar la legibilidad (ej. `1_000_000`).
- Prefiere la inferencia de tipos `var` cuando el tipo sea evidente en la asignación derecha (ej. `var clientes = new List<string>();`).

---

## 1.2 Arrays y Operaciones con Arrays

### Explicación Conceptual
Un **Array** es una estructura de datos de tamaño fijo que almacena una colección secuencial de elementos del mismo tipo. En C#, los arrays son tipos por referencia derivados de `System.Array`.
Existen tres variantes principales:
1. **Unidimensionales**: `int[] numeros = new int[5];`
2. **Multidimensionales (rectangulares)**: `int[,] matriz = new int[3, 3];`
3. **Arrays Dentados (Jagged / Arrays de Arrays)**: `int[][] tabla = new int[3][];`

### Ejemplo Mínimo Funcional
```csharp
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] valores = { 45, 12, 89, 3, 27 };

        // Ordenamiento
        Array.Sort(valores);
        Console.WriteLine("Array ordenado: " + string.Join(", ", valores));

        // Búsqueda lineal y binaria
        int indice = Array.BinarySearch(valores, 27);
        Console.WriteLine($"El número 27 está en el índice: {indice}");

        // Consultas con LINQ
        int maximo = valores.Max();
        int[] pares = valores.Where(n => n % 2 == 0).ToArray();

        Console.WriteLine($"Valor máximo: {maximo}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Dado un arreglo de calificaciones de estudiantes `int[] notas = { 85, 92, 60, 45, 78, 95, 30 };`, escribe un algoritmo que:
1. Ordene el arreglo de menor a mayor.
2. Calcule el promedio de las notas.
3. Genere un nuevo arreglo únicamente con las notas aprobatorias (mayores o iguales a 60) usando LINQ o un bucle.

### Buenas Prácticas
- Utiliza `List<T>` de `System.Collections.Generic` si la colección necesita cambiar de tamaño dinámicamente. Reserva arrays para tamaños fijos o alto rendimiento.
- Utiliza `Array.Empty<T>()` en lugar de asignar un arreglo de tamaño 0 (`new T[0]`) para evitar asignaciones innecesarias en memoria.

---

## 1.3 Operaciones con Cadenas (String)

### Explicación Conceptual
En C#, `string` es un alias para `System.String`. Las cadenas son **inmutables**: cualquier modificación realizada sobre una cadena no altera la original, sino que crea un nuevo objeto en memoria (*Heap*).
C# ofrece características avanzadas para el manejo de cadenas:
- **Interpolación de Cadenas**: `$"{variable}"`
- **Cadenas Verbatim**: `@"C:\Ruta\Archivo.txt"` (ignora secuencias de escape como `\n` o `\t`)
- **Cadenas Raw (C# 11+)**: `"""Texto en varias líneas"""`
- Métodos comunes: `Substring`, `Contains`, `Replace`, `Split`, `Trim`, `ToUpper`, `ToLower`, `string.Join`.

### Ejemplo Mínimo Funcional
```csharp
using System;

class Program
{
    static void Main()
    {
        string mensaje = "  Hola Mundo C# .NET  ";
        
        // Limpieza e interpolación
        string limpio = mensaje.Trim();
        string ruta = @"C:\Proyectos\CSharp\Main.cs";
        
        // Reemplazo y división
        string reemplazado = limpio.Replace("Mundo", "Desarrollador");
        string[] palabras = limpio.Split(' ');

        Console.WriteLine($"Original limpio: '{limpio}'");
        Console.WriteLine($"Reemplazado: '{reemplazado}'");
        Console.WriteLine($"Palabras encontradas: {palabras.Length}");
        
        // Comparación segura ignorando mayúsculas/minúsculas
        bool sonIguales = string.Equals(limpio, "HOLA MUNDO C# .NET", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine($"¿Son iguales?: {sonIguales}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Dada la cadena de entrada `"Juan Perez, 30 años, Desarrollador C#"`, crea un programa que extraiga el nombre, la edad y la profesión separando los elementos por la coma `,`, limpie los espacios sobrantes y muestre un saludo formateado como: `"Nombre: Juan Perez | Edad: 30 años | Rol: Desarrollador C#"`.

### Buenas Prácticas
- Para comparar cadenas ignorando mayúsculas y minúsculas, utiliza siempre `string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase)` en lugar de convertir ambas cadenas a `ToLower()` o `ToUpper()`.

---

## 1.4 Uso de StringBuilder

### Explicación Conceptual
Debido a que `string` es inmutable, realizar múltiples concatenaciones dentro de un bucle genera una alta cantidad de objetos basura que cargan al *Garbage Collector* (GC). 
`StringBuilder` (ubicado en `System.Text`) proporciona un búfer de caracteres mutable que permite modificar, agregar o eliminar texto de manera extremadamente eficiente sin reasignar memoria constantemente.

### Ejemplo Mínimo Funcional
```csharp
using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder("Reporte de Ventas:\n");
        sb.AppendLine("------------------------");

        for (int i = 1; i <= 5; i++)
        {
            sb.AppendFormat("Item #{0}: ${1:F2}\n", i, i * 15.5);
        }

        sb.Insert(0, "=== ENCABEZADO PRINCIPAL ===\n");
        sb.Replace("Item", "Producto");

        string resultadoFinal = sb.ToString();
        Console.WriteLine(resultadoFinal);
    }
}
```

### Ejercicio Práctico
**Consigna**: Escribe una función que reciba un número entero `N` y construya mediante `StringBuilder` un patrón en forma de pirámide de asteriscos de `N` filas (ej. para N=3: fila 1 `"*"`, fila 2 `"**"`, fila 3 `"***"`). Muestra el tiempo transcurrido o el resultado generado.

### Buenas Prácticas
- Utiliza `StringBuilder` siempre que vayas a concatenar cadenas en bucles o en más de 3 o 4 iteraciones complejas.
- Si conoces de antemano la longitud aproximada del texto, inicializa el `StringBuilder` especificando su capacidad inicial: `new StringBuilder(1024);`.

---

## 1.5 DateTime y TimeSpan

### Explicación Conceptual
- `DateTime`: Representa una fecha y hora específicas (desde el 1 de enero del año 0001 hasta el 31 de diciembre de 9999).
- `TimeSpan`: Representa una duración o intervalo de tiempo (diferencia entre dos `DateTime`).
- `DateTimeOffset`: Representa fecha y hora junto con un desplazamiento con respecto a UTC (Coordinated Universal Time), ideal para aplicaciones distribuidas.

### Ejemplo Mínimo Funcional
```csharp
using System;

class Program
{
    static void Main()
    {
        DateTime ahora = DateTime.Now;
        DateTime fechaNacimiento = new DateTime(1995, 8, 20);

        // Operaciones y cálculo de TimeSpan
        TimeSpan edadTranscurrida = ahora - fechaNacimiento;
        double diasVividos = edadTranscurrida.TotalDays;

        DateTime fechaFutura = ahora.AddDays(30).AddHours(2);

        Console.WriteLine($"Fecha Actual: {ahora:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"Días vividos aproximados: {(int)diasVividos}");
        Console.WriteLine($"Fecha futura (+30d 2h): {fechaFutura}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Escribe un programa que solicite al usuario su fecha de cumpleaños (ejemplo `YYYY-MM-DD`), calcule cuántos días faltan para su próximo cumpleaños y determine si el año actual es bisiesto usando `DateTime.IsLeapYear`.

### Buenas Prácticas
- Utiliza `DateTime.UtcNow` o `DateTimeOffset` al almacenar fechas en bases de datos o servicios backend para evitar ambigüedades por zonas horarias locales.

---

## 1.6 Formateo Numérico y de Fechas

### Explicación Conceptual
C# provee una sintaxis unificada para formatear valores numéricos y fechas mediante especificadores de formato estándar y personalizados:
- **Números**:
  - `C` o `c`: Moneda (*Currency*)
  - `N` o `n`: Número con separador de miles
  - `P` o `p`: Porcentaje
  - `F2`: Punto fijo con 2 decimales
  - `X`: Hexadecimal
- **Fechas**:
  - `d`: Fecha corta
  - `D`: Fecha larga
  - `yyyy-MM-dd HH:mm:ss`: Formato personalizado ISO 8601

### Ejemplo Mínimo Funcional
```csharp
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        decimal monto = 125400.50M;
        DateTime hoy = DateTime.Now;

        // Formateo según cultura específica (EE.UU. vs España)
        string montoUS = monto.ToString("C", new CultureInfo("en-US"));
        string montoES = monto.ToString("C", new CultureInfo("es-ES"));

        string fechaFormateada = hoy.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));

        Console.WriteLine($"Monto US: {montoUS}");
        Console.WriteLine($"Monto ES: {montoES}");
        Console.WriteLine($"Fecha en Español: {fechaFormateada}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Crea una rutina que formatee un número decimal `0.185` como porcentaje (`18.50%`), un número entero `1500000` como moneda en dólares (`$1,500,000.00`) y la fecha actual en formato `yyyy/MM/dd HH:mm`.

### Buenas Prácticas
- Pasa explícitamente `CultureInfo.InvariantCulture` cuando generes cadenas destinadas a persistencia o APIs (para evitar que en algunos países cambien comas por puntos).

---

## 1.7 Expresiones Regulares con Regex

### Explicación Conceptual
El espacio de nombres `System.Text.RegularExpressions` proporciona la clase `Regex` para realizar búsquedas, validaciones y reemplazos de patrones sobre cadenas.
Operaciones principales:
- `Regex.IsMatch(texto, patron)`: Retorna `true` si el patrón coincide.
- `Regex.Match(texto, patron)`: Retorna la primera coincidencia y sus grupos de captura.
- `Regex.Matches(texto, patron)`: Retorna todas las coincidencias.
- `Regex.Replace(texto, patron, reemplazo)`: Reemplaza coincidencias.

### Ejemplo Mínimo Funcional
```csharp
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string email = "usuario.ejemplo@dominio.com";
        string patronEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        bool esValido = Regex.IsMatch(email, patronEmail);
        Console.WriteLine($"¿Email válido?: {esValido}");

        // Extracción de números en un texto
        string texto = "El pedido #45821 tiene un costo de $350.";
        string patronNumero = @"\d+";

        Match match = Regex.Match(texto, patronNumero);
        if (match.Success)
        {
            Console.WriteLine($"Número de pedido encontrado: {match.Value}");
        }
    }
}
```

### Ejercicio Práctico
**Consigna**: Crea una expresión regular para validar un código de producto que debe cumplir la regla: **2 letras mayúsculas, un guion `-` y 4 dígitos numéricos** (Ejemplo válido: `"AB-1234"`). Escribe una función de prueba que valide 3 códigos.

### Buenas Prácticas
- En .NET 7+, utiliza el atributo `[GeneratedRegex]` en métodos parciales para que el compilador genere el código de la expresión regular a nivel de compilación, obteniendo un rendimiento óptimo sin overhead en tiempo de ejecución.

---

## 1.8 Control de Flujo

### Explicación Conceptual
C# incluye todas las estructuras estándar de control de flujo:
- **Selección**: `if / else if / else`, `switch` condicional y *Switch Expressions* (sintaxis moderna).
- **Bucles**: `for`, `foreach` (para iterar sobre `IEnumerable`), `while`, `do-while`.
- **Saltos**: `break`, `continue`, `return`.

### Ejemplo Mínimo Funcional
```csharp
using System;

class Program
{
    static void Main()
    {
        int opcion = 2;

        // Switch Expression (C# 8+)
        string estado = opcion switch
        {
            1 => "Iniciado",
            2 => "En Proceso",
            3 => "Completado",
            _ => "Desconocido"
        };
        Console.WriteLine($"Estado: {estado}");

        // Bucle foreach con filtro continue
        string[] frutas = { "Manzana", "Banana", "Cereza", "Durazno" };
        foreach (var fruta in frutas)
        {
            if (fruta.StartsWith("B")) continue;
            Console.WriteLine($"Fruta: {fruta}");
        }
    }
}
```

### Ejercicio Práctico
**Consigna**: Escribe un programa que utilice un bucle `for` para iterar del 1 al 30. Aplica la lógica *FizzBuzz*:
- Si el número es divisible por 3, imprime `"Fizz"`.
- Si es divisible por 5, imprime `"Buzz"`.
- Si es divisible por ambos, imprime `"FizzBuzz"`.
- En otro caso, imprime el número.

### Buenas Prácticas
- Favorece las *Switch Expressions* frente a las sentencias `switch` tradicionales cuando se trate de mapear un valor a un resultado, ya que son más compactas y legibles.

---

## 1.9 Clases, Constructores, Métodos y Propiedades

### Explicación Conceptual
C# es un lenguaje orientado a objetos basado en clases.
- **Campos (Fields)**: Variables privadas que guardan el estado.
- **Propiedades (Properties)**: Exponen los campos con encapsulamiento mediante `get` e `init`/`set`.
- **Constructores**: Métodos especiales para inicializar objetos.
- **Métodos**: Definen el comportamiento. Soporta miembros con cuerpo de expresión (`=>`).

### Ejemplo Mínimo Funcional
```csharp
using System;

public class Producto
{
    // Auto-properties con init-only (C# 9+)
    public int Id { get; init; }
    public string Nombre { get; set; }
    public decimal Precio { get; private set; }

    // Constructor
    public Producto(int id, string nombre, decimal precioInicial)
    {
        Id = id;
        Nombre = nombre;
        Precio = precioInicial > 0 ? precioInicial : throw new ArgumentException("Precio inválido");
    }

    // Método con expresión sintética
    public void AplicarDescuento(decimal porcentaje) => 
        Precio -= Precio * (porcentaje / 100M);

    public override string ToString() => $"[{Id}] {Nombre} - ${Precio:F2}";
}

class Program
{
    static void Main()
    {
        Producto p1 = new Producto(101, "Laptop Gaming", 1200.00M);
        p1.AplicarDescuento(10);
        Console.WriteLine(p1.ToString());
    }
}
```

### Ejercicio Práctico
**Consigna**: Diseña una clase `CuentaBancaria` con las propiedades `NumeroCuenta` (solo lectura), `Titular` y `Saldo` (privada en escritura). Incluye métodos `Depositar(decimal monto)` y `Retirar(decimal monto)` que validen que el saldo no quede negativo.

### Buenas Prácticas
- Mantén los campos de la clase siempre privados (`private`) y expón el acceso únicamente a través de propiedades.

---

## 1.10 Parámetros ref y out

### Explicación Conceptual
Por defecto, los argumentos en C# se pasan **por valor**. Las palabras clave `ref` y `out` permiten pasar argumentos **por referencia**:
- `ref`: El parámetro debe estar inicializado antes de enviarse al método. El método puede leerlo y modificarlo.
- `out`: El parámetro no necesita inicialización previa. El método **está obligado** a asignarle un valor antes de finalizar.
- `in`: Pasa por referencia pero de solo lectura (evita copias en estructuras grandes).

### Ejemplo Mínimo Funcional
```csharp
using System;

class Program
{
    // Uso de ref para modificar una variable externa
    static void Duplicar(ref int numero)
    {
        numero *= 2;
    }

    // Uso de out para retornar múltiples valores
    static bool Dividir(int dividendo, int divisor, out int cociente, out int resto)
    {
        if (divisor == 0)
        {
            cociente = 0;
            resto = 0;
            return false;
        }

        cociente = dividendo / divisor;
        resto = dividendo % divisor;
        return true;
    }

    static void Main()
    {
        int valor = 10;
        Duplicar(ref valor);
        Console.WriteLine($"Valor duplicado: {valor}");

        if (Dividir(17, 5, out int c, out int r))
        {
            Console.WriteLine($"Cociente: {c}, Resto: {r}");
        }
    }
}
```

### Ejercicio Práctico
**Consigna**: Escribe una función `CalcularEstadisticas(int[] numeros, out int min, out int max, out double promedio)` que procese un arreglo y devuelva simultáneamente el mínimo, el máximo y el promedio usando parámetros `out`.

### Buenas Prácticas
- Para retornos múltiples, considera utilizar **Tuplas** (`(int cociente, int resto) Dividir(...)`) en lugar de parámetros `out`, ya que producen código más limpio y composable en C# moderno.

---

## 1.11 Delegados y Eventos

### Explicación Conceptual
- **Delegado**: Tipo que representa referencias a métodos con una firma y tipo de retorno específicos. C# provee delegados genéricos integrados: `Action<T>` (sin retorno) y `Func<T, TResult>` (con retorno).
- **Evento**: Mecanismo que permite a una clase notificar a otras cuando ocurre algo relevante (patrón *Publicador-Suscriptor*). Se declaran con la palabra clave `event`.

### Ejemplo Mínimo Funcional
```csharp
using System;

public class ProcesadorPedidos
{
    // Definición de evento usando Action
    public event Action<string>? PedidoProcesado;

    public void Procesar(string idPedido)
    {
        Console.WriteLine($"Procesando pedido {idPedido}...");
        // Disparar el evento si hay suscriptores
        PedidoProcesado?.Invoke(idPedido);
    }
}

class Program
{
    static void Main()
    {
        ProcesadorPedidos procesador = new ProcesadorPedidos();

        // Suscripción al evento mediante expresión lambda
        procesador.PedidoProcesado += (id) => Console.WriteLine($"[NOTIFICACIÓN] El pedido {id} se completó con éxito.");

        procesador.Procesar("ORD-9982");
    }
}
```

### Ejercicio Práctico
**Consigna**: Crea una clase `Alarma` que contenga un evento `OnAlarmaDisparada`. Crea un método `Activar(int segundos)` que simule una cuenta regresiva y al finalizar invoque el evento. Desde la clase `Program`, suscríbete al evento e imprime un mensaje de alerta.

### Buenas Prácticas
- Siempre invoca los eventos usando el operador de navegación nula `?.Invoke(...)` para evitar excepciones de tipo `NullReferenceException` si no existen suscriptores.

---

## 1.12 struct y enum

### Explicación Conceptual
- **`enum`**: Un tipo por valor que define un conjunto de constantes compuestas por nombres descriptivos vinculados a valores enteros.
- **`struct`**: Un tipo por valor que se almacena en la pila (*Stack*). Es ideal para objetos pequeños, ligeros e inmutables que representan valores matemáticos o coordenadas (ej. `Point`, `Vector`, `Complex`).

### Ejemplo Mínimo Funcional
```csharp
using System;

public enum EstadoCivil
{
    Soltero = 1,
    Casado,
    Divorciado,
    Viudo
}

public readonly struct Punto2D
{
    public double X { get; }
    public double Y { get; }

    public Punto2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double DistanciaAlOrigen() => Math.Sqrt(X * X + Y * Y);
}

class Program
{
    static void Main()
    {
        EstadoCivil estado = EstadoCivil.Casado;
        Punto2D punto = new Punto2D(3, 4);

        Console.WriteLine($"Estado: {estado} (Valor entero: {(int)estado})");
        Console.WriteLine($"Distancia punto al origen: {punto.DistanciaAlOrigen()}");
    }
}
```

### Ejercicio Práctico
**Consigna**: Define un `enum` llamado `NivelAcceso` con los valores `Invitado`, `Usuario`, `Supervisor`, `Administrador`. Luego crea una estructura `readonly struct UsuarioSistema` que contenga el `Nombre` y el `NivelAcceso`. Escribe un programa que determine si el usuario tiene permisos de administración.

### Buenas Prácticas
- Haz que tus estructuras (`struct`) sean **inmutables** declarándolas como `readonly struct`. Esto evita efectos secundarios indeseados y mejora el rendimiento con el paso por referencia `in`.

---

## 1.13 Compilación desde Línea de Comandos con csc y .NET CLI

### Explicación Conceptual
C# puede compilarse directamente desde la consola mediante:
1. **Compilador tradicional (`csc.exe`)**: Compila directamente archivos de código `.cs` a archivos ejecutables `.exe` o librerías `.dll`.
2. **Herramienta moderna de línea de comandos (.NET CLI - `dotnet`)**: La herramienta estándar multiplataforma para crear, compilar y ejecutar proyectos .NET.

### Comandos Principales en la Consola

#### A. Compilación con `csc.exe` (Roslyn Compiler)
```bash
# Compilación simple de un archivo .cs a .exe
csc Program.cs

# Especificar nombre de salida y compilar múltiples archivos
csc /out:MiAplicacion.exe Program.cs Helpers.cs

# Compilar como librería DLL
csc /target:library /out:MiLibreria.dll Utilidades.cs
```

#### B. Flujo Estándar con .NET CLI (`dotnet`)
```bash
# Crear un nuevo proyecto de consola
dotnet new console -o MiProyectoConsola

# Restaurar dependencias del proyecto
dotnet restore

# Compilar el proyecto en modo Debug
dotnet build

# Compilar en modo Release para producción
dotnet build -c Release

# Compilar y ejecutar la aplicación inmediatamente
dotnet run
```

### Ejemplo Mínimo Funcional
Si creas un archivo sencillo llamado `HolaMundo.cs`:
```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("¡Hola desde la línea de comandos de C#!");
    }
}
```
Puedes ejecutar en tu consola de Windows / PowerShell:
```powershell
csc HolaMundo.cs
.\HolaMundo.exe
```

### Ejercicio Práctico
**Consigna**: Abre tu terminal de comandos en la carpeta del proyecto, ejecuta el comando `dotnet build` y analiza los archivos generados en las carpetas `/bin/Debug/net8.0/`. Identifica la diferencia entre la DLL del proyecto, el ejecutable `.exe` y el archivo de símbolos `.pdb`.

### Buenas Prácticas
- En proyectos profesionales modernos, utiliza siempre **.NET CLI (`dotnet`)** y archivos de proyecto `.csproj` en lugar de invocar `csc` manualmente, ya que gestiona automáticamente dependencias, paquetes NuGet y múltiples plataformas target.
