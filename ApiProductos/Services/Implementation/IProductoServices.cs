using ApiProductos.Data.Models;
using ApiProductos.DTOs;

namespace ApiProductos.Services.Implementation
{
    public interface IProductoService
    {
        Task<ResponseDto<Producto>> GetList();
        Task<ResponseDto<Producto>> GetById(int id);
        Task<ResponseDto<Producto>> Update(int Id, UpdateProductoDto model);
        Task<ResponseDto<Producto>> Create(CreateProductoDto model);
        Task<ResponseDto<Producto>> Delete(int id);
    }
}
