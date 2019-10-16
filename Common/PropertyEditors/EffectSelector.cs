using Common.Enums;
using Common.PropEditorsForms;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Common.PropertyEditors
{
    public class EffectSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (SingleSelectorEditorForm effectSelectorForm = new SingleSelectorEditorForm(SelectorTypes.Effects))
            {
                effectSelectorForm.Text = "Выберите эффект";
                if (effectSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    return effectSelectorForm.SelectedId;
                }
            }
            return value;
        }
    }
}
