using Interfaces;
using Enums;
namespace Models;

public class Game : IGame
    {
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public required string Location { get; set; }
        public required FieldType FieldType { get; set; }
        public int PlayersPerTeam { get; set; }
        public int MaxTeams { get; set; }
        public int MaxPlayers { get; set; }
        public DateTime CreationDate { get; set; }
    }