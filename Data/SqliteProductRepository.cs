using System.Data;
using App01.Core.Entities;
using App01.Core.Interfaces;
using Microsoft.Data.Sqlite;

namespace App01.Data
{
    public class SqliteProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public SqliteProductRepository(string connectionString = DatabaseInitializer.ConnectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetAll()
        {
            var list = new List<Product>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                SELECT p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, p.UnitPrice, p.UnitsInStock, p.Discontinued 
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                ORDER BY p.ProductID ASC;";

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapProduct(reader));
            }
            return list;
        }

        public Product? GetById(int productId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                SELECT p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, p.UnitPrice, p.UnitsInStock, p.Discontinued 
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                WHERE p.ProductID = @id;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", productId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapProduct(reader);
            }
            return null;
        }

        public List<Product> GetByCategory(int categoryId)
        {
            var list = new List<Product>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                SELECT p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, p.UnitPrice, p.UnitsInStock, p.Discontinued 
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                WHERE p.CategoryID = @catId
                ORDER BY p.ProductName ASC;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@catId", categoryId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapProduct(reader));
            }
            return list;
        }

        public int Insert(Product product)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                INSERT INTO Products (ProductName, CategoryID, UnitPrice, UnitsInStock, Discontinued)
                VALUES (@name, @catId, @price, @stock, @disc);
                SELECT last_insert_rowid();";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", product.ProductName);
            cmd.Parameters.AddWithValue("@catId", product.CategoryId);
            cmd.Parameters.AddWithValue("@price", product.UnitPrice);
            cmd.Parameters.AddWithValue("@stock", product.UnitsInStock);
            cmd.Parameters.AddWithValue("@disc", product.Discontinued ? 1 : 0);

            long lastId = (long)(cmd.ExecuteScalar() ?? 0);
            return (int)lastId;
        }

        public bool UpdatePrice(int productId, decimal newPrice)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "UPDATE Products SET UnitPrice = @price WHERE ProductID = @id;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@price", newPrice);
            cmd.Parameters.AddWithValue("@id", productId);

            int affected = cmd.ExecuteNonQuery();
            return affected > 0;
        }

        public bool Delete(int productId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Products WHERE ProductID = @id;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", productId);

            int affected = cmd.ExecuteNonQuery();
            return affected > 0;
        }

        private static Product MapProduct(IDataRecord record)
        {
            return new Product
            {
                ProductId = record.GetInt32(0),
                ProductName = record.GetString(1),
                CategoryId = record.IsDBNull(2) ? 0 : record.GetInt32(2),
                CategoryName = record.IsDBNull(3) ? "Sin Categoría" : record.GetString(3),
                UnitPrice = Convert.ToDecimal(record.GetValue(4)),
                UnitsInStock = Convert.ToInt16(record.GetValue(5)),
                Discontinued = Convert.ToInt32(record.GetValue(6)) == 1
            };
        }
    }
}
