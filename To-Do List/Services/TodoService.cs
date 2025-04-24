using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using To_Do_List.Models;

namespace To_Do_List.Services
{
    public class TodoService : ITodoService
    {
        private List<TodoItem> _tasks;
        public const string FILE_PATH = "todos.json";

        public TodoService()
        {
            LoadTasks();
        }

        public IEnumerable<TodoItem> GetAllTasks()
        {
            return _tasks.ToList();
        }

        public void AddTask(TodoItem task)
        {
            _tasks.Add(task);
            SaveTasks();
        }

        public void RemoveTask(Guid taskId)
        {
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (taskToRemove != null)
            {
                _tasks.Remove(taskToRemove);
                SaveTasks();
            }
        }

        public void UpdateTask(TodoItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Text = task.Text;
                existingTask.IsCompleted = task.IsCompleted;
                SaveTasks();
            }
        }

        public void ClearCompletedTasks()
        {
            _tasks.RemoveAll(t => t.IsCompleted);
            SaveTasks();
        }

        private void LoadTasks()
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    string json = File.ReadAllText(FILE_PATH);
                    _tasks = JsonSerializer.Deserialize<List<TodoItem>>(json);
                }
                else
                {
                    _tasks = new List<TodoItem>();
                }
            }
            catch (Exception)
            {
                _tasks = new List<TodoItem>();
            }
        }

        private void SaveTasks()
        {
            try
            {
                string json = JsonSerializer.Serialize(_tasks);
                File.WriteAllText(FILE_PATH, json);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
