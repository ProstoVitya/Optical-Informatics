using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SecondLab
{
    class Writer
    {
        FunctionModel3D _model = new FunctionModel3D();
        StreamWriter writer;


        public void WriteFuctionValues(bool isGauss)
        {
            writer = new StreamWriter("FunctionValues.txt");
            var functionValues = _model.FunctionValues2(isGauss);
            var uList = _model.UList();
            for (int i = 0; i < uList.Count; i++)
            {
                writer.WriteLine($"{uList[i]}");
                foreach (var item in functionValues[i])
                {
                    writer.Write($" {item}");
                }
                writer.WriteLine();
            }
        }

        public void WriteFftValues(bool isGauss)
        {
            writer = new StreamWriter("FftValues.txt");
            var functionValues = _model.FftValues2(isGauss);
            var uList = _model.UList();
            for (int i = 0; i < uList.Count; i++)
            {
                writer.WriteLine($"{uList[i]}");
                foreach (var item in functionValues[i])
                {
                    writer.Write($" {item}");
                }
                writer.WriteLine();
            }
        }
        
        public void WriteFftValuesXlsx(bool isGauss)
        {

        }
    }
}
