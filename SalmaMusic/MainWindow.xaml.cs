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
using System.IO;
using Microsoft.Win32;
using SalmaMusic.Model;

namespace SalmaMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //List<string> songList = new List<string>();
            //OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true};
            //openFileDialog.ShowDialog(this);
            //MusicContentModel musicContentModel = new MusicContentModel();
            //WorkFlowManager.SaveUsercontrol(musicContentModel);
            //MusicContainer = musicContentModel.GetView();


            //foreach (var file in openFileDialog.FileNames) 
            //{
            //    Music newMusic = new Music();
            //    songList.Add(file);
            //}

        }
    }
}
