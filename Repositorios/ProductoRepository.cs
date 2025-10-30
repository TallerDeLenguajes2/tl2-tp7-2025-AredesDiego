using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

public class ProductoRepository
{
    private string conection_string = "Data Source=Tienda.db";

    public void CrearProducto(Productos producto)
    {
        using var conexion = new SqliteConnection(conection_string);
        conexion.Open();

        string sql = "INSERT INTO Productos (descripcion, precio) VALUES (@descripcion, @precio)";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));

        comando.ExecuteNonQuery();
    }
    public void ModificarProducto(int id, Productos productos)
    {
        using var conexion = new SqliteConnection(conection_string);
        conexion.Open();

        string sql = "UPDATE Productos SET Descripcion, Precio WHERE idProducto = @id";
        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idProducto", id));
        comando.Parameters.Add(new SqliteParameter("@Descripcion", productos.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@Precio", productos.Precio));

        comando.ExecuteNonQuery();
    }
    public List<Productos> ListarProductos()
    {
        string sql = "SELECT * FROM Productos;";

        List<Productos> listaProductos = [];

        using var connection = new SqliteConnection(conection_string);
        connection.Open();

        var comando = new SqliteCommand(sql, connection);

        using (SqliteDataReader reader = comando.ExecuteReader())
        {
            var sqlite_command = new SqliteCommand(sql, connection);

            while (reader.Read())
            {
                var producto = new Productos()
                {
                    idProducto = Convert.ToInt32(reader["idProducto"]),
                    Descripcion = reader["Descripcion"].ToString(),
                    Precio = Convert.ToInt32(reader["Precio"])
                };
                listaProductos.Add(producto);
            }
        }

        connection.Close();

        return listaProductos;
    }
    
    
}