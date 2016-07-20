using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

using EricYou.DirectUI.Skin;
using EricYou.DirectUI.Utils;
using System.IO;

namespace EricYou.DirectUI.TestUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序皮肤名
        /// </summary>
        public static string skinName = string.Empty;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appBaseDirectory = CommonFunctions.GetAppBaseDirectory();
            
            skinName = ConfigurationManager.AppSettings["SkinName"];
            if(string.IsNullOrEmpty(skinName))
            {
                skinName = "Default";
            }

            DUISkinInfo skinInfo = new DUISkinInfo(appBaseDirectory, skinName);
            DUISkinManager.CreateSkinManager(skinInfo,false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new RoundPanelTestForm());
            //Application.Run(new NotifyTest.NotifyTest());
        }
    }
}
