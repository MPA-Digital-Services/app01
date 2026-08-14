using App01.Core.Entities;
using App01.Core.Interfaces;

namespace App01.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Obtenertodos() => _productRepository.GetAll();

        public Product? ObtenerPorId(int id) => _productRepository.GetById(id);

        public List<Product> ObtenerPorCategoria(int categoryId) => _productRepository.GetByCategory(categoryId);

        public (bool exito, string mensaje, int idGenerado) CrearProducto(string nombre, decimal precio, int categoryId, short stock)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(nombre))
                return (false, "El nombre del producto no puede estar vacío.", 0);

            if (precio <= 0)
                return (false, "El precio unitario debe ser mayor a cero.", 0);

            if (stock < 0)
                return (false, "El stock no puede ser negativo.", 0);

            var nuevo = new Product
            {
                ProductName = nombre.Trim(),
                UnitPrice = precio,
                CategoryId = categoryId,
                UnitsInStock = stock,
                Discontinued = false
            };

            int nuevoId = _productRepository.Insert(nuevo);
            return (true, $"Producto '{nombre}' creado exitosamente con ID #{nuevoId}.", nuevoId);
        }

        public (bool exito, string mensaje) ActualizarPrecio(int productoId, decimal nuevoPrecio)
        {
            if (nuevoPrecio <= 0)
                return (false, "El nuevo precio debe ser mayor a cero.");

            var prod = _productRepository.GetById(productoId);
            if (prod == null)
                return (false, $"No se encontró el producto con ID #{productoId}.");

            bool ok = _productRepository.UpdatePrice(productoId, nuevoPrecio);
            return ok 
                ? (true, $"Precio del producto #{productoId} actualizado de ${prod.UnitPrice:F2} a ${nuevoPrecio:F2}.")
                : (false, "No se pudo actualizar el precio en la base de datos.");
        }

        public (bool exito, string mensaje) EliminarProducto(int productoId)
        {
            var prod = _productRepository.GetById(productoId);
            if (prod == null)
                return (false, $"No se encontró el producto con ID #{productoId}.");

            bool ok = _productRepository.Delete(productoId);
            return ok
                ? (true, $"Producto #{productoId} ({prod.ProductName}) eliminado correctamente.")
                : (false, "No se pudo eliminar el producto.");
        }
    }
}
