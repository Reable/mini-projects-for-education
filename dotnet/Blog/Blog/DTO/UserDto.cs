using System.ComponentModel.DataAnnotations;

namespace Blog.DTO;

public record UserDto(int Id, string Login);

public record RegisterUserRequest(
    [Required(ErrorMessage = "Нужно ввести ваш логин")] string Login, 
    [Required, MinLength(10, ErrorMessage = "Минимум 10 символов")] string Password, 
    [property: Compare(nameof(RegisterUserRequest.Password), ErrorMessage = "Пароли не совпадают")]  string ConfirmedPassword
);

public record LoginUserRequest(
    [Required] string Login,
    [Required, MinLength(10, ErrorMessage = "Минимум 10 символов")] string Password
);