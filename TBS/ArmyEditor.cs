using Common;
using Common.Repositories;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TBS
{
    public partial class ArmyEditor : Form
    {
        RepositoryWorker _repWorker;
        ImageList _icons = new ImageList();
        public ArmyEditor()
        {
            InitializeComponent();
            _icons.ImageSize = new Size(50, 50);
            lv_units.LargeImageList = _icons;
            _repWorker = RepositoryWorker.GetInstance();
            var units = _repWorker.BaseUnits;
            for (int unitCounter = 0; unitCounter < units.Count; unitCounter++)
            {
                var unit = units[unitCounter];
                _icons.Images.Add(unit.Icon == null? new Bitmap(1, 1): unit.Icon);
                var item = new ListViewItem(unit.Name, unitCounter);
                lv_units.Items.Add(item);
            }
            units = null;
        }

        private void lv_units_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectedUnitPos == -1) return;

            if (e.Button == MouseButtons.Left)
                lv_units.DoDragDrop(_repWorker.BaseUnits[_selectedUnitPos], DragDropEffects.Copy);

        }

        int _selectedUnitPos=-1;
        private void lv_units_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var snd = (ListView)sender;
            if (snd.SelectedIndices.Count == 0)
            {
                _selectedUnitPos = -1;
                return;
            }
            _selectedUnitPos = snd.SelectedIndices[0];
        }
    }
}
