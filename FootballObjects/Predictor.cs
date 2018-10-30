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
        public string YourName => "Your Name Here";

        public int Predict(ITeam HomeTeam, ITeam AwayTeam)
        {
            // Return a number greater than zero to predict the home team
            // Return a number less than zero for the away team
            // Return zero to leave it to chance (We'll do a "Coin Flip" to determine outcome)
             return 1;
        }
    }
}
