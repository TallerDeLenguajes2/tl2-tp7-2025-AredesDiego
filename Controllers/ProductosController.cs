using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ProductoRepository _repo;

    public ProductosController()
    {
        _repo = new ProductoRepository();
    }

    [HttpPost]
    public ActionResult CrearProducto([FromBody] Productos nuevoProducto)
    {
        try
        {
            _repo.CrearProducto(nuevoProducto);
            return Ok("Producto creado exitosamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear producto: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public ActionResult ModificarProducto(int id, [FromBody] Productos producto)
    {
        try
        {
            bool actualizado = _repo.ModificarProducto(id, producto);
            if (!actualizado)
                return NotFound($"No se encontró el producto con ID {id}.");
            
            return Ok("Producto actualizado correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al modificar producto: {ex.Message}");
        }
    }


    [HttpGet]
    public ActionResult<List<Productos>> ListarProductos()
    {
        var lista = _repo.ListarProductos();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public ActionResult<Productos> ObtenerProducto(int id)
    {
        var producto = _repo.ObtenerDetalles(id);

        if (producto == null)
            return NotFound($"No se encontró el producto con ID {id}.");

        return Ok(producto);
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarProducto(int id)
    {
        bool eliminado = _repo.EliminarProducto(id);

        if (!eliminado)
            return NotFound($"No se encontró el producto con ID {id}.");

        return NoContent();
    }
}
