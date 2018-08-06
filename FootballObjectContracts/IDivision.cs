namespace FootballObjectContracts
{
    public interface IDivision
    {
        string Name { get; }
        ITeam[] Teams { get; }

        double WinPercentage(ITeam team);
    }
}
