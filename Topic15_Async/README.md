# Topic15 — Async/Await и асинхронное программирование

## Цель

Понять асинхронное программирование в C#, когда и как использовать async/await, работать с Task и исключениями в асинхронном коде.

---

### Для полного новичка: быстрый маршрут

- Прочитайте: "Что такое async?", "Task vs Task<T>", "async/await синтаксис"
- Запустите Program.cs: `dotnet run`
- Особое внимание: примеры для QA (WebDriver, HTTP запросы)
- Вернитесь к чек-листу в конце

---

## Содержание

1. [Что такое асинхронность?](#1-что-такое-асинхронность)
2. [Thread vs Task vs Async/Await](#2-thread-vs-task-vs-asyncawait)
3. [Task и Task<T>](#3-task-и-taskt)
4. [async/await синтаксис](#4-asyncawait-синтаксис)
5. [Исключения в async коде](#5-исключения-в-async-коде)
6. [Async patterns и best practices](#6-async-patterns-и-best-practices)
7. [Практические примеры для QA](#7-практические-примеры-для-qa)
8. [Частые ошибки](#8-частые-ошибки)

---

## 1. Что такое асинхронность?

### Синхронный код (блокирующий)

```csharp
// ❌ Синхронный: код ЖДЕТ завершения
Console.WriteLine("Начало");
System.Threading.Thread.Sleep(2000);  // Ждём 2 секунды
Console.WriteLine("Конец");

// Проблема: UI заморожен, ничего не происходит 2 секунды!
```

### Асинхронный код (неблокирующий)

```csharp
// ✅ Асинхронный: код НЕ ЖДЕТ
Console.WriteLine("Начало");
await Task.Delay(2000);  // "Отпустите" операцию, вернитесь позже
Console.WriteLine("Конец");

// Преимущество: программа может делать другие дела в этот момент!
```

### Аналогия из жизни

```
Синхронный: вы стоите в очереди за кофе и ждёте, пока его сделают
Асинхронный: кассир вам номер, вы идёте читать газету, когда номер вызовут — забираете кофе
```

### Почему это важно для QA?

- **WebDriver операции** занимают время (клики, ввод текста, ожидание элемента)
- **Асинхронный код** позволяет тестам работать быстрее
- **Параллельные тесты** работают одновременно, не блокируя друг друга

---

## 2. Thread vs Task vs Async/Await

### ❌ Thread (старый способ)

```csharp
// Создаём новый поток (тяжёлая операция!)
var thread = new System.Threading.Thread(() =>
{
    Console.WriteLine("Работа в потоке");
});
thread.Start();
thread.Join();  // Ждём завершения

// Проблема: потоки дорогие, сложно управлять
```

### 🟡 Task (лучше)

```csharp
// Task = "задача" (легче чем thread)
Task task = Task.Run(() =>
{
    System.Threading.Thread.Sleep(1000);
    Console.WriteLine("Задача выполнена");
});

task.Wait();  // Ждём завершения
// Или: await task;

// Лучше: pooling потоков, меньше памяти
```

### ✅ Async/Await (современный способ)

```csharp
// async/await = явное управление асинхронностью
async Task DoWorkAsync()
{
    Console.WriteLine("Начало");
    await Task.Delay(1000);  // Асинхронно ждём 1 сек
    Console.WriteLine("Конец");
}

// Вызов:
await DoWorkAsync();

// Лучше: чистый синтаксис, обработка исключений, CancellationToken
```

### Таблица сравнения

| Способ          | Сложность | Производительность | Управление | Рекомендация         |
| --------------- | --------- | ------------------ | ---------- | -------------------- |
| **Thread**      | Высокая   | ❌ Плохая          | Трудно     | ❌ Не используйте    |
| **Task**        | Средняя   | ✅ Хорошая         | Средне     | 🟡 Для фонового кода |
| **Async/Await** | Низкая    | ✅ Отличная        | Легко      | ✅ ИСПОЛЬЗУЙТЕ       |

---

## 3. Task и Task<T>

### Task — ничего не возвращает

```csharp
// Task = асинхронная операция, которая ничего не возвращает
async Task WaitAndPrint()
{
    await Task.Delay(1000);
    Console.WriteLine("Готово!");
}

// Использование
await WaitAndPrint();
```

### Task<T> — возвращает значение

```csharp
// Task<T> = асинхронная операция, которая возвращает значение
async Task<int> GetNumberAsync()
{
    await Task.Delay(1000);
    return 42;
}

// Использование
int result = await GetNumberAsync();  // result = 42
```

### Практический пример для QA

```csharp
// Асинхронно получаем текст элемента
async Task<string> GetElementTextAsync(string xpath)
{
    await Task.Delay(500);  // Имитируем поиск элемента
    return "Button Text";
}

// Использование
string text = await GetElementTextAsync("//*[@id='submit']");
Console.WriteLine($"Текст кнопки: {text}");
```

---

## 4. Async/Await синтаксис

### Основное правило

```
async Task или async Task<T>  = функция может быть await-ed
await операция                = ждём асинхронного результата
```

### Простой пример

```csharp
// ❌ Неправильно: функция async, но нет await
async Task DoWork()
{
    Console.WriteLine("Hello");  // Нет async операций!
}

// ✅ Правильно: есть await асинхронная операция
async Task DoWork()
{
    await Task.Delay(1000);
}
```

### Цепочка вызовов

```csharp
// Функция 1
async Task<int> FetchDataAsync()
{
    await Task.Delay(1000);
    return 42;
}

// Функция 2 вызывает Функцию 1
async Task<int> ProcessDataAsync()
{
    int data = await FetchDataAsync();  // Ждём результат
    return data * 2;
}

// Главная функция
async Task Main()
{
    int result = await ProcessDataAsync();
    Console.WriteLine(result);  // 84
}
```

### async void — ЗАПРЕЩЕНО! ❌

```csharp
// ❌ НИКОГДА не делайте так!
async void DoSomething()  // async void — ошибка!
{
    await Task.Delay(1000);
}

// Проблемы:
// 1. Нельзя await-ить
// 2. Исключения не обрабатываются
// 3. Трудно отследить ошибки

// ✅ Правильно:
async Task DoSomething()
{
    await Task.Delay(1000);
}
```

---

## 5. Исключения в async коде

### Обработка исключений

```csharp
async Task<string> FetchUrlAsync(string url)
{
    // Имитируем ошибку сети
    if (url.Contains("invalid"))
        throw new HttpRequestException("Invalid URL");

    await Task.Delay(500);
    return "Success";
}

// Обработка
try
{
    string result = await FetchUrlAsync("invalid");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"❌ Ошибка: {ex.Message}");
}
```

### AggregateException в Task.WhenAll

```csharp
var tasks = new[]
{
    Task.Delay(100),
    Task.FromException(new Exception("Task 2 failed")),
    Task.Delay(100)
};

try
{
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    // ex.InnerException содержит исходную ошибку
    Console.WriteLine($"❌ {ex.InnerException?.Message}");
}
```

---

## 6. Async Patterns и Best Practices

### Pattern 1: Параллельное выполнение (Task.WhenAll)

```csharp
async Task<(string, string, string)> FetchMultipleAsync()
{
    var task1 = FetchUserAsync();
    var task2 = FetchPostsAsync();
    var task3 = FetchCommentsAsync();

    // Выполняются ПАРАЛЛЕЛЬНО, ждём всех
    await Task.WhenAll(task1, task2, task3);

    return (task1.Result, task2.Result, task3.Result);
}

async Task<string> FetchUserAsync() { await Task.Delay(100); return "User"; }
async Task<string> FetchPostsAsync() { await Task.Delay(150); return "Posts"; }
async Task<string> FetchCommentsAsync() { await Task.Delay(200); return "Comments"; }
```

### Pattern 2: Гонка (Task.WhenAny)

```csharp
async Task<string> FetchWithTimeoutAsync()
{
    var fetchTask = FetchUserAsync();
    var timeoutTask = Task.Delay(5000);  // Таймаут 5 сек

    // Кто первый завершится?
    var completed = await Task.WhenAny(fetchTask, timeoutTask);

    if (completed == timeoutTask)
    {
        Console.WriteLine("❌ Таймаут!");
        return null;
    }

    return fetchTask.Result;
}

async Task<string> FetchUserAsync() { await Task.Delay(1000); return "User"; }
```

### Pattern 3: CancellationToken

```csharp
async Task FetchWithCancellationAsync(CancellationToken ct)
{
    try
    {
        await Task.Delay(5000, ct);  // Можно отменить!
        Console.WriteLine("✅ Завершено");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("❌ Отменено");
    }
}

// Использование
var cts = new CancellationTokenSource();
var task = FetchWithCancellationAsync(cts.Token);

// После 1 секунды отменяем
await Task.Delay(1000);
cts.Cancel();  // Отмена!

await task;
```

---

## 7. Практические примеры для QA

### Пример 1: WebDriver асинхронный поиск элемента

```csharp
public class AsyncWebDriver
{
    async Task<string> FindAndGetTextAsync(string xpath, int timeoutSeconds = 10)
    {
        var startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < timeoutSeconds)
        {
            try
            {
                // Имитируем поиск элемента
                if (xpath.Contains("found"))
                    return "Button Text";

                await Task.Delay(500);  // Ждём 500ms и пробуем снова
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Попытка не удалась: {ex.Message}");
                await Task.Delay(500);
            }
        }

        throw new TimeoutException($"Element {xpath} not found after {timeoutSeconds}s");
    }
}

// Использование
var driver = new AsyncWebDriver();
string text = await driver.FindAndGetTextAsync("//*[@id='found']");
Console.WriteLine($"Текст: {text}");
```

### Пример 2: Параллельные тесты

```csharp
async Task RunAllTestsInParallelAsync()
{
    var tests = new[]
    {
        TestLoginAsync(),
        TestSearchAsync(),
        TestCheckoutAsync(),
        TestProfileAsync()
    };

    // Все тесты работают одновременно!
    await Task.WhenAll(tests);

    Console.WriteLine("✅ Все тесты пройдены!");
}

async Task TestLoginAsync() { await Task.Delay(1000); Console.WriteLine("✅ Login"); }
async Task TestSearchAsync() { await Task.Delay(800); Console.WriteLine("✅ Search"); }
async Task TestCheckoutAsync() { await Task.Delay(1200); Console.WriteLine("✅ Checkout"); }
async Task TestProfileAsync() { await Task.Delay(900); Console.WriteLine("✅ Profile"); }
```

### Пример 3: Retry асинхронно

```csharp
async Task<T> RetryAsync<T>(Func<Task<T>> operation, int maxAttempts = 3, int delayMs = 1000)
{
    for (int i = 1; i <= maxAttempts; i++)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex) when (i < maxAttempts)
        {
            Console.WriteLine($"⚠️  Попытка {i} провалилась: {ex.Message}");
            await Task.Delay(delayMs);
        }
    }

    throw new Exception($"Operation failed after {maxAttempts} attempts");
}

// Использование
var result = await RetryAsync(async () =>
{
    // Имитируем API запрос
    await Task.Delay(500);
    if (new Random().Next(2) == 0)
        throw new HttpRequestException("Server error");
    return "Success";
});

Console.WriteLine($"✅ Результат: {result}");
```

---

## 8. Частые ошибки

### ❌ Ошибка 1: .Result вместо await (deadlock!)

```csharp
// ❌ ОПАСНО: может привести к deadlock!
async Task<int> GetValueAsync()
{
    await Task.Delay(1000);
    return 42;
}

// Неправильно:
int value = GetValueAsync().Result;  // Может зависнуть!

// Правильно:
int value = await GetValueAsync();
```

### ❌ Ошибка 2: async void

```csharp
// ❌ Никогда не делайте:
async void FetchData()  // async void = зло!
{
    await Task.Delay(1000);
}

// Правильно:
async Task FetchData()
{
    await Task.Delay(1000);
}
```

### ❌ Ошибка 3: Забыли await

```csharp
// ❌ Ошибка: забыли await
async Task DoWork()
{
    var task = Task.Delay(1000);  // Забыли await!
    Console.WriteLine("Готово");  // Выполнится немедленно!
}

// Правильно:
async Task DoWork()
{
    await Task.Delay(1000);  // await!
    Console.WriteLine("Готово");  // Выполнится через 1 сек
}
```

### ❌ Ошибка 4: Нет обработки исключений

```csharp
// ❌ Исключение потеряется:
Task task = FetchAsync();  // Забыли await!
// Если FetchAsync выбросит exception — мы его не поймаем!

// Правильно:
try
{
    await FetchAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
```

### ✅ Ошибка 5: Закрыли Dispose слишком рано

```csharp
// ❌ Неправильно:
async Task FetchAsync()
{
    using (var client = new HttpClient())
    {
        var response = client.GetAsync("http://example.com");  // Забыли await!
    }  // client disposed, но запрос ещё идёт!
}

// Правильно:
async Task FetchAsync()
{
    using (var client = new HttpClient())
    {
        var response = await client.GetAsync("http://example.com");  // await!
    }
}
```

---

## 9. ЧЕК-ЛИСТ ПРОВЕРКИ ЗНАНИЙ 🎯

### Вопрос 1: Что такое async/await и почему это нужно?

**Ответ:** async/await позволяет написать асинхронный код, который выглядит синхронным. Нужно для неблокирующих операций (WebDriver, HTTP запросы, файлы).

### Вопрос 2: Task vs Task<T> — в чем разница?

**Ответ:** Task — асинхронная операция без возврата. Task<T> — возвращает значение типа T.

### Вопрос 3: Можно ли использовать async void?

**Ответ:** ❌ Нет! Только async Task. async void используется только для обработчиков событий (event handlers).

### Вопрос 4: Что произойдёт, если использовать .Result вместо await?

**Ответ:** Может произойти deadlock или блокировка потока. Всегда используйте await.

### Вопрос 5: Как запустить несколько async операций параллельно?

**Ответ:** Task.WhenAll(task1, task2, task3) — все запускаются одновременно.

### Вопрос 6: Как обработать исключение в async коде?

**Ответ:** try/catch как обычно, async код пробросит исключение в await точку.

### Вопрос 7: Что такое CancellationToken?

**Ответ:** Токен для отмены асинхронной операции (например, таймаут тестов).

### Вопрос 8: Как использовать async/await в QA тестах?

**Ответ:** Для поиска элементов с таймаутом, параллельных тестов, retry логики, работы с WebDriver.

---

## Файлы в проекте

- `Program.cs` — примеры всех концепций
- `README.md` — этот файл

---

**Запустите:** `cd Topic15_Async && dotnet run`

**Готово к использованию!** ✅
