using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballObjectContracts
{
    public interface IPredictor {
        string YourName { get; }
        int Predict(ITeam HomeTeam, ITeam AwayTeam);
    }

}
