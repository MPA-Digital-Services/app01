namespace App01.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public bool Discontinued { get; set; }

        public override string ToString()
        {
            return $"[{ProductId}] {ProductName} - ${UnitPrice:F2} (Stock: {UnitsInStock})";
        }
    }
}
