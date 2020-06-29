using System.Windows;
using LiquibaseEditor.ViewModels;

namespace LiquibaseEditor.Views
{
    public partial class GenerateView
    {
        public GenerateView()
        {
            DataContext = new GenerateViewModel();
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as GenerateViewModel;

            viewModel?.Generate();
        }

        public static void Show()
        {
            var view = new GenerateView();
            var window = new Window
            {
                Title = "Generate",
                Content = view,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
                Width = 450,
                Height = 340
            };

            window.ShowDialog();
        }
    }
}