using System.Windows;
using System.Windows.Controls;

namespace NextUnit.TestExplorerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainViewModel mainViewModel = new MainViewModel();
            InitializeComponent();
            this.DataContext = mainViewModel;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxTestExecution.ScrollToEnd();
        }
    }
}