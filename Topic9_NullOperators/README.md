# Topic9 — Null-операторы (?., ??, ??=, !) (Полный курс для начинающих)

## Цель
Понять, как работают операторы для работы с null значениями, и как писать безопасный код, который не падает с NullReferenceException.

---

## 1. Что такое null? (Для самых начинающих)

### Аналогия
```
null = "ничего", "отсутствие значения", "неизвестно"

Реальная жизнь:
- У вас есть полка для картин
- На полке может быть картина (объект)
- Или полка может быть пустой (null)

В программировании:
- user может быть объектом User
- Или user может быть null (никакого объекта)
```

### Проблема

```csharp
User user = null;

// ? NullReferenceException! "Попытка открыть доступ к null"
Console.WriteLine(user.Name);

// ? То же самое
string name = user.Name;  // ?? Падает!

// ? Нужна проверка
if (user != null)
{
    Console.WriteLine(user.Name);  // Безопасно
}
```

---

## 2. Null-conditional оператор (?.)

### Что это?

Оператор `?.` проверяет, не null ли объект, и если null — возвращает null вместо ошибки.

### Основное использование

```csharp
User user = null;

// БЕЗ ?. (ошибка)
// Console.WriteLine(user.Name);  // ? NullReferenceException!

// С ?. (безопасно)
Console.WriteLine(user?.Name);  // null (ничего не произойдет)

// Другой пример
User user = new User { Name = "Alice" };
Console.WriteLine(user?.Name);  // "Alice" (объект не null, доступ разрешен)

string? name = user?.Name;  // Результат — string или null
```

### С методами

```csharp
public class User
{
    public string GetName()
    {
        return "Alice";
    }
}

User user = null;

// БЕЗ ?.
// string name = user.GetName();  // ? NullReferenceException!

// С ?.
string? name = user?.GetName();  // null (метод не вызывается)

user = new User();
name = user?.GetName();  // "Alice"
```

### С индексацией

```csharp
List<string> items = null;

// БЕЗ ?.
// Console.WriteLine(items[0]);  // ? NullReferenceException!

// С ?.
string? item = items?[0];  // null (доступ не выполняется)

items = new List<string> { "a", "b", "c" };
item = items?[0];  // "a"
```

### Цепочки ?.

```csharp
public class Page
{
    public User User { get; set; }
}

public class User
{
    public string Name { get; set; }
}

Page page = null;

// Безопасная цепочка доступа
string? name = page?.User?.Name;  // null (page null)

page = new Page();  // User null по умолчанию
name = page?.User?.Name;  // null (User null)

page.User = new User { Name = "Alice" };
name = page?.User?.Name;  // "Alice"
```

---

## 3. Null-coalescing оператор (??)

### Что это?

Оператор `??` возвращает левое значение, если оно не null, или правое значение, если левое null.

### Основное использование

```csharp
string? title = null;

// БЕЗ ?? (нужна проверка)
string displayTitle;
if (title != null)
{
    displayTitle = title;
}
else
{
    displayTitle = "Default Title";
}

// С ?? (одна строка)
displayTitle = title ?? "Default Title";  // "Default Title"

title = "Custom Title";
displayTitle = title ?? "Default Title";  // "Custom Title"
```

### С null-conditional

```csharp
User? user = null;

// Цепочка: если user null, используй "Unknown"
string name = user?.Name ?? "Unknown";

user = new User { Name = "Alice" };
name = user?.Name ?? "Unknown";  // "Alice"

user = new User { Name = null };
name = user?.Name ?? "Unknown";  // "Unknown"
```

### Со множественными значениями

```csharp
string? config1 = null;
string? config2 = null;
string? config3 = "value3";
string defaultValue = "default";

// Первый не null
string result = config1 ?? config2 ?? config3 ?? defaultValue;
// result = "value3"
```

---

## 4. Null-coalescing assignment (??=)

### Что это?

Оператор `??=` присваивает значение ТОЛЬКО если переменная null.

### Использование

```csharp
List<string> cache = null;

// БЕЗ ??=
if (cache == null)
{
    cache = new List<string>();
}

// С ??=
cache ??= new List<string>();  // Инициализирует если null

// Добавляем элемент
cache.Add("item1");
```

### Реальный пример

```csharp
public class TestSession
{
    private List<string> logs;
    
    public void LogAction(string action)
    {
        logs ??= new List<string>();  // Инициализируем при первом использовании
        logs.Add(action);
    }
    
    public List<string> GetLogs()
    {
        return logs ?? new List<string>();  // Возвращаем пустой список если null
    }
}

var session = new TestSession();
session.LogAction("Open page");
session.LogAction("Click button");

var logs = session.GetLogs();  // { "Open page", "Click button" }
```

---

## 5. Null-forgiving оператор (!)

### Что это?

Оператор `!` говорит компилятору: "Я гарантирую, что это не null, несмотря на тип".

### Использование (НЕ рекомендуется!)

```csharp
string? text = GetSomeText();  // Может быть null

// ? Без ! — компилятор волнуется
// int length = text.Length;  // Ошибка! text может быть null

// С ! — игнорируем опасность (ПЛОХО!)
int length = text!.Length;  // Компилятор молчит, но если null ? ??

// ? Правильно — проверить
if (text != null)
{
    int length = text.Length;  // Безопасно
}
```

### Когда использовать ! (редко)

```csharp
string text = "hello";

// Компилятор думает, что может быть null
string? maybeNull = text;

// Мы ЗНАЕМ, что text инициализирован (присвоили "hello")
string? result = maybeNull!;  // ! может помочь в специфичных случаях

// Но это плохая практика — лучше проверить!
```

---

## 6. Практический пример для QA

```csharp
public class LoginPage
{
    public Button? LoginButton { get; set; }
    public TextField? UsernameField { get; set; }
    public string? BaseUrl { get; set; }
}

public class Button
{
    public string? Text { get; set; }
    
    public void Click() { }
}

public class TextField
{
    public void Type(string text) { }
}

// Использование с null-операторами
public class LoginTest
{
    public void TestLogin(LoginPage? page)
    {
        if (page == null)
            return;
        
        // Безопасный доступ к вложенным объектам
        string buttonText = page.LoginButton?.Text ?? "Login";
        Console.WriteLine($"Button text: {buttonText}");
        
        // Инициализация при необходимости
        page.UsernameField ??= new TextField();
        page.UsernameField.Type("alice");
        
        // Условное выполнение
        page.LoginButton?.Click();  // Кликнем если кнопка не null
        
        // Значение по умолчанию
        string url = page.BaseUrl ?? "https://example.com";
        Console.WriteLine($"URL: {url}");
    }
}
```

---

## 7. Практическая таблица: Когда что использовать

| Ситуация | Оператор | Пример |
|----------|----------|---------|
| Безопасный доступ к property | `?.` | `user?.Name` |
| Безопасный вызов метода | `?.` | `user?.GetName()` |
| Безопасная индексация | `?.[...]` | `list?[0]` |
| Значение по умолчанию | `??` | `text ?? "default"` |
| Инициализация если null | `??=` | `cache ??= new List()` |
| "Я уверен, что не null" | `!` | `text!.Length` (РЕДКО!) |

---

## 8. Частые ошибки новичков

### ? Ошибка 1: Забыли ?
```csharp
User? user = null;

// ? Без ?
// string name = user.Name;  // ? NullReferenceException!

// ? С ?
string? name = user?.Name;  // null
```

### ? Ошибка 2: Неправильный порядок ??
```csharp
string a = "A";
string b = "B";

// ? Неправильно думать
string result = null ?? a ?? b;  // Вернет "A"

// ? Правильный порядок
string result = a ?? b ?? "default";  // Вернет "A"
```

### ? Ошибка 3: Использование ! без проверки
```csharp
string? text = GetText();  // Может быть null

// ? ПЛОХО! Если null ? ??
int length = text!.Length;

// ? ХОРОШО! Проверяем первым
if (text != null)
{
    int length = text.Length;
}
```

### ? Ошибка 4: ?? с методом, вызывающим побочные эффекты
```csharp
// ? Опасно
string name = user?.Name ?? GetDefaultName();  // GetDefaultName вызывается!

// ? Безопаснее
string name = user?.Name ?? "Default";
```

---

## 9. Комбинирование операторов (примеры)

```csharp
public class ComplexExample
{
    public class Config
    {
        public string? ApiUrl { get; set; }
        public int? Timeout { get; set; }
    }
    
    public void Configure(Config? config)
    {
        // Комбинирование ?.  и ??
        string url = config?.ApiUrl ?? "https://default.com";
        
        // Комбинирование ?. и ??=
        config ??= new Config();
        config.ApiUrl ??= "https://default.com";
        
        // Цепочка ?. 
        int timeout = config?.Timeout ?? 30;
        
        Console.WriteLine($"URL: {url}, Timeout: {timeout}s");
    }
}
```

---

## 10. Лучшие практики

? **DO:**
- Используйте `?.` для безопасного доступа
- Используйте `??` для значений по умолчанию
- Используйте `??=` для инициализации при необходимости
- Проверяйте null перед использованием, если сомневаетесь

? **DON'T:**
- Не используйте `!` без очень серьезной причины
- Не игнорируйте null-safety предупреждения компилятора
- Не смешивайте `?.` и `??` в сложных выражениях без скобок
- Не забывайте, что `?.` возвращает null, а не default value

---

## Файлы в проекте:
- `Program.cs` — примеры null-операторов
- `LoginPage.cs` — использование в Page Object
- `Config.cs` — работа с конфигурацией
