namespace Blog.Exceptions;


public class RecordsNotExistsExceptions() : Exception($"Запись не найдена");

public class NotAccessToUpdateRecordExceptions() : Exception($"Нет доступа к изменению записи");
