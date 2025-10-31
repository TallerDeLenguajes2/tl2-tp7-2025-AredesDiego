using Microsoft.Data.Sqlite;

public class PresupuestosRepository
{
	private string conection_string = "Data Source=Tienda.db";
	public void CrearPresupuesto(Presupuestos presupuestos)
	{
		using var conexion = new SqliteConnection(conection_string);
		conexion.Open();

		string sql = "INSERT INTO Presupuestos (idPresupuestos, NombreDestinatario, FechaCreacion) VALUES (@idPresupuestos, @NombreDestinatario, FechaCreacion)";

		using var comando = new SqliteCommand(sql, conexion);

		comando.Parameters.Add(new SqliteParameter("@idPresupuestos", presupuestos.idPresupuesto));
		comando.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuestos.NombreDestinatario));
		comando.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuestos.FechaCreacion));

		comando.ExecuteNonQuery();
	}
	public List<Presupuestos> ListarPresupuestos()
	{
		string sql = "SELECT * FROM Presupuestos;";

		List<Presupuestos> listaPresupuestos = [];
		
		using var connection = new SqliteConnection(conection_string);
		connection.Open();

		var comando = new SqliteCommand(sql, connection);

		using (SqliteDataReader reader = comando.ExecuteReader())
		{
			var sqlite_command = new SqliteCommand(sql, connection);
			
			while (reader.Read())
			{
				var presupuestos = new Presupuestos()
				{
					idPresupuesto = Convert.ToInt32(reader["idPresupuestos"]),
					NombreDestinatario = reader["NombreDestinatario"].ToString(),
					FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
				};
				
				listaPresupuestos.Add(presupuestos);
			}
		}

		connection.Close();

		return listaPresupuestos;
	}
	public Presupuestos ObtenerDetalles(int id)
	{
		using var conexion = new SqliteConnection(conection_string);
		conexion.Open();

		string sql = @"
						SELECT 
							p.idPresupuestos,
							p.NombreDestinatario,
							p.FechaCreacion,
							pr.idProducto,
							pr.Descripcion,
							pr.Precio,
							d.Cantidad
						FROM Presupuestos p
						INNER JOIN PresupuestoDetalle d ON p.idPresupuestos = d.idPresupuesto
						INNER JOIN Productos pr ON d.idProducto = pr.idProducto
						WHERE p.idPresupuestos = @id;
					";

		using var comando = new SqliteCommand(sql, conexion);
		comando.Parameters.Add(new SqliteParameter("@id", id));

		using var lector = comando.ExecuteReader();
		
		Presupuestos presupuesto = null;

		while (lector.Read())
		{

			if (presupuesto == null) //Si encontró un registro
			{
				presupuesto = new Presupuestos()
				{
					idPresupuesto = Convert.ToInt32(lector["idPresupuestos"]),
					NombreDestinatario = lector["NombreDestinatario"].ToString(),
					FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
					Detalle = new List<PresupuestosDetalle>()
				};
			}
			
			var producto = new Productos()
			{
				idProducto = Convert.ToInt32(lector["idProducto"]),
				Descripcion = lector["Descripcion"].ToString(),
				Precio = Convert.ToInt32(lector["Precio"])
			};

			var detalle = new PresupuestosDetalle()
			{
				Producto = producto,
				Cantidad = Convert.ToInt32(lector["Cantidad"])
			};

			presupuesto.Detalle.Add(detalle);
		}
		
		return presupuesto;
	}
    

	/*   public AgregarProdcuto() //Agregar un producto y una cantidad a un presupuesto (recibe un id)
	{
		
	} */

	public void EliminarPresupuesto(int id)
	{
		using var conexion = new SqliteConnection(conection_string);
		conexion.Open();

		string sql = "DELETE FROM Presupuestos WHERE idPresupuestos = @id";
		using var comando = new SqliteCommand(sql, conexion);

		comando.Parameters.Add(new SqliteParameter("@id", id));
		comando.ExecuteNonQuery();
	}
}