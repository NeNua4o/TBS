using Common.PropEditorsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.PropEditors
{
    public class EffectPropertyEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (EffectPropertyEditorForm effPropEditorForm = new EffectPropertyEditorForm())
            {
                effPropEditorForm.Text = "Выберите эффект";
                if (effPropEditorForm.ShowDialog() == DialogResult.OK)
                {
                    return effPropEditorForm.SelectedId;
                }
            }
            return value;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                Bitmap bmp = (Bitmap)e.Value;
                Rectangle destRect = e.Bounds;
                bmp.MakeTransparent();
                e.Graphics.DrawImage(bmp, destRect);
            }
        }
    }
}
