using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureCapture
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] arg)
        {

            try
            {
                if (arg.Length == 0)
                {
                    MessageBox.Show("No parameter passed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }

                string fileName = arg[0];
                //MessageBox.Show(fileName);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(fileName));

                if (System.IO.File.Exists(fileName))
                    return 0;

                return 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 1;

        }
    }
}
