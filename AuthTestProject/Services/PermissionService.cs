using AuthTestProject.Data;
using AuthTestProject.Entities;
using AuthTestProject.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthTestProject.Services
{
    public class PermissionService : IPermissionService
    {
        public readonly UserDbContext _context;

        public PermissionService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionResponseDto?> AddPermission(PermissionDto request)
        {
            var user = await _context.Users
                .Include(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                return null!;

            if(user.Permission.Any(p => p.Name == request.Name))
                return null!;

            var permission = new Permission
            {
                Name = request.Name,
                UserId = user.Id,
                User = user
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return new PermissionResponseDto
            {
                Id = permission.Id,
                Name = permission.Name,
                UserId = user.Id,
                Username = user.Username
            };
        }
    }
}