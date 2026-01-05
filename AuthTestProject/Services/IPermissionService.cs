using AuthTestProject.Entities;
using AuthTestProject.Models;


namespace AuthTestProject.Services
{
    public interface IPermissionService
    {
        Task<PermissionResponseDto?> AddPermission(PermissionDto permissionDto);
    }
}