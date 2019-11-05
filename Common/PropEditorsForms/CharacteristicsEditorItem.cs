using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Enums;
using Common.Models;

namespace Common.PropEditorsForms
{
    public partial class CharacteristicsEditorItem : Form
    {
        List<CharType> _eChars = new List<CharType>();

        public CharItem Item;

        public CharacteristicsEditorItem(SerializableDictionary<CharType, float> result)
        {
            InitializeComponent();
            foreach (var item in result.Keys)
                _eChars.Add(item);

            foreach (CharType item in Enum.GetValues(typeof(CharType)))
            {
                if (_eChars.Contains(item)) continue;
                cb_charType.Items.Add(item);
            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            if (cb_charType.SelectedIndex == -1) return;
            float val;
            if (!float.TryParse(tb_val.Text, out val))
                return;
            Item = new CharItem()
            {
                Key = (CharType)cb_charType.SelectedItem,
                Value = val
            };
            DialogResult = DialogResult.OK;
        }
    }
}
