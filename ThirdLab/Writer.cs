using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThirdLab
{
    public class Writer
    {
        public void Write2DFunction(Complex[,] functionValues, string funcName, bool printPhase)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Phase");
                for (int i = 0; i < functionValues.GetLength(0); i++)
                {
                    for (int j = 0; j < functionValues.GetLength(0); j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = printPhase ?
                            functionValues[i,j].Phase : functionValues[i,j].Magnitude;
                    }
                }
                if (printPhase)
                    workbook.SaveAs($"Graphics//{funcName}Phase.xlsx");
                else
                    workbook.SaveAs($"Graphics//{funcName}Amplitude.xlsx");
            }
        }

        public void WriteFunction(Func<List<Complex>> func, string funcName, bool printPhase)
        {
            var functionValues = func();
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Phase");
                for (int i = 0; i < functionValues.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = printPhase ?
                        functionValues[i].Phase : functionValues[i].Magnitude;
                }
                if (printPhase)
                    workbook.SaveAs($"Graphics//{funcName}Phase.xlsx");
                else
                    workbook.SaveAs($"Graphics//{funcName}Amplitude.xlsx");
            }
        }
    }
}
