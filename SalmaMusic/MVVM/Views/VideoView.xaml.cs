using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalmaMusic.MVVM.Views
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        public VideoView()
        {
            InitializeComponent();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(MyCanvas);
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 10;
            ellipse.Height = 10;
            ellipse.Fill = Brushes.Blue;
            Canvas.SetLeft(ellipse, mousePosition.X); // X-coordinate of the top-left corner of the ellipse
            Canvas.SetTop(ellipse, mousePosition.Y);
            MyCanvas.Children.Add(ellipse);
        }
    }
}
