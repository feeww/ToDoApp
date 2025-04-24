using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Models;

namespace To_Do_List.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAllTasks();
        void AddTask(TodoItem task);
        void RemoveTask(Guid taskId);
        void UpdateTask(TodoItem task);
        void ClearCompletedTasks();
    }
}
