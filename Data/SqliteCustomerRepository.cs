using System.Data;
using App01.Core.Entities;
using App01.Core.Interfaces;
using Microsoft.Data.Sqlite;

namespace App01.Data
{
    public class SqliteCustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public SqliteCustomerRepository(string connectionString = DatabaseInitializer.ConnectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT CustomerID, CompanyName, ContactName, ContactTitle, City, Country FROM Customers ORDER BY CustomerID ASC;";
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapCustomer(reader));
            }
            return list;
        }

        public Customer? GetById(string customerId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT CustomerID, CompanyName, ContactName, ContactTitle, City, Country FROM Customers WHERE CustomerID = @id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", customerId.ToUpper());

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapCustomer(reader);
            }
            return null;
        }

        public List<Customer> GetByCountry(string country)
        {
            var list = new List<Customer>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT CustomerID, CompanyName, ContactName, ContactTitle, City, Country FROM Customers WHERE LOWER(Country) = LOWER(@country);";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@country", country);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapCustomer(reader));
            }
            return list;
        }

        public bool Insert(Customer customer)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = @"
                INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, City, Country)
                VALUES (@id, @company, @contact, @title, @city, @country);";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", customer.CustomerId.ToUpper());
            cmd.Parameters.AddWithValue("@company", customer.CompanyName);
            cmd.Parameters.AddWithValue("@contact", (object?)customer.ContactName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@title", (object?)customer.ContactTitle ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city", (object?)customer.City ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country", (object?)customer.Country ?? DBNull.Value);

            int affected = cmd.ExecuteNonQuery();
            return affected > 0;
        }

        public bool Delete(string customerId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Customers WHERE CustomerID = @id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", customerId.ToUpper());

            int affected = cmd.ExecuteNonQuery();
            return affected > 0;
        }

        private static Customer MapCustomer(IDataRecord record)
        {
            return new Customer
            {
                CustomerId = record.GetString(0),
                CompanyName = record.GetString(1),
                ContactName = record.IsDBNull(2) ? null : record.GetString(2),
                ContactTitle = record.IsDBNull(3) ? null : record.GetString(3),
                City = record.IsDBNull(4) ? null : record.GetString(4),
                Country = record.IsDBNull(5) ? null : record.GetString(5)
            };
        }
    }
}
