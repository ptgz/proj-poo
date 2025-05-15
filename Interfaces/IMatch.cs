namespace Interfaces;
using Enums;
 public interface IMatch
    {
        int MatchId { get; set; }
        int GameId { get; set; }
        int Team1Id { get; set; }
        int Team2Id { get; set; }
        int Team1Score { get; set; }
        int Team2Score { get; set; }
        int WinningTeamId { get; set; }
        DateTime CreationDate { get; set; }
    }
    