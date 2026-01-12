namespace Blog.Exceptions;

public class UsersExistsExceptions(string login) : Exception($"Пользователь с логином {login} уже существует");

public class UsersNotExistsExceptions() : Exception($"Пользователя не существует");

public class UserVerifyExceptions() : Exception($"Никоректные данные логина или пароля");
