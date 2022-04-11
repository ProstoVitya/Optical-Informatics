using OxyPlot;
using OxyPlot.Series;
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
            var myModel = new PlotModel();
            var scatterSeries = new ScatterSeries();
            var xList = _model.UList();
            for (int i = 0; i < xList.Count; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(xList[i], _result[i].Phase));
            }
            myModel.Series.Add(scatterSeries);
            plotView1.Model = myModel;
        }

        //amplitude
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var myModel = new PlotModel();
            var scatterSeries = new ScatterSeries();
            var xList = _model.UList();
            for (int i = 0; i < xList.Count; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(xList[i], _result[i].Magnitude));
            }
            myModel.Series.Add(scatterSeries);
            plotView1.Model = myModel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _model = new FunctionModel();
            _result = _model.AnaliticFurier();
            //_result = _model.FftValues();
        }
    }
}
