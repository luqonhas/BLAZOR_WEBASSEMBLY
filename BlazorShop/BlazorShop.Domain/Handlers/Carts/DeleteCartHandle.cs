﻿using BlazorShop.Domain.Commands.Carts;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Carts
{
    public class DeleteCartHandle : Notifiable<Notification>, IHandlerCommand<DeleteCartCommand>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteCartHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public ICommandResult Handler(DeleteCartCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the cart you want to delete", command.Notifications);
            }

            var searchedCart = _cartRepository.SearchById(command.Id);

            if (searchedCart == null)
            {
                return new GenericCommandResult(false, "Cart not found", command.Notifications);
            }

            _cartRepository.Delete(searchedCart.Id);

            return new GenericCommandResult(false, "Cart deleted successfully!", "");
        }
    }
}
