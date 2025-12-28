# Topic7 — Исключения и управление ресурсами (Полный курс для начинающих)

## Цель
Понять, как правильно обрабатывать ошибки в C#, когда использовать try/catch/finally, и как управлять ресурсами через IDisposable.

---

## 1. Что такое исключение? (Для самых начинающих)

### Аналогия
```
Обычный ход программы:
1. Сделай шаг вперед
2. Включи свет
3. Открой дверь
4. Войди в комнату

С ошибкой (исключением):
1. Сделай шаг вперед ?
2. Включи свет ?? ОШИБКА! Нет электричества!
   ? Программа прерывается
3. Открой дверь ? (никогда не выполнится)
4. Войди в комнату ? (никогда не выполнится)

С обработкой исключения:
1. Сделай шаг вперед ?
2. Попытка: включи свет
   ? ?? ОШИБКА! Нет электричества!
   ? Обработка: зажги свечу вместо света ?
3. Открой дверь ?
4. Войди в комнату ?
```

### В программировании:

```csharp
// БЕЗ обработки (программа падает)
int[] numbers = { 1, 2, 3 };
Console.WriteLine(numbers[10]);  // ?? IndexOutOfRangeException!
Console.WriteLine("Never printed");  // Эта строка не выполнится

// С обработкой (программа продолжает работать)
int[] numbers = { 1, 2, 3 };
try
{
    Console.WriteLine(numbers[10]);
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");  // Обработка ошибки
}
Console.WriteLine("Program continues");  // ? Выполнится!
```

---

## 2. Try / Catch / Finally

### Структура

```csharp
try
{
    // Код, который может выбросить исключение
    int result = 10 / int.Parse("0");  // ?? DivideByZeroException
}
catch (DivideByZeroException ex)
{
    // Обработка конкретной ошибки
    Console.WriteLine($"Нельзя делить на ноль: {ex.Message}");
}
catch (FormatException ex)
{
    // Обработка другой ошибки
    Console.WriteLine($"Неправильный формат: {ex.Message}");
}
catch (Exception ex)
{
    // Ловушка для любых других исключений
    Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
}
finally
{
    // Выполнится ВСЕГДА, независимо от ошибки
    Console.WriteLine("Очищаем ресурсы");
}

// Вывод:
// Нельзя делить на ноль: Attempted to divide by zero.
// Очищаем ресурсы
```

### Порядок исключений важен!

```csharp
try
{
    // ...
}
// ? Правильный порядок (от специфичного к общему)
catch (DivideByZeroException ex)
{
    // Сначала специфичные
}
catch (FormatException ex)
{
    // Потом другие специфичные
}
catch (Exception ex)
{
    // И в конце общее Exception
}

// ? НЕПРАВИЛЬНЫЙ порядок
catch (Exception ex)
{
    // Если поймаем общее исключение первым,
    // специфичные обработчики никогда не выполнятся!
}
catch (DivideByZeroException ex)
{
    // Эта строка не сработает никогда!
}
```

### Finally (всегда выполняется)

```csharp
try
{
    Console.WriteLine("1. Попытка");
    throw new Exception("Ошибка!");
    Console.WriteLine("2. Никогда не выполнится");  // ? Пропущено
}
catch (Exception ex)
{
    Console.WriteLine("3. Поймали ошибку");
    // return;  // Даже если здесь return, finally все равно выполнится!
}
finally
{
    Console.WriteLine("4. Finally ВСЕГДА выполнится");  // ? Выполнится
}

// Вывод:
// 1. Попытка
// 3. Поймали ошибку
// 4. Finally ВСЕГДА выполнится
```

---

## 3. Пользовательские исключения

### Создание собственного исключения

```csharp
// Создаем свой класс исключения
public class InvalidUsernameException : Exception
{
    public InvalidUsernameException(string message) : base(message) { }
}

// Использование
public class UserValidator
{
    public void ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidUsernameException("Username не может быть пустым!");
        }
        
        if (username.Length < 3)
        {
            throw new InvalidUsernameException("Username должен быть минимум 3 символа!");
        }
    }
}

// Ловим и обрабатываем
try
{
    var validator = new UserValidator();
    validator.ValidateUsername("ab");  // Слишком короткий
}
catch (InvalidUsernameException ex)
{
    Console.WriteLine($"Ошибка валидации: {ex.Message}");
}
```

---

## 4. IDisposable и using

### Проблема: управление ресурсами

```csharp
// Класс, который использует ресурсы (например, файл)
public class FileReader
{
    private StreamReader reader;
    
    public FileReader(string filePath)
    {
        reader = new StreamReader(filePath);
    }
    
    public void Close()
    {
        reader?.Close();  // Нужно вручную закрыть!
    }
}

// Проблема: можно забыть закрыть
FileReader reader = new FileReader("file.txt");
// ... используем reader ...
reader.Close();  // ? Что если забыли это написать?
```

### Решение 1: IDisposable

```csharp
// Реализуем IDisposable
public class FileReader : IDisposable
{
    private StreamReader reader;
    
    public FileReader(string filePath)
    {
        reader = new StreamReader(filePath);
    }
    
    // Метод для освобождения ресурсов
    public void Dispose()
    {
        reader?.Close();
        Console.WriteLine("Resources cleaned up");
    }
}

// Использование с using
using (FileReader reader = new FileReader("file.txt"))
{
    // Используем reader
}  // Dispose() вызовется автоматически!

// Вывод:
// Resources cleaned up
```

### Решение 2: using var (C# 8+)

```csharp
// Еще проще!
using var reader = new FileReader("file.txt");
// Используем reader
// После выхода из блока, Dispose() вызовется автоматически!
```

### Пример: FakeDriver для тестов

```csharp
public class FakeDriver : IDisposable
{
    private List<string> logs = new List<string>();
    
    public void LogAction(string action)
    {
        logs.Add(action);
        Console.WriteLine($"Action: {action}");
    }
    
    public void SaveLogsToFile(string filePath)
    {
        File.WriteAllLines(filePath, logs);
    }
    
    public void Dispose()
    {
        // Очистка: сохранить логи, закрыть соединения, и т.д.
        Console.WriteLine("Cleanup: Closing connections...");
        SaveLogsToFile("test_logs.txt");
    }
}

// Использование
using (var driver = new FakeDriver())
{
    driver.LogAction("Open page");
    driver.LogAction("Click button");
    driver.LogAction("Verify text");
}  // ? Автоматически вызовется Dispose()

// Вывод:
// Action: Open page
// Action: Click button
// Action: Verify text
// Cleanup: Closing connections...
```

### Полная реализация IDisposable (лучшие практики)

```csharp
public class Resource : IDisposable
{
    private bool disposed = false;
    
    public void DoSomething()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(Resource));
        }
        Console.WriteLine("Doing something...");
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);  // Не нужен финализатор
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Освобождаем управляемые ресурсы
                Console.WriteLine("Cleaning up managed resources");
            }
            
            // Освобождаем неуправляемые ресурсы
            Console.WriteLine("Cleaning up unmanaged resources");
            
            disposed = true;
        }
    }
    
    ~Resource()
    {
        Dispose(false);
    }
}
```

---

## 5. Best practices для обработки исключений

### ? DO:

```csharp
// Ловите специфичные исключения
try
{
    int.Parse("123");
}
catch (FormatException ex)  // ? Специфичное
{
    Console.WriteLine("Ошибка формата");
}

// Логируйте исключения
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.GetType().Name}");
    Console.WriteLine($"Message: {ex.Message}");
    Console.WriteLine($"StackTrace: {ex.StackTrace}");
}

// Пробрасывайте исключение если не знаете как его обработать
catch (Exception ex)
{
    Console.WriteLine("Cannot handle this");
    throw;  // ? Пробросить дальше с сохранением стека вызовов
}

// Используйте using для IDisposable
using var resource = new Resource();
resource.DoSomething();
```

### ? DON'T:

```csharp
// ? Не ловите все подряд
try
{
    // код
}
catch (Exception)  // ? ПЛОХО! Скрываете все ошибки
{
    // ignoring
}

// ? Не создавайте исключение без текста
throw new Exception();  // ? Непонятно, что случилось

// ? Не игнорируйте исключения
try { /* код */ }
catch { }  // ? Что произошло? Почему молчим?

// ? Не забывайте про using
FileStream stream = new FileStream("file.txt", FileMode.Open);
// ? Если вернуть раньше, поток остается открыт!
```

---

## 6. Частые ошибки новичков

### ? Ошибка 1: Неправильный порядок catch блоков
```csharp
try
{
    // ...
}
catch (Exception ex)  // ? Слишком общий, первым!
{
    Console.WriteLine("Ошибка");
}
catch (FormatException ex)  // ? Никогда не выполнится!
{
    Console.WriteLine("Ошибка формата");
}

// ? Правильно
catch (FormatException ex)
{
    Console.WriteLine("Ошибка формата");
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка");
}
```

### ? Ошибка 2: Забыли throw
```csharp
try
{
    int x = 10 / int.Parse("0");
}
catch (Exception ex)
{
    Console.WriteLine("Error");
    // ? Забыли throw! Ошибка просто исчезает!
}
Console.WriteLine("Program continues");  // Программа продолжает работать!

// ? Если нужно пробросить
catch (Exception ex)
{
    Console.WriteLine("Error");
    throw;  // ? Пробросить ошибку дальше
}
```

### ? Ошибка 3: Забыли using
```csharp
FileStream stream = new FileStream("file.txt", FileMode.Open);
// ? Если исключение, поток не закроется!

// ? Правильно
using var stream = new FileStream("file.txt", FileMode.Open);
// ? Даже если исключение, поток закроется
```

---

## 7. Типы исключений в .NET

| Исключение | Когда выбрасывается |
|-----------|-------------------|
| `ArgumentException` | Неправильный аргумент |
| `ArgumentNullException` | Аргумент null |
| `DivideByZeroException` | Деление на ноль |
| `FormatException` | Неправильный формат |
| `IndexOutOfRangeException` | Индекс вне массива |
| `InvalidOperationException` | Операция недействительна |
| `KeyNotFoundException` | Ключ не найден в Dictionary |
| `NotImplementedException` | Метод не реализован |
| `NullReferenceException` | Обращение к null |
| `OverflowException` | Переполнение |
| `StackOverflowException` | Переполнение стека |
| `TimeoutException` | Истекло время ожидания |

---

## Файлы в проекте:
- `Program.cs` — примеры обработки исключений
- `ElementNotFoundException.cs` — пользовательское исключение
- `FakeDriver.cs` — класс с IDisposable
