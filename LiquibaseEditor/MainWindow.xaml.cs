using System.Windows;
using LiquibaseEditor.ViewModels;

namespace LiquibaseEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new GenerateViewModel();
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as GenerateViewModel;

            viewModel?.Generate();
        }
    }
}