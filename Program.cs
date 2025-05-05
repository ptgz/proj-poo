using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using Renci.SshNet;
using Models;

class Program
{
    static void Main()
    {
        DBHandler dBH = new DBHandler();

        // teste de players
        Player gabriel = new Player("036621","Gabriel",24,"Goalkeeper","Grupo do UNASP");
        Console.WriteLine(gabriel.Group);

        Player unknown = dBH.GetPlayerByRa(gabriel.Ra);
        Console.WriteLine(unknown.Group);

        Player.Remove(gabriel.Ra);

        // teste de jogos
        Game jogo1 = new Game("0",1,"Mansão do Kristian","Normal",11);
        Console.WriteLine(jogo1.Id);
        
        Game jogo = dBH.GetGameById(jogo1.Id);
        Console.WriteLine(jogo.Id);
        Console.WriteLine(dBH.GetAllGames());

        Game.Remove(jogo1.Id);


    

    }
}

