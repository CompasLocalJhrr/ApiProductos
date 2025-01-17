using ApiProductos.Data;
using ApiProductos.Data.Models;
using ApiProductos.DTOs;
using ApiProductos.Repository.Implementation;
using Azure;
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


        /// <summary>
        /// Metodo que ejecuta Sp para consultar productos
        /// </summary>
        /// <returns></returns>
        public async Task<spResponseListData<Producto>> spGetList()
        { 

            // Parametros para dar manejo a la respuesta del SP por eso se configutan en Direction OutPut
            var Success = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

            // Se realiza la ejecuciòn del SP
            var productos = await _dbContex.Productos.FromSqlRaw(
                "EXEC spConsultarProductos @Success OUT, @Message OUT",
                Success,
                message
            ).ToListAsync();
             

            // Se realiza la respuesta del metodo apartir de la respuesta del SP.
            return new spResponseListData<Producto>
            {
                Success = (bool)Success.Value,
                Message = message.Value.ToString(),
                ListData = productos
            };
        }

         
        public async Task<Producto>? GetById(int id)
        {
            Producto? Producto = new Producto();
            Producto = await _dbContex.Productos.Where(e => e.Id == id && e.NullDate == null).FirstOrDefaultAsync();
            return Producto;
        }

        public async Task<spResponseData<Producto>>? spGetById(int id)
        {

            // Parametros para dar manejo a la respuesta del SP por eso se configutan en Direction OutPut
            var Success = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

            // Se realiza la ejecuciòn del SP
            var producto = await _dbContex.Productos.FromSqlRaw(
                "EXEC spConsultarIdProducto @IdProducto,  @Success OUT, @Message OUT",
                new SqlParameter("@IdProducto", id),
                Success,
                message
            ).FirstOrDefaultAsync();


            // Se realiza la respuesta del metodo apartir de la respuesta del SP.
            return new spResponseData<Producto>
            {
                Success = (bool)Success.Value,
                Message = message.Value.ToString(),
                Data = producto
            };
        }
         
        public async Task<Producto> Create(Producto model)
        {
            var entityEntry = await _dbContex.AddAsync(model);
            await _dbContex.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task<spResponseData<Producto>> spCreate(Producto model)
        {

            // Parametros para dar manejo a la respuesta del SP por eso se configutan en Direction OutPut
            var Success = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

            // Se realiza la ejecuciòn del SP
            var productos = await _dbContex.Productos.FromSqlRaw(
                "EXEC spCrearProducto @Nombre, @Descripcion, @Precio, @Stock, @IdBodega, @Success OUT, @Message OUT",
                new SqlParameter("@Nombre", model.Nombre),
                new SqlParameter("@Descripcion", model.Descripcion),
                new SqlParameter("@Precio", model.Precio),
                new SqlParameter("@Stock", model.Stock),
                new SqlParameter("@IdBodega", 1),
                Success,
                message
            ).ToListAsync();

            var producto = productos.FirstOrDefault();

            // Se realiza la respuesta del metodo apartir de la respuesta del SP.
            return new spResponseData<Producto>
            {
                Success = (bool)Success.Value,
                Message = message.Value.ToString(),
                Data = producto
            };
        }

        public async Task<spBaseResponse> Delete(Producto model)
        {               
            // Parametros para dar manejo a la respuesta del SP por eso se configutan en Direction OutPut
            var Success = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

            // Llamada al procedimiento almacenado para eliminar un producto
            await _dbContex.Database.ExecuteSqlRawAsync(
                "EXEC spEliminarProducto @IdProducto, @Success OUT, @Message OUT",
                new SqlParameter("@IdProducto", model.Id),
                Success,
                message
            );

            // Se realiza la respuesta del metodo apartir de la respuesta del SP.
            return new spBaseResponse
            {
                Success = (bool)Success.Value,
                Message = message.Value.ToString()
            };
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
