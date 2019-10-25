using Common.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class TurnQueue : UserControl
    {
        public TurnQueue()
        {
            InitializeComponent();
        }

        public void SetQueue(List<Unit> units)
        {
            flp.Controls.Clear();
            // Выводим только 15 юнитов.
            var end = units.Count > 15 ? 15 : units.Count;
            for (int i = 0; i < end; i++)
            {
                var newQueueItem = new TurnQueueItem(units[i]);
                flp.Controls.Add(newQueueItem);
            }
        }
    }
}
