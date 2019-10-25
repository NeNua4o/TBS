using Common.Models;
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
            var alfa = 255;
            var col = Color.FromArgb(alfa, Color.Blue);
            _teams.Add(new Team() { Id = 1, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
            col = Color.FromArgb(alfa, Color.Red);
            _teams.Add(new Team() { Id = 2, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
            col = Color.FromArgb(alfa, Color.Orange);
            _teams.Add(new Team() { Id = 3, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
            col = Color.FromArgb(alfa, Color.Green);
            _teams.Add(new Team() { Id = 4, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
            col = Color.FromArgb(alfa, Color.Violet);
            _teams.Add(new Team() { Id = 5, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
            col = Color.FromArgb(alfa, Color.Brown);
            _teams.Add(new Team() { Id = 6, Color = col, Pen = new Pen(col), Brush = new SolidBrush(col) });
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
