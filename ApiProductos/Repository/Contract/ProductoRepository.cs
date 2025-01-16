using ApiProductos.Data;
using ApiProductos.Data.Models;
using ApiProductos.Repository.Implementation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiProductos.Repository.Contract
{
    public class ProductoRepository : IProductoRepository
    {
        private CompasProductosContext _dbContex;

        public ProductoRepository(CompasProductosContext dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<List<Producto>> GetList()
        {
            return await _dbContex.Productos.Where(p => p.NullDate == null).ToListAsync();
        }

         
        public async Task<Producto>? GetById(int id)
        {
            Producto? Producto = new Producto();
            Producto = await _dbContex.Productos.Where(e => e.Id == id && e.NullDate == null).FirstOrDefaultAsync();
            return Producto;
        }

        //public async Task<List<Producto>> spGetById(int id)
        //{

        //    var statusCode = new SqlParameter("@StatusCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //    var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

        //    await _dbContex.Database.ExecuteSqlRawAsync(
        //        "EXEC sp_ExampleProcedure @Param1, @Param2, @StatusCode OUT, @Message OUT",
        //        //new SqlParameter("@Param1", param1),
        //        //new SqlParameter("@Param2", param2),
        //        new SqlParameter("@IdProducto", id),
        //        statusCode,
        //        message
        //    );

        //    return new ProcedureResult
        //    {
        //        StatusCode = (int)statusCode.Value,
        //        Message = message.Value.ToString()
        //    };
        //}

        public async Task<Producto> Create(Producto model)
        {
            var entityEntry = await _dbContex.AddAsync(model);
            await _dbContex.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<bool> Delete(Producto model)
        {
            // Llamada al procedimiento almacenado para eliminar un producto
            await _dbContex.Database.ExecuteSqlRawAsync("EXEC spEliminarProducto @Id = {0}", model.Id);
            return true;
        }

        public async Task<Producto>? GetByName(string Name)
        {
            Producto? Producto = new Producto();
            Producto = await _dbContex.Productos.Where(e => e.Nombre == Name && e.NullDate == null).FirstOrDefaultAsync();
            return Producto;
        }

        public async Task<bool> Update(Producto model)
        {
            _dbContex.Update(model);
            await _dbContex.SaveChangesAsync();
            return true;
        }
    }
}
