# Topic7 — Исключения и управление ресурсами (Полный курс для начинающих)

## Цель

Понять, как правильно обрабатывать ошибки в C#, когда использовать try/catch/finally, и как управлять ресурсами через IDisposable.

---

### Для полного новичка: быстрый маршрут

- Прочитайте разделы: "Что такое исключение?", "Try/Catch/Finally", "IDisposable и using".
- Запустите Program.cs и посмотрите, как ведет себя программа при ошибке и при обработке.
- Вернитесь к чек‑листу: под каждым вопросом есть короткий ответ, а подробности выше.

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
Console.WriteLine(numbers[10]);  //  IndexOutOfRangeException!
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
Console.WriteLine("Program continues");  //  Выполнится!
```

---

## 2. Try / Catch / Finally

### Структура

```csharp
try
{
    // Код, который может выбросить исключение
    int result = 10 / int.Parse("0");  //  DivideByZeroException
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
//  Правильный порядок (от специфичного к общему)
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

//  НЕПРАВИЛЬНЫЙ порядок
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
    Console.WriteLine("2. Никогда не выполнится");  // ℹ Пропущено
}
catch (Exception ex)
{
    Console.WriteLine("3. Поймали ошибку");
    // return;  // Даже если здесь return, finally все равно выполнится!
}
finally
{
    Console.WriteLine("4. Finally ВСЕГДА выполнится");  //  Выполнится
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
reader.Close();  //  Что если забыли это написать?
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
}  //  Автоматически вызовется Dispose()

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
catch (FormatException ex)  // ℹ Специфичное
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
    throw;  // ℹ Пробросить дальше с сохранением стека вызовов
}

// Используйте using для IDisposable
using var resource = new Resource();
resource.DoSomething();
```

### ? DON'T:

```csharp
//  Не ловите все подряд
try
{
    // код
}
catch (Exception)  //  ПЛОХО! Скрываете все ошибки
{
    // ignoring
}

//  Не создавайте исключение без текста
throw new Exception();  //  Непонятно, что случилось

//  Не игнорируйте исключения
try { /* код */ }
catch { }  //  Что произошло? Почему молчим?

//  Не забывайте про using
FileStream stream = new FileStream("file.txt", FileMode.Open);
// ℹ Если вернуть раньше, поток остается открыт!
```

---

## 6. Частые ошибки новичков

### ? Ошибка 1: Неправильный порядок catch блоков

```csharp
try
{
    // ...
}
catch (Exception ex)  //  Слишком общий, первым!
{
    Console.WriteLine("Ошибка");
}
catch (FormatException ex)  //  Никогда не выполнится!
{
    Console.WriteLine("Ошибка формата");
}

//  ПРАВИЛЬНО
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
    //  Забыли throw! Ошибка просто исчезает!
}
Console.WriteLine("Program continues");  // Программа продолжает работать!

// ℹ Если нужно пробросить
catch (Exception ex)
{
    Console.WriteLine("Error");
    throw;  // ℹ Пробросить ошибку дальше
}
```

### ? Ошибка 3: Забыли using

```csharp
FileStream stream = new FileStream("file.txt", FileMode.Open);
// ℹ Если исключение, поток не закроется!

//  ПРАВИЛЬНО
using var stream = new FileStream("file.txt", FileMode.Open);
//  Даже если исключение, поток закроется
```

---

## 7. Типы исключений в .NET

| Исключение                  | Когда выбрасывается         |
| --------------------------- | --------------------------- |
| `ArgumentException`         | Неправильный аргумент       |
| `ArgumentNullException`     | Аргумент null               |
| `DivideByZeroException`     | Деление на ноль             |
| `FormatException`           | Неправильный формат         |
| `IndexOutOfRangeException`  | Индекс вне массива          |
| `InvalidOperationException` | Операция недействительна    |
| `KeyNotFoundException`      | Ключ не найден в Dictionary |
| `NotImplementedException`   | Метод не реализован         |
| `NullReferenceException`    | Обращение к null            |
| `OverflowException`         | Переполнение                |
| `StackOverflowException`    | Переполнение стека          |
| `TimeoutException`          | Истекло время ожидания      |

---

## Файлы в проекте:

- `Program.cs` — примеры обработки исключений
- `ElementNotFoundException.cs` — пользовательское исключение
- `FakeDriver.cs` — класс с IDisposable

---

## 8. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ 🎯

### Вопрос 1: Что такое исключение и когда оно возникает?

Краткий ответ: Исключение — сигнал об ошибке во время выполнения (например, деление на ноль, выход за границы массива, неверный формат). Без обработки программа прерывается.

### Вопрос 2: Для чего нужен try/catch/finally?

Краткий ответ: try — код с риском; catch — обработка конкретной ошибки; finally — выполняется всегда (для очистки ресурсов), даже если было исключение.

### Вопрос 3: Почему порядок catch важен?

Краткий ответ: Сначала ловите специфичные исключения, потом общие (Exception). Иначе общее перехватит всё и до специфичных обработчиков дело не дойдет.

### Вопрос 4: Когда пробрасывать (rethrow) исключение?

Краткий ответ: Если не можете корректно обработать — логируйте и пробрасывайте `throw;`, чтобы сохранить стек вызовов. Не “глотайте” ошибки.

### Вопрос 5: Что такое IDisposable и чем помогает using?

Краткий ответ: IDisposable определяет освобождение ресурсов. `using` автоматически вызывает `Dispose()` при выходе из блока — ресурсы не «утекут» даже при ошибке.

### Вопрос 6: Как правильно реализовать Dispose по паттерну?

Краткий ответ: Реализуйте `Dispose()` → вызывайте `Dispose(bool disposing)` → `GC.SuppressFinalize(this)` → защищайтесь от повторного вызова и от использования после Dispose.

### Вопрос 7: Какие частые ошибки у новичков?

Краткий ответ: Неправильный порядок catch; проглатывание исключений без `throw`; забытый `using` для ресурсов; создание Exception без сообщения.

### Вопрос 8: Как спроектировать свое исключение?

Краткий ответ: Наследуйтесь от `Exception`, добавьте понятное сообщение/контекст (например, селектор элемента), при необходимости — дополнительные поля, и документируйте класс.
