using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Helper;
using Application.Interfaces;
using BCrypt.Net;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<object>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.EmailOrPhone)
                       ?? await _userRepository.GetByPhoneNumberAsync(loginRequest.EmailOrPhone);

            if (user == null || !PasswordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email/phone or password.");
            List<object> response = new List<object>();
            response.Add(JwtTokenHelper.GenerateToken(user, _configuration));
            response.Add(user);
            return response;
        }

        public async Task<string> SignUpAsync(User user)
        {
            if (await _userRepository.GetByEmailAsync(user.Email) != null)
                throw new ArgumentException("A user with this email already exists.");

            if (await _userRepository.GetByPhoneNumberAsync(user.PhoneNumber) != null)
                throw new ArgumentException("A user with this phone number already exists.");

            user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);
            await _userRepository.AddAsync(user);

            return "User signed up successfully!";
        }
    }
}
