using Common.Enums;
using Common.PropEditorsForms;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Common.PropertyEditors
{
    public class EffectMSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (MultiSelectorEditorForm effectMSelectorForm = new MultiSelectorEditorForm(SelectorTypes.Effects, value))
            {
                effectMSelectorForm.Text = "Выберите эффекты";
                if (effectMSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    return effectMSelectorForm.Ids.ToArray();
                }
            }
            return value;
        }
    }
}
