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
    /// Interaction logic for ToDoView.xaml
    /// </summary>
    public partial class ToDoView : UserControl
    {
        readonly string[] daysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        readonly string[] Hours = new string[] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
        List<string> Mins = new List<string>();
        private ShowCommentView showCommentView;
        public ToDoView()
        {
            InitializeComponent();
            MinGenerator();
            TasksButton.Background = Brushes.White;
            PreviousButton = TasksButton;
            //daysOfWeek.ToList().ForEach(day => DaysBox.Items.Add(day));
            Hours.ToList().ForEach(hour => HourBox.Items.Add(hour));

        }

        private void MinGenerator()
        {
            for (int i = 0; i < 60; i++)
            {
                MinBox.Items.Add(i.ToString());
            }
        }

        private Button PreviousButton;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectTodoMenu((Button)sender);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectTodoMenu((Button)sender);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SelectTodoMenu((Button)sender);
        }
        private void SelectTodoMenu(Button button)
        {
            if(PreviousButton is not null) 
            {
                PreviousButton.Background = Brushes.Transparent;
            }
            button.Background = Brushes.White;
            TaskContentPanel.Visibility = Visibility.Collapsed;
            CreateTaskContentPanel.Visibility = Visibility.Collapsed;
            ShowAllContentPanel.Visibility = Visibility.Collapsed;
            switch (button.Content.ToString())
            {
                case "Tasks":
                    TaskContentPanel.Visibility = Visibility.Visible;
                    break;
                case "Create new Tasks":
                    CreateTaskContentPanel.Visibility = Visibility.Visible;
                    break;
                case "All tasks":
                    ShowAllContentPanel.Visibility = Visibility.Visible;
                    break;
            }
            PreviousButton = button;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var a = sender as TextBox;
            Point mousePosition = e.GetPosition(this);
            showCommentView = new ShowCommentView(a.Text);
            showCommentView.Left = mousePosition.X+10;
            showCommentView.Top = mousePosition.Y+10;
            showCommentView.Show();
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            showCommentView.Hide();
        }
    }
}
