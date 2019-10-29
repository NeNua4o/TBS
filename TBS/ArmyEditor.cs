using Common;
using Common.Repositories;
using Common.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TBS
{
    public partial class ArmyEditor : Form
    {
        RepositoryWorker _repWorker;
        ImageList _icons = new ImageList();
        List<Pl> _pls;
        public ArmyEditor(List<Pl> pls)
        {
            InitializeComponent();
            _icons.ImageSize = new Size(50, 50);
            lv_units.LargeImageList = _icons;
            _repWorker = RepositoryWorker.Instance();
            var units = _repWorker.BaseUnits;
            for (int unitCounter = 0; unitCounter < units.Count; unitCounter++)
            {
                var unit = units[unitCounter];
                _icons.Images.Add(unit.Icon == null? new Bitmap(1, 1): unit.Icon);
                var item = new ListViewItem(unit.Name, unitCounter);
                lv_units.Items.Add(item);
            }
            units = null;
            _pls = pls;
            armyBrowser1.Set(_pls[0]);
            armyBrowser2.Set(_pls[1]);
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

        private void b_apply_Click(object sender, System.EventArgs e)
        {
            _repWorker.SaveAll();
            Close();
        }
    }
}
