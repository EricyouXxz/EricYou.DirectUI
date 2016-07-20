using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EricYou.DirectUI.Forms.Extentions
{
    /// <summary>
    /// 自定义带placeholder的textbox控件
    /// </summary>
    public class DUIPlaceholderTextBox : TextBox
    {

        bool isPlaceHolder = true;

        string _placeHolderText;

        private Color _oldForeColor;

        private char _oldPasswordChar;

        public string PlaceHolderText
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                setPlaceholder();
            }
        }

        /// <summary>
        /// 获取控件当前是否显示的是placeholder
        /// </summary>
        public bool IsPlaceHolder
        {
            get { return isPlaceHolder; }
        }

        /// <summary>
        /// when the control loses focus, the placeholder is shown
        /// </summary>
        private void setPlaceholder()
        {
            if (string.IsNullOrEmpty(this.Text))
            {

                this._oldPasswordChar = this.PasswordChar;
                this._oldForeColor = this.ForeColor;

                this.ForeColor = Color.Gray;
                this.PasswordChar = '\0';

                this.Text = PlaceHolderText;

                //this.Font = new Font(this.Font, FontStyle.Italic);
                isPlaceHolder = true;
            }
        }

        /// <summary>
        /// when the control is focused, the placeholder is removed
        /// </summary>
        private void removePlaceHolder()
        {

            if (isPlaceHolder)
            {
                this.ForeColor = this._oldForeColor;
                this.PasswordChar = _oldPasswordChar;

                this.Text = "";
                //this.Font = new Font(this.Font, FontStyle.Regular);
                isPlaceHolder = false;
            }
        }

        /// <summary>
        /// 清除输入文本，显示placeholder
        /// </summary>
        public void ClearText()
        {
            this.Text = string.Empty;
            setPlaceholder();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DUIPlaceholderTextBox()
        {
            //注册获得焦点和失去焦点事件
            GotFocus += removePlaceHolder;
            LostFocus += setPlaceholder;
        }

        /// <summary>
        /// 设置placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setPlaceholder(object sender, EventArgs e)
        {
            setPlaceholder();
        }

        /// <summary>
        /// 隐藏placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removePlaceHolder(object sender, EventArgs e)
        {
            removePlaceHolder();
        }
    }
}
