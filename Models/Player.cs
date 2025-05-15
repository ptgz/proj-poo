using Interfaces;
using Enums;
using MySql.Data.MySqlClient;
using Models;
namespace Models;

public class Player : Interfaces.IPlayer
    {
        public int PlayerId { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public PlayerPosition Position { get; set; }
    }