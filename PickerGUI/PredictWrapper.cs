using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FootballObjectContracts;

namespace PickerGUI
{
    class PredictWrapper
    {
        private IPredictor _predictor;
        private ILeague _league;
        private ILeague _leagueLastWeek;
        private const string DATA_PATH = "../../../ConsoleApp/NFLData/";
        public PredictWrapper(string DLLPath, int year, int week)
        {
            _league = new LeagueBuilder(DLLPath).BuildNFLLeague($"{DATA_PATH}NFLStats_{year}.xml", $"{DATA_PATH}NFL_{year}.xml");
            _leagueLastWeek = new LeagueBuilder(DLLPath).BuildNFLLeague($"{DATA_PATH}NFLStats_{year}.xml", $"{DATA_PATH}NFL_{year}.xml", (week -1));

            Assembly asm = Assembly.LoadFile(DLLPath);
            Type predictor = asm.GetType("FootballObjects.Predictor");
            try
            {
                _predictor = Activator.CreateInstance(predictor, _leagueLastWeek) as IPredictor;
            }
            catch (MissingMethodException)
            {
                _predictor = Activator.CreateInstance(predictor) as IPredictor;
            }
        }

        public string PredictorName => _predictor.YourName;

        public Dictionary<string,string> GetPredictions(int week)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();

            List<ITeam> allTeams = _league.Conferences.SelectMany(c => c.Divisions).SelectMany(d => d.Teams).ToList();

            foreach (ITeam team in allTeams)
            {
                IGame game = team.Games.Where(g => g.Week == week).FirstOrDefault();
                if (game == null) continue;
                ITeam homeTeam = _leagueLastWeek.GetTeam(game.HomeTeam);
                ITeam awayTeam = _leagueLastWeek.GetTeam(game.AwayTeam);
                string key = awayTeam.ID + " at " + homeTeam.ID;             

                if (!p.ContainsKey(key))
                {
                    int prediction = _predictor.Predict(homeTeam, awayTeam);
                    if (prediction > 0)
                        p[key] = homeTeam.ID;
                    else if (prediction < 0)
                        p[key] = awayTeam.ID;
                    else
                        p[key] = PickRandom(homeTeam, awayTeam).ID;
                }
            }
            return p;
        }

        private ITeam PickRandom(ITeam team1, ITeam team2)
        {
            return (new Random(DateTime.Now.Millisecond).Next(0, 10000) > 5000) ? team1 : team2;
        }
    }
}
