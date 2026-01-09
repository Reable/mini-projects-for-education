using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class User
{
    [Required]
    public int Id { get; set; } = 0;

    [Required]
    [MinLength(5, ErrorMessage = "Login must be at least 5 characters long (/Models/User -> Login)")]
    public string Login { get; set; } = "";
    
    [Required]
    [MinLength(10,  ErrorMessage = "Password must be at least 10 characters long (/Models/User -> Password)")]
    public string Password { get; set; } = "";
}