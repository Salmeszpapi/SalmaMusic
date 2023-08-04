using SalmaMusic.DbHelper;
using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SalmaMusic.MVVM.ViewModels
{
    public class ToDoViewModel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand ViewToDoTaskCreated { get; set; }
        public string TaskName { get; set; }
        public string DaySelected { get; set; }
        public int HoursSelected { get; set; }
        public int MinsSelected { get; set; }
        public string CommendText { get; set; }
        public ObservableCollection<TodoItem> ToDoItems { get; set; }
        private MyDataProvider myDataProvider= new MyDataProvider();

        public ToDoViewModel()
        {
            ViewToDoTaskCreated = new RelayCommand(ToDoCreated);

            ToDoItems = new ObservableCollection<TodoItem>();
            OnPropertyChanged(nameof(ToDoItems));
            LoadTasks();
        }

        private void LoadTasks()
        {
            var asd = myDataProvider.ToDoTask.ToList();
            asd.ForEach(item => ToDoItems.Add(item));
        }

        public void ToDoCreated()
        {
            TodoItem todoItem = new TodoItem(TaskName, new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Today.Day, HoursSelected, MinsSelected,0), CommendText);
            myDataProvider = new MyDataProvider();
            myDataProvider.ToDoTask.Add(todoItem);
            myDataProvider.SaveChanges();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
