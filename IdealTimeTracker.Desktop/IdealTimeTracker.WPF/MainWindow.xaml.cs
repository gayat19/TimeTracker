using IdealTimeTracker.WPF.Utility.Helper;
using IdealTimeTracker.WPF.ViewModel;
using System.ComponentModel;
using System.Timers;
using System.Windows;

namespace IdealTimeTracker.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += MainWindow_Loaded;
            _timer = new System.Timers.Timer();
            _timer.Start();
            _timer.Elapsed += Timer_Elapsed;
            
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                var vir = VirtualDesktop.EnumerateVirtualDesktops();
                if (vir.Count > 1)
                    VirtualDesktopManager.CloseAllVirtualDesktops(vir.Count);
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
                var vir = VirtualDesktop.EnumerateVirtualDesktops();
                if (vir.Count > 1)
                    VirtualDesktopManager.CloseAllVirtualDesktops(vir.Count);
        }


        private void Window_StateChanged(object? sender, EventArgs e)
        {
            MainViewModel? mainViewModel = this.DataContext as MainViewModel;
            if (this.WindowState != WindowState.Maximized &&  mainViewModel.CurrentViewModel is not HomeViewModel )
            {
                this.WindowState = WindowState.Maximized;
                this.Topmost = true;
            }
            if(this.WindowState == WindowState.Maximized)
            {
                this.Topmost = true;
            }
           
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MainViewModel? mainViewModel = this.DataContext as MainViewModel;
            if(mainViewModel._popupWindow.IsOpen) {

                e.Cancel = true;
                return;
            }
            if(mainViewModel == null)
            {
                return;
            }
            if (mainViewModel.canExit()) {
                mainViewModel?.CloseWindow();
                //Closing -= MainWindow_Closing;
                //Application.Current.Shutdown();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
            }
           
        }
    }
}