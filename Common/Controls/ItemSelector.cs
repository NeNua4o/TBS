using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ItemSelector : UserControl
    {
        public int Id = -1;
        public ItemSelector(int id, Image icon, string name)
        {
            InitializeComponent();
            Id = id;
            pb_icon.Image = icon;
            l_name.Text = name;
        }

        private void pb_icon_Click(object sender, System.EventArgs e)
        {
            InvokeOnClick(this, e);
        }

        private void l_name_Click(object sender, System.EventArgs e)
        {
            InvokeOnClick(this, e);
        }
    }
}
