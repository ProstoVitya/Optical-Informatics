using System;

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
            FunctionModel3D _model = new FunctionModel3D();
            var writer = new Writer();
            writer.WriteValuesXlsx(_model.FftValues2, false, true);
            //writer.WriteFuctionValues(true);
            //writer.WriteFftValues(true);
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
