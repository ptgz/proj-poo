namespace Interfaces;
using Enums;
using Models;

public interface IGame
    {
        int GameId { get; set; }
        DateTime Date { get; set; }
        string Location { get; set; }
        FieldType FieldType { get; set; }
        int PlayersPerTeam { get; set; }
        int MaxTeams { get; set; }
        int MaxPlayers { get; set; }
        DateTime CreationDate { get; set; }
    }