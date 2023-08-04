using Microsoft.Win32;
using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ICommand DeleteCanvasContent { get; set; }
        Ellipse ellipse = new Ellipse();
        Brush selectedColorBrush;
        public VideoView()
        {
            InitializeComponent();
            

        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //ColorPicker.SelectedItem = ColorPicker.Items[0];
            //ColorPicker.Text = ColorPicker.Items[0].ToString();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePosition = e.GetPosition(MyCanvas);
                ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Fill = selectedColorBrush;
                Canvas.SetLeft(ellipse, mousePosition.X); // X-coordinate of the top-left corner of the ellipse
                Canvas.SetTop(ellipse, mousePosition.Y);
                MyCanvas.Children.Add(ellipse);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap((int)MyCanvas.ActualWidth, (int)MyCanvas.ActualHeight, 96, 96, PixelFormats.Default);
            rtb.Render(MyCanvas);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }

        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPicker.SelectedItem is ColorItem selectedColorItem)
            {
                // Access the selected ColorItem and its properties
                selectedColorBrush = selectedColorItem.ColorBrush;
                string selectedColorDisplayName = selectedColorItem.DisplayName;
                ellipse.Fill = selectedColorBrush;
            }
        }
    }
}
