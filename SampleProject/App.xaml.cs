using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using SampleProject.Constants;
using SampleProject.ViewModels;
using SampleProject.Views;
using System.Windows;


namespace SampleProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var navigationManager = new NavigationManager(mainWindow);

            navigationManager.Register<Main>(NavigationKeys.Main, () => new MainViewModel(navigationManager));
            navigationManager.Register<AddView>(NavigationKeys.Add, () => new AddViewModel(navigationManager));

            mainWindow.Show();
            navigationManager.Navigate(NavigationKeys.Main);
        }
    }
}