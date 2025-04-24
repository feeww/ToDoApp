using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace To_Do_List
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NewTaskTextBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Enter && DataContext is ViewModels.MainViewModel viewModel)
                {
                    if (viewModel.AddTaskCommand.CanExecute(null))
                    {
                        viewModel.AddTaskCommand.Execute(null);
                    }
                }
            };
        }
    }
}