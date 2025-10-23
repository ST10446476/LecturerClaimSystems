using System.Configuration;
using System.Data;
using System.Windows;

namespace LecturerClaimSystems;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Global exception handling
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception;
        LogError(exception);
        MessageBox.Show($"A critical error occurred: {exception?.Message}\n\nThe application will now close.",
                      "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        LogError(e.Exception);
        MessageBox.Show($"An error occurred: {e.Exception.Message}\n\nPlease try again.",
                      "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }

    private void LogError(Exception exception)
    {
        try
        {
            string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
            string errorMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {exception?.Message}\n{exception?.StackTrace}\n\n";
            System.IO.File.AppendAllText(logPath, errorMessage);
        }
        catch
        {
            // Silently fail if logging fails
        }
    }
}


