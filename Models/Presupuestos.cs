public class Presupuestos
{
    public int idPresupuesto { get; set; }
    public string nombreDestinario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<PresupuestosDetalle> detalle { get; set; }

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