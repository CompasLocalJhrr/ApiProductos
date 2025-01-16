using Microsoft.AspNetCore.Mvc;
using ApiProductos.Services.Implementation;
using ApiProductos.DTOs;

namespace ApiProductos.Controllers
{
    /// <summary>
    /// Controller de CRUP de Productos
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productService;

        public ProductoController(IProductoService productService)
        {
            _productService = productService;
        }


        /// <summary>
        /// Metodo que obtine el listado de los productos
        /// </summary>
        /// <returns>ResponseDto Objeto que almacena todos los datos de la respuesta desde el success hasta la ListData</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetList();

            return Ok(result);
        }

        /// <summary>
        /// Metodo que obtine un producto filtrando por el Id
        /// </summary>
        /// <param name="id">Id del producto que deseamos consultar</param>
        /// <returns>ResponseDto Objeto que almacena todos los datos de la respuesta desde el success hasta la data</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productService.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Metodo para crear un producto
        /// </summary>
        /// <param name="producto">Objeto que todos los datos del producto a crear</param>
        /// <returns>ResponseDto Objeto que almacena todos los datos de la respuesta desde el success hasta la data</returns>
        [HttpPost]
        public async Task<IActionResult> Post([Bind("Nombre,Descripcion,Precio,Stock")] CreateProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.Create(producto);

            return Ok(result);
        }

        /// <summary>
        /// Permite modificar los datos de un producto
        /// </summary>
        /// <param name="id"> Id del producto a modificar</param>
        /// <param name="producto"> Objeto que contiene los datos a modificar del producto</param>
        /// <returns>ResponseDto Objeto que almacena todos los datos de la respuesta desde el success hasta la data</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [Bind("Nombre,Descripcion,Precio,Stock")] UpdateProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var result = await _productService.Update(id, producto);

            return Ok(result);
        }

        /// <summary>
        /// Da de baja los productos en el sistema
        /// </summary>
        /// <param name="id"> Id del producto a Eliminar</param>
        /// <returns>ResponseDto Objeto que almacena todos los datos de la respuesta desde el success hasta la data</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productService.Delete(id);

            return Ok(result);
        }
    }
}
