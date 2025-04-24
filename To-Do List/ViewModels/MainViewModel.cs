using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using To_Do_List.Commands;
using To_Do_List.Models;
using To_Do_List.Services;

namespace To_Do_List.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITodoService _todoService;
        private string _newTaskText;

        public ObservableCollection<TodoItem> Tasks { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCompletedCommand { get; }

        public string NewTaskText
        {
            get => _newTaskText;
            set
            {
                if (_newTaskText != value)
                {
                    _newTaskText = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string TaskCountText => $"{Tasks.Count} Tasks, {Tasks.Count(t => t.IsCompleted)} Completed";

        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
            Tasks = new ObservableCollection<TodoItem>(_todoService.GetAllTasks());

            AddTaskCommand = new RelayCommand(AddTask, CanAddTask);
            DeleteCommand = new RelayCommand(DeleteTask);
            ClearCompletedCommand = new RelayCommand(ClearCompletedTasks, CanClearCompletedTasks);

            foreach (var task in Tasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }
        }

        private bool CanAddTask(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewTaskText);
        }

        private void AddTask(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewTaskText))
                return;
            var newTask = new TodoItem(NewTaskText.Trim());
            Tasks.Add(newTask);
            _todoService.AddTask(newTask);

            newTask.PropertyChanged += Task_PropertyChanged;

            NewTaskText = string.Empty;
            UpdateTaskCountText();
        }

        private void DeleteTask(object parameter)
        {
            if (parameter is TodoItem task)
            {
                task.PropertyChanged -= Task_PropertyChanged;
                Tasks.Remove(task);
                _todoService.RemoveTask(task.Id);
                UpdateTaskCountText();
            }
        }

        private bool CanClearCompletedTasks(object parameter)
        {
            return Tasks.Any(t => t.IsCompleted);
        }

        private void ClearCompletedTasks(object parameter)
        {
            var completedTasks = Tasks.Where(t => t.IsCompleted).ToList();
            foreach (var task in completedTasks)
            {
                task.PropertyChanged -= Task_PropertyChanged;
                Tasks.Remove(task);
            }

            _todoService.ClearCompletedTasks();
            UpdateTaskCountText();
        }

        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsCompleted))
            {
                if (sender is TodoItem task)
                {
                    _todoService.UpdateTask(task);
                    UpdateTaskCountText();
                }
            }
        }

        private void UpdateTaskCountText()
        {
            OnPropertyChanged(nameof(TaskCountText));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
