using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using HandwritingInputSimulator.ViewModels;

namespace HandwritingInputSimulator.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindow_ViewModel();
    }

    private void TitleBar_MouseButtonLeftDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        WindowInteropHelper helper = new WindowInteropHelper(this);
        if (e.ChangedButton == MouseButton.Left)
        {
            if (e.ClickCount == 2)
                AdjustWindowSize();
            else
                SendMessage(helper.Handle, 161, 2, 0);
        }
    }

    /// <summary>
    /// Adjusts the WindowSize to correct parameters when Maximize button is clicked
    /// </summary>
    private void AdjustWindowSize()
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.Maximized;
        }
    }

    /// <summary>
    /// Theis method allow us to use the events to capture the mouse signals and send messages to move and drug the window;
    /// </summary>
    /// <param name="hWnd">the identifire of the window</param>
    /// <param name="wMsg">The message</param>
    /// <param name="wParam">Additional information of the message</param>
    /// <param name="lParam">Additional information of the message</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    /// <summary>
    /// This method hendle the maximum Heigth before the window will maximized;
    /// The window is maximized to the desktom workspace only.
    /// </summary>
    private void TitleBar_MouseEnter(object sender, MouseEventArgs e) => this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
}
