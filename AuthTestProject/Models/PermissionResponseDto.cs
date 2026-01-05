using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthTestProject.Models
{
    public class PermissionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}