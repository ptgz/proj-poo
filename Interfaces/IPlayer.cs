namespace Interfaces;
using Enums;
public interface IPlayer
    {
        int PlayerId { get; set; }
        string Name { get; set; }
        int Age { get; set; }
        PlayerPosition Position { get; set; }
    }