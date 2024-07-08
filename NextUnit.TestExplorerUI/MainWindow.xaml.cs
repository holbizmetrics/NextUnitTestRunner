using NextUnit.TestExplorer;
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

			AppDomain.CurrentDomain.AssemblyLoad += TestAdapterProvider.CurrentDomain_AssemblyLoad;
			AppDomain.CurrentDomain.AssemblyResolve += TestAdapterProvider.CurrentDomain_AssemblyResolve;
			AppDomain.CurrentDomain.DomainUnload += TestAdapterProvider.CurrentDomain_DomainUnload;
			AppDomain.CurrentDomain.UnhandledException += TestAdapterProvider.CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.FirstChanceException += TestAdapterProvider.CurrentDomain_FirstChanceException;
			AppDomain.CurrentDomain.ProcessExit += TestAdapterProvider.CurrentDomain_ProcessExit;
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += TestAdapterProvider.CurrentDomain_ReflectionOnlyAssemblyResolve;
			AppDomain.CurrentDomain.TypeResolve += TestAdapterProvider.CurrentDomain_TypeResolve;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxTestExecution.ScrollToEnd();
        }
    }
}