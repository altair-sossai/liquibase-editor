using System.Windows;
using LiquibaseEditor.Views;

namespace LiquibaseEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateOnClick(object sender, RoutedEventArgs e)
        {
            GenerateView.Show();
        }
    }
}