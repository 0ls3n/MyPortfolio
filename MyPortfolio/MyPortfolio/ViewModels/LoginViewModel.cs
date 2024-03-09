﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPortfolio.Models;
using MyPortfolio.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Windows.Interop;
using System.Text.Json;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        

        string[] scopes = new string[] { "user.read" };

        public event PropertyChangedEventHandler? PropertyChanged;

        MainViewModel mvm;

        string usernameText = string.Empty;
        public string UsernameText 
        { 
            get => usernameText;

            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }

        string passwordText = string.Empty;
        public string PasswordText 
        {
            get => passwordText; 
            set
            {
                passwordText = value;
                OnPropertyChanged("PasswordText");
            }
        }

        private PersonRepository personRepository;

        Person personToLogin;

        public LoginViewModel() 
        {
            personRepository = new PersonRepository();
        }

        public ICommand LoginCommand { get; set; } = new LoginButtonCommand();

        //public bool Login()
        //{
        //    personToLogin = personRepository.FindPerson(UsernameText);

        //    if (personToLogin != null && personToLogin.Password == PasswordText)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public void ReadUserFromJSON(string jsonFile)
        {
            MicrosoftUser microsoftPerson = null;
            try
            {
                microsoftPerson = JsonSerializer.Deserialize<MicrosoftUser>(jsonFile);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            if (microsoftPerson != null)
            {
                if (personRepository.FindPerson(microsoftPerson.id) != null)
                {
                    //MessageBox.Show($"Id: {microsoftPerson.id}, DisplayName: {microsoftPerson.displayName}");
                    personToLogin = microsoftPerson;
                } else
                {
                    //MessageBox.Show($"Person with Id: {microsoftPerson.id} does not exist in the current register");
                    personRepository.CreateNewMicrosoftPerson(microsoftPerson);
                    personToLogin = microsoftPerson;
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
