using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lab2_PersonInfo.Models;
using Lab2_PersonInfo.Navigation;

namespace Lab2_PersonInfo.ViewModels
{
    class PersonInfoViewModel : INotifyPropertyChanged
    {
        private List<INavigatable<PersonInfoNavigationType>> _viewModels = [];

        private INavigatable<PersonInfoNavigationType> currentViewModel;

        public event PropertyChangedEventHandler? PropertyChanged;

        private Person? _person;
        public Person? Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public INavigatable<PersonInfoNavigationType> CurrentViewModel
        {
            get => currentViewModel;
            private set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public PersonInfoViewModel()
        {
            Navigate(PersonInfoNavigationType.PersonInfoInput);
        }

        internal void Navigate(PersonInfoNavigationType type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewModelType == type)
                return;

            INavigatable<PersonInfoNavigationType>? viewModel = GetViewModel(type);
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
                CurrentViewModel.IsEnabled = true;
            }
        }

        private INavigatable<PersonInfoNavigationType>? GetViewModel(PersonInfoNavigationType type)
        {
            INavigatable<PersonInfoNavigationType>? viewModel = _viewModels.FirstOrDefault(vm => vm.ViewModelType == type);

            if (viewModel == null)
            {
                switch (type)
                {
                    case PersonInfoNavigationType.PersonInfoInput:
                        viewModel = new PersonInfoInputViewModel(() => Navigate(PersonInfoNavigationType.PersonInfoOutput), this);
                        break;
                    case PersonInfoNavigationType.PersonInfoOutput:
                        viewModel = new PersonInfoOutputViewModel(() => Navigate(PersonInfoNavigationType.PersonInfoInput), this);
                        break;
                    default:
                        return null;
                }
                _viewModels.Add(viewModel);
            }
            return viewModel;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
