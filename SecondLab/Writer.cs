using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SecondLab
{
    class Writer
    {
        public void WriteValuesXlsx(Func<bool, List<List<Complex>>> func, bool isGauss, bool printPhase)
        {
            var functionValues = func(isGauss);
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Phase");
                for (int i = 0; i < functionValues.Count; i++)
                {
                    for (int j = 0; j < functionValues[i].Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = printPhase ?
                            functionValues[i][j].Phase : functionValues[i][j].Magnitude;
                    }
                }
                workbook.SaveAs("Data//Data.xlsx");
            }
        }
    }
}
