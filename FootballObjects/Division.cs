using FootballObjectContracts;

namespace FootballObjects
{
    public class Division : IDivision
    {
        public string Name { get; }
        public ITeam[] Teams { get; }

        public Division(string name, ITeam[] teams)
        {
            this.Name = name;
            this.Teams = teams;
        }
        

        public override string ToString()
        {
            return this.Name;
        }

        public double WinPercentage(ITeam team)
        {
            throw new System.NotImplementedException();
        }
    }
}
