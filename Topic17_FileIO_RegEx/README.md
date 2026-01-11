# Topic17 — Файлы, JSON, XML и регулярные выражения

## Цель

Работать с файловой системой, парсить JSON/XML, использовать регулярные выражения для поиска и обработки текста.

---

### Для полного новичка: быстрый маршрут

- Прочитайте: "File и Directory", "JSON", "Regular Expressions"
- Запустите Program.cs: `dotnet run`
- Практика: примеры для QA (config, логи, HTML парсинг)
- Вернитесь к чек-листу в конце

---

## Содержание

1. [Файловая система — File и Directory](#1-файловая-система--file-и-directory)
2. [JSON парсинг и сериализация](#2-json-парсинг-и-сериализация)
3. [XML парсинг](#3-xml-парсинг)
4. [Регулярные выражения (Regex)](#4-регулярные-выражения-regex)
5. [StreamReader и StreamWriter](#5-streamreader-и-streamwriter)
6. [Практические примеры для QA](#6-практические-примеры-для-qa)
7. [Частые ошибки](#7-частые-ошибки)
8. [Чек-лист](#8-чек-лист-проверки-знаний)

---

## 1. Файловая система — File и Directory

### File класс (работа с одним файлом)

```csharp
using System.IO;

// ✅ Существует ли файл?
bool exists = File.Exists("config.json");

// ✅ Прочитать весь файл в строку
string content = File.ReadAllText("config.json");

// ✅ Прочитать весь файл в строки (массив)
string[] lines = File.ReadAllLines("log.txt");

// ✅ Записать в файл
File.WriteAllText("output.txt", "Hello World");

// ✅ Добавить в конец файла (не перезаписывать)
File.AppendAllText("log.txt", "New line\n");

// ✅ Прочитать все байты
byte[] bytes = File.ReadAllBytes("image.png");

// ✅ Записать байты
File.WriteAllBytes("image.png", bytes);

// ✅ Удалить файл
File.Delete("temp.txt");

// ✅ Скопировать файл
File.Copy("source.txt", "destination.txt", overwrite: true);
```

### Directory класс (работа с папками)

```csharp
using System.IO;

// ✅ Существует ли директория?
bool exists = Directory.Exists("logs");

// ✅ Создать директорию
Directory.CreateDirectory("logs");

// ✅ Получить все файлы в папке
string[] files = Directory.GetFiles("logs");

// ✅ Получить все подпапки
string[] dirs = Directory.GetDirectories("src");

// ✅ Получить все файлы рекурсивно (включая подпапки)
string[] allFiles = Directory.GetFiles("src", "*.*", SearchOption.AllDirectories);

// ✅ Удалить папку (рекурсивно)
Directory.Delete("temp", recursive: true);

// ✅ Переместить
Directory.Move("oldName", "newName");
```

### Path класс (работа с путями)

```csharp
using System.IO;

// ✅ Объединить пути
string fullPath = Path.Combine("C:", "Users", "Documents", "file.txt");
// Результат: C:\Users\Documents\file.txt

// ✅ Получить имя файла
string filename = Path.GetFileName("C:\\Users\\file.txt");  // "file.txt"

// ✅ Получить расширение
string ext = Path.GetExtension("file.json");  // ".json"

// ✅ Получить директорию
string dir = Path.GetDirectoryName("C:\\Users\\file.txt");  // "C:\\Users"

// ✅ Получить имя без расширения
string name = Path.GetFileNameWithoutExtension("file.json");  // "file"

// ✅ Текущая директория проекта
string currentDir = Directory.GetCurrentDirectory();

// ✅ Временная папка системы
string tempDir = Path.GetTempPath();
```

---

## 2. JSON парсинг и сериализация

### Встроенный System.Text.Json (встроен в .NET)

```csharp
using System.Text.Json;

// ✅ Объект → JSON
var config = new { Name = "Chrome", Port = 9222 };
string json = JsonSerializer.Serialize(config);
// Результат: {"Name":"Chrome","Port":9222}

// ✅ JSON → объект
string jsonStr = "{\"Name\":\"Chrome\",\"Port\":9222}";
var parsed = JsonSerializer.Deserialize<dynamic>(jsonStr);
```

### Newtonsoft.Json (популярнее, нужно установить)

```bash
dotnet add package Newtonsoft.Json
```

```csharp
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ✅ JSON → JObject (красивый парсинг)
string json = File.ReadAllText("config.json");
JObject config = JObject.Parse(json);

string name = config["browserName"].ToString();  // "Chrome"
int port = config["port"].ToObject<int>();       // 9222

// ✅ Объект → JSON
var settings = new
{
    browserName = "Firefox",
    port = 4444,
    headless = true
};

string jsonOutput = JsonConvert.SerializeObject(settings, Formatting.Indented);

// ✅ Массив в JSON
var drivers = new[]
{
    new { name = "Chrome", version = "120" },
    new { name = "Firefox", version = "121" }
};

string jsonArray = JsonConvert.SerializeObject(drivers);
```

### Пример: config.json

```json
{
  "browserName": "Chrome",
  "baseUrl": "https://example.com",
  "timeout": 10000,
  "headless": true,
  "proxy": {
    "type": "http",
    "address": "proxy.example.com",
    "port": 8080
  }
}
```

---

## 3. XML парсинг

### XDocument (современный способ)

```csharp
using System.Xml.Linq;

// ✅ Загрузить XML файл
XDocument doc = XDocument.Load("config.xml");

// ✅ Загрузить из строки
XDocument doc = XDocument.Parse("<root><item>value</item></root>");

// ✅ Получить элемент
XElement root = doc.Root;

// ✅ Получить первый элемент с именем
XElement item = root.Element("item");  // <item>value</item>

// ✅ Получить все элементы
IEnumerable<XElement> items = root.Elements("item");

// ✅ Получить текст
string text = item.Value;  // "value"

// ✅ Получить атрибут
string id = item.Attribute("id")?.Value;  // атрибут id

// ✅ Создать новый XML
var newDoc = new XDocument(
    new XElement("root",
        new XElement("item", "value1"),
        new XElement("item", "value2")
    )
);

newDoc.Save("output.xml");
```

### Пример: config.xml

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <browser name="Chrome">
    <path>C:\chromedriver.exe</path>
    <version>120</version>
  </browser>
  <timeouts>
    <implicit>10</implicit>
    <explicit>30</explicit>
  </timeouts>
</configuration>
```

```csharp
// ✅ Парсить пример выше
XDocument doc = XDocument.Load("config.xml");

string browserPath = doc.Root
    .Element("browser")
    ?.Element("path")
    ?.Value;

int timeout = int.Parse(
    doc.Root
        .Element("timeouts")
        ?.Element("implicit")
        ?.Value ?? "10"
);
```

---

## 4. Регулярные выражения (Regex)

### Что такое Regex?

**Regex** — это паттерн для поиска и замены текста. Например:

```
\d+         = одна или больше цифр
\w+         = одно или больше буквы/цифры/_
[a-z]+      = одна или больше строчные буквы
[\w-\.]+    = буквы, цифры, дефис или точка
```

### Основные операции

```csharp
using System.Text.RegularExpressions;

// ✅ Является ли строка email?
bool isEmail = Regex.IsMatch("test@example.com", @"^[\w\.-]+@[\w\.-]+\.\w+$");

// ✅ Найти первое совпадение
Match match = Regex.Match("The year is 2024", @"\d+");
if (match.Success)
    Console.WriteLine(match.Value);  // "2024"

// ✅ Найти все совпадения
MatchCollection matches = Regex.Matches("a1b2c3", @"\d+");
foreach (var m in matches)
    Console.WriteLine(m.Value);  // 1, 2, 3

// ✅ Заменить
string result = Regex.Replace("Hello 123 World", @"\d+", "***");
// Результат: "Hello *** World"

// ✅ Разделить
string[] parts = Regex.Split("one,two,three", @",");
// Результат: ["one", "two", "three"]
```

### Полезные паттерны для QA

```csharp
// 📧 Email
string emailRegex = @"^[\w\.-]+@[\w\.-]+\.\w+$";

// 🔗 URL
string urlRegex = @"^https?://[\w\.-]+\.\w+";

// 📱 Номер телефона (формат: +1-123-456-7890)
string phoneRegex = @"^\+?\d{1,3}-?\d{1,3}-?\d{4}-?\d{4}$";

// 💳 Кредитная карта (4-4-4-4 цифры)
string creditCardRegex = @"^\d{4}-?\d{4}-?\d{4}-?\d{4}$";

// 🏷️ IP адрес
string ipRegex = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";

// 🔐 Пароль (минимум 8 символов, буквы + цифры)
string passwordRegex = @"^(?=.*[a-z])(?=.*\d).{8,}$";

// 📝 Дата (формат: dd/mm/yyyy)
string dateRegex = @"^\d{2}/\d{2}/\d{4}$";

// 🔎 HTML тег
string htmlTagRegex = @"<(\w+)[^>]*>";
```

### Примеры использования

```csharp
// Извлечь все числа
MatchCollection numbers = Regex.Matches("Price: $99.99", @"\d+\.\d+");
// ["99.99"]

// Извлечь HTML теги
MatchCollection tags = Regex.Matches("<h1>Title</h1>", @"<(\w+)>");
// ["h1"]

// Валидация
bool isValidEmail = Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");

// Замена с группами
string result = Regex.Replace("2024-01-15", @"(\d{4})-(\d{2})-(\d{2})", "$3/$2/$1");
// Результат: "15/01/2024"
```

---

## 5. StreamReader и StreamWriter

### Для больших файлов (построчно)

```csharp
using System.IO;

// ✅ Читать большой файл построчно
using (StreamReader reader = new StreamReader("large_log.txt"))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine(line);
        // Обработка каждой строки
    }
}

// ✅ Писать в файл построчно
using (StreamWriter writer = new StreamWriter("output.txt"))
{
    writer.WriteLine("Строка 1");
    writer.WriteLine("Строка 2");
    writer.WriteLine("Строка 3");
}

// ✅ Добавить в конец файла
using (StreamWriter writer = new StreamWriter("log.txt", append: true))
{
    writer.WriteLine($"[{DateTime.Now}] Новое событие");
}
```

### Когда использовать Stream vs File?

```
✅ File.ReadAllText()      = маленькие файлы (<10MB)
✅ StreamReader            = большие файлы (логи, данные)
✅ File.AppendAllText()    = добавить в конец
✅ StreamWriter(append)    = много дописываний подряд
```

---

## 6. Практические примеры для QA

### Пример 1: Читать config файл

```csharp
// config.json
{
  "baseUrl": "https://app.example.com",
  "browser": "Chrome",
  "timeout": 10000
}

// Код
using Newtonsoft.Json.Linq;

var config = JObject.Parse(File.ReadAllText("config.json"));
string baseUrl = config["baseUrl"].ToString();
string browser = config["browser"].ToString();
int timeout = config["timeout"].ToObject<int>();

Console.WriteLine($"Opening {baseUrl} in {browser}");
```

### Пример 2: Парсить логи

```csharp
// log.txt
[2024-01-15 10:23:45] INFO: Login successful
[2024-01-15 10:24:12] ERROR: Element not found
[2024-01-15 10:25:33] WARNING: Timeout reached

// Код
using System.Text.RegularExpressions;

string[] logLines = File.ReadAllLines("log.txt");
foreach (var line in logLines)
{
    Match match = Regex.Match(line, @"\[(.*?)\]\s+(\w+):\s(.+)");
    if (match.Success)
    {
        string timestamp = match.Groups[1].Value;   // 2024-01-15 10:23:45
        string level = match.Groups[2].Value;       // INFO, ERROR, WARNING
        string message = match.Groups[3].Value;     // Login successful

        Console.WriteLine($"{timestamp} | {level} | {message}");
    }
}
```

### Пример 3: Создать тестовые данные

```csharp
// Создать CSV файл с тестовыми данными
var testData = new[]
{
    new { Email = "user1@test.com", Password = "Pass123!" },
    new { Email = "user2@test.com", Password = "Pass456!" },
    new { Email = "user3@test.com", Password = "Pass789!" }
};

using (StreamWriter writer = new StreamWriter("test_data.csv"))
{
    writer.WriteLine("Email,Password");
    foreach (var item in testData)
    {
        writer.WriteLine($"{item.Email},{item.Password}");
    }
}
```

### Пример 4: Парсить HTML ответ

```csharp
// HTML ответ от сервера
string html = """
<div class="user-profile">
    <h1>John Doe</h1>
    <span class="email">john@example.com</span>
    <span class="phone">+1-234-567-8900</span>
</div>
""";

// Извлечь данные regex-ом
var nameMatch = Regex.Match(html, @"<h1>(.*?)</h1>");
var emailMatch = Regex.Match(html, @"<span class=""email"">(.*?)</span>");
var phoneMatch = Regex.Match(html, @"<span class=""phone"">(.*?)</span>");

Console.WriteLine($"Name: {nameMatch.Groups[1].Value}");
Console.WriteLine($"Email: {emailMatch.Groups[1].Value}");
Console.WriteLine($"Phone: {phoneMatch.Groups[1].Value}");
```

### Пример 5: Проверить API ответ

```csharp
// API ответ
string apiResponse = """
{
    "status": "success",
    "data": {
        "id": 123,
        "name": "Product A",
        "price": 99.99
    }
}
""";

// Парсить JSON
var response = JObject.Parse(apiResponse);
if (response["status"].ToString() == "success")
{
    string productName = response["data"]["name"].ToString();
    decimal price = response["data"]["price"].ToObject<decimal>();

    Console.WriteLine($"✅ Product: {productName}, Price: ${price}");
}
```

---

## 7. Частые ошибки

### ❌ Ошибка 1: Не закрыли файл

```csharp
// ❌ ОПАСНО: файл остаётся заблокирован
StreamReader reader = new StreamReader("file.txt");
string content = reader.ReadToEnd();
// reader не закрыт!

// ✅ Правильно: используйте using
using (StreamReader reader = new StreamReader("file.txt"))
{
    string content = reader.ReadToEnd();
}  // reader автоматически закрыт

// ✅ Или так (C# 8.0+)
using StreamReader reader = new StreamReader("file.txt");
string content = reader.ReadToEnd();
```

### ❌ Ошибка 2: File.Exists() но файла нет

```csharp
// ❌ Может выбросить исключение
string content = File.ReadAllText("config.json");

// ✅ Проверьте сначала
if (File.Exists("config.json"))
{
    string content = File.ReadAllText("config.json");
}
else
{
    Console.WriteLine("❌ Файл не найден!");
}
```

### ❌ Ошибка 3: Неправильный Regex

```csharp
// ❌ Неправильный паттерн для email
bool isEmail = Regex.IsMatch(email, @"\w+@\w+");  // Слишком простой!
// "test123" пройдёт проверку!

// ✅ Правильный паттерн
bool isEmail = Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
```

### ❌ Ошибка 4: Кодировка файла

```csharp
// ❌ Если файл в UTF-8, а вы читаете как ASCII
string content = File.ReadAllText("file.txt");  // Может быть ошибка!

// ✅ Правильно: укажите кодировку
string content = File.ReadAllText("file.txt", Encoding.UTF8);
```

### ❌ Ошибка 5: Путь к файлу hardcoded

```csharp
// ❌ Путь не работает на других машинах
File.WriteAllText("C:\\Users\\John\\Documents\\test.txt", "data");

// ✅ Используйте Path.Combine
string filePath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "data",
    "test.txt"
);
File.WriteAllText(filePath, "data");
```

---

## 8. ЧЕК-ЛИСТ ПРОВЕРКИ ЗНАНИЙ 🎯

### Вопрос 1: Как читать весь файл в одну строку?

**Ответ:** `string content = File.ReadAllText("file.txt");`

### Вопрос 2: Как прочитать большой файл построчно?

**Ответ:** Используйте `StreamReader` в цикле `while ((line = reader.ReadLine()) != null)`

### Вопрос 3: Как парсить JSON?

**Ответ:** `var obj = JObject.Parse(jsonString);` (после `dotnet add package Newtonsoft.Json`)

### Вопрос 4: Как найти все цифры в строке?

**Ответ:** `Regex.Matches(text, @"\d+")`

### Вопрос 5: Как проверить email regex-ом?

**Ответ:** `Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$")`

### Вопрос 6: Как создать папку, если её нет?

**Ответ:** `Directory.CreateDirectory("folder");`

### Вопрос 7: Как получить имя файла без расширения?

**Ответ:** `Path.GetFileNameWithoutExtension("file.json");`

### Вопрос 8: Почему нужен using при работе с файлами?

**Ответ:** Чтобы автоматически закрыть файл и освободить ресурсы

---

## Файлы в проекте

- `Program.cs` — примеры всех концепций
- `README.md` — этот файл
- `EXPECTED_OUTPUT.md` — ожидаемый вывод примеров

---

**Запустите:** `cd Topic17_FileIO_RegEx && dotnet run`

**Готово к использованию!** ✅
