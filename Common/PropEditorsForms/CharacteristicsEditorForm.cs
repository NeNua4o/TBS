using Common.Enums;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.PropEditorsForms
{
    public partial class CharacteristicsEditorForm : Form
    {
        public SerializableDictionary<CharType, float> Result = new SerializableDictionary<CharType, float>();

        bool initializing = true;

        public CharacteristicsEditorForm(object rawValue)
        {
            InitializeComponent();

            var value = rawValue as SerializableDictionary<CharType, float>;
            if (value == null)
                return;

            foreach (var item in value)
            {
                Result.Add(item.Key, item.Value);
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = item.Key });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = item.Value });
                dataGrid.Rows.Add(row);
            }
            initializing = false;
        }

        private void b_add_Click(object sender, System.EventArgs e)
        {
            var chEi = new CharacteristicsEditorItem(Result);
            if (chEi.ShowDialog() == DialogResult.OK)
            {
                Result.Add(chEi.Item.Key, chEi.Item.Value);
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = chEi.Item.Key });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = chEi.Item.Value });
                dataGrid.Rows.Add(row);
            }
            chEi.Dispose();
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (initializing)
                return;
            var rawKey = dataGrid.Rows[e.RowIndex].Cells[0].Value;
            var rawValue = dataGrid.Rows[e.RowIndex].Cells[1].Value;
            var key = (CharType)Convert.ChangeType(rawKey, typeof(CharType));
            float val;
            if (!float.TryParse((string)rawValue, out val))
                return;
            Result[key] = val;
        }

        private void b_apply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
