using Common.PropEditorsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.PropEditors
{
    public class EffectMultiPropertyEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (EffectMultiPropertyEditorForm effPropEditorForm = new EffectMultiPropertyEditorForm(value))
            {
                effPropEditorForm.Text = "Выберите эффекты";
                if (effPropEditorForm.ShowDialog() == DialogResult.OK)
                {
                    return effPropEditorForm.Ids.ToArray();
                }
            }
            return value;
        }
    }
}
