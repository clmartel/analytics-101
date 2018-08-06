namespace FootballObjectContracts
{
    public interface ITeam
    {
        string City { get; }
        string NickName { get; }
        string ID { get; }
        IGame[] Games { get; }
        int Wins { get; }
        int Losses { get; }
        int Ties { get; }
        int PointsScored { get; }
        int PointsAllowed { get; }
        int TouchdownsScored { get; }
        int TouchdownsAllowed { get; }
        double WinPercentage { get; }
        int HeadToHead(string opponentID);
        int WonLostTied(IGame game);

    }
}
