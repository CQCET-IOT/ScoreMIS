using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScoreMIS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FlashForm flashform = new FlashForm();
            //flashform.ShowDialog();
            ////Login login = new Login();
            ////login.ShowDialog();
            Application.Run(new Form1());
            
        }
    }
}
