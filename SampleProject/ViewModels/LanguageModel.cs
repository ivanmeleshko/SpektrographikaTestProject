using SampleProject.Lang;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SampleProject.ViewModels
{

    public class LanguageModel : INotifyPropertyChanged
    {

        #region Fields

        private static LanguageModel instance;
        private string add;
        private string find;
        private string personInfo;
        private string cancel;
        private string addPersonInfo;
        private string headerSurname;
        private string headerName;
        private string headerPatronymic;
        private string headerAge;
        private string headerDesiredSalary;

        #endregion Fields


        #region Properties

        public string CurrentCulture { get; set; }


        public string Add
        {
            get => add;
            set
            {
                add = Language.Add;
                OnPropertyChanged("Add");
            }
        }

        public string Find
        {
            get => find;
            set
            {
                find = Language.Find;
                OnPropertyChanged("Find");
            }
        }


        public string Cancel
        {
            get => cancel;
            set
            {
                cancel = Language.Cancel;
                OnPropertyChanged("Cancel");
            }
        }


        public string PersonInfo
        {
            get => personInfo;
            set
            {
                personInfo = Language.PersonInfo;
                OnPropertyChanged("PersonInfo");
            }
        }


        public string AddPersonInfo
        {
            get => addPersonInfo;
            set
            {
                addPersonInfo = Language.AddPersonInfo;
                OnPropertyChanged("AddPersonInfo");
            }
        }

        
        public string HeaderSurname
        {
            get => headerSurname;
            set
            {
                headerSurname = Language.Surname;
                OnPropertyChanged("HeaderSurname");
            }
        }


        public string HeaderName
        {
            get => headerName;
            set
            {
                headerName = Language.Name;
                OnPropertyChanged("HeaderName");
            }
        }


        public string HeaderPatronymic
        {
            get => headerPatronymic;
            set
            {
                headerPatronymic = Language.Patronymic;
                OnPropertyChanged("HeaderPatronymic");
            }
        }


        public string HeaderAge
        {
            get => headerAge;
            set
            {
                headerAge = Language.Age;
                OnPropertyChanged("HeaderAge");
            }
        }


        public string HeaderDesiredSalary
        {
            get => headerDesiredSalary;
            set
            {
                headerDesiredSalary = Language.DesiredSalary;
                OnPropertyChanged("HeaderDesiredSalary");
            }
        }

        #endregion Properties


        #region Methods

        public static LanguageModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LanguageModel();
            }

            return instance;
        }


        protected void SetLanguage(string culture)
        {
            CurrentCulture = culture;

            if (!string.IsNullOrEmpty(culture))
            {
                Language.Culture = CultureInfo.GetCultureInfo(culture);
            }

            Add = Language.Add;
            Find = Language.Find;
            PersonInfo = Language.PersonInfo;
            AddPersonInfo = Language.AddPersonInfo;
            Cancel = Language.Cancel;
            HeaderSurname = Language.Surname;
            HeaderName = Language.Name;
            HeaderPatronymic = Language.Patronymic;
            HeaderAge = Language.Age;
            HeaderDesiredSalary = Language.DesiredSalary;
        }


        protected async void SetLanguageAsync(string culture) => await Task.Run(() => SetLanguage(culture));

        #endregion Methods


        #region INotifyPropertyChanged member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion INotifyPropertyChanged member

    }
}
