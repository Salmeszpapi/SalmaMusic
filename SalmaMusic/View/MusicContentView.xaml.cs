﻿using SalmaMusic.ViewModel;
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

namespace SalmaMusic.View
{
    /// <summary>
    /// Interaction logic for MusicContentView.xaml
    /// </summary>
    public partial class MusicContentView : UserControl
    {
        public MusicContentView()
        {
            InitializeComponent();
        }

        private void MusicContentViewModel_MusicChanged(object? sender, EventArgs e)
        {
            
        }
    }
}
