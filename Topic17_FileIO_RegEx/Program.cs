using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("📚 Topic17: Файлы, JSON, XML и RegEx\n");
        Console.WriteLine(new string('=', 60));

        // ✅ Example 1: File операции
        Example1_FileOperations();
        
        // ✅ Example 2: Directory операции
        Example2_DirectoryOperations();
        
        // ✅ Example 3: Path класс
        Example3_PathOperations();
        
        // ✅ Example 4: StreamReader - построчное чтение
        Example4_StreamReader();
        
        // ✅ Example 5: StreamWriter - построчная запись
        Example5_StreamWriter();
        
        // ✅ Example 6: JSON с System.Text.Json
        Example6_JsonBasic();
        
        // ✅ Example 7: Regex - поиск совпадений
        Example7_RegexMatches();
        
        // ✅ Example 8: Regex - валидация
        Example8_RegexValidation();
        
        // ✅ Example 9: Regex - замена и разделение
        Example9_RegexReplaceAndSplit();
        
        // ✅ Example 10: Практический пример - парсинг логов
        Example10_ParseLogs();
        
        // ✅ Example 11: Практический пример - создание CSV
        Example11_GenerateCSV();
        
        // ✅ Example 12: Практический пример - парсинг конфига
        Example12_ReadConfig();

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("✅ Все примеры выполнены!");
    }

    // ======================== EXAMPLE 1 ========================
    // 🟢 File операции
    static void Example1_FileOperations()
    {
        Console.WriteLine("\n📌 Example 1: File операции");
        Console.WriteLine("────────────────────────────────");
        
        string testFile = Path.Combine(Directory.GetCurrentDirectory(), "test_file.txt");
        
        // ✅ Создать файл
        File.WriteAllText(testFile, "Hello World!");
        Console.WriteLine($"✅ Создали файл: {Path.GetFileName(testFile)}");
        
        // ✅ Проверить существует ли файл
        if (File.Exists(testFile))
        {
            Console.WriteLine("✅ Файл существует");
        }
        
        // ✅ Прочитать содержимое
        string content = File.ReadAllText(testFile);
        Console.WriteLine($"✅ Содержимое: {content}");
        
        // ✅ Добавить в конец файла
        File.AppendAllText(testFile, "\nДополнительная строка");
        content = File.ReadAllText(testFile);
        Console.WriteLine($"✅ После добавления:\n{content}");
        
        // ✅ Удалить файл
        File.Delete(testFile);
        Console.WriteLine($"✅ Удалили файл");
    }

    // ======================== EXAMPLE 2 ========================
    // 🟢 Directory операции
    static void Example2_DirectoryOperations()
    {
        Console.WriteLine("\n📌 Example 2: Directory операции");
        Console.WriteLine("────────────────────────────────");
        
        string testDir = Path.Combine(Directory.GetCurrentDirectory(), "test_dir");
        
        // ✅ Создать папку
        Directory.CreateDirectory(testDir);
        Console.WriteLine($"✅ Создали папку: test_dir");
        
        // ✅ Создать файлы в папке
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Content 1");
        File.WriteAllText(Path.Combine(testDir, "file2.txt"), "Content 2");
        File.WriteAllText(Path.Combine(testDir, "file3.txt"), "Content 3");
        
        // ✅ Получить все файлы
        string[] files = Directory.GetFiles(testDir);
        Console.WriteLine($"✅ Файлы в папке:");
        foreach (var file in files)
        {
            Console.WriteLine($"   - {Path.GetFileName(file)}");
        }
        
        // ✅ Проверить существует ли папка
        if (Directory.Exists(testDir))
        {
            Console.WriteLine("✅ Папка существует");
        }
        
        // ✅ Удалить папку со всем содержимым
        Directory.Delete(testDir, recursive: true);
        Console.WriteLine($"✅ Удалили папку test_dir");
    }

    // ======================== EXAMPLE 3 ========================
    // 🟢 Path класс
    static void Example3_PathOperations()
    {
        Console.WriteLine("\n📌 Example 3: Path операции");
        Console.WriteLine("────────────────────────────────");
        
        // ✅ Объединить пути
        string fullPath = Path.Combine("Users", "Documents", "file.json");
        Console.WriteLine($"✅ Объединённый путь: {fullPath}");
        
        // ✅ Получить имя файла
        string filename = Path.GetFileName("C:\\Users\\Documents\\config.json");
        Console.WriteLine($"✅ Имя файла: {filename}");
        
        // ✅ Получить расширение
        string ext = Path.GetExtension("config.json");
        Console.WriteLine($"✅ Расширение: {ext}");
        
        // ✅ Получить имя без расширения
        string nameOnly = Path.GetFileNameWithoutExtension("config.json");
        Console.WriteLine($"✅ Имя без расширения: {nameOnly}");
        
        // ✅ Получить директорию
        string dir = Path.GetDirectoryName("C:\\Users\\Documents\\file.txt");
        Console.WriteLine($"✅ Директория: {dir}");
        
        // ✅ Текущая рабочая директория
        string currentDir = Directory.GetCurrentDirectory();
        Console.WriteLine($"✅ Текущая папка: {currentDir}");
    }

    // ======================== EXAMPLE 4 ========================
    // 🟢 StreamReader - построчное чтение (для больших файлов)
    static void Example4_StreamReader()
    {
        Console.WriteLine("\n📌 Example 4: StreamReader - построчное чтение");
        Console.WriteLine("────────────────────────────────");
        
        // Создаём тестовый файл
        string testFile = Path.Combine(Directory.GetCurrentDirectory(), "large_file.txt");
        using (StreamWriter writer = new StreamWriter(testFile))
        {
            for (int i = 1; i <= 5; i++)
            {
                writer.WriteLine($"Строка {i}");
            }
        }
        
        Console.WriteLine("✅ Читаем файл построчно:");
        
        // ✅ Читать построчно
        using (StreamReader reader = new StreamReader(testFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"   {line}");
            }
        }
        
        // Очистка
        File.Delete(testFile);
    }

    // ======================== EXAMPLE 5 ========================
    // 🟢 StreamWriter - построчная запись
    static void Example5_StreamWriter()
    {
        Console.WriteLine("\n📌 Example 5: StreamWriter - построчная запись");
        Console.WriteLine("────────────────────────────────");
        
        string testFile = Path.Combine(Directory.GetCurrentDirectory(), "output.log");
        
        // ✅ Писать построчно
        using (StreamWriter writer = new StreamWriter(testFile))
        {
            writer.WriteLine("[2024-01-15 10:00:00] INFO: Тест начался");
            writer.WriteLine("[2024-01-15 10:00:01] INFO: Шаг 1 выполнен");
            writer.WriteLine("[2024-01-15 10:00:02] ✅ PASSED");
        }
        
        Console.WriteLine("✅ Записали в логи:");
        string[] lines = File.ReadAllLines(testFile);
        foreach (var line in lines)
        {
            Console.WriteLine($"   {line}");
        }
        
        // Очистка
        File.Delete(testFile);
    }

    // ======================== EXAMPLE 6 ========================
    // 🟢 JSON с System.Text.Json
    static void Example6_JsonBasic()
    {
        Console.WriteLine("\n📌 Example 6: JSON с System.Text.Json");
        Console.WriteLine("────────────────────────────────");
        
        // ✅ Объект → JSON
        var config = new
        {
            browserName = "Chrome",
            port = 9222,
            headless = true
        };
        
        string json = System.Text.Json.JsonSerializer.Serialize(config);
        Console.WriteLine($"✅ Объект → JSON:\n   {json}");
        
        // ✅ JSON → объект
        string jsonInput = """{"browserName":"Firefox","port":4444}""";
        using var doc = System.Text.Json.JsonDocument.Parse(jsonInput);
        var root = doc.RootElement;
        
        string browser = root.GetProperty("browserName").GetString();
        int port = root.GetProperty("port").GetInt32();
        
        Console.WriteLine($"✅ JSON → объект:");
        Console.WriteLine($"   Browser: {browser}");
        Console.WriteLine($"   Port: {port}");
    }

    // ======================== EXAMPLE 7 ========================
    // 🟢 Regex - поиск совпадений
    static void Example7_RegexMatches()
    {
        Console.WriteLine("\n📌 Example 7: Regex - поиск совпадений");
        Console.WriteLine("────────────────────────────────");
        
        string text = "Цены: $99.99, $150.00, $75.50";
        
        // ✅ Найти все цены
        MatchCollection matches = Regex.Matches(text, @"\$(\d+\.\d{2})");
        Console.WriteLine($"✅ Найденные цены:");
        foreach (Match match in matches)
        {
            Console.WriteLine($"   {match.Value}");
        }
        
        // ✅ Найти HTML теги
        string html = "<h1>Title</h1><p>Content</p>";
        MatchCollection tags = Regex.Matches(html, @"<(\w+)>");
        Console.WriteLine($"✅ Найденные теги:");
        foreach (Match match in tags)
        {
            Console.WriteLine($"   {match.Groups[1].Value}");
        }
        
        // ✅ Извлечь email
        string text2 = "Свяжитесь с нами: admin@example.com или support@site.org";
        MatchCollection emails = Regex.Matches(text2, @"[\w\.-]+@[\w\.-]+\.\w+");
        Console.WriteLine($"✅ Найденные email:");
        foreach (Match match in emails)
        {
            Console.WriteLine($"   {match.Value}");
        }
    }

    // ======================== EXAMPLE 8 ========================
    // 🟢 Regex - валидация
    static void Example8_RegexValidation()
    {
        Console.WriteLine("\n📌 Example 8: Regex - валидация");
        Console.WriteLine("────────────────────────────────");
        
        // ✅ Email валидация
        string[] emails = { "test@example.com", "invalid-email", "user+tag@domain.co.uk" };
        string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        
        Console.WriteLine("✅ Проверка email:");
        foreach (var email in emails)
        {
            bool valid = Regex.IsMatch(email, emailPattern);
            Console.WriteLine($"   {email}: {(valid ? "✅ Valid" : "❌ Invalid")}");
        }
        
        // ✅ IP адрес валидация
        string[] ips = { "192.168.1.1", "256.1.1.1", "10.0.0.1" };
        string ipPattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
        
        Console.WriteLine("\n✅ Проверка IP адреса:");
        foreach (var ip in ips)
        {
            bool valid = Regex.IsMatch(ip, ipPattern);
            Console.WriteLine($"   {ip}: {(valid ? "✅ Valid" : "❌ Invalid")}");
        }
        
        // ✅ URL валидация
        string[] urls = { "https://example.com", "http://test.org", "not a url" };
        string urlPattern = @"^https?://[\w\.-]+\.\w+";
        
        Console.WriteLine("\n✅ Проверка URL:");
        foreach (var url in urls)
        {
            bool valid = Regex.IsMatch(url, urlPattern);
            Console.WriteLine($"   {url}: {(valid ? "✅ Valid" : "❌ Invalid")}");
        }
    }

    // ======================== EXAMPLE 9 ========================
    // 🟢 Regex - замена и разделение
    static void Example9_RegexReplaceAndSplit()
    {
        Console.WriteLine("\n📌 Example 9: Regex - замена и разделение");
        Console.WriteLine("────────────────────────────────");
        
        // ✅ Замена
        string text = "Hello 123 World 456";
        string result = Regex.Replace(text, @"\d+", "[NUMBER]");
        Console.WriteLine($"✅ Замена:\n   Исходное: {text}\n   Результат: {result}");
        
        // ✅ Замена с форматированием даты
        string date = "2024-01-15";
        string reformatted = Regex.Replace(date, @"(\d{4})-(\d{2})-(\d{2})", "$3/$2/$1");
        Console.WriteLine($"✅ Переформат даты:\n   {date} → {reformatted}");
        
        // ✅ Разделение
        string csv = "one,two,three,four";
        string[] parts = Regex.Split(csv, ",");
        Console.WriteLine($"✅ Разделение CSV:");
        foreach (var part in parts)
        {
            Console.WriteLine($"   - {part}");
        }
        
        // ✅ Разделение по пробелам/запятым
        string mixed = "apple,banana orange; grape";
        string[] items = Regex.Split(mixed, @"[,;\s]+");
        Console.WriteLine($"✅ Разделение по разным разделителям:");
        foreach (var item in items.Where(x => !string.IsNullOrEmpty(x)))
        {
            Console.WriteLine($"   - {item}");
        }
    }

    // ======================== EXAMPLE 10 ========================
    // 🟢 Практический пример: парсинг логов
    static void Example10_ParseLogs()
    {
        Console.WriteLine("\n📌 Example 10: Парсинг логов");
        Console.WriteLine("────────────────────────────────");
        
        // Создаём тестовый лог файл
        string logFile = Path.Combine(Directory.GetCurrentDirectory(), "test.log");
        using (StreamWriter writer = new StreamWriter(logFile))
        {
            writer.WriteLine("[2024-01-15 10:00:00] INFO: Тест начался");
            writer.WriteLine("[2024-01-15 10:00:05] ✅ Login successful");
            writer.WriteLine("[2024-01-15 10:00:12] ⚠️ Warning: Slow response");
            writer.WriteLine("[2024-01-15 10:00:18] ❌ ERROR: Element not found");
        }
        
        // ✅ Парсить логи
        string[] lines = File.ReadAllLines(logFile);
        Console.WriteLine("✅ Разобранные логи:");
        
        foreach (var line in lines)
        {
            Match match = Regex.Match(line, @"\[(.*?)\]\s+(.+?):\s+(.+)");
            if (match.Success)
            {
                string timestamp = match.Groups[1].Value;
                string level = match.Groups[2].Value;
                string message = match.Groups[3].Value;
                
                Console.WriteLine($"   Time: {timestamp} | Type: {level} | Msg: {message}");
            }
        }
        
        File.Delete(logFile);
    }

    // ======================== EXAMPLE 11 ========================
    // 🟢 Практический пример: создание CSV
    static void Example11_GenerateCSV()
    {
        Console.WriteLine("\n📌 Example 11: Генерация CSV файла");
        Console.WriteLine("────────────────────────────────");
        
        string csvFile = Path.Combine(Directory.GetCurrentDirectory(), "test_data.csv");
        
        // ✅ Создать CSV с данными
        using (StreamWriter writer = new StreamWriter(csvFile))
        {
            writer.WriteLine("Email,Password,Role");
            writer.WriteLine("user1@test.com,Pass123!,Admin");
            writer.WriteLine("user2@test.com,Pass456!,User");
            writer.WriteLine("user3@test.com,Pass789!,User");
        }
        
        Console.WriteLine("✅ Созданный CSV:");
        string[] lines = File.ReadAllLines(csvFile);
        foreach (var line in lines)
        {
            Console.WriteLine($"   {line}");
        }
        
        // ✅ Парсить CSV
        Console.WriteLine("\n✅ Парсированные данные:");
        using (StreamReader reader = new StreamReader(csvFile))
        {
            string header = reader.ReadLine();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                Console.WriteLine($"   Email: {values[0]}, Password: {values[1]}, Role: {values[2]}");
            }
        }
        
        File.Delete(csvFile);
    }

    // ======================== EXAMPLE 12 ========================
    // 🟢 Практический пример: чтение конфига
    static void Example12_ReadConfig()
    {
        Console.WriteLine("\n📌 Example 12: Чтение конфига");
        Console.WriteLine("────────────────────────────────");
        
        string configFile = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
        
        // ✅ Создать конфиг
        string configContent = """
        {
            "baseUrl": "https://example.com",
            "browser": "Chrome",
            "timeout": 10000,
            "headless": true
        }
        """;
        
        File.WriteAllText(configFile, configContent);
        
        // ✅ Прочитать конфиг
        string content = File.ReadAllText(configFile);
        Console.WriteLine("✅ Содержимое конфига:");
        foreach (var line in content.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(line))
                Console.WriteLine($"   {line}");
        }
        
        // ✅ Парсить конфиг с regex
        Console.WriteLine("\n✅ Извлечённые параметры:");
        
        var baseUrlMatch = Regex.Match(content, @"""baseUrl"":\s*""([^""]+)""");
        if (baseUrlMatch.Success)
            Console.WriteLine($"   BaseUrl: {baseUrlMatch.Groups[1].Value}");
        
        var browserMatch = Regex.Match(content, @"""browser"":\s*""([^""]+)""");
        if (browserMatch.Success)
            Console.WriteLine($"   Browser: {browserMatch.Groups[1].Value}");
        
        var timeoutMatch = Regex.Match(content, @"""timeout"":\s*(\d+)");
        if (timeoutMatch.Success)
            Console.WriteLine($"   Timeout: {timeoutMatch.Groups[1].Value}ms");
        
        File.Delete(configFile);
    }
}
