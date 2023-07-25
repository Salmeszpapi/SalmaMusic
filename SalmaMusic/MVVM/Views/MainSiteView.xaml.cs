using DynamicData;
using Microsoft.Win32;
using SalmaMusic.EventHandlers;
using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Reflection;
using System.Resources;
using NAudio.Wave;
using SalmaMusic.MVVM.ViewModels;
using SalmaMusic.MVVM.Models;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace SalmaMusic.View
{
    /// <summary>
    /// Interaction logic for MainSiteView.xaml
    /// </summary>
    public partial class MainSiteView : Window
    {
        private Button CurrentMenuButton { get; set; }

        public MainSiteView()
        {
            InitializeComponent();
            CurrentMenuButton = FavoritesMenuButton;
            CurrentMenuButton.Background = Brushes.White;
        }

        private void Button_MouseDown22(object sender, MouseButtonEventArgs e)
        {
            Button myButton = (Button)sender;
            myButton.Background = Brushes.Black;
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentMenuButton.Background = Brushes.Transparent;
            CurrentMenuButton = (Button)sender;
            CurrentMenuButton.Background = Brushes.White;
        }

        private void Button_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            CurrentMenuButton.Background = Brushes.Transparent;
            CurrentMenuButton = (Button)sender;
            CurrentMenuButton.Background = Brushes.White;
        }
    }
}
