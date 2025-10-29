public class Presupuestos
{
    private int idPresupuesto;
    private string nombreDestinario;
    private DateTime FechaCreacion;
    private List<PresupuestosDetalle> detalle;

    public double MontoPresupuestoConIva(List<PresupuestosDetalle> detalle)
    {
        return MontoPresupuesto(detalle) * 1.21;
    }
    private double MontoPresupuesto(List<PresupuestosDetalle> detalle)
    {
        double total = 0;

        foreach (var presupuesto_detalle in detalle)
        {
            total += presupuesto_detalle.producto.precio;
        }
        return total;
    }

    public int CantidadProductos()
    {
        int cantidad = 0;
        foreach (var item in detalle)
        {
            cantidad += item.cantidad;
        }
        return detalle.Count; 
    } 
}