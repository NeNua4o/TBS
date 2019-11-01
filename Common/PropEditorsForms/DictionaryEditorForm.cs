using Common.Enums;
using Common.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.PropEditorsForms
{
    public partial class DictionaryEditorForm : Form
    {
        public SerializableDictionary<CharType, float> Result = new SerializableDictionary<CharType, float>();

        public DictionaryEditorForm(object value)
        {
            InitializeComponent();

        }
    }
}
