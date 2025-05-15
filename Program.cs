using System;
using System.Collections.Generic;
using System.Linq;

using Enums;
using MySqlConnector;
using Renci.SshNet;
using Models;
using Org.BouncyCastle.Asn1.Cms;

using System;
class Program
{
    static void Main()
    {
        DBHandler db = new DBHandler();

        Console.WriteLine("testando criacao de jogador");
        var player = new Player
        {
            Name = "osama",
            Age = 54,
            Position = PlayerPosition.AT
        };
        db.CreatePlayer(player);
        Console.WriteLine("ok");

        Console.WriteLine("pegando tdds os players");
        foreach (var p in db.GetAllPlayers())
        {
            Console.WriteLine($"{p.PlayerId}: {p.Name}, {p.Age}, {p.Position}");
        }

        Console.WriteLine("criando jogo");
        var game = new Game
        {
            Date = DateTime.Now.AddDays(3),
            Location = "esconderijo do ninja",
            FieldType = FieldType.indoor,
            PlayersPerTeam = 5,
            MaxTeams = 6,
            MaxPlayers = 13,
            CreationDate = DateTime.Now
        };
        db.CreateGame(game);
        Console.WriteLine("ok");

        Console.WriteLine("pegando todos os jogos");
        foreach (var g in db.GetAllGames())
        {
            Console.WriteLine($"{g.GameId}: {g.Date}, {g.Location}, {g.FieldType}");
        }

        Console.WriteLine("ok");
    }
}
