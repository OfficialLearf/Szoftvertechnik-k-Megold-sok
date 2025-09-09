using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloXaml.Models;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloXaml.ViewModels
{
    public partial class PersonListPageViewModel : ObservableObject
    {

        public PersonListPageViewModel()
        {
            NewPerson.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(NewPerson.Age) || e.PropertyName == nameof(NewPerson.Name))
                {
                    DecreaseAgeCommand.NotifyCanExecuteChanged();
                    IncreaseAgeCommand.NotifyCanExecuteChanged();
                    OnPropertyChanged(nameof(IsAddEnabled));

                }
            };
        }

        [RelayCommand(CanExecute = nameof(IsDecrementEnabled))]
        public void DecreaseAge()
        {
            NewPerson.Age--;
        }

        [RelayCommand(CanExecute = nameof(IsIncrementEnabled))]
        public void IncreaseAge()
        {
            NewPerson.Age++;
        }


        [ObservableProperty]
        private Person _newPerson = new();

        public ObservableCollection<Person> People { get; } = new();
        public bool IsAddEnabled => !string.IsNullOrWhiteSpace(NewPerson.Name) && NewPerson.Age >= 0 && NewPerson.Age <= 150;

        public bool IsDecrementEnabled => NewPerson.Age > 0;

        public bool IsIncrementEnabled => NewPerson.Age < 150;

        public void AddPersonToList()
        {

            People.Add(new Person { Name = NewPerson.Name, Age = NewPerson.Age });
            NewPerson.Name = string.Empty;
            NewPerson.Age = 0;

        }   
    }
}
