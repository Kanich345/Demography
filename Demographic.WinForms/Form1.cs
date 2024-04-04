using Demographic.FileOperations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demographic.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAge_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBxInitialAge.Text = openFileDialog1.FileName;
            }
        }

        private void btnTableDeath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBxDeath.Text = openFileDialog1.FileName;
                
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FileManager.Instance.SetFilenameInitialAge(txtBxInitialAge.Text);
            FileManager.Instance.SetFilenameDeath(txtBxDeath.Text);

            if (txtBxDeath.Text == "" || txtBxInitialAge.Text == "")
            {
                MessageBox.Show("Выберите файл");
            }
            else
            {
                int startYear = (int)numericUpDown1.Value;
                int finishYear = (int)numericUpDown2.Value;
                int population = (int)numericUpDown3.Value;

                Engine engine = new Engine(population, startYear, finishYear);
                engine.Start();

                DrawChartPopulation(engine);
                DrawChartMalePopulation(engine);
                DrawChartFemalePopulation(engine);
                DrawChartMalePopulationByAge(engine);
                DrawChartFemalePopulationByAge(engine);
            }
        }

        private void DrawChartPopulation(Engine engine)
        {
            var statisticPopulation = engine.StatisticPopulation;

            foreach (var item in statisticPopulation)
            {
                chart1.Series[0].Points.AddXY(item.Item1, item.Item2);
            }
        }

        private void DrawChartMalePopulation(Engine engine)
        {
            var statisticPopulation = engine.StatisticMalePopulation;

            foreach (var item in statisticPopulation)
            {
                chart2.Series[0].Points.AddXY(item.Item1, item.Item2);
            }
        }

        private void DrawChartFemalePopulation(Engine engine)
        {
            var statisticPopulation = engine.StatisticFemalePopulation;

            foreach (var item in statisticPopulation)
            {
                chart2.Series[1].Points.AddXY(item.Item1, item.Item2);
            }
        }

        private void DrawChartMalePopulationByAge(Engine engine)
        {
            var statisticPopulation = engine.StatisticByAgeMale;

            foreach (var item in statisticPopulation)
            {
                chart3.Series[0].Points.Add(item);
            }
        }

        private void DrawChartFemalePopulationByAge(Engine engine)
        {
            var statisticPopulation = engine.StatisticByAgeFemale;

            foreach (var item in statisticPopulation)
            {
                chart4.Series[0].Points.Add(item);
            }
        }
    }
}
