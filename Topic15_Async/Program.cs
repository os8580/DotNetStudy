using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("📚 Topic15: Async/Await в C#\n");
        Console.WriteLine(new string('=', 60));

        // ✅ Example 1: Простой async/await
        await Example1_SimpleAsync();
        
        // ✅ Example 2: Task<T> — возвращаем значение
        await Example2_TaskWithReturn();
        
        // ✅ Example 3: Параллельное выполнение (Task.WhenAll)
        await Example3_Parallel();
        
        // ✅ Example 4: Гонка между задачами (Task.WhenAny)
        await Example4_RaceCondition();
        
        // ✅ Example 5: Исключения в async
        await Example5_Exceptions();
        
        // ✅ Example 6: CancellationToken
        await Example6_Cancellation();
        
        // ✅ Example 7: Цепочка async вызовов
        await Example7_ChainedCalls();
        
        // ✅ Example 8: Async для WebDriver
        await Example8_WebDriverPattern();
        
        // ✅ Example 9: Retry logic
        await Example9_RetryLogic();
        
        // ✅ Example 10: Параллельные тесты
        await Example10_ParallelTests();
        
        // ✅ Example 11: Таймаут с CancellationToken
        await Example11_Timeout();
        
        // ✅ Example 12: Async всё до конца
        await Example12_AsyncAllTheWay();

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("✅ Все примеры выполнены!");
    }

    // ======================== EXAMPLE 1 ========================
    // 🟢 Простой async/await — самое базовое
    static async Task Example1_SimpleAsync()
    {
        Console.WriteLine("\n📌 Example 1: Простой async/await");
        Console.WriteLine("────────────────────────────────");
        
        Console.WriteLine("Начало работы...");
        await Task.Delay(1000);  // 🟢 Ждём 1 сек без блокировки
        Console.WriteLine("✅ Работа завершена!");
    }

    // ======================== EXAMPLE 2 ========================
    // 🟢 Task<T> — функция возвращает значение
    static async Task Example2_TaskWithReturn()
    {
        Console.WriteLine("\n📌 Example 2: Task<T> — возвращаем значение");
        Console.WriteLine("────────────────────────────────");
        
        int result1 = await GetNumberAsync(42);
        Console.WriteLine($"Получено число: {result1}");
        
        string result2 = await GetStringAsync("Hello");
        Console.WriteLine($"Получена строка: {result2}");
    }

    static async Task<int> GetNumberAsync(int value)
    {
        await Task.Delay(500);
        return value * 2;
    }

    static async Task<string> GetStringAsync(string text)
    {
        await Task.Delay(300);
        return $"{text} World!";
    }

    // ======================== EXAMPLE 3 ========================
    // 🟢 Параллельное выполнение (Task.WhenAll)
    static async Task Example3_Parallel()
    {
        Console.WriteLine("\n📌 Example 3: Параллельное выполнение");
        Console.WriteLine("────────────────────────────────");
        
        var stopwatch = Stopwatch.StartNew();
        
        // 🟢 Все три задачи запускаются ОДНОВРЕМЕННО
        var task1 = FetchDataAsync("Task 1", 1000);
        var task2 = FetchDataAsync("Task 2", 800);
        var task3 = FetchDataAsync("Task 3", 1200);
        
        // Ждём всех
        await Task.WhenAll(task1, task2, task3);
        
        stopwatch.Stop();
        Console.WriteLine($"✅ Все задачи завершены за {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"   (Если бы последовательно: 3000ms, параллельно: ~1200ms)");
    }

    static async Task FetchDataAsync(string name, int delayMs)
    {
        await Task.Delay(delayMs);
        Console.WriteLine($"  ✅ {name} завершено за {delayMs}ms");
    }

    // ======================== EXAMPLE 4 ========================
    // 🟢 Гонка между задачами (Task.WhenAny)
    static async Task Example4_RaceCondition()
    {
        Console.WriteLine("\n📌 Example 4: Кто первый? (Task.WhenAny)");
        Console.WriteLine("────────────────────────────────");
        
        var task1 = DelayedResultAsync("Задача 1", 1500);
        var task2 = DelayedResultAsync("Задача 2", 500);
        var task3 = DelayedResultAsync("Задача 3", 1000);
        
        // 🟢 Ждём ПЕРВОГО завершения
        var completed = await Task.WhenAny(task1, task2, task3);
        
        Console.WriteLine($"✅ Первая завершилась: {completed.Result}");
    }

    static async Task<string> DelayedResultAsync(string name, int delayMs)
    {
        await Task.Delay(delayMs);
        return name;
    }

    // ======================== EXAMPLE 5 ========================
    // 🟢 Исключения в async коде
    static async Task Example5_Exceptions()
    {
        Console.WriteLine("\n📌 Example 5: Обработка исключений");
        Console.WriteLine("────────────────────────────────");
        
        try
        {
            await MayFailAsync(true);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"❌ Поймали исключение: {ex.Message}");
        }
        
        // 🟢 Успешный вызов
        await MayFailAsync(false);
        Console.WriteLine("✅ Вторая попытка успешна");
    }

    static async Task MayFailAsync(bool shouldFail)
    {
        await Task.Delay(300);
        if (shouldFail)
            throw new InvalidOperationException("Ошибка в async операции!");
    }

    // ======================== EXAMPLE 6 ========================
    // 🟢 CancellationToken — отмена операции
    static async Task Example6_Cancellation()
    {
        Console.WriteLine("\n📌 Example 6: CancellationToken");
        Console.WriteLine("────────────────────────────────");
        
        var cts = new CancellationTokenSource();
        var task = LongOperationAsync(cts.Token);
        
        // Отменяем через 1 сек
        _ = Task.Delay(1000).ContinueWith(_ => cts.Cancel());
        
        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("⚠️  Операция отменена!");
        }
    }

    static async Task LongOperationAsync(CancellationToken ct)
    {
        for (int i = 0; i < 5; i++)
        {
            ct.ThrowIfCancellationRequested();  // 🟢 Проверяем, не отменена ли
            await Task.Delay(500);
            Console.WriteLine($"  📍 Шаг {i + 1}");
        }
    }

    // ======================== EXAMPLE 7 ========================
    // 🟢 Цепочка async вызовов
    static async Task Example7_ChainedCalls()
    {
        Console.WriteLine("\n📌 Example 7: Цепочка async вызовов");
        Console.WriteLine("────────────────────────────────");
        
        int result = await Step1Async();
        result = await Step2Async(result);
        result = await Step3Async(result);
        
        Console.WriteLine($"✅ Финальный результат: {result}");
    }

    static async Task<int> Step1Async() { await Task.Delay(200); Console.WriteLine("  ✅ Шаг 1: получили 10"); return 10; }
    static async Task<int> Step2Async(int value) { await Task.Delay(200); int result = value * 2; Console.WriteLine($"  ✅ Шаг 2: умножили на 2 = {result}"); return result; }
    static async Task<int> Step3Async(int value) { await Task.Delay(200); int result = value + 5; Console.WriteLine($"  ✅ Шаг 3: добавили 5 = {result}"); return result; }

    // ======================== EXAMPLE 8 ========================
    // 🟢 Паттерн для WebDriver
    static async Task Example8_WebDriverPattern()
    {
        Console.WriteLine("\n📌 Example 8: WebDriver паттерн");
        Console.WriteLine("────────────────────────────────");
        
        var driver = new AsyncWebDriver();
        
        try
        {
            string text = await driver.FindElementAndGetTextAsync("//*[@id='button']", 3);
            Console.WriteLine($"✅ Текст найден: {text}");
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
        }
    }

    class AsyncWebDriver
    {
        public async Task<string> FindElementAndGetTextAsync(string xpath, int timeoutSeconds)
        {
            var startTime = DateTime.Now;
            
            while ((DateTime.Now - startTime).TotalSeconds < timeoutSeconds)
            {
                // 🟢 Имитируем поиск элемента
                await Task.Delay(500);
                
                if (xpath.Contains("button"))
                {
                    return "Click Me!";
                }
            }
            
            throw new TimeoutException($"Element {xpath} not found after {timeoutSeconds}s");
        }
    }

    // ======================== EXAMPLE 9 ========================
    // 🟢 Retry логика
    static async Task Example9_RetryLogic()
    {
        Console.WriteLine("\n📌 Example 9: Retry логика");
        Console.WriteLine("────────────────────────────────");
        
        try
        {
            var result = await RetryAsync(
                async () =>
                {
                    await Task.Delay(200);
                    // 🟢 Сначала ошибка, потом успех
                    if (DateTime.Now.Millisecond % 3 == 0)
                        return "Success!";
                    throw new Exception("Временная ошибка");
                },
                maxAttempts: 3,
                delayMs: 500
            );
            
            Console.WriteLine($"✅ Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Провалилось после всех попыток: {ex.Message}");
        }
    }

    static async Task<T> RetryAsync<T>(Func<Task<T>> operation, int maxAttempts = 3, int delayMs = 1000)
    {
        for (int i = 1; i <= maxAttempts; i++)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex) when (i < maxAttempts)
            {
                Console.WriteLine($"  ⚠️  Попытка {i} провалилась: {ex.Message}");
                await Task.Delay(delayMs);
            }
        }
        
        throw new Exception($"Operation failed after {maxAttempts} attempts");
    }

    // ======================== EXAMPLE 10 ========================
    // 🟢 Параллельные тесты
    static async Task Example10_ParallelTests()
    {
        Console.WriteLine("\n📌 Example 10: Параллельные тесты");
        Console.WriteLine("────────────────────────────────");
        
        var stopwatch = Stopwatch.StartNew();
        
        // 🟢 Все тесты работают одновременно
        await Task.WhenAll(
            TestLoginAsync(),
            TestSearchAsync(),
            TestCheckoutAsync(),
            TestProfileAsync()
        );
        
        stopwatch.Stop();
        Console.WriteLine($"✅ Все тесты пройдены за {stopwatch.ElapsedMilliseconds}ms");
    }

    static async Task TestLoginAsync() { await Task.Delay(1000); Console.WriteLine("  ✅ Login тест"); }
    static async Task TestSearchAsync() { await Task.Delay(800); Console.WriteLine("  ✅ Search тест"); }
    static async Task TestCheckoutAsync() { await Task.Delay(1200); Console.WriteLine("  ✅ Checkout тест"); }
    static async Task TestProfileAsync() { await Task.Delay(900); Console.WriteLine("  ✅ Profile тест"); }

    // ======================== EXAMPLE 11 ========================
    // 🟢 Таймаут операции
    static async Task Example11_Timeout()
    {
        Console.WriteLine("\n📌 Example 11: Таймаут операции");
        Console.WriteLine("────────────────────────────────");
        
        var cts = new CancellationTokenSource(2000);  // 🟢 Таймаут 2 сек
        
        try
        {
            await SlowOperationAsync(5000, cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("⏱️  Таймаут! Операция заняла больше 2 сек");
        }
    }

    static async Task SlowOperationAsync(int delayMs, CancellationToken ct)
    {
        await Task.Delay(delayMs, ct);
        Console.WriteLine($"✅ Операция завершена за {delayMs}ms");
    }

    // ======================== EXAMPLE 12 ========================
    // 🟢 Async всё до конца
    static async Task Example12_AsyncAllTheWay()
    {
        Console.WriteLine("\n📌 Example 12: Async всё до конца");
        Console.WriteLine("────────────────────────────────");
        
        Console.WriteLine("Инициализируем...");
        await InitializeAsync();
        
        Console.WriteLine("Подключаемся...");
        await ConnectAsync();
        
        Console.WriteLine("Выполняем запрос...");
        string data = await FetchDataAsync();
        
        Console.WriteLine($"✅ Данные получены: {data}");
        
        Console.WriteLine("Закрываем соединение...");
        await CloseAsync();
        
        Console.WriteLine("✅ Всё завершено!");
    }

    static async Task InitializeAsync() { await Task.Delay(300); }
    static async Task ConnectAsync() { await Task.Delay(400); }
    static async Task<string> FetchDataAsync() { await Task.Delay(600); return "Important Data"; }
    static async Task CloseAsync() { await Task.Delay(200); }
}
