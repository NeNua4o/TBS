using Common;
using System.Windows.Forms;

namespace TBS
{
    public partial class ArmyEditor : Form
    {
        RepositoryWorker _repWorker;
        public ArmyEditor()
        {
            InitializeComponent();
            _repWorker = RepositoryWorker.GetInstance();
            for (int unitCounter = 0; unitCounter < _repWorker.BaseUnits.Count; unitCounter++)
            {

            }
        }


        
    }
}
