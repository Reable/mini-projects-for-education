namespace Blog.Exceptions;

public class UsersExistsExceptions(string login) : Exception($"Пользователь с логином {login} уже существует");

public class UsersNotExistsExceptions(string login) : Exception($"Пользователя с логином {login} не существует");

public class UserVerifyExceptions() : Exception($"Никоректные данные логина или пароля");
