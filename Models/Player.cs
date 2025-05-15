using Interfaces;
using MySql.Data.MySqlClient;
namespace Models;

public class Player : IPlayer {
    
    
    public Player() {}
    public Player(string ra, string name, int age, string position, string group = "N/A")
    {
        DBHandler db = new DBHandler();
        db.SqlQuery($"INSERT INTO players (ra, name, age, position, `group`) VALUES ('{ra}', '{name}', {age}, '{position}', '{group}')");

        this.Ra = ra;
        this.Name = name;
        this.Age = age;
        this.Position = position;
        this.Group = group;


    }

    public static void Remove(string ra)
	{
        DBHandler db = new DBHandler();
		db.SqlQuery($"DELETE FROM players WHERE ra = '{ra}'");
    }
    



    
}

