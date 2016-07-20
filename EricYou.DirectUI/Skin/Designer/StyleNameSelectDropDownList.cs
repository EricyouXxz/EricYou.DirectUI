using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using EricYou.DirectUI.Skin.Styles;

namespace EricYou.DirectUI.Skin.Designer
{
    public partial class StyleNameSelectDropDownList : UserControl
    {
        private string _originalStyleName;
        private string _newStyleName;
        private Type _dUIStyleControlType;
        private bool _canceling = false;
        private IWindowsFormsEditorService _service;

        public string NewStyleName
        {
            get { return _newStyleName; }
            set { _newStyleName = value; }
        }

        public StyleNameSelectDropDownList(IWindowsFormsEditorService service, Type DUIStyleControlType, string orignalStyleName)
        {
            InitializeComponent();

            this._service = service;
            this._dUIStyleControlType = DUIStyleControlType;
            this._originalStyleName = orignalStyleName;
        }

        private void LayoutSelectDropDownList_Load(object sender, EventArgs e)
        {
            _newStyleName = _originalStyleName;

            IDictionary<string, IDUIStyle> styleDict 
                = DUISkinManager.GetCurrentSkinManager().GetStyleDict(this._dUIStyleControlType);
            if (styleDict != null && styleDict.Count > 0)
            {
                foreach (string styleName in styleDict.Keys)
                {
                    this.lbStyleList.Items.Add(styleName);
                    if (styleName == _newStyleName)
                    {
                        this.lbStyleList.SelectedIndex = this.lbStyleList.Items.Count - 1;
                    }
                }
                
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Cancel)
            {
                _newStyleName = _originalStyleName;
                _canceling = true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void LayoutSelectDropDownList_Leave(object sender, EventArgs e)
        {
            if (!_canceling)
            {
                if (this.lbStyleList.SelectedItem != null)
                {
                    this._newStyleName = this.lbStyleList.SelectedItem as string;
                }
            }
        }

        private void lbLayoutList_Click(object sender, EventArgs e)
        {
            if (this.lbStyleList.SelectedItem != null)
            {
                this._newStyleName = this.lbStyleList.SelectedItem as string;
                if (this._service != null)
                {
                    this._service.CloseDropDown();
                }
            }
            
        }


    }
}
