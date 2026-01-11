using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Record
{
    public int Id { get; set; } = 0;

    public int UserId { get; set; } = 0;
    
    public required string Title { get; set; } = "";

    public string Content { get; set; } = "";
    
}