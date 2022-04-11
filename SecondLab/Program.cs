using System;
using System.Windows.Forms;

namespace SecondLab
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var writer = new Writer();
            writer.WriteFuctionValues(true);
            writer.WriteFftValues(true);
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
