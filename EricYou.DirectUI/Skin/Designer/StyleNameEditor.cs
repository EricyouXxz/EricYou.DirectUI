using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using EricYou.DirectUI.Forms;
using EricYou.DirectUI.Skin.Styles.Controls;

namespace EricYou.DirectUI.Skin.Designer
{
    public class StyleNameEditor : UITypeEditor
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
                    //多选情况
                    if (context.Instance.GetType() == typeof(object[]))
                    {
                        Type paramType = typeof(object);
                        string paramStyleName = null;
                        object[] controlList = (object[])context.Instance;  //多选的控件列表

                        for(int i=0; i<controlList.Length; i++)             //遍历所有多选的控件
                        {
                            IDUIStyleControl control = controlList[i] as IDUIStyleControl;
                            if (i == 0)
                            {
                                //记录第一个控件的类型和StyleName参数值
                                paramType = control.GetType();
                                paramStyleName = control.DUIStyleName;
                            }
                            else
                            {
                                //若后续控件的StyleName与第一个不一样，则清空参数paramStyleName
                                if (!paramStyleName.Equals(control.DUIStyleName))
                                {
                                    paramStyleName = string.Empty;
                                }
                                //若后续控件的类型与第一个不一样，则改变参数paramType为object类型，使后续样式选择下拉框不显示任何样式
                                if (paramType != control.GetType())
                                {
                                    paramType = typeof(object);
                                }
                            }
                        }
                        
                        //使用构造后的paramType和paramStyleName作为参数构造属性选择下拉框
                        StyleNameSelectDropDownList editingDropDown 
                            = new StyleNameSelectDropDownList(editorService, paramType, paramStyleName);
                        editorService.DropDownControl(editingDropDown);

                        //当返回值部位空时，将返回值返回设计器；否则返回原值到设计器（撤销属性设置的情况）
                        if (!string.IsNullOrEmpty(editingDropDown.NewStyleName))
                        {
                            return editingDropDown.NewStyleName;
                        }
                        else
                        {
                            return value;   //存储了修改前的控件属性原值
                        }
                    }
                    //单选情况
                    else
                    {
                        IDUIStyleControl originalControl = (IDUIStyleControl)context.Instance;
                        StyleNameSelectDropDownList editingDropDown
                            = new StyleNameSelectDropDownList(editorService, originalControl.GetType(), originalControl.DUIStyleName);
                        editorService.DropDownControl(editingDropDown);

                        return editingDropDown.NewStyleName;
                    }
                    
                    
                }
            }
            return base.EditValue(context, provider, value);
        }
    }
}
