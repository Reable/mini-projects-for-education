using System.ComponentModel.DataAnnotations;

namespace Blog.DTO;

public record RecordDto(int Id, int UserId, string Title, string Contend);

public record CreateRecordRequest(
    [Required] int UserId,
    [Required, MinLength(5, ErrorMessage = "Минимальная длина 5 символов")] string Title,
    [Required, MinLength(10, ErrorMessage = "Минимальная длина 10 символов")] string Content
);

public record UpdateRecordRequest(
    [Required] int RecordId,
    [Required] int UserId,
    [Required, MinLength(5, ErrorMessage = "Минимальная длина 5 символов")] string Title,
    [Required, MinLength(10, ErrorMessage = "Минимальная длина 10 символов")] string Content
);

public record DeleteRecordRequest(
    [Required] int RecordId,
    [Required] int UserId
);