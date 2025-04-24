using System.Configuration;
using System.Data;
using System.Windows;
using To_Do_List.Services;
using To_Do_List.ViewModels;

namespace To_Do_List
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ITodoService todoService = new TodoService();

            var mainViewModel = new MainViewModel(todoService);

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }

}
