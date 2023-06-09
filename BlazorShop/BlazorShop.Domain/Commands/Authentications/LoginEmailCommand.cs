﻿using BlazorShop.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.Authentications
{
    public class LoginEmailCommand : Notifiable<Notification>, ICommand
    {
        public LoginEmailCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsEmail(Email, "email", "Enter a valid email address")
                .IsGreaterThan(Password, 6, "The 'password' field must have at least 6 characters")
            );
        }
    }
}
