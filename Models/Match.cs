using Interfaces;
using Enums;
namespace Models;

using System;

public class Match : Interfaces.IMatch
    {
        public int MatchId { get; set; }
        public int GameId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int WinningTeamId { get; set; }
        public DateTime CreationDate { get; set; }
    }