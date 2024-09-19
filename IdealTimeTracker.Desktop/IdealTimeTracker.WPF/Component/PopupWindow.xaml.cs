using IdealTimeTracker.WPF.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace IdealTimeTracker.WPF
{

    public partial class PopupWindow : Window
    {
        public bool IsExit { set; get; }
        public bool IsOpen { set; get; }

        public PopupWindow()
        {
            InitializeComponent();
            Closing += PopupWindow_Closing;
            IsVisibleChanged += PopupWindow_IsVisibleChanged;
            this.StateChanged += Window_StateChanged;

        }

        private void Window_StateChanged(object? sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                // Your code to handle the minimize event
                this.WindowState = WindowState.Maximized;
                this.Topmost = true;
            }
        }

        private void PopupWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((this.DataContext) as PopupViewModel)?.loaded();
        }

        private void PopupWindow_Closing(object? sender, CancelEventArgs e)
        {

            e.Cancel = true;
            ((this.DataContext) as PopupViewModel)?.closePopup();

            if (IsOpen)
            {
                if (((this.DataContext) as PopupViewModel)?.Index != -1)
                {
                    this.Visibility = Visibility.Hidden;
                    this.IsOpen = false;
                }
            }
           

            //if(IsExit)
            //{
            //    Closing -= PopupWindow_Closing;
            //}
        
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var popupViewModel = ((this.DataContext) as PopupViewModel);
            if (popupViewModel == null || popupViewModel.Index == 0 || popupViewModel.Index == PopupViewModel.REASON_INDEX && popupViewModel.Reason is null)
                return;
            popupViewModel.submit();
            this.Visibility = Visibility.Hidden;
            this.IsOpen = false;
        }
    }
}
