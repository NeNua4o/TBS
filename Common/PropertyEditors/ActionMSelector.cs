using Common.Enums;
using Common.PropEditorsForms;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Common.PropertyEditors
{
    public class ActionMSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (MultiSelectorEditorForm actionMSelectorForm = new MultiSelectorEditorForm(SelectorTypes.Actions, value))
            {
                actionMSelectorForm.Text = "Выберите действия";
                if (actionMSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    return actionMSelectorForm.Ids.ToArray();
                }
            }
            return value;
        }
    }
}