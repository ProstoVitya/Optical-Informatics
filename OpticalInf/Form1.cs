﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts.Wpf;
using System.Numerics;

namespace OpticalInf
{
    public partial class Form1 : Form
    {
        private FunctionModel _model;
        private List<Complex> _result;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _model = new FunctionModel();
            _result = _model.Result();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < _result.Count; i++)
            {
                lables.Add(_model.Ksi[i].ToString());
            }
            for (int i = 0; i < _model.Ksi.Count; i++)
            {
                values.Add(_result[i].Real);
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
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < _result.Count; i++)
            {
                lables.Add(_model.Ksi[i].ToString());
            }
            for (int i = 0; i < _model.Ksi.Count; i++)
            {
                values.Add(_result[i].Imaginary);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Imaginary",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var lables = new List<string>();
            var collection = new SeriesCollection();

            var values = new ChartValues<double>();
            for (int i = 0; i < _result.Count; i++)
            {
                lables.Add(_result[i].Real.ToString());
            }
            for (int i = 0; i < _model.Ksi.Count; i++)
            {
                values.Add(_result[i].Imaginary);
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Core",
                Labels = lables
            });

            var line = new LineSeries();
            line.Values = values;

            collection.Add(line);
            cartesianChart1.Series = collection;
        }
    }
}
