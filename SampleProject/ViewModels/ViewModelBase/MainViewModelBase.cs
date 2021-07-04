using Egor92.MvvmNavigation;
using SampleProject.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace SampleProject.ViewModels.ViewModelBase
{

    public class MainViewModelBase
    {

        public const string DB = "Filename=PersonsDB.db";
        public NavigationManager NavigationManager { get; set; }
        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();


        public MainViewModelBase(NavigationManager navigation)
        {
            NavigationManager = navigation;
        }

        
        public static void BeginInvokeOnMainThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

    }
}
