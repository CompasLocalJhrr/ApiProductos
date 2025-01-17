using ApiProductos.Data.Models;
using ApiProductos.DTOs;

namespace ApiProductos.Repository.Implementation
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetList();
        Task<spResponseListData<Producto>> spGetList();
        Task<Producto>? GetById(int id);
        Task<spResponseData<Producto>>? spGetById(int id);
        Task<Producto>? GetByName(string Name);
        Task<bool> Update(Producto model);
        Task<Producto> Create(Producto model);
        Task<spResponseData<Producto>> spCreate(Producto model);
        Task<spBaseResponse> Delete(Producto model);
    }
}
