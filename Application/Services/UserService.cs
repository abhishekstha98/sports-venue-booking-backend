using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            // Check if the user already exists
            if (await _userRepository.GetByEmailAsync(user.Email) != null ||
                await _userRepository.GetByPhoneNumberAsync(user.PhoneNumber) != null)
            {
                throw new Exception("User already exists with the given email or phone number.");
            }

            // Hash the password
            //user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Save the user to the database
            await _userRepository.AddAsync(user);

            return true;
        }
    }
}
