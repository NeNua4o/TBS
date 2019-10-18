using Common;
using System.Windows.Forms;

namespace TBS
{
    public partial class ArmyEditor : Form
    {
        RepositoryWorker _repWorker;
        ImageList _icons;
        public ArmyEditor()
        {
            InitializeComponent();
            _repWorker = RepositoryWorker.GetInstance();
            _icons = new ImageList();
            for (int unitCounter = 0; unitCounter < _repWorker.BaseUnits.Count; unitCounter++)
            {
                var unit = _repWorker.BaseUnits[unitCounter];
                _icons.Images.Add(unit.Icon);
                lv_units.Items.Add(new ListViewItem(unit.Name, unitCounter));
            }
        }


        
    }
}
