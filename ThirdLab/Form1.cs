using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;

namespace ThirdLab
{
    public partial class Form1 : Form
    {
        public List<double> _xList;
        public List<Complex> _functionList;
        FunctionModel _model;
        public Form1()
        {
            _model = new FunctionModel();
            _xList = _model.XList;
            _functionList = _model.Hankel();
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
/*            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < _xList.Count; i++)
            {
                lables.Add(_xList[i].ToString());
            }
            for (int i = 0; i < _xList.Count; i++)
            {
                values.Add(_functionList[i].Phase);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Real",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;*/
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /*var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < _xList.Count; i++)
            {
                lables.Add(_xList[i].ToString());
            }
            for (int i = 0; i < _xList.Count; i++)
            {
                values.Add(_functionList[i].Magnitude);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Real",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;*/
        }
    }
}
