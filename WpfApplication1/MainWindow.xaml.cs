using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using VectorMath;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private double _length;
        private double _angle;

        public bool MultiThread
        {
            get { return _multiThread; }
            set
            {
                _multiThread = value;
                OnPropertyChanged();
            }
        }

        public int Variation
        {
            get { return _variation; }
            set
            {
                _variation = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Angle = Math.PI / 4;
            Length = 0.63;
            SlowDown = 2;
            Variation = 80;
            Generate();
        }
        string _output;
        private int _variation;
        private bool _multiThread;
        private int _slowDown;
        public string Output { get { return _output; } set { _output = value; OnPropertyChanged(); } }

        public double Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                OnPropertyChanged();
                //Generate();
            }
        }

        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChanged();
                //Generate();
            }
        }

        public int SlowDown
        {
            get { return _slowDown; }
            set
            {
                _slowDown = value; 
                OnPropertyChanged();
            }
        }

        private async Task Generate3D()
        {
            Viewport3D.Children.Clear();
            var vect = new CylinderVector(Viewport3D, new Vector3D(100,0,100), new Vector3D(100,100,100) );
            vect.Draw();
            //var vect = new LineVector(Viewport3D, new BasicVector.Vector(300, 0), new BasicVector.Vector(300, 100));
            //var list = new List<LineVector> { vect };
            //for (var index = 0; index < list.Count; index++)
            //{
            //    var lineVector = list[index];

            //    await Draw(list, lineVector);


            //    //lineVector.StrokeThickness = 3.0 / (Convert.ToDouble(index) + .5);
            //}
            //Output = $"Finished! Number of branches: \n {Grid.Children.Count}";
        }
        private async Task Generate()
        {

            Grid.Children.Clear();
            var vect = new LineVector(Grid, new BasicVector.Vector(300, 0), new BasicVector.Vector(300, 100));
            var list = new List<LineVector>(10000) { vect };
            for (var index = 0; index < list.Count; index++)
            {
                var lineVector = list[index];
                Output = index.ToString();
                await Draw(list, lineVector);


                //lineVector.StrokeThickness = 3.0 / (Convert.ToDouble(index) + .5);
            }
            Output = $"Finished! Number of branches: \n {Grid.Children.Count}";
            list = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async Task Draw(List<LineVector> list, LineVector lineVector)
        {
            lineVector.Angle = Angle;
            lineVector.LengthChange = Length;
            lineVector.VariationMin = Variation;
            lineVector.MultiThread = MultiThread;
            lineVector.slowdown = SlowDown;
            await lineVector.Draw();

            var left = lineVector.Left();
            var right = lineVector.Right();
            if (left != null && right != null)
            {
                list.Add(left);
                list.Add(right);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var bttn = sender as Button;
            bttn.IsEnabled = false;
            await Generate();
            bttn.IsEnabled = true;
        }
    }
}
