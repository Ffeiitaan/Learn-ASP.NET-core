using System.ComponentModel.DataAnnotations;
using CrudOrders.Data;
using CrudOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudOrders.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // View
        public async Task<UserDto> GetById(Guid Id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == Id);

            if(user == null) 
                throw new KeyNotFoundException("User not found");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                }).ToListAsync();
        }
    }
}