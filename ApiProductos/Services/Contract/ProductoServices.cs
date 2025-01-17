using ApiProductos.Data.Models;
using ApiProductos.DTOs;
using ApiProductos.Repository.Implementation;
using ApiProductos.Services.Implementation;

namespace ApiProductos.Services.Contract
{
    public class ProductoService : IProductoService
    {
        private IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ResponseDto<Producto>> GetList()
        {
            var result = new ResponseDto<Producto>();
            try
            {
                List<Producto> list = new List<Producto>();
                var spResult = await _productoRepository.spGetList();

                if (!spResult.Success)
                {
                    result.SetError(spResult.Message);
                    return result;
                } 

                result.ListData = spResult.ListData;

                return result;
            }
            catch (Exception ex)
            {
                result.SetError($"Ocurrio un Error: {ex.Message}");
                return result;
            }
        }

        public async Task<ResponseDto<Producto>> Create(CreateProductoDto model)
        {
            var result = new ResponseDto<Producto>();
            try
            {

                var IsExistProducto = await _productoRepository.GetByName(model.Nombre);

                if (IsExistProducto != null)
                {
                    result.SetError("Ya existe un producto con ese nombre");
                    return result;
                }

                var producto = new Producto()
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    Precio = model.Precio,
                    Stock = model.Stock
                };

                var spResult = await _productoRepository.spCreate(producto);

                if (!spResult.Success)
                {
                    result.SetError(spResult.Message);
                    return result; 
                }

                result.Data = spResult.Data;
                return result;
            }
            catch (Exception ex)
            {
                result.SetError($"Ocurrio un Error: {ex.Message}");
            }

            return result;
        }

        public async Task<ResponseDto<Producto>> Delete(int id)
        {
            var result = new ResponseDto<Producto>();
            try
            {
                // Validamos que el producto existe ante de mandarlo a eliminar
                var producto = await _productoRepository.GetById(id);

                if (producto == null)
                {
                    result.SetError("Producto no encontrado");
                    return result;
                }

                // Se llama a metodo Delete en el Repository que ejecuta el SP
                var resultDelete = await _productoRepository.Delete(producto);

                // Si hubo algo error dentro del SP se pasa respuesta general
                if (!resultDelete.Success)
                {
                    result.SetError(resultDelete.Message);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.SetError($"Ocurrio un Error: {ex.Message}");
            }

            return result;
        }

        public async Task<ResponseDto<Producto>> GetById(int id)
        {
            var result = new ResponseDto<Producto>();

            try
            {
                var producto = await _productoRepository.GetById(id);

                if (producto == null)
                {
                    result.SetError("Producto no encontrado");
                    return result;
                }

                result.Data = producto;
                return result;
            }
            catch (Exception ex)
            {
                result.SetError($"Ocurrio un Error: {ex.Message}");
            }

            return result;
        }

        public async Task<ResponseDto<Producto>> Update(int Id, UpdateProductoDto model)
        {
            var result = new ResponseDto<Producto>();

            try
            {
                var producto = await _productoRepository.GetById(Id);

                if (producto == null)
                {
                    result.SetError("Producto no encontrado");
                    return result;
                }

                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.Precio = model.Precio;
                producto.Stock = model.Stock;


                await _productoRepository.Update(producto);

                result.Data = producto;
                return result;
            }
            catch (Exception ex)
            {
                result.SetError($"Ocurrio un Error: {ex.Message}");
            }

            return result;
        }
    }
}
