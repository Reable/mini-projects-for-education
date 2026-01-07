using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class User
{
    [Required]
    public required string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MinLength(5, ErrorMessage = "Login must be at least 5 characters long (/Models/User -> Login)")]
    public required string Login { get; set; } = "";
    
    [Required]
    [MinLength(10,  ErrorMessage = "Password must be at least 10 characters long (/Models/User -> Password)")]
    public required string Password { get; set; } = "";
}