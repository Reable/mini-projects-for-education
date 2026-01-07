namespace Blog.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string login) 
        : base($"Пользователь с логином {login} уже существует") { }

    public string? Login { get; set; }
}