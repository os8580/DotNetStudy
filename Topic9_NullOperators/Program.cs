using Topic9_NullOperators;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("============ ДЕМОНСТРАЦИЯ NULL-ОПЕРАТОРОВ ============\n");

        // 1. Оператор ?? (Null-coalescing)
        DemoNullCoalescing();

        // 2. Оператор ??= (Null-coalescing assignment)
        DemoNullCoalescingAssignment();

        // 3. Оператор ?. (Null-conditional)
        DemoNullConditional();

        // 4. Оператор ! (Null-forgiving)
        DemoNullForgiving();

        // 5. Комбинирование операторов
        DemoCombiningOperators();
    }

    // Демонстрация: ?? (Null-coalescing)
    static void DemoNullCoalescing()
    {
        Console.WriteLine("--- 1. Оператор ?? (Значение по умолчанию) ---");

        Config config = new Config();
        config.BaseUrl = null;

        // Если BaseUrl null, используем дефолтный
        string urlToUse = config.BaseUrl ?? "https://default-stage.com";
        Console.WriteLine($"✓ Открываем URL: {urlToUse}");

        // С заполненным значением
        config.BaseUrl = "https://production.com";
        urlToUse = config.BaseUrl ?? "https://default-stage.com";
        Console.WriteLine($"✓ Открываем URL: {urlToUse}");

        Console.WriteLine();
    }

    // Демонстрация: ??= (Null-coalescing assignment)
    static void DemoNullCoalescingAssignment()
    {
        Console.WriteLine("--- 2. Оператор ??= (Ленивая инициализация) ---");

        List<string> driverLogs = null;

        // Инициализируем список если он null
        driverLogs ??= new List<string>();
        driverLogs.Add("Step 1: Opened page");
        driverLogs.Add("Step 2: Entered credentials");

        Console.WriteLine($"✓ Логи инициализированы. Записей: {driverLogs.Count}");

        // Повторный вызов не пересоздаст список
        driverLogs ??= new List<string>();
        Console.WriteLine($"✓ Логи (после повторного ??=): {driverLogs.Count} записей");

        Console.WriteLine();
    }

    // Демонстрация: ?. (Null-conditional)
    static void DemoNullConditional()
    {
        Console.WriteLine("--- 3. Оператор ?. (Безопасный доступ) ---");

        LoginPage page = null;

        // Безопасный доступ к свойству
        string title = page?.Title;
        Console.WriteLine($"✓ Заголовок (page null): {title ?? "Unknown"}");

        // Создаем страницу с контентом
        page = new LoginPage 
        { 
            Title = "Login Page",
            LoginButton = new Button { Text = "Sign In" }
        };

        title = page?.Title;
        Console.WriteLine($"✓ Заголовок (page не null): {title ?? "Unknown"}");

        // Безопасный вызов метода
        string? errorMessage = page?.GetErrorMessage();
        Console.WriteLine($"✓ Ошибка: {errorMessage ?? "No errors"}");

        // Безопасный доступ к вложенным объектам
        string buttonText = page?.LoginButton?.Text ?? "Unknown button";
        Console.WriteLine($"✓ Текст кнопки: {buttonText}");

        Console.WriteLine();
    }

    // Демонстрация: ! (Null-forgiving)
    static void DemoNullForgiving()
    {
        Console.WriteLine("--- 4. Оператор ! (Null-forgiving - ОСТОРОЖНО!) ---");

        string? error = GetErrorMessage();

        if (error != null)
        {
            // Если мы уверены, что error не null, можем использовать !
            string criticalError = error!;
            Console.WriteLine($"✓ Critical error: {criticalError.ToUpper()}");
        }

        Console.WriteLine();
    }

    // Демонстрация: комбинирование операторов
    static void DemoCombiningOperators()
    {
        Console.WriteLine("--- 5. Комбинирование операторов ---");

        // Сценарий 1: Конфиг с дефолтными значениями
        Config config = new Config();
        config.ApiUrl = null;
        config.Timeout = null;

        string url = config.ApiUrl ?? "https://api.default.com";
        int timeout = config.Timeout ?? 30;

        Console.WriteLine($"✓ API URL: {url}");
        Console.WriteLine($"✓ Timeout: {timeout}s");

        // Сценарий 2: Page Object с инициализацией
        LoginPage page = null;

        // Инициализируем если null
        page ??= new LoginPage();

        // Инициализируем поля если они null
        page.UsernameField ??= new TextField();
        page.LoginButton ??= new Button { Text = "Login" };

        Console.WriteLine($"✓ Page инициализирована");
        Console.WriteLine($"✓ Username field готов к вводу");
        page.UsernameField.Type("alice");

        // Безопасный клик
        page.LoginButton?.Click();

        Console.WriteLine();
    }

    static string? GetErrorMessage()
    {
        return "Fatal Error 500";
    }
}