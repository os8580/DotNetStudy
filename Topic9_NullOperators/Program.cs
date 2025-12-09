using Topic9_NullOperators;

class Program
{
    // Поле для демонстрации ??=
    // Представь, что это "Тяжелый" драйвер
    static List<string> _driverLogs;

    static void Main(string[] args)
    {
        Console.WriteLine("--- 1. Оператор ?? (Запасной вариант) ---");
        Config config = new Config();
        config.BaseUrl = null; // Забыли указать в файле
        //config.BaseUrl = "BASE_URL здесь"; // URL указан

        // Если в конфиге null, берем дефолтный стейдж
        string urlToUse = config.BaseUrl ?? "https://default-stage.com";
        Console.WriteLine($"Открываем URL: {urlToUse}");


        Console.WriteLine("\n--- 2. Оператор ??= (Ленивая инициализация) ---");
        // Сценарий: Мы хотим писать логи, но список логов еще не создан (null).

        // Console.WriteLine(_driverLogs.Count); // Упадет! (NullReference)

        // Старый способ:
        // if (_driverLogs == null) _driverLogs = new List<string>();

        // Новый способ:
        // "Если _driverLogs null, то создай new List. Иначе ничего не делай."
        _driverLogs ??= new List<string>();
        Console.WriteLine($"Логи инициализированы. Записей: {_driverLogs.Count}");

        // Повторный вызов не пересоздаст список, так как он уже не null
        _driverLogs ??= new List<string>();
        Console.WriteLine($"Логи (после повторного ??=): {_driverLogs.Count}"); // Все еще 1


        Console.WriteLine("\n--- 3. Оператор ?. (Элвис - Спасение от краша) ---");
        LoginPage page = null; // Страница не инициализировалась (упал драйвер)

        // Обычный доступ:
        // Console.WriteLine(page.Title); // БА-БАХ! NullReferenceException

        // Безопасный доступ:
        // "Если page существует, дай Title. Иначе верни null (и не падай)."
        string title = page?.Title;

        // Красивая комбинация ?. и ??
        // "Попробуй взять Title. Если страницы нет ИЛИ тайтла нет, напиши 'Unknown'"
        Console.WriteLine($"Заголовок страницы: {title ?? "Unknown (Page is null)"}");


        Console.WriteLine("\n--- 4. Оператор ! (Dammit operator) ---");
        // Сценарий: У нас есть метод, который возвращает nullable строку (string?)
        // Но мы на 100% уверены, что в данном тесте она вернется.

        string? error = GetErrorStub(); // Метод возвращает "string?" (может быть null)

        // Если мы пытаемся запихнуть это в обычную string (не nullable), компилятор ругается warning-ом.
        // Мы ставим !, чтобы сказать: "Я знаю, что делаю, ошибки тут не будет".

        string criticalError = error!;

        Console.WriteLine($"Critical Error text: {criticalError.ToUpper()}");
    }
    static string? GetErrorStub()
    {
        return "Fatal Error 500";
    }
}