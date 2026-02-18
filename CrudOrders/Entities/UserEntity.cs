namespace CrudOrders.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    
    public string Role { get; set; } = "User";
    
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}