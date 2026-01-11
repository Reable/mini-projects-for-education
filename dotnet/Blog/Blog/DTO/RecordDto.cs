using System.ComponentModel.DataAnnotations;

namespace Blog.DTO;

public record RecordDto(int Id, int UserId, string Title, string Contend);

public record CreateRecordDto(
    [Required] int Id,
    [Required] int UserId,
    [Required, MinLength(5, ErrorMessage = "Минимальная длина 5 символов")] string Title,
    [Required, MinLength(10, ErrorMessage = "Минимальная длина 10 символов")] string Content
);

public record UpdateRecordDto(
    [Required] int RecordId,
    [Required] int UserId,
    [Required, MinLength(5, ErrorMessage = "Минимальная длина 5 символов")] string Title,
    [Required, MinLength(10, ErrorMessage = "Минимальная длина 10 символов")] string Content
);

public record DeleteRecordDto(
    [Required] int RecordId,
    [Required] int UserId
);