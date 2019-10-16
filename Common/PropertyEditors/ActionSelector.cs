using Common.Enums;
using Common.PropEditorsForms;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Common.PropertyEditors
{
    public class ActionSelector:UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.Actions))
            {
                actionSelectorForm.Text = "Выберите действие";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    return actionSelectorForm.SelectedId;
                }
            }
            return value;
        }
    }
}
