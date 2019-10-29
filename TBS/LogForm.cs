using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BS
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        internal void AddS(List<string> msgs)
        {
            for (int i = 0; i < msgs.Count; i++)
                lb.Items.Add(msgs[i]);
        }

        internal void Reset()
        {
            lb.Items.Clear();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            //base.OnClosing(e);
        }
    }
}
