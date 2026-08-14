# Capítulo 3: Conexión a Base de Datos Northwind (ADO.NET y Entity Framework Core)

Northwind es una base de datos relacional de ejemplo clásica desarrollada por Microsoft que representa las operaciones comerciales de una empresa importadora/exportadora de alimentos (clientes, proveedores, productos, pedidos, categorías).

En este capítulo aprenderás a interactuar con Northwind desde una aplicación de consola C# usando **ADO.NET** (la infraestructura de acceso a datos de bajo nivel en .NET) y opcionalmente **Entity Framework Core** (el ORM moderno de Microsoft).

---

## 3.1 Instalación y Preparación de la Base de Datos Northwind

### Opción A: SQL Server / SQL LocalDB / Express
1. Descarga el script de creación de Northwind (`instnwnd.sql`) desde el repositorio oficial de Microsoft.
2. Abre SQL Server Management Studio (SSMS) o la extensión de SQL Server en VS Code.
3. Ejecuta el script para crear la base de datos `Northwind`.
4. Tu cadena de conexión (*Connection String*) típica será:
   ```text
   Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;
   ```

### Opción B: SQLite Portátil (Ideal para Desarrollo Rápido / Multiplataforma)
Para garantizar que este tutorial sea 100% ejecutable de forma autónoma sin instalar SQL Server, utilizaremos **Microsoft.Data.Sqlite** con un archivo de base de datos SQLite pre-cargado (`northwind.db`).
Cadena de conexión SQLite:
```text
Data Source=northwind.db;
```

---

## 3.2 Acceso a Datos con ADO.NET Pure

ADO.NET utiliza las siguientes clases fundamentales del espacio de nombres `System.Data.SqlClient` o `Microsoft.Data.SqlClient`:

1. **`SqlConnection`**: Administra la conexión física con la base de datos.
2. **`SqlCommand`**: Representa la instrucción SQL o procedimiento almacenado a ejecutar.
3. **`SqlDataReader`**: Proporciona un lector de solo lectura y avance rápido sobre las filas devueltas.
4. **`SqlParameter`**: Permite inyectar parámetros de forma segura previniendo ataques de **SQL Injection**.

### Ejemplo de Consulta SELECT en ADO.NET:
```csharp
using Microsoft.Data.SqlClient; // O Microsoft.Data.Sqlite

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    string query = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock FROM Products WHERE UnitPrice > @PrecioMinimo";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        // Parámetro seguro
        command.Parameters.AddWithValue("@PrecioMinimo", 20.00M);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine($"{"ID",-5} | {"Nombre Producto",-35} | {"Precio",-10} | {"Stock",-6}");
            Console.WriteLine(new string('-', 65));

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                decimal precio = reader.GetDecimal(2);
                short stock = reader.GetInt16(3);

                Console.WriteLine($"{id,-5} | {nombre,-35} | {precio,10:C2} | {stock,6}");
            }
        }
    }
}
```

---

## 3.3 Consultas CRUD con ADO.NET

### 1. SELECT (Búsqueda por Filtro)
```csharp
public static Customer? BuscarClientePorId(string connectionString, string customerId)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    string sql = "SELECT CustomerID, CompanyName, ContactName, City, Country FROM Customers WHERE CustomerID = @id";
    
    using var cmd = new SqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("@id", customerId);

    using var reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        return new Customer
        {
            CustomerId = reader.GetString(0),
            CompanyName = reader.GetString(1),
            ContactName = reader.IsDBNull(2) ? null : reader.GetString(2),
            City = reader.IsDBNull(3) ? null : reader.GetString(3),
            Country = reader.IsDBNull(4) ? null : reader.GetString(4)
        };
    }
    return null;
}
```

### 2. INSERT (Inserción de Registros)
```csharp
public static int InsertarProducto(string connectionString, string nombre, decimal precio, int categoriaId)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    
    string sql = @"INSERT INTO Products (ProductName, UnitPrice, CategoryID, Discontinued) 
                   VALUES (@Nombre, @Precio, @CategoriaId, 0);
                   SELECT SCOPE_IDENTITY();"; // Retorna el ID autogenerado en SQL Server

    using var cmd = new SqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("@Nombre", nombre);
    cmd.Parameters.AddWithValue("@Precio", precio);
    cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);

    // ExecuteScalar retorna la primera columna de la primera fila
    object result = cmd.ExecuteScalar();
    return Convert.ToInt32(result);
}
```

### 3. UPDATE (Actualización de Precios)
```csharp
public static int ActualizarPrecioProducto(string connectionString, int productoId, decimal nuevoPrecio)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    string sql = "UPDATE Products SET UnitPrice = @NuevoPrecio WHERE ProductID = @Id";

    using var cmd = new SqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("@NuevoPrecio", nuevoPrecio);
    cmd.Parameters.AddWithValue("@Id", productoId);

    int filasAfectadas = cmd.ExecuteNonQuery();
    return filasAfectadas;
}
```

### 4. DELETE (Eliminación de Registros)
```csharp
public static bool EliminarProducto(string connectionString, int productoId)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    string sql = "DELETE FROM Products WHERE ProductID = @Id AND ProductID NOT IN (SELECT DISTINCT ProductID FROM [Order Details])";

    using var cmd = new SqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("@Id", productoId);

    int filasAfectadas = cmd.ExecuteNonQuery();
    return filasAfectadas > 0;
}
```

---

## 3.4 Opción Alternativa: Entity Framework Core (EF Core)

Entity Framework Core es el ORM (*Object-Relational Mapper*) oficial de .NET. Permite interactuar con la base de datos usando objetos C# fuertemente tipados y consultas **LINQ** sin escribir sentencias SQL manuales.

### Mapeo de Entidad con EF Core:
```csharp
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
    public bool Discontinued { get; set; }
}

public class NorthwindDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;");
    }
}
```

### Consulta LINQ con EF Core:
```csharp
using var db = new NorthwindDbContext();

// SELECT con filtro LINQ
var productosCaros = db.Products
    .Where(p => p.UnitPrice > 50 && !p.Discontinued)
    .OrderByDescending(p => p.UnitPrice)
    .ToList();
```

---

## 3.5 Ejercicios Prácticos del Capítulo 3

1. **Listar Productos**: Crea una función que liste todos los productos de una categoría específica dada por el usuario.
2. **Buscar Clientes por País**: Crea una función que devuelva todos los clientes radicados en un país determinado (ej. "USA", "Germany", "Mexico").
3. **Insertar Nuevo Cliente**: Implementa un formulario de consola para registrar un cliente ingresando su `CustomerID` (5 letras), `CompanyName` y `ContactName`.
4. **Actualizar Stock**: Modifica la cantidad de `UnitsInStock` de un producto existente.
5. **Eliminar Cliente Inactivo**: Elimina un cliente asegurándote de capturar la excepción si tiene pedidos asociados (`SqlException` por restricción de Foreign Key).
