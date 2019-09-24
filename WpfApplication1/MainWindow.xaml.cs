using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WpfApplication1
{
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
