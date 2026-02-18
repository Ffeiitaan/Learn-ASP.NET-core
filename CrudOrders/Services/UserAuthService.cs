using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CrudOrders.Data;
using CrudOrders.Entities;
using CrudOrders.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


namespace CrudOrders.Services
{
    public class UserAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserAuthService(
            AppDbContext context, 
            IConfiguration configuration,
            IPasswordHasher<UserEntity> passwordHasher
            )
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
        {
            if(await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new InvalidOperationException("User with this email already exists");

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Role = Roles.User
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if(user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var verificationResult  = VerifyPassword(user, request.Password);

            if(verificationResult  == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");

            if(verificationResult  == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                await _context.SaveChangesAsync();
            }
            
            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                Role = user.Role
            };
        }

        private PasswordVerificationResult  VerifyPassword(UserEntity user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}