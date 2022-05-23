using System;
using System.Windows.Forms;

namespace ThirdLab
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FunctionModel model = new FunctionModel();
            Fourier fourier = new Fourier(model);
            var writer = new Writer();
           /* writer.WriteFunction(model.GetGaussLaggerList, "GetGaussLaggerList", true);
            writer.WriteFunction(model.GetGaussLaggerList, "GetGaussLaggerList", false);
            writer.WriteFunction(model.Hankel, "Hankel", true);
            writer.WriteFunction(model.Hankel, "Hankel", false);
            writer.Write2DFunction(model.RestoreImage(model.GetGaussLaggerList), "RestoreGauss", true);
            writer.Write2DFunction(model.RestoreImage(model.GetGaussLaggerList), "RestoreGauss", false);*/
            writer.Write2DFunction(fourier.FftValues(), "FftValues", true);
            writer.Write2DFunction(fourier.FftValues(), "FftValues", false);
           

/*            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

        }
    }
}
