using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TodoXaml.Models;

namespace TodoXaml.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> Todos { get; set; } = new()
        {
            new TodoItem()
            {
                Id = 3,
                Title = "Add Neptun code to neptun.txt",
                Description = "GVK1G8",
                Priority = Priority.Normal,
                IsDone = false,
                Deadline = new DateTime(2024, 11, 08)
            },
            new TodoItem()
            {
                Id = 1,
                Title = "Buy milk",
                Description = "Should be lactose and gluten free!",
                Priority = Priority.Low,
                IsDone = true,
                Deadline = DateTimeOffset.Now + TimeSpan.FromDays(1)
            },
            new TodoItem()
            {
                Id = 2,
                Title = "Do the Computer Graphics homework",
                Description = "Ray tracing, make it shiny and gleamy! :)",
                Priority = Priority.High,
                IsDone = false,
                Deadline = new DateTime(2024, 11, 08)
            }
        };

        public List<Priority> Priorities { get; } =
            Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();

        private TodoItem _editedTodo;
        public TodoItem EditedTodo
        {
            get => _editedTodo;
            set
            {
                _editedTodo = value;
                IsFormVisible = value != null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditedTodo)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFormVisible)));
            }
        }

        private bool _isFormVisible;
        public bool IsFormVisible
        {
            get => _isFormVisible;
            set
            {
                if (_isFormVisible != value)
                {
                    _isFormVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFormVisible)));
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            EditedTodo = new TodoItem
            {
                Id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1,
                Title = string.Empty, 
                Description = string.Empty,
                Deadline = DateTime.Today.AddDays(1), 
                Priority = Priority.Normal,
                IsDone = false 
            };
        }


        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (EditedTodo != null)
            {
                Todos.Add(EditedTodo);
                EditedTodo = null; 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
      
    }
}
