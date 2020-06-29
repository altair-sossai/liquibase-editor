using System.Windows;
using LiquibaseEditor.ViewModels;

namespace LiquibaseEditor.Views
{
    public partial class DiffView
    {
        public DiffView()
        {
            DataContext = new DiffViewModel();
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as DiffViewModel;

            viewModel?.Diff();
        }

        public static void Show()
        {
            var view = new DiffView();
            var window = new Window
            {
                Title = "Diff",
                Content = view,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
                Width = 450,
                Height = 450
            };

            window.ShowDialog();
        }
    }
}