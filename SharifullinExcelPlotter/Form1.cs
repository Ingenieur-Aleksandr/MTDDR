using System;
using System.Collections.Generic;
using static System.Math;
using System.Windows.Forms;

namespace MTDDR
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.Visible = false;// таблица создана для корректного поведения окон chart и chart1 при изменении размера формы
            // здесь false позволяет эффективно использовать пространство формы - изначально в приложении доступна только кнопка
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "csv files (*.csv)|*.csv"// устанавливает фильтр на читаемый формат файла
            };

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;// запуск окна выбора файла
            try
            {
                var ReadFile = new CsvCurveReader();
                List<Data> points = ReadFile.GetData(openDialog.FileName);// метод получения списка точек из класса DispersionCalculations
                
                chart1.ChartAreas[0].AxisX.Title = "Десятичный логарифм частоты, Гц";
                chart1.ChartAreas[0].AxisX.Crossing = 0;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

                chart1.ChartAreas[0].AxisY.Title = "Фаза, град";
                chart1.ChartAreas[0].AxisY.Crossing = 0;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                

                chart2.ChartAreas[0].AxisX.Title = "Десятичный логарифм частоты, Гц";
                chart2.ChartAreas[0].AxisX.Crossing = 0;
                chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

                chart2.ChartAreas[0].AxisY.Title = "Десятичный логарифм сопротивления, Ом*м";
                chart2.ChartAreas[0].AxisY.Crossing = 0;
                chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                
                // визуальное оформление chart и chart1: 
                // Axis.Title - название оси
                // MajorGrid.Enabled - линии, перпендикулярные оси в точках (выставляется false, чтобы не было сетки),
                // .Crossing - свойство объекта пересекать какую-либо точку противоположной оси (выставлено ноль, для прохождения осей через начало координат)

                for (int i = 0; i < points.Count; i++)
                {
                    double x = Log10(points[i].freq);
                    double rho = Log10(points[i].rho);
                    double phi = (points[i].phi);
                    // значения аргументов    

                    DispersionCalculationsForPhase Value1 = new DispersionCalculationsForPhase();

                    // создание новых экземпляров объекта классов

                    double y1 = Value1.DispersePhase(20, x, points);

                    // значения дисперсионных соотношений

                    chart1.Series[0].Points.AddXY(-x, y1);
                    chart1.Series[1].Points.AddXY(-x, phi);
                    chart2.Series[0].Points.AddXY(-x, rho);
                }// цикл построения графиков с помомощью метода .Points.AddXY

                tableLayoutPanel1.Visible = true;
                chart2.Visible = true;
                openBtn.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Кажется, что-то пошло не так:  " + ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }// обработка ошибок при открытии некорректного .csv файла
        }// обработчик нажатия кнопки

        private void chart_DoubleClick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            tableLayoutPanel1.Visible = false;
            openBtn.Visible = true;
        }// обработчик возврата в начало двойным кликом по chart

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            tableLayoutPanel1.Visible = false;
            openBtn.Visible = true;
        }// обработчик возврата в начало двойным кликом по chart1
    }
}
