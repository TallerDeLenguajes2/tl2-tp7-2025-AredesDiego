using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

public class ProductoRepository
{
    private string conection_string = "Data Source = Tienda.db";
    public void CrearNuevoProducto(string conection_string)
    {
        string querryString = "SELECT * FROM Presupuestos;";

        using (SqliteConnection connection = new SqliteConnection(conection_string))
        {
            var sqlite_command = new SqliteCommand(conection_string, connection);
            connection.Open();

            using (SqliteDataReader reader = sqlite_command.ExecuteReader())
            {
                while(reader.Read())
                {
                    
                }
            }
            connection.Close();
        }
    }
}