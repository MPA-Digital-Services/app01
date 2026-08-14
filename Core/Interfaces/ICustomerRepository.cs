using App01.Core.Entities;

namespace App01.Core.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer? GetById(string customerId);
        List<Customer> GetByCountry(string country);
        bool Insert(Customer customer);
        bool Delete(string customerId);
    }
}
