using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Interfaces;


namespace Application.Services
{
    class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            // Implementar a lógica de cadastro
            return null;
        }

        public async Task<TokenDto> AuthenticateUserAsync(LoginDto loginDto)
        {
            // Implementar a lógica de autenticação
            return null;
        }
    }
}
