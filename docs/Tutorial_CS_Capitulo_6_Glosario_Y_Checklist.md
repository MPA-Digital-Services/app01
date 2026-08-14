# Capítulo 6: Glosario Técnico y Checklist de Dominio C#

---

## 6.1 Glosario Técnico de C# y .NET

1. **CLR (Common Language Runtime)**: El motor de ejecución virtual de .NET que gestiona la ejecución de aplicaciones, manejo de memoria, seguridad y recolección de basura.
2. **IL / CIL (Intermediate Language)**: El código de lenguaje intermedio orientado a pila generado al compilar código C#, el cual posteriormente el JIT convierte a código máquina.
3. **JIT (Just-In-Time Compiler)**: Componente del CLR que traduce el código IL a instrucciones nativas de la arquitectura del procesador en tiempo de ejecución.
4. **Garbage Collector (GC)**: Administrador automático de memoria en .NET que libera memoria asignada a objetos en el *Heap* que ya no tienen referencias activas.
5. **Stack (Pila)**: Región de memoria rápida de acceso LIFO utilizada para almacenar variables locales y tipos por valor (`struct`, `enum`, `int`).
6. **Heap (Montículo)**: Región de memoria utilizada para la asignación dinámica de objetos de tipos por referencia (`class`, `string`, `array`).
7. **Value Type (Tipo por Valor)**: Tipo de dato cuyos datos se almacenan directamente en la memoria asignada (ej. `int`, `struct`, `bool`).
8. **Reference Type (Tipo por Referencia)**: Tipo de dato que almacena una referencia o puntero a la ubicación de los datos reales en el Heap (ej. `class`, `interface`, `delegate`).
9. **Boxing / Unboxing**: Proceso de convertir un tipo por valor a un objeto tipo por referencia (`object`) y viceversa. Produce impacto en rendimiento.
10. **Nullable<T> / `T?`**: Estructura que permite a un tipo por valor tomar el valor especial `null`.
11. **Auto-properties**: Sintaxis abreviada en C# para declarar propiedades sin declarar explícitamente un campo privado de respaldo (`public int Id { get; set; }`).
12. **Init-Only Properties**: Propiedades que solo se pueden asignar durante la creación del objeto o en su constructor (`public string Name { get; init; }`).
13. **Extension Methods**: Métodos estáticos que permiten agregar nuevos métodos a tipos existentes sin modificar el tipo original.
14. **Delegate (Delegado)**: Un puntero seguro a métodos con una firma determinada.
15. **Lambda Expression**: Sintaxis anónima y compacta para escribir delegados o funciones en línea (`x => x * 2`).
16. **LINQ (Language Integrated Query)**: Conjunto de sintaxis integrada en C# para realizar consultas declarativas sobre colecciones, bases de datos o XML.
17. **ADO.NET**: Conjunto de clases en la biblioteca de .NET para el acceso desacoplado y orientado a conexión a fuentes de datos SQL.
18. **ORM (Object-Relational Mapper)**: Herramienta de software que automatiza la conversión de datos entre tablas relacionales de BD y objetos C# (ej. Entity Framework Core).
19. **SqlDataReader**: Lector de avance rápido y solo lectura de resultados SQL en ADO.NET.
20. **SqlCommand**: Clase que ejecuta instrucciones SQL (SELECT, INSERT, UPDATE, DELETE) o procedimientos almacenados.
21. **SQL Injection**: Vulnerabilidad de seguridad donde datos maliciosos no sanitizados alteran la consulta SQL. Se previene usando `SqlParameter`.
22. **DbContext**: Clase central de Entity Framework Core que gestiona las conexiones con la base de datos y el seguimiento de cambios (*change tracking*).
23. **DbSet<T>**: Colección que representa una tabla en la base de datos dentro de EF Core.
24. **Pattern Matching**: Sintaxis que permite evaluar la estructura y tipo de objetos en expresiones `switch` o `if`.
25. **Async / Await**: Palabras clave en C# para programación asíncrona no bloqueante basada en el patrón TAP (*Task-based Asynchronous Pattern*).
26. **Task**: Objeto que representa una operación asíncrona que se está ejecutando o se ejecutará.
27. **Records (`record`)**: Tipo de referencia inmutable orientado a datos introducido en C# 9 con igualdad basada en valores.
28. **Tuplas (`ValueTuple`)**: Estructura liviana para agrupar múltiples valores de forma anónima `(int id, string nombre)`.
29. **Exception Handling**: Mecanismo mediante `try-catch-finally` para capturar y controlar errores de tiempo de ejecución.
30. **CSC (C# Compiler)**: El compilador nativo de la plataforma Roslyn que transforma archivos `.cs` a binarios ejecutable o librerías.

---

## 6.2 Checklist de Dominio del Lenguaje C#

Marca las casillas a medida que consolides tus conocimientos:

### Nivel 1: Principiante (Fundamentos y Sintaxis)
- [ ] Comprensión entre tipos por valor vs tipos por referencia.
- [ ] Declaración y uso adecuado de tipos primitivos (`int`, `decimal`, `bool`, `char`).
- [ ] Manipulación de cadenas de texto con interpolación (`$""`) y cadenas verbatim (`@""`).
- [ ] Uso eficiente de `StringBuilder` para concatenaciones masivas.
- [ ] Dominio de estructuras de control de flujo (`if`, `switch`, `for`, `foreach`, `while`).
- [ ] Definición de clases con constructores, propiedades auto-implementadas y métodos.
- [ ] Manejo básico de arreglos (`System.Array`) y colecciones genéricas (`List<T>`).
- [ ] Compilación y ejecución de aplicaciones de consola con .NET CLI (`dotnet run`).

### Nivel 2: Intermedio (Programación Orientada a Objetos y Colecciones)
- [ ] Aplicación de parámetros `ref` y `out` para paso por referencia.
- [ ] Definición e inmutabilidad con `struct` y enumeraciones `enum`.
- [ ] Manejo de fechas y duraciones con `DateTime`, `DateTimeOffset` y `TimeSpan`.
- [ ] Formateo personalizado de números y fechas mediante `CultureInfo`.
- [ ] Validación de entradas con expresiones regulares `Regex`.
- [ ] Manejo defensivo de errores con `try-catch-finally` y excepciones personalizadas.
- [ ] Uso de delegados integrados (`Action`, `Func`, `Predicate`) y eventos.
- [ ] Uso básico de consultas declarativas LINQ (`Where`, `Select`, `OrderBy`, `GroupBy`).

### Nivel 3: Avanzado (Persistencia de Datos y Arquitectura)
- [ ] Conexión a bases de datos con ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`).
- [ ] Prevención estricta de SQL Injection mediante `SqlParameter`.
- [ ] Implementación de operaciones CRUD completas (SELECT, INSERT, UPDATE, DELETE).
- [ ] Modelado de entidades y consultas con Entity Framework Core.
- [ ] Arquitectura en Capas (N-Tier/N-Layer) para separar UI, Servicios y Repositorios.
- [ ] Creación de interfaces y diseño orientado a componentes desacoplados (Inyección de Dependencias).
- [ ] Formateo visual y menús interactivos resilientes para aplicaciones de consola.
