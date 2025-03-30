using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab2_PersonInfo.Models;
using Lab2_PersonInfo.Navigation;

namespace Lab2_PersonInfo.ViewModels
{
    class PersonInfoInputViewModel : INavigatable<PersonInfoNavigationType>, INotifyPropertyChanged
    {
        public PersonInfoNavigationType ViewModelType => PersonInfoNavigationType.PersonInfoInput;

        public RelayCommand ToPersonInfoOutputCommand { get; }

        private bool _isEnabled = true;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime? _birthDate;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                UpdateCanExecute();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                UpdateCanExecute();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                UpdateCanExecute();
            }
        }

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                UpdateCanExecute();
            }
        }

        private Action _toPersonInfoOutput;
        private PersonInfoViewModel _personInfoViewModel;

        public PersonInfoInputViewModel(Action toPersonInfoOutput, PersonInfoViewModel personInfoViewModel)
        {
            _toPersonInfoOutput = toPersonInfoOutput;
            _personInfoViewModel = personInfoViewModel;
            ToPersonInfoOutputCommand = new RelayCommand(proceedCheck, CanExecute);
        }

        private async void proceedCheck()
        {
            IsEnabled = false;
            LoaderVisibility = Visibility.Visible;

            try
            {
                Person person = await Task.Run(() => new Person(FirstName, LastName, Email, (DateTime)BirthDate));
                _personInfoViewModel.Person = person;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign in failed: {ex.Message}");
                IsEnabled = true;
                LoaderVisibility = Visibility.Collapsed;
                return;
            }
            _toPersonInfoOutput?.Invoke();


            LoaderVisibility = Visibility.Collapsed;
            
        }

        private bool CanExecute()
        {
            return !String.IsNullOrWhiteSpace(FirstName)
                    && !String.IsNullOrWhiteSpace(LastName)
                    && !String.IsNullOrWhiteSpace(Email)
                    && BirthDate != null;
        }
        private void UpdateCanExecute()
        {
            ToPersonInfoOutputCommand.NotifyCanExecuteChanged();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
