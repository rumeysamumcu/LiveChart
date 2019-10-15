
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;


namespace WindowsFormsApp1
{
    public partial class ConstantChanges : Form
    {
        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
            public double Values { get; set; }
        }
        public ConstantChanges()
        {
            InitializeComponent();
        }
        private void ConstantChanges_Load(object sender, EventArgs e)
        {
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            var mapper = Mappers.Xy<MeasureModel>()
                 .X(model => model.Values)   //use DateTime.Ticks as X
                 .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the ChartValues property will store our values array
            ChartValues = new ChartValues<MeasureModel>();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                                             {
                    Title="Series 1",
                    Values = ChartValues,
                    PointGeometrySize =18,
                    StrokeThickness = 4,
                    DataLabels = true,
                    FontSize = 16f,
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(55, 71, 79)),
                    LabelPoint = point =>  "x="+ point.X.ToString()
                }
                
            };
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Tools",
                FontSize = 8,
                Separator = new Separator { Step = 1, IsEnabled = false },
                ShowLabels = false,
            });
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Tools",
                FontSize = 8,
                Separator = new Separator { Step = 1, IsEnabled = false },
                ShowLabels = false,
            });
            SetAxisLimits(System.DateTime.Now);
            //The next code simulates data changes every 500 ms
            Timer = new Timer
            {
                Interval = 500
            };
            Timer.Tick += TimerOnTick;
            R = new Random();
            Timer.Start();
        }
        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Timer Timer { get; set; }
        public Random R { get; set; }
        private void SetAxisLimits(System.DateTime now)
        {
            cartesianChart1.AxisX[0].MinValue = sayac - 8;
            cartesianChart1.AxisX[0].MaxValue = sayac + 1;

            cartesianChart1.Dock = DockStyle.Bottom;
            Controls.Add(cartesianChart1);
        }
        int sayac = 0;
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var now = System.DateTime.Now;
            sayac++;

            ChartValues.Add(new MeasureModel
            {
                //  DateTime = now,
                Value = R.Next(0, 10),
                Values = sayac,

            });
            SetAxisLimits(now);

            ////lets only use the last 30 values
            if (ChartValues.Count > 30) ChartValues.RemoveAt(0);
        }}}

