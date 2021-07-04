using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SampleProject.Constants;
using SampleProject.Models;
using SampleProject.ViewModels.ViewModelBase;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace SampleProject.ViewModels
{

    [AddINotifyPropertyChangedInterface]
    public class AddViewModel : MainViewModelBase
    {

        #region Properties, Commands

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Age { get; set; }
        public string DesiredSalary { get; set; }
        public bool IsValid { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ValidateCommand { get; set; }

        #endregion Properties, Commands

        #region Ctor

        public AddViewModel(NavigationManager navigationManager) : base(navigationManager)
        {
            AddCommand = new RelayCommand(obj => AddAsync(), (s) => IsValid);
            CancelCommand = new RelayCommand(obj => Cancel());
            ValidateCommand = new RelayCommand(obj => ValidateInput());
        }

        #endregion Ctor

        #region Methods

        public void Add()
        {
            Person person = new Person(Surname, Name, Patronymic, Convert.ToInt32(Age), Convert.ToInt32(DesiredSalary));

            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite(DB)
                .Options;

            try
            {
                using var db = new PersonContext(options);
                db.Persons.AddRange(person);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public async void AddAsync()
        {
            await Task.Run(() => Add());
            MessageBox.Show("The Person has been saved to the database");
            NavigationManager.Navigate(NavigationKeys.Main);
        }


        public void Cancel() => NavigationManager.Navigate(NavigationKeys.Main);


        public void ValidateInput()
        {
            Person person = new Person(
                Surname,
                Name,
                Patronymic,
                string.IsNullOrEmpty(Age) ? 0 : Convert.ToInt32(Age),
                string.IsNullOrEmpty(DesiredSalary) ? 0 : Convert.ToInt32(DesiredSalary));

            IsValid = person.IsValid();
        }

        #endregion Methods
    }
}
