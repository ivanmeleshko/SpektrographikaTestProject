using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SampleProject.Constants;
using SampleProject.Lang;
using SampleProject.Models;
using SampleProject.ViewModels.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace SampleProject.ViewModels
{

     public class MainViewModel : MainViewModelBase
    {

        #region Fields, Properties

        private ObservableCollection<Person> persons = new ObservableCollection<Person>();
        private const string en = "en";
        private const string de = "de";

        public Person SelectedPerson { get; set; }
        public string Filter { get; set; }
        
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand UkCommand { get; set; }
        public RelayCommand DeCommand { get; set; }

        #endregion Fields, Properties


        #region Ctor

        public MainViewModel(NavigationManager navigationManager) : base(navigationManager)
        {
            AddCommand = new RelayCommand(obj => AddPerson());
            RemoveCommand = new RelayCommand(obj => RemovePersonAsync());
            FindCommand = new RelayCommand(obj => Find());
            UkCommand = new RelayCommand(obj => SetLanguageAsync(en));
            DeCommand = new RelayCommand(obj => SetLanguageAsync(de));
            GetPersonsAsync();
        }

        #endregion Ctor


        #region Methods

        private async void GetPersonsAsync() => await Task.Run(() => GetPersons());
        

        private async void RemovePersonAsync() => await Task.Run(() => RemovePerson());


        public void AddPerson() => NavigationManager.Navigate(NavigationKeys.Add);


        private void GetPersons()
        {
            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite(DB)
                .Options;

            try
            {
                using var db = new PersonContext(options);

                db.Database.EnsureCreated();
                //var people = CreateFakeData();
                //db.Persons.AddRange(people);
                db.SaveChanges();

                var pers = from b in db.Persons.AsParallel() orderby b.Surname select b;

                foreach (var p in pers)
                {
                    BeginInvokeOnMainThread(new Action(() => { Persons.Add(p); }));
                    persons.Add(p);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void RemovePerson()
        {
            if (SelectedPerson != null)
            {
                var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite(DB)
                .Options;

                try
                {
                    using var db = new PersonContext(options);
                    db.Persons.Remove(SelectedPerson);
                    db.SaveChanges();
                    BeginInvokeOnMainThread(new Action(() => { Persons.Remove(SelectedPerson); }));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                SelectedPerson = null;
            }
        }


        public void Find()
        {
            Persons.Clear();

            foreach (Person p in persons)
            {
                if (p.Name.Contains(Filter, StringComparison.OrdinalIgnoreCase)
                    || p.Surname.Contains(Filter, StringComparison.OrdinalIgnoreCase)
                    || p.Patronymic.Contains(Filter, StringComparison.OrdinalIgnoreCase)
                    || p.Age.ToString().Contains(Filter)
                    || p.DesiredSalary.ToString().Contains(Filter))
                {
                    Persons.Add(p);
                }
            }
        }      


        static IEnumerable<Person> CreateFakeData()
        {
            var people = new List<Person>
            {
                new Person
                {
                    Surname="Neuer",
                    Name = "Manuel",
                    Patronymic = "ManuelsVater",
                    Age = 33,
                    DesiredSalary = 3500

                },
                new Person
                {
                    Surname="Mueller",
                    Name = "Thomas",
                    Patronymic = "ThomassVater",
                    Age = 32,
                    DesiredSalary = 3200
                }
            };

            return people;
        }

        #endregion Methods

    }
}
