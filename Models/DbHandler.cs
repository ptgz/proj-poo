using MySqlConnector;
using System.Text;
using Models;
using System.Data;

public class DBHandler
{
	public MySqlConnection connection;

	public DBHandler()
	{
		string connectionString = "Server=127.0.0.1;Database=mysql;User=root;Password=6366;";
		connection = new MySqlConnection(connectionString);
	}



	public string SqlQuery(string query)
	{	
    if (connection.State != ConnectionState.Open)
		{
			connection.Open(); 
		}
    MySqlCommand command = new MySqlCommand(query, connection);
    using (MySqlDataReader dr = command.ExecuteReader())
    {
        StringBuilder result = new StringBuilder();

        while (dr.Read())
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                result.Append($"{dr.GetName(i)}: {dr[i]}");
                if (i < dr.FieldCount - 1)
                    result.Append(", ");
            }
            result.AppendLine();
        }

        return result.ToString();
    }}

	public Player GetPlayerByRa(string ra)
    {
        if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}
        string query = "SELECT * FROM players WHERE ra = @ra";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@ra", ra);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            if (dr.Read())
            {
                return new Player
                {
                    Ra = dr.GetString("ra"),
                    Name = dr.GetString("name"),
                    Age = dr.GetInt32("age"),
                    Position = dr.GetString("position"),
					Group = dr.GetString("group")
                };
            }
            return null;
		}

    }

	public List<Player> GetAllPlayers()
	{
    var players = new List<Player>();
        if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}
        string query = "SELECT * FROM players";
        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            while (dr.Read())
            {
                var player = new Player
                {
                    Ra = dr.GetString("ra"),
                    Name = dr.GetString("name"),
                    Age = dr.GetInt32("age"),
                    Position = dr.GetString("position"),
					Group = dr.GetString("group")
                };
                players.Add(player);
            }
    }

    return players;
	}

	public string UpdatePlayer(string ra, string column, string value)
{
    using (var connection = new MySqlConnection("Server=127.0.0.1;Database=mysql;User=root;Password=6366;"))
    {
        if (connection.State != ConnectionState.Open)
		{
			connection.Open(); 
		}
        
        try
        {
            var allowedColumns = new HashSet<string> { "ra","name", "age", "position","group"}; 
            if (!allowedColumns.Contains(column.ToLower()))
            {
                throw new ArgumentException("Invalid column name");
            }


            string query = $"UPDATE players SET {column} = @value WHERE ra = @ra";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
     
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@ra", ra);

      
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0 ? "OK" : "No rows updated";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Processing failed: {e.Message}");
            throw;
        }
    }
}






/// GAMES ====================================== GAMES ================================ GAMES


public Game GetGameById(string id)
    {
        if (connection.State != ConnectionState.Open)
		{
			connection.Open(); 
		}
        string query = "SELECT * FROM games WHERE id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            if (dr.Read())
            {
                return new Game
                {
                    Id = dr.GetString("id"),
                    Date = dr.GetInt32("date"),
                    Place = dr.GetString("place"),
                    FieldType = dr.GetString("fieldtype"),
					PlayersPerTeam = dr.GetInt32("playersperteam")
                };
            }
            return null;
		}

    }

	public List<Game> GetAllGames()
	{
    var games = new List<Game>();
        if (connection.State != ConnectionState.Open)
		{
			connection.Open(); 
		}

        string query = "SELECT * FROM games";
        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            while (dr.Read())
            {
                var game = new Game
                {
                    Id = dr.GetString("id"),
                    Date = dr.GetInt32("date"),
                    Place = dr.GetString("place"),
                    FieldType = dr.GetString("fieldtype"),
					PlayersPerTeam = dr.GetInt32("playersperteam")
                };
                games.Add(game);
            }
    }

    return games;
	}

	public string UpdateGame(string id, string column, string value)
{
    using (var connection = new MySqlConnection("Server=127.0.0.1;Database=mysql;User=root;Password=6366;"))
    {
        if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}
        
        try
        {
            var allowedColumns = new HashSet<string> { "id","date", "place", "fieldtype","playersperteam" };
            if (!allowedColumns.Contains(column.ToLower()))
            {
                throw new ArgumentException("Invalid column name");
            }

            string query = $"UPDATE players SET {column} = @value WHERE id = @id";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0 ? "OK" : "No rows updated";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Processing failed: {e.Message}");
            throw;
        }
    }
}


}

