﻿using BlazorShop.Domain.Commands.Authentications;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Utils;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Authentications
{
    public class LoginEmailHandle : Notifiable<Notification>, IHandlerCommand<LoginEmailCommand>
    {
        private readonly IUserRepository _authRepository;

        public LoginEmailHandle(IUserRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public ICommandResult Handler(LoginEmailCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Enter the data correctly", command.Notifications);
            }

            var searchedUser = _authRepository.SearchByEmail(command.Email);

            if (searchedUser == null)
            {
                return new GenericCommandResult(false, "Invalid e-mail or password", "");
            }

            // Descriptografar a senha do usuário encontrado e comparar com a senha fornecida
            if (!Password.ValidateHashes(command.Password, searchedUser.Password))
            {
                return new GenericCommandResult(false, "Invalid e-mail or password", "");
            }

            return new GenericCommandResult(true, "Successfully logged in!", searchedUser);
        }

    }
}
