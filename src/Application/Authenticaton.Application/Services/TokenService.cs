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
    class TokenService
    {
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(TokenDto token, Login login)
        {
            // Implementar a lógica de cadastro
            return null;
        }

        public async Task<TokenDto> Refresh(TokenDto loginDto)
        {
            // Implementar a lógica de autenticação
            return null;
        }
    }
}
