using Common.PropEditorsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.PropertyEditors
{
    public class DictionaryEditor:UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (CharacteristicsEditorForm actionSelectorForm = new CharacteristicsEditorForm(value))
            {
                actionSelectorForm.Text = "Характеристики";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    return actionSelectorForm.Result;
                }
            }
            return value;
        }
    }
}
