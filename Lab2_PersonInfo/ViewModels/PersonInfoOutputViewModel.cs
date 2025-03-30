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
    class PersonInfoOutputViewModel : INavigatable<PersonInfoNavigationType>
    {
        public PersonInfoNavigationType ViewModelType => PersonInfoNavigationType.PersonInfoOutput;
        public RelayCommand ToPersonInfoInputCommand { get; }

        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        PersonInfoViewModel _personInfoViewModel;

        public Person Person { get => _personInfoViewModel.Person; }

        public PersonInfoOutputViewModel(Action toPersonInfoInput, PersonInfoViewModel personInfoViewModel)
        {
            _personInfoViewModel = personInfoViewModel;
            ToPersonInfoInputCommand = new RelayCommand(toPersonInfoInput);
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
