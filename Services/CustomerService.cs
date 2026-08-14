using App01.Core.Entities;
using App01.Core.Interfaces;

namespace App01.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> ObtenerTodos() => _customerRepository.GetAll();

        public Customer? ObtenerPorId(string customerId) => _customerRepository.GetById(customerId);

        public List<Customer> BuscarPorPais(string pais) => _customerRepository.GetByCountry(pais);

        public (bool exito, string mensaje) RegistrarCliente(string id, string compañia, string? contacto, string? ciudad, string? pais)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 5)
                return (false, "El CustomerID debe ser exactamente de 5 caracteres.");

            if (string.IsNullOrWhiteSpace(compañia))
                return (false, "El nombre de la compañía es obligatorio.");

            id = id.ToUpper();
            var existente = _customerRepository.GetById(id);
            if (existente != null)
                return (false, $"Ya existe un cliente registrado con el ID '{id}'.");

            var nuevo = new Customer
            {
                CustomerId = id,
                CompanyName = compañia.Trim(),
                ContactName = contacto?.Trim(),
                City = ciudad?.Trim(),
                Country = pais?.Trim()
            };

            bool ok = _customerRepository.Insert(nuevo);
            return ok
                ? (true, $"Cliente '{id}' - '{compañia}' registrado exitosamente.")
                : (false, "Error de persistencia al registrar cliente.");
        }

        public (bool exito, string mensaje) EliminarCliente(string customerId)
        {
            var cliente = _customerRepository.GetById(customerId);
            if (cliente == null)
                return (false, $"Cliente '{customerId}' no encontrado.");

            bool ok = _customerRepository.Delete(customerId);
            return ok
                ? (true, $"Cliente '{customerId}' eliminado correctamente.")
                : (false, "No se pudo eliminar el cliente.");
        }
    }
}
