using Microsoft.Data.Sqlite;

namespace App01.Data
{
    public static class DatabaseInitializer
    {
        public const string ConnectionString = "Data Source=northwind.db;";

        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createTablesSql = @"
                CREATE TABLE IF NOT EXISTS Categories (
                    CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CategoryName TEXT NOT NULL,
                    Description TEXT
                );

                CREATE TABLE IF NOT EXISTS Products (
                    ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductName TEXT NOT NULL,
                    CategoryID INTEGER,
                    UnitPrice DECIMAL(18, 2) NOT NULL DEFAULT 0,
                    UnitsInStock INTEGER NOT NULL DEFAULT 0,
                    Discontinued INTEGER NOT NULL DEFAULT 0,
                    FOREIGN KEY(CategoryID) REFERENCES Categories(CategoryID)
                );

                CREATE TABLE IF NOT EXISTS Customers (
                    CustomerID TEXT PRIMARY KEY,
                    CompanyName TEXT NOT NULL,
                    ContactName TEXT,
                    ContactTitle TEXT,
                    City TEXT,
                    Country TEXT
                );
            ";

            using var cmd = new SqliteCommand(createTablesSql, connection);
            cmd.ExecuteNonQuery();

            // Seed data if empty
            SeedCategories(connection);
            SeedProducts(connection);
            SeedCustomers(connection);
        }

        private static void SeedCategories(SqliteConnection connection)
        {
            using var checkCmd = new SqliteCommand("SELECT COUNT(*) FROM Categories", connection);
            long count = (long)(checkCmd.ExecuteScalar() ?? 0);
            if (count > 0) return;

            string seedSql = @"
                INSERT INTO Categories (CategoryName, Description) VALUES
                ('Bebidas', 'Refrescos, cafés, tés, cervezas y licores'),
                ('Condimentos', 'Salsas dulces y picantes, estragón y sazones'),
                ('Repostería', 'Postres, golosinas y panes dulces'),
                ('Lácteos', 'Quesos, manteca y leches');
            ";
            using var cmd = new SqliteCommand(seedSql, connection);
            cmd.ExecuteNonQuery();
        }

        private static void SeedProducts(SqliteConnection connection)
        {
            using var checkCmd = new SqliteCommand("SELECT COUNT(*) FROM Products", connection);
            long count = (long)(checkCmd.ExecuteScalar() ?? 0);
            if (count > 0) return;

            string seedSql = @"
                INSERT INTO Products (ProductName, CategoryID, UnitPrice, UnitsInStock, Discontinued) VALUES
                ('Té Chai Northwind', 1, 18.00, 39, 0),
                ('Cerveza Chang', 1, 19.00, 17, 0),
                ('Sirope de Anís', 2, 10.00, 13, 0),
                ('Sazón del Chef Anton', 2, 22.00, 53, 0),
                ('Mezcla Gumbo del Chef Anton', 2, 21.35, 0, 1),
                ('Mermelada de Arándanos', 2, 25.00, 120, 0),
                ('Peras Secas del Tío Bob', 3, 30.00, 15, 0),
                ('Salsa de Arándanos', 2, 40.00, 6, 0),
                ('Queso Manchego La Mancha', 4, 38.00, 86, 0),
                ('Queso Cabrales Asturiano', 4, 21.00, 22, 0);
            ";
            using var cmd = new SqliteCommand(seedSql, connection);
            cmd.ExecuteNonQuery();
        }

        private static void SeedCustomers(SqliteConnection connection)
        {
            using var checkCmd = new SqliteCommand("SELECT COUNT(*) FROM Customers", connection);
            long count = (long)(checkCmd.ExecuteScalar() ?? 0);
            if (count > 0) return;

            string seedSql = @"
                INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, City, Country) VALUES
                ('ALFKI', 'Alfreds Futterkiste', 'Maria Anders', 'Representante de Ventas', 'Berlín', 'Germany'),
                ('ANATR', 'Ana Trujillo Emparedados y helados', 'Ana Trujillo', 'Propietaria', 'México D.F.', 'Mexico'),
                ('ANTON', 'Antonio Moreno Taquería', 'Antonio Moreno', 'Propietario', 'México D.F.', 'Mexico'),
                ('AROUT', 'Around the Horn', 'Thomas Hardy', 'Representante de Ventas', 'Londres', 'UK'),
                ('BERGS', 'Berglunds snabbköp', 'Christina Berglund', 'Administradora', 'Luleå', 'Sweden'),
                ('BLONP', 'Blondel père et fils', 'Frédérique Citeaux', 'Gerente de Marketing', 'Estrasburgo', 'France');
            ";
            using var cmd = new SqliteCommand(seedSql, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
