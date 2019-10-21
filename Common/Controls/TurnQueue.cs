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
            for (int i = 0; i < units.Count; i++)
            {
                var newQueueItem = new TurnQueueItem(units[i]);
                flp.Controls.Add(newQueueItem);
            }
        }
    }
}
