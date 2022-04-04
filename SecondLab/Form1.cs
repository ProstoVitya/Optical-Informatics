using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondLab
{
    public partial class Form1 : Form
    {
        private FunctionModel _model;
        private List<Complex> _result;

        public Form1()
        {
            InitializeComponent();
        }

        //phase
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            var step = 1d / 20d;
            for (double i = -5; i < 5; i += step)
            {
                lables.Add(i.ToString());
            }
            for (int i = 0; i < _result.Count; i++)
            {
                values.Add(_result[i].Imaginary);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Phase",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;
        }

        //amplitude
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            var step = 1d / 20d;
            for (double i = -5; i < 5; i += step)
            {
                lables.Add(i.ToString());
            }
            for (int i = 0; i < _result.Count; i++)
            {
                values.Add(_result[i].Real);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Phase",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _model = new FunctionModel();
            _result = _model.GetFunction(true).ToList();
        }
    }
}
