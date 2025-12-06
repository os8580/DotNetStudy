// --- 1. LIST (Список) ---
Console.WriteLine("--- LIST: Список багов ---");

// Создаем список строк
List<string> bugs = new List<string>();

// Добавляем (Add)
bugs.Add("Bug #1: Кнопка не нажимается");
bugs.Add("Bug #2: 404 на главной");
bugs.Add("Bug #3: Текст вылезает за границы");

// Обращение по индексу (как в массиве)
Console.WriteLine($"Первый баг: {bugs[0]}");

// Узнать количество
Console.WriteLine($"Всего багов: {bugs.Count}");

// Удалить элемент
bugs.Remove("Bug #2: 404 на главной"); // Удаляем по значению
                                       // bugs.RemoveAt(0); // Или можно удалить по индексу

// Перебор (foreach)
Console.WriteLine("\nОставшиеся баги:");
foreach (var bug in bugs)
{
    Console.WriteLine(bug);
}

// Проверка наличия (Contains) - очень полезно для Assert
bool hasCritical = bugs.Contains("Bug #1: Кнопка не нажимается");
Console.WriteLine($"\nЕсть ли критический баг? {hasCritical}");

Console.WriteLine("\n----------------------------");
Console.WriteLine("--- DICTIONARY: Конфиг теста ---");

// Ключ - string (название настройки), Значение - string (значение)
Dictionary<string, string> config = new Dictionary<string, string>();

// Добавление
config.Add("url", "https://google.com");
config.Add("browser", "Chrome");
config.Add("timeout", "30");

// Альтернативный синтаксис добавления (безопаснее, если ключ уже есть - он его перезапишет)
config["username"] = "admin";

// Получение значения по Ключу (Работает мгновенно, не перебирая всё)
Console.WriteLine($"Запускаем браузер: {config["browser"]}");
Console.WriteLine($"Открываем URL: {config["url"]}");

// ВАЖНО: Если ключа нет, программа упадет с ошибкой!
// Console.WriteLine(config["password"]); // ОШИБКА KeyNotFoundException

// Безопасное получение (TryGetValue) - Часто спрашивают!
if (config.TryGetValue("password", out string pass))
{
    Console.WriteLine($"Пароль: {pass}");
}
else
{
    Console.WriteLine("Пароль не найден в конфиге (используем дефолтный).");
}

// Перебор словаря
Console.WriteLine("\nВсе настройки:");
foreach (KeyValuePair<string, string> item in config)
{
    Console.WriteLine($"{item.Key} : {item.Value}");
}