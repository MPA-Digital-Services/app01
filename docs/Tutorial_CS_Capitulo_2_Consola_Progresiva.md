# Capítulo 2: Construcción de una Aplicación de Consola Paso a Paso

En este capítulo aprenderás a estructurar y evolucionar una aplicación de consola profesional de C# pasando por las distintas fases del diseño de software: desde la captura e ingreso de datos hasta la modularización y el manejo seguro de errores.

---

## 2.1 Estructura del Proyecto

Una aplicación .NET moderna se organiza mediante un archivo de proyecto (`.csproj`) y archivos de código fuente (`.cs`).

### Estructura Recomendada de Carpetas:
```
c:\developer\app01\
├── app01.csproj          # Configuración del proyecto, runtime y paquetes
├── Program.cs            # Punto de entrada (Main)
├── Models/               # Clases de dominio y datos (Persona, Producto, etc.)
├── Services/             # Lógica de negocio y procesamiento
├── Helpers/              # Utilidades de consola y validadores
└── docs/                 # Documentación del proyecto
```

---

## 2.2 Lectura e Interacción de Datos desde Consola

El espacio de nombres `System` proporciona la clase estática `Console` para operaciones de entrada y salida (I/O).

### Métodos Principales:
- `Console.WriteLine(valor)`: Imprime texto y agrega un salto de línea.
- `Console.Write(valor)`: Imprime texto en la misma línea.
- `Console.ReadLine()`: Lee una línea completa ingresada por el usuario como `string`.
- `Console.ReadKey()`: Captura la siguiente tecla presionada (útil para pausar la ejecución o leer contraseñas).
- `Console.Clear()`: Limpia la pantalla de la consola.

```csharp
Console.Write("Ingrese su usuario: ");
string? usuario = Console.ReadLine();

Console.Write("Ingrese su clave: ");
string clave = "";
while (true)
{
    var key = Console.ReadKey(intercept: true);
    if (key.Key == ConsoleKey.Enter) break;
    if (key.Key == ConsoleKey.Backspace && clave.Length > 0)
    {
        clave = clave[..^1];
        Console.Write("\b \b");
    }
    else if (!char.IsControl(key.KeyChar))
    {
        clave += key.KeyChar;
        Console.Write("*");
    }
}
Console.WriteLine("\nAutenticando...");
```

---

## 2.3 Validaciones de Entrada de Datos

Nunca debes confiar en la entrada del usuario. Al convertir cadenas a tipos numéricos o fechas, el método `Parse` arroja una excepción si el formato es incorrecto. El patrón recomendado es usar **`TryParse`**.

### Ejemplo de Patrón de Lectura Segura:
```csharp
public static int LeerEntero(string mensaje, int min, int max)
{
    int resultado;
    do
    {
        Console.Write($"{mensaje} ({min}-{max}): ");
        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out resultado) && resultado >= min && resultado <= max)
        {
            return resultado;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] Entrada inválida. Debe ser un entero entre {min} y {max}.");
        Console.ResetColor();

    } while (true);
}
```

---

## 2.4 Uso de Métodos y Clases

Para evitar crear bloques monolíticos de código dentro del método `Main`, la lógica debe dividirse en clases y métodos con **Responsabilidad Única (SRP - Single Responsibility Principle)**.

### Separación de Responsabilidades:
1. **Entidades**: Representan la información (`Estudiante`, `Cliente`).
2. **Servicios**: Realizan operaciones (`EstudianteService`).
3. **Interfaz de Usuario (UI)**: Gestiona la interacción en consola.

---

## 2.5 Manejo de Excepciones

El manejo defensivo de errores mediante bloques `try-catch-finally` previene que la aplicación falle inesperadamente ante condiciones no deseadas (ej. falta de memoria, errores de red, o formato incorrecto).

```csharp
try
{
    int resultado = 100 / int.Parse(Console.ReadLine() ?? "0");
    Console.WriteLine($"Resultado: {resultado}");
}
catch (FormatException ex)
{
    Console.WriteLine($"[ERROR DE FORMATO] Debe ingresar solo números: {ex.Message}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("[ERROR MATEMÁTICO] No se puede dividir entre cero.");
}
catch (Exception ex) // Captura genérica final
{
    Console.WriteLine($"[ERROR INESPERADO] {ex.Message}");
}
finally
{
    Console.WriteLine("Operación finalizada.");
}
```

---

## 2.6 Modularización y Reutilización del Código

La modularización implica empaquetar código reutilizable en métodos estáticos o de instancia dentro de clases dedicadas. 

### Ejercicio Práctico del Capítulo 2
**Consigna**: Crea un módulo de consola interactivo que permita registrar los datos de un empleado (Nombre, Edad, Salario), aplique validaciones con `TryParse` a cada campo y muestre una ficha resumida en consola con formato de colores.
