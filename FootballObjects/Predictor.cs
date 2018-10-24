using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballObjectContracts;

namespace FootballObjects
{
    public class Predictor : IPredictor
    {
        public int Predict(ITeam HomeTeam, ITeam AwayTeam)
        {
            // Return a number greater than zero to predict the home team
            // Return a number less than zero for the away team
            // Return zero to leave it to chance (We'll do a "Coin Flip" to determine outcome)
            return 0;

            // Example: To pick the away team if the away team has a better win percentage: 

            if (AwayTeam.WinPercentage > HomeTeam.WinPercentage)
                return -1;
            else
                return 1;
        }
    }
}
