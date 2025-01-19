using System;
using System.Threading.Tasks;
using Application.Helper;
using Application.Interfaces;
using BCrypt.Net;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
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

        // Login method
        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            // Retrieve the user by email or phone number
            var user = await _userRepository.GetByEmailAsync(loginRequest.EmailOrPhone)
                       ?? await _userRepository.GetByPhoneNumberAsync(loginRequest.EmailOrPhone);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email/phone or password.");

            // Generate JWT token
            var token = JwtTokenHelper.GenerateToken(user, _configuration);

            return token;
        }

        // Sign-up method
        public async Task<string> SignUpAsync(User user)
        {
            // Check if the user already exists
            if (await _userRepository.GetByEmailAsync(user.Email) != null)
                throw new InvalidOperationException("A user with this email already exists.");

            if (await _userRepository.GetByPhoneNumberAsync(user.PhoneNumber) != null)
                throw new InvalidOperationException("A user with this phone number already exists.");

            // Hash the password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Save the user
            await _userRepository.AddAsync(user);

            return "User signed up successfully!";
        }
    }
}
