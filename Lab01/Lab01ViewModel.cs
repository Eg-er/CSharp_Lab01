using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;


namespace Lab01
{
    internal sealed class Lab01ViewModel : INotifyPropertyChanged
    {
        private static readonly string[] WesternZodiacs =
        {
            "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio",
            "Sagittarius", "Capricorn"
        };

        private static readonly string[] ChineseZodiacs =
            {"Monkey", "Rooster", "Dog", "Pig","Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};

        #region Fields
        private readonly User _user;
        private RelayCommand<object> _submit;
        #endregion
        
        #region Properties

        public string Age
        {
            get { return _user.Age; }
            set 
            {
                _user.Age = value; 
                OnPropertyChanged(); 
            }
        }

        public DateTime Date
        {
            get { return _user.Date; }
            set
            {
                _user.Date = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get { return _user.WesternZodiac; }
            set
            {
                _user.WesternZodiac = value;
                OnPropertyChanged();
            }
            
        }

        public string ChineseZodiac
        {
            get { return _user.ChineseZodiac; }
            set
            {
                _user.ChineseZodiac = value; 
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> Submit
        {
            get { return _submit ?? (_submit = new RelayCommand<object>(SubmitForm)); }
        }
        #endregion
        internal Lab01ViewModel()
        {
            _user = new User();
            Date = DateTime.Today;
        }
        private async void SubmitForm(object obj)
        {
            Age = "";
            WesternZodiac = "";
            ChineseZodiac = "";
            await Task.Run(Calculate);
        }

        private void Calculate()
        {
            try
            {
                CalculateAge();
                CalculateWesternZodiac();
                CalculateChineseZodiac();
                CalculateBirthday();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void CalculateAge()
        {
            var now = DateTime.Today;
            var age = now.Year - Date.Year;
            if (age > 0 && age <= 135)
            {
                Age = "You are " + age + " now!";
                if (age > 100)
                {
                    Age += " How did you learn how to use a computer?!?";
                }
            }
            else
            {
                throw new ArgumentException("It is impossible to have such age!!!");
            }
            
        }

        private void CalculateBirthday()
        {
            if (Date.Month == DateTime.Now.Month && Date.Day == DateTime.Now.Day)
            {
                MessageBox.Show("З днем народження!!! Щастя, здоровля, зарахів у Бублика!!!");
            }
        }

        private void CalculateWesternZodiac()
        {
            var month = Date.Month;
            var day = Date.Day;
            var res = "";
            switch (month)
            {
                case 2:
                    res+= day >= 19 ? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 1: case 4:
                    res+= day>=20? WesternZodiacs[month-1] :(month==1?WesternZodiacs[WesternZodiacs.Length-1]:WesternZodiacs[month-2]);
                    break;
                case 3: case 5: case 6:
                    res+= day>=21? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 11: case 12:
                    res+= day>=22? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 7: case 8: case 9: case 10:
                    res+= day>=23? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
            }
            WesternZodiac = "Your western zodiac sign: " + res;
        }

        private void CalculateChineseZodiac()
        {
            var year = Date.Year;
            var res = ChineseZodiacs[year % 12];
            ChineseZodiac = "Your chinese zodiac sign: " + res;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}