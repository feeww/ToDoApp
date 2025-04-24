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
using System.Windows.Media;
using To_Do_List.ViewModels;
using System.Configuration;

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

            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                ThemeToggle.IsChecked = viewModel.IsDarkTheme;
                ApplyTheme(viewModel.IsDarkTheme);
            }
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme(true);
            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                viewModel.IsDarkTheme = true;
            }
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyTheme(false);
            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                viewModel.IsDarkTheme = false;
            }
        }

        private void ApplyTheme(bool isDarkTheme)
        {
            ResourceDictionary appResources = Application.Current.Resources;

            var mergedDicts = appResources.MergedDictionaries;

            ResourceDictionary themeDict = new ResourceDictionary
            {
                Source = new Uri(isDarkTheme ?
                    "Themes/DarkTheme.xaml" :
                    "Themes/LightTheme.xaml",
                    UriKind.Relative)
            };

            ResourceDictionary commonDict = new ResourceDictionary
            {
                Source = new Uri("Themes/CommonStyles.xaml", UriKind.Relative)
            };

            mergedDicts.Clear();
            mergedDicts.Add(themeDict);
            mergedDicts.Add(commonDict);

            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                viewModel.IsDarkTheme = isDarkTheme;
            }
        }
    }
}