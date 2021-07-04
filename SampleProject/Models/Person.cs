using PropertyChanged;
using System;
using System.ComponentModel;


namespace SampleProject.Models
{

    [AddINotifyPropertyChangedInterface]
    public class Person : IDataErrorInfo
    {

        #region Properties

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public int DesiredSalary { get; set; }

        #endregion Properties

        #region Ctors

        public Person() { }


        public Person(string surname, string name, string patronymic, int age, int desiredSalary)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Age = age;
            DesiredSalary = desiredSalary;
        }

        #endregion Ctors

        #region Methods

        public void Reset()
        {
            Surname = default(string);
            Name = default(string);
            Patronymic = default(string);
            Age = default(int);
            DesiredSalary = default(int);
        }


        public bool IsValid()
        {
            var surameValid = SurnameValidation();
            var nameValid = NameValidation();
            var patronymicValid = PatronymicValidation();
            var ageValid = AgeValidation();
            var desiradSalaryValid = DesiredSalaryValidation();

            var result = surameValid == null
                && nameValid == null
                && patronymicValid == null
                && ageValid == null
                && desiradSalaryValid == null;

            return result;
        }


        private string SurnameValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.Surname))
                result = "Please enter a Surname";
            else if (this.Surname.Length > 15)
                result = "The Surname is too long";

            return result;
        }


        private string NameValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.Name))
                result = "Please enter a Name";
            else if (this.Surname.Length > 20)
                result = "The Name is too long";

            return result;
        }


        private string PatronymicValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.Patronymic))
                result = "Please enter a Patronymic";
            else if (this.Patronymic.Length > 25)
                result = "The Patronymic is too long";

            return result;
        }


        private string AgeValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.Age.ToString()))
                result = "Please enter an Age";

            if (Age <= 0 || Age >= 150)
                result = "Please enter a valid Age";

            return result;
        }


        private string DesiredSalaryValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.DesiredSalary.ToString()))
                result = "Please enter a Desired Salary";

            if (DesiredSalary <= 0 || DesiredSalary >= 10000000)
                result = "Please enter a valid Desired Salary";

            return result;
        }

        #endregion Methods

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }


        public string this[string fieldName]
        {
            get
            {
                string result = null;

                switch (fieldName)
                {
                    case "Surname":
                        result = this.SurnameValidation();
                        break;
                    case "Name":
                        result = this.NameValidation();
                        break;
                    case "Patronymic":
                        result = this.PatronymicValidation();
                        break;
                    case "Age":
                        result = this.AgeValidation();
                        break;
                    case "DesiredSalary":
                        result = this.DesiredSalaryValidation();
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        #endregion IDataErrorInfo Members

    }
}
