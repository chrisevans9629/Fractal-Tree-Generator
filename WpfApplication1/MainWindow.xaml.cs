﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Prism.Commands;
using Prism.Mvvm;
using VectorMath;

namespace WpfApplication1
{

   

    public class MainWindowViewModel : BindableBase
    {
        string _output;
        private int _variation;
        private bool _multiThread;
        private int _slowDown;
        private double _length;
        private double _angle;
        private bool _isBusy;
        public Canvas Grid { get; set; }
        public MainWindowViewModel(Canvas grid)
        {
            Grid = grid;
            Angle = Math.PI / 4;
            Length = 0.63;
            SlowDown = 2;
            Variation = 80;
            Generate();
            GenerateCommand = new DelegateCommand(async ()=> await  Generate(), () => !IsBusy).ObservesProperty(()=> IsBusy);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private bool cancel;
        private void Cancel()
        {
            cancel = true;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy,value);
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand GenerateCommand { get; set; }
        public bool MultiThread
        {
            get { return _multiThread; }
            set => SetProperty(ref _multiThread, value);
        }

        public int Variation
        {
            get { return _variation; }
            set => SetProperty(ref _variation, value);
        }
        public string Output
        {
            get { return _output; }
            set => SetProperty(ref _output, value);

        }

        public double Angle
        {
            get { return _angle; }
            set => SetProperty(ref _angle, value);
        }

        public double Length
        {
            get { return _length; }
            set => SetProperty(ref _length, value);
        }

        public int SlowDown
        {
            get => _slowDown;
            set => SetProperty(ref _slowDown, value);
        }
        private async Task Generate()
        {
            IsBusy = true;
            Grid.Children.Clear();
            var vect = new LineVector(Grid, new BasicVector.Vector(300, 0), new BasicVector.Vector(300, 100));
            var list = new List<LineVector>(10000) { vect };
            for (var index = 0; index < list.Count; index++)
            {
                if (cancel)
                {
                    cancel = !cancel;
                    break;
                }
                var lineVector = list[index];
                Output = index.ToString();
                await Draw(list, lineVector);


                //lineVector.StrokeThickness = 3.0 / (Convert.ToDouble(index) + .5);
            }
            Output = $"Finished! Number of branches: \n {Grid.Children.Count}";
            list = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            IsBusy = false;
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

       
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
      
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(Grid);
        }
      
       

        //private async Task Generate3D()
        //{
        //    Viewport3D.Children.Clear();
        //    var vect = new CylinderVector(Viewport3D, new Vector3D(100,0,100), new Vector3D(100,100,100) );
        //    vect.Draw();
        //    //var vect = new LineVector(Viewport3D, new BasicVector.Vector(300, 0), new BasicVector.Vector(300, 100));
        //    //var list = new List<LineVector> { vect };
        //    //for (var index = 0; index < list.Count; index++)
        //    //{
        //    //    var lineVector = list[index];

        //    //    await Draw(list, lineVector);


        //    //    //lineVector.StrokeThickness = 3.0 / (Convert.ToDouble(index) + .5);
        //    //}
        //    //Output = $"Finished! Number of branches: \n {Grid.Children.Count}";
        //}
      

      

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

       
    }
}
