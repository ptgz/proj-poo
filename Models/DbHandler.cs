using MySqlConnector;
using System.Text;
using Models;
using Enums;
using System.Data;
using Interfaces;

public class DBHandler
{
    private readonly MySqlConnection connection;

    public DBHandler()
    {
        string connectionString = "Server=127.0.0.1;Database=erp;User=root;Password=6366;";
        connection = new MySqlConnection(connectionString);
    }

    private void EnsureConnectionOpen()
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
    }

    public string SqlQuery(string query)
    {
        EnsureConnectionOpen();
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
        }
    }

    // CRUD: PLAYER

    public Player GetPlayerById(int id)
    {
        EnsureConnectionOpen();
        string query = "SELECT * FROM players WHERE player_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            if (dr.Read())
            {
                return new Player
                {
                    PlayerId = dr.GetInt32("player_id"),
                    Name = dr.GetString("name"),
                    Age = dr.GetInt32("age"),
                };
            }
        }
        return null;
    }

    public List<Player> GetAllPlayers()
    {
        EnsureConnectionOpen();
        var players = new List<Player>();
        string query = "SELECT * FROM players";
        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            while (dr.Read())
            {
                players.Add(new Player
                {
                    PlayerId = dr.GetInt32("player_id"),
                    Name = dr.GetString("name"),
                    Age = dr.GetInt32("age"),
                });
            }
        }
        return players;
    }

    public void CreatePlayer(Player player)
    {
        EnsureConnectionOpen();
        string query = "INSERT INTO players (name, age) VALUES (@name, @age)";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", player.Name);
        command.Parameters.AddWithValue("@age", player.Age);
        command.ExecuteNonQuery();
    }

    public void UpdatePlayer(Player player)
    {
        EnsureConnectionOpen();
        string query = "UPDATE players SET name = @name, age = @age WHERE player_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", player.PlayerId);
        command.Parameters.AddWithValue("@name", player.Name);
        command.Parameters.AddWithValue("@age", player.Age);
        command.ExecuteNonQuery();
    }

    public void DeletePlayer(int playerId)
    {
        EnsureConnectionOpen();
        string query = "DELETE FROM players WHERE player_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", playerId);
        command.ExecuteNonQuery();
    }

    // CRUD: GAME

    public Game GetGameById(int id)
    {
        EnsureConnectionOpen();
        string query = "SELECT * FROM games WHERE game_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            if (dr.Read())
            {
                return new Game
                {
                    GameId = dr.GetInt32("game_id"),
                    Date = dr.GetDateTime("date"),
                    Location = dr.GetString("location"),
                    FieldType = Enum.Parse<FieldType>(dr.GetString("field_type")),
                    PlayersPerTeam = dr.GetInt32("players_per_team"),
                    MaxTeams = dr.GetInt32("max_teams"),
                    MaxPlayers = dr.GetInt32("max_players"),
                    CreationDate = dr.GetDateTime("creation_date")
                };
            }
        }
        return null;
    }

    public List<Game> GetAllGames()
    {
        EnsureConnectionOpen();
        var games = new List<Game>();
        string query = "SELECT * FROM games";
        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            while (dr.Read())
            {
                games.Add(new Game
                {
                    GameId = dr.GetInt32("game_id"),
                    Date = dr.GetDateTime("date"),
                    Location = dr.GetString("location"),
                    FieldType = Enum.Parse<FieldType>(dr.GetString("field_type")),
                    PlayersPerTeam = dr.GetInt32("players_per_team"),
                    MaxTeams = dr.GetInt32("max_teams"),
                    MaxPlayers = dr.GetInt32("max_players"),
                    CreationDate = dr.GetDateTime("creation_date")
                });
            }
        }
        return games;
    }

    public void CreateGame(Game game)
    {
        EnsureConnectionOpen();
        string query = "INSERT INTO games (date, location, field_type, players_per_team, max_teams, max_players, creation_date) " +
                       "VALUES (@date, @location, @field_type, @players_per_team, @max_teams, @max_players, @creation_date)";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@date", game.Date);
        command.Parameters.AddWithValue("@location", game.Location);
        command.Parameters.AddWithValue("@field_type", game.FieldType.ToString());
        command.Parameters.AddWithValue("@players_per_team", game.PlayersPerTeam);
        command.Parameters.AddWithValue("@max_teams", game.MaxTeams);
        command.Parameters.AddWithValue("@max_players", game.MaxPlayers);
        command.Parameters.AddWithValue("@creation_date", game.CreationDate);
        command.ExecuteNonQuery();
    }

    public void UpdateGame(Game game)
    {
        EnsureConnectionOpen();
        string query = "UPDATE games SET date = @date, location = @location, field_type = @field_type, " +
                       "players_per_team = @players_per_team, max_teams = @max_teams, max_players = @max_players, " +
                       "creation_date = @creation_date WHERE game_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", game.GameId);
        command.Parameters.AddWithValue("@date", game.Date);
        command.Parameters.AddWithValue("@location", game.Location);
        command.Parameters.AddWithValue("@field_type", game.FieldType.ToString());
        command.Parameters.AddWithValue("@players_per_team", game.PlayersPerTeam);
        command.Parameters.AddWithValue("@max_teams", game.MaxTeams);
        command.Parameters.AddWithValue("@max_players", game.MaxPlayers);
        command.Parameters.AddWithValue("@creation_date", game.CreationDate);
        command.ExecuteNonQuery();
    }

    public void DeleteGame(int gameId)
    {
        EnsureConnectionOpen();
        string query = "DELETE FROM games WHERE game_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", gameId);
        command.ExecuteNonQuery();
    }

    // CRUD: MATCH

    public Match GetMatchById(int id)
    {
        EnsureConnectionOpen();
        string query = "SELECT * FROM matches WHERE match_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            if (dr.Read())
            {
                return new Match
                {
                    MatchId = dr.GetInt32("match_id"),
                    GameId = dr.GetInt32("game_id"),
                    Team1Id = dr.GetInt32("team_1_id"),
                    Team2Id = dr.GetInt32("team_2_id"),
                    Team1Score = dr.GetInt32("team_1_score"),
                    Team2Score = dr.GetInt32("team_2_score"),
                    WinningTeamId = dr.GetInt32("winning_team_id"),
                    CreationDate = dr.GetDateTime("creation_date")
                };
            }
        }
        return null;
    }

    public List<Match> GetAllMatches()
    {
        EnsureConnectionOpen();
        var matches = new List<Match>();
        string query = "SELECT * FROM matches";
        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader dr = command.ExecuteReader())
        {
            while (dr.Read())
            {
                matches.Add(new Match
                {
                    MatchId = dr.GetInt32("match_id"),
                    GameId = dr.GetInt32("game_id"),
                    Team1Id = dr.GetInt32("team_1_id"),
                    Team2Id = dr.GetInt32("team_2_id"),
                    Team1Score = dr.GetInt32("team_1_score"),
                    Team2Score = dr.GetInt32("team_2_score"),
                    WinningTeamId = dr.GetInt32("winning_team_id"),
                    CreationDate = dr.GetDateTime("creation_date")
                });
            }
        }
        return matches;
    }

    public void CreateMatch(Match match)
    {
        EnsureConnectionOpen();
        string query = "INSERT INTO matches (game_id, team_1_id, team_2_id, team_1_score, team_2_score, winning_team_id, creation_date) " +
                       "VALUES (@game_id, @team1_id, @team2_id, @team1_score, @team2_score, @winning_team_id, @creation_date)";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@game_id", match.GameId);
        command.Parameters.AddWithValue("@team1_id", match.Team1Id);
        command.Parameters.AddWithValue("@team2_id", match.Team2Id);
        command.Parameters.AddWithValue("@team1_score", match.Team1Score);
        command.Parameters.AddWithValue("@team2_score", match.Team2Score);
        command.Parameters.AddWithValue("@winning_team_id", match.WinningTeamId);
        command.Parameters.AddWithValue("@creation_date", match.CreationDate);
        command.ExecuteNonQuery();
    }

    public void UpdateMatch(Match match)
    {
        EnsureConnectionOpen();
        string query = "UPDATE matches SET game_id = @game_id, team_1_id = @team1_id, team_2_id = @team2_id, " +
                       "team_1_score = @team1_score, team_2_score = @team2_score, winning_team_id = @winning_team_id, " +
                       "creation_date = @creation_date WHERE match_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", match.MatchId);
        command.Parameters.AddWithValue("@game_id", match.GameId);
        command.Parameters.AddWithValue("@team1_id", match.Team1Id);
        command.Parameters.AddWithValue("@team2_id", match.Team2Id);
        command.Parameters.AddWithValue("@team1_score", match.Team1Score);
        command.Parameters.AddWithValue("@team2_score", match.Team2Score);
        command.Parameters.AddWithValue("@winning_team_id", match.WinningTeamId);
        command.Parameters.AddWithValue("@creation_date", match.CreationDate);
        command.ExecuteNonQuery();
    }

    public void DeleteMatch(int matchId)
    {
        EnsureConnectionOpen();
        string query = "DELETE FROM matches WHERE match_id = @id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", matchId);
        command.ExecuteNonQuery();
    }

    public void AddPlayerToTeam(int teamId, int playerId, PlayerPosition position)
    {
    EnsureConnectionOpen();
    string query = "INSERT INTO players_in_teams (team_id, player_id, position) VALUES (@team_id, @player_id, @position)";
    var cmd = new MySqlCommand(query, connection);
    cmd.Parameters.AddWithValue("@team_id", teamId);
    cmd.Parameters.AddWithValue("@player_id", playerId);
    cmd.Parameters.AddWithValue("@position", position.ToString());
    cmd.ExecuteNonQuery();
    }

}
