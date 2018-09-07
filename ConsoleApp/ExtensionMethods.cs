using FootballObjects;
using FootballObjectContracts;
using System;
using System.Linq;

namespace ConsoleApp
{
    public static class ExtensionMethods
    {
        public static TimeSpan ToTimeSpan(this string topString)
        {
            int min = int.Parse(topString.Split(':')[0]);
            int sec = int.Parse(topString.Split(':')[1]);
            return new TimeSpan(0, min, sec);
        }

        public static string StripPath(this string fileNameWithPath)
        {
            int lastSlashPosition = fileNameWithPath.LastIndexOf('/') + 1;
            return fileNameWithPath.Substring(lastSlashPosition, fileNameWithPath.Length - lastSlashPosition);
        }

        public static IConference GetConference(this ILeague league, string conferenceName)
        {
            return league.Conferences.FirstOrDefault(c => c.Name == conferenceName);
        }

        public static IDivision GetDivision(this ILeague league, string conferenceName, string divisionName)
        {
            return league.GetConference(conferenceName).Divisions.FirstOrDefault(d => d.Name == divisionName);
        }

        public static ITeam GetTeam(this ILeague league, string id)
        {
            return league.Conferences.SelectMany(c => c.Divisions).SelectMany(d => d.Teams).FirstOrDefault(t => t.ID == id);
        }
    }
}
