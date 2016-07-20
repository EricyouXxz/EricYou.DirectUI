using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EricYou.DirectUI.Skin.Designer
{
    public partial class LayoutSelectDropDownList : UserControl
    {
        private string _originalLayoutName;
        private string _newLayoutName;
        private bool _canceling = false;
        private IWindowsFormsEditorService _service;

        public string NewLayoutName
        {
            get { return _newLayoutName; }
            set { _newLayoutName = value; }
        }

        public LayoutSelectDropDownList(IWindowsFormsEditorService service, string orignalLayoutName)
        {
            InitializeComponent();

            this._service = service;
            this._originalLayoutName = orignalLayoutName;
        }

        private void LayoutSelectDropDownList_Load(object sender, EventArgs e)
        {
            _newLayoutName = _originalLayoutName;

            ICollection<string> layoutList = DUISkinManager.GetCurrentSkinManager().Layouts.Keys;
            if (layoutList != null && layoutList.Count > 0)
            {
                //this.lbLayoutList.DataSource = layoutList;
                //this.lbLayoutList.SelectedItem = _newLayoutName;
                foreach (string layoutName in layoutList)
                {
                    this.lbLayoutList.Items.Add(layoutName);
                    if (layoutName == _newLayoutName)
                    {
                        this.lbLayoutList.SelectedIndex = this.lbLayoutList.Items.Count - 1;
                    }
                }
                
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Cancel)
            {
                _newLayoutName = _originalLayoutName;
                _canceling = true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void LayoutSelectDropDownList_Leave(object sender, EventArgs e)
        {
            if (!_canceling)
            {
                if (this.lbLayoutList.SelectedItem != null)
                {
                    this._newLayoutName = this.lbLayoutList.SelectedItem as string;
                }
            }
        }

        private void lbLayoutList_Click(object sender, EventArgs e)
        {
            if (this.lbLayoutList.SelectedItem != null)
            {
                this._newLayoutName = this.lbLayoutList.SelectedItem as string;
            }
            if (this._service != null)
            {
                this._service.CloseDropDown();
            }
        }


    }
}
