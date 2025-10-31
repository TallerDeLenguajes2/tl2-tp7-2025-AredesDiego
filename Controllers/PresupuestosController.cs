using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PresupuestosController : ControllerBase
{
    private readonly PresupuestosRepository _repo;

    public PresupuestosController()
    {
        _repo = new PresupuestosRepository();
    }


    [HttpPost("Crear Presupuesto")]
    public ActionResult CrearPresupuesto([FromBody] Presupuestos nuevoPresupuesto)
    {
        try
        {
            _repo.CrearPresupuesto(nuevoPresupuesto);
            return Ok("Presupuesto creado exitosamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear presupuesto: {ex.Message}");
        }
    }

    [HttpPost("Agregar Presupuesto")]
    public ActionResult AgregarProducto(int id, [FromBody] PresupuestosDetalle detalle)
    {
        try
        {
            _repo.AgregarProductoAPresupuesto(id, detalle.Producto.idProducto, detalle.Cantidad);
            return Ok("Producto agregado correctamente al presupuesto.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al agregar producto al presupuesto: {ex.Message}");
        }
    }

    [HttpGet("Listar Presupuestos")]
    public ActionResult<List<Presupuestos>> ListarPresupuestos()
    {
        var lista = _repo.ListarPresupuestos();
        return Ok(lista);
    }

    [HttpGet("Obtener Presupuesto")]
    public ActionResult<Presupuestos> ObtenerDetalles(int id)
    {
        var presupuesto = _repo.ObtenerDetalles(id);

        if (presupuesto == null)
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        return Ok(presupuesto);
    }

    [HttpDelete("Eliminar Presupuesto")]
    public ActionResult EliminarPresupuesto(int id)
    {
        try
        {
            bool eliminado = _repo.EliminarPresupuesto(id);

            if (!eliminado)
                return NotFound($"No se encontró el presupuesto con ID {id}.");

            return NoContent(); // http 204
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar presupuesto: {ex.Message}");
        }
    }
}
