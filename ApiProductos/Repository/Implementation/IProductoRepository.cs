using ApiProductos.Data.Models;

namespace ApiProductos.Repository.Implementation
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetList();
        Task<Producto>? GetById(int id);
        Task<Producto>? GetByName(string Name);
        Task<bool> Update(Producto model);
        Task<Producto> Create(Producto model);
        Task<bool> Delete(Producto model);
    }
}
