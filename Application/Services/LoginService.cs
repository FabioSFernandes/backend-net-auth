using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    class LoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Login(TokenDto token, Login login)
        {
            // Implementar a lógica de cadastro
            return null;
        }

        public async Task<TokenDto> Logout(TokenDto loginDto, Login login)
        {
            // Implementar a lógica de autenticação
            return null;
        }

        public async Task<TokenDto> Logout(TokenDto loginDto)
        {
            // Implementar a lógica de autenticação
            return null;
        }

        public async Task<TokenDto> LoginBlock(TokenDto loginDto)
        {
            // Implementar a lógica de autenticação
            return null;
        }


    }
}
