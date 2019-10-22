using NLog;
using System.Collections.Generic;
using System.Drawing;

namespace Common.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        List<Team> _teams = new List<Team>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public TeamRepository()
        {
            _teams.Add(new Team() { Id = 1, Color = Color.Blue });
            _teams.Add(new Team() { Id = 2, Color = Color.Red });
            _teams.Add(new Team() { Id = 3, Color = Color.Orange });
            _teams.Add(new Team() { Id = 4, Color = Color.Green });
            _teams.Add(new Team() { Id = 5, Color = Color.Violet });
            _teams.Add(new Team() { Id = 6, Color = Color.Brown });
        }

        public List<Team> GetItems()
        {
            return _teams;
        }

        public void LoadItems()
        {
        }

        public void SaveItems()
        {
        }

        public Team CreateItem()
        {
            return null;
        }

        public void RemoveItem(Team item)
        {
        }
    }
}
