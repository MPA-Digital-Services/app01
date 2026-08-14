namespace App01.Core.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public override string ToString()
        {
            return $"[{CustomerId}] {CompanyName} - {ContactName ?? "N/A"} ({City}, {Country})";
        }
    }
}
