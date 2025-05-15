using Interfaces;
namespace Models;

public class Game : IGame
{
    public string Id;
    public int Date; // unix time ta?
    public string Place; 
    public string FieldType;
    public int PlayersPerTeam;
    List<Player> ListaDeInteressados;


    public Game() {}
    public Game(string id, int date, string place, string fieldType, int playersPerTeam)
    {

        DBHandler db = new DBHandler();
        if (id == "0") {
            id = Guid.NewGuid().ToString(); 
        }

        db.SqlQuery($"INSERT INTO games VALUES ('{id}', '{date}', '{place}', '{fieldType}',{playersPerTeam})");
        this.Id = id;
        this.Date = date;
        this.Place = place;
        this.FieldType = fieldType;
        this.PlayersPerTeam = playersPerTeam;


    }

    public static void Remove(string id)
	{
        DBHandler db = new DBHandler();
		db.SqlQuery($"DELETE FROM games WHERE id = '{id}'");
    }
}