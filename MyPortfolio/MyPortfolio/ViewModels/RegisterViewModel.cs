﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPortfolio.Commands;
using MyPortfolio.Models;

namespace MyPortfolio.ViewModels
{
    internal class RegisterViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private PersonRepository personRepository;

        string username = string.Empty;
        public string UsernameText 
        {
            get => username; 
            set
            {
                username = value;
                OnPropertyChanged("UsernameText");
            } 
        }
        
        string password = string.Empty;
        public string PasswordText 
        {
            get => password; 
            set
            {
                password = value;
                OnPropertyChanged("PasswordText");
            }
        }

        string email = string.Empty;
        public string EmailText 
        {
            get => email; 
            set
            {
                email = value;
                OnPropertyChanged("EmailText");
            }
        }

        string displayName = string.Empty;
        public string DisplayNameText 
        {
            get => displayName; 
            set
            {
                displayName = value;
                OnPropertyChanged("DisplayNameText");
            } 
        }

        public RegisterViewModel()
        {
            personRepository = new PersonRepository();
        }

        public ICommand RegisterButtonCommand { get; set; } = new RegisterButtonCommand();

        public void RegisterNewPerson()
        {
            foreach (Person person in personRepository.GetPersonList())
            {
                if (person.Username == UsernameText)
                {
                    MessageBox.Show("User does already exist!");
                    return;
                }
            }

            Person personToCreate = new Person(UsernameText, PasswordText, EmailText, DisplayNameText);

            personRepository.CreateNewPerson(personToCreate);

            MessageBox.Show("Succesfully created a new person!!");
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
