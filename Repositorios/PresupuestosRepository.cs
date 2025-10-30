using Microsoft.Data.Sqlite;
public class PresupuestosRepository
{
  private string conection_string = "Data Source=Tienda.db";

  public void CrearPresupuesto(Presupuestos presupuestos)
  {
    using var conexion = new SqliteConnection(conection_string);
    conexion.Open();

    string sql = "INSERT INTO Productos (descripcion, precio) VALUES (@descripcion, @precio)";

    using var comando = new SqliteCommand(sql, conexion);

    comando.Parameters.Add(new SqliteParameter("@escripcion", presupuestos));
    comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));

    comando.ExecuteNonQuery();
  }

  public List<Presupuestos> ListarProductos()
  {

  }

  public PresupuestosDetalle ObtenerDetalles(int id)
  {

  }

  /*   public AgregarProdcuto() //Agregar un producto y una cantidad a un presupuesto (recibe un id)
    {
        
    } */

  public void EliminarPresupuesto(int id)
  {

  }
}