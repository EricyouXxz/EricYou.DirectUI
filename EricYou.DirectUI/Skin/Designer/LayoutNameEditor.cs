using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using EricYou.DirectUI.Forms;

namespace EricYou.DirectUI.Skin.Designer
{
    public class LayoutNameEditor: UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    DUIForm originalControl = (DUIForm)context.Instance;
                    LayoutSelectDropDownList editingDropDown = new LayoutSelectDropDownList(editorService, originalControl.LayoutName);
                    editorService.DropDownControl(editingDropDown);

                    return editingDropDown.NewLayoutName;
                }
            }
            return base.EditValue(context, provider, value);
        }
    }
}
