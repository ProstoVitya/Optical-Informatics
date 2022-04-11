/*using OxyPlot;
using OxyPlot.Series;*/
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Numerics;
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
            #region oxypplot
            /*var myModel = new PlotModel();
            var scatterSeries = new ScatterSeries();
            var xList = _model.UList();
            for (int i = 0; i < xList.Count; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(xList[i], _result[i].Phase));
            }
            myModel.Series.Add(scatterSeries);
            plotView1.Model = myModel;*/
            #endregion oxypplot
            #region lifecharts
            var xList = _model.UList();
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < xList.Count; i++)
            {
                lables.Add(xList[i].ToString());
            }
            for (int i = 0; i < xList.Count; i++)
            {
                values.Add(_result[i].Phase);
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
            cartesianChart1.Series = collection;
            #endregion lifecharts
        }

        //amplitude
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            #region oxypplot
            /* var myModel = new PlotModel();
             var scatterSeries = new ScatterSeries();
             var xList = _model.UList();
             for (int i = 0; i < xList.Count; i++)
             {
                 scatterSeries.Points.Add(new ScatterPoint(xList[i], _result[i].Magnitude));
             }
             myModel.Series.Add(scatterSeries);
             plotView1.Model = myModel;*/
            #endregion oxyplot
            #region lifecharts
            var xList = _model.UList();
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < xList.Count; i++)
            {
                lables.Add(xList[i].ToString());
            }
            for (int i = 0; i < xList.Count; i++)
            {
                values.Add(_result[i].Magnitude);
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
            cartesianChart1.Series = collection;
            #endregion lifecharts
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _model = new FunctionModel();
            _result = _model.AnaliticFurier();
            //_result = _model.FftValues();
            //_result = _model.FuctionValues();
            //_result = _model.GaussValues();
        }
    }
}
