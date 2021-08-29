using PropertyChanged;
using SampleProject.Lang;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SampleProject.Models
{

    public class LanguageModel : INotifyPropertyChanged
    {

        #region Properties

        public string CurrentCulture { get; set; }
        public string Add { get; set; }
        public string Find { get; set; }
        public string Cancel { get; set; }
        public string PersonInfo { get; set; }
        public string AddPersonInfo { get; set; }
        public string HeaderSurname { get; set; }
        public string HeaderName { get; set; }
        public string HeaderPatronymic { get; set; }
        public string HeaderAge { get; set; }
        public string HeaderDesiredSalary { get; set; }

        #endregion Properties


        #region Methods

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
