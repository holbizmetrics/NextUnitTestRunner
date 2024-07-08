using NextUnit.TestMethodCompletionDetector;
using System.Windows;

namespace NextUnit.TestCompletenessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel = new MainViewModel();
        public MainWindow()
        {
			InitializeComponent();
            this.DataContext = mainViewModel;
        }
    }
}