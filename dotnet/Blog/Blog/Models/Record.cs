using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Record
{
    [Required]
    public required string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public required string UserId { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [MinLength(10, ErrorMessage = "Min length for Title 10 characters"), MaxLength(50)]
    public required string Title { get; set; } = "";

    [Required]
    [MinLength(10, ErrorMessage = "Min length for Content 10 characters")]
    public required string Content { get; set; } = "";
    
}