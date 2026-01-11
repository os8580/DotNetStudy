# Topic14 — Делегаты, функции и функциональное программирование

## Цель

Понять делегаты, Action/Func, lambda-выражения и функциональное программирование в C#. Научиться передавать функции как параметры, использовать callbacks, events и асинхронное программирование.

---

### Для полного новичка: быстрый маршрут

- Прочитайте: "Что такое делегат?", "Lambda vs Named", "Action/Func", "Callbacks"
- Запустите Program.cs: `dotnet run`
- Вернитесь к чек-листу в конце документа

---

## Содержание

1. [Что такое делегат?](#1-что-такое-делегат)
2. [Named vs Lambda vs Arrow](#2-named-vs-lambda-vs-arrow)
3. [Action<T> и Func<T>](#3-actiont-и-funct)
4. [Predicate и委托 patterns](#4-predicate-и-custom-delegates)
5. [Callbacks и Event Handling](#5-callbacks-и-event-handling)
6. [Практические примеры для QA](#6-практические-примеры-для-qa)
7. [LINQ и функциональное программирование](#7-linq-и-функциональное-программирование)
8. [Лучшие практики](#8-лучшие-практики)

---

## 1. Что такое делегат?

### Делегат — это тип данных для функций

```csharp
// Обычная переменная — хранит число
int number = 42;

// Делегат — это переменная, которая хранит ФУНКЦИЮ
public delegate void PrintDelegate(string text);  // Делегат, который принимает string, ничего не возвращает

// Можем присвоить функцию переменной
PrintDelegate printer = Console.WriteLine;  // Console.WriteLine — это функция!

// И вызовем как функцию
printer("Hello!");  // Вывод: Hello!
```

### Аналогия из жизни

Представьте, что делегат — это **инструкция для мастера**:

```
1. Обычная функция = мастер выполняет конкретную работу
2. Делегат = вы даёте мастеру готовую инструкцию, которую он исполнит
3. Callback = мастер, завершив основную работу, исполнит дополнительную инструкцию
```

### Почему это нужно?

```csharp
// ❌ БЕЗ делегатов: функция делает ВСЁ
public void ProcessData(int[] data)
{
    foreach (var item in data)
    {
        Console.WriteLine(item);  // Печатаем ВСЕ элементы
    }
}

// ✅ С делегатами: функция гибкая
public delegate void ProcessDelegate(int item);

public void ProcessData(int[] data, ProcessDelegate action)
{
    foreach (var item in data)
    {
        action(item);  // Выполняем переданную функцию
    }
}

// Теперь можем печатать, складывать, сортировать — всё зависит от функции!
ProcessData(new[] { 1, 2, 3 }, Console.WriteLine);           // Печать
ProcessData(new[] { 1, 2, 3 }, x => Console.WriteLine(x * 2)); // Печать * 2
```

---

## 2. Named vs Lambda vs Arrow

### 1️⃣ Named функции (обычные методы)

```csharp
// Обычный метод
public void PrintNumber(int x)
{
    Console.WriteLine($"Число: {x}");
}

// Использование с делегатом
Action<int> action = PrintNumber;  // Action = делегат для void
action(5);  // Вывод: Число: 5
```

### 2️⃣ Lambda-выражения (anonymous functions)

```csharp
// Lambda = анонимная функция (нет имени, пишется прямо на месте)
Action<int> action = (x) => Console.WriteLine($"Число: {x}");
action(5);  // Вывод: Число: 5

// Без параметров:
Action greet = () => Console.WriteLine("Hello!");
greet();  // Вывод: Hello!

// С возвращаемым значением:
Func<int, int> square = (x) => x * x;
int result = square(5);  // result = 25
```

### 3️⃣ Стрелочные функции (arrow notation в теле)

```csharp
// Краткая lambda с телом из одной строки = стрелочная функция
Func<int, int> double_value = x => x * 2;  // Скобки не нужны для одного параметра!
Console.WriteLine(double_value(5));  // 10

// С несколькими параметрами скобки нужны
Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine(add(3, 4));  // 7

// С телом из нескольких строк используем {}
Func<int, int> complexCalculation = x =>
{
    int doubled = x * 2;
    int added = doubled + 10;
    return added;  // Обязателен return!
};
Console.WriteLine(complexCalculation(5));  // 20
```

### Когда использовать?

```csharp
// ✅ Named — когда логика сложная и повторяется
public decimal CalculateDiscount(decimal price)
{
    if (price > 1000) return price * 0.9m;
    if (price > 500) return price * 0.95m;
    return price;
}
var discounted = CalculateDiscount(2000);

// ✅ Lambda — когда логика простая и используется в одном месте
var discounted = LINQ
    .Where(p => p > 500)          // Простая lambda
    .Select(p => ApplyDiscount(p));  // Сложная логика — named функция

// ✅ Arrow — самая читаемая для простых операций
Func<int, bool> isPositive = x => x > 0;
```

---

## 3. Action<T> и Func<T>

### Action — ничего не возвращает (void)

```csharp
// Action = функция, которая ничего не возвращает

Action greet = () => Console.WriteLine("Hello!");
greet();  // Выполнится, но ничего не вернёт

Action<string> printMessage = (msg) => Console.WriteLine(msg);
printMessage("Test");  // Выполнится

Action<int, int> printSum = (a, b) => Console.WriteLine($"Сумма: {a + b}");
printSum(3, 4);  // Выполнится
```

### Func — возвращает значение (return)

```csharp
// Func<Input1, Input2, ..., Output> = функция, которая возвращает значение
// Последний тип параметра всегда Return type!

// Func<Output>
Func<string> getName = () => "Alice";
string name = getName();  // name = "Alice"

// Func<Input, Output>
Func<int, bool> isEven = (x) => x % 2 == 0;
bool result = isEven(4);  // result = true

// Func<Input1, Input2, ..., Output>
Func<int, int, int> multiply = (x, y) => x * y;
int product = multiply(3, 4);  // product = 12
```

### Action vs Func — Таблица

| Что | Action | Func |
|-----|--------|------|
| Возвращает значение? | ❌ Нет | ✅ Да |
| Return type | `void` | Указывается последним |
| Пример | `Action<int> p = x => Print(x);` | `Func<int, bool> e = x => x > 5;` |
| Использование | Выполнение побочных эффектов | Вычисления и трансформации |

---

## 4. Predicate и Custom Delegates

### Predicate<T> — функция для проверки условий

```csharp
// Predicate = функция, которая возвращает bool (проверяет условие)
// Это просто Func<T, bool> с красивым имением!

Predicate<int> isPositive = x => x > 0;
bool result = isPositive(5);  // true

// Используется в методах коллекций
int[] numbers = { -2, -1, 0, 1, 2, 3 };
int[] positive = Array.FindAll(numbers, isPositive);  // { 1, 2, 3 }

// Или прямо в LINQ
var positiveLinq = numbers.Where(x => x > 0).ToArray();  // { 1, 2, 3 }
```

### Custom Delegates — свои делегаты

```csharp
// Определяем свой делегат
public delegate decimal PriceCalculator(decimal basePrice, int quantity);

// Разные реализации одного делегата
PriceCalculator retailPrice = (price, qty) => price * qty;  // Обычная цена
PriceCalculator wholesalePrice = (price, qty) => price * qty * 0.8m;  // Оптовая с скидкой

decimal orderRetail = retailPrice(100, 5);  // 500
decimal orderWholesale = wholesalePrice(100, 5);  // 400

// Делегат с несколькими параметрами
public delegate void NotificationDelegate(string title, string message, int priority);

NotificationDelegate sendEmail = (t, m, p) => 
    Console.WriteLine($"[{p}] Email: {t} - {m}");

NotificationDelegate sendSMS = (t, m, p) => 
    Console.WriteLine($"[{p}] SMS: {t}");

sendEmail("Alert", "Server down!", 1);  // [1] Email: Alert - Server down!
sendSMS("Warning", "Low disk", 2);      // [2] SMS: Warning
```

---

## 5. Callbacks и Event Handling

### Callbacks — функция вызывает другую функцию

```csharp
// Callback = вы передаёте функцию, которая будет вызвана позже

// Функция, которая принимает callback
public void FetchUserData(string userId, Action<string> onSuccess, Action<string> onError)
{
    // Имитируем асинхронный запрос
    if (userId == "admin")
    {
        onSuccess("User: Admin");  // Вызываем успешный callback
    }
    else
    {
        onError("User not found");  // Вызываем callback ошибки
    }
}

// Использование
FetchUserData(
    "admin",
    result => Console.WriteLine($"✅ {result}"),  // onSuccess callback
    error => Console.WriteLine($"❌ {error}")      // onError callback
);

// Вывод: ✅ User: Admin
```

### Event Handling — callback для событий

```csharp
// EventHandler = делегат для событий
public class Button
{
    public event EventHandler Clicked;  // Событие

    public void Click()
    {
        Console.WriteLine("Button clicked!");
        Clicked?.Invoke(this, EventArgs.Empty);  // Триггерим событие
    }
}

// Подписываемся на событие
var button = new Button();
button.Clicked += (sender, e) => Console.WriteLine("Handler 1 triggered");
button.Clicked += (sender, e) => Console.WriteLine("Handler 2 triggered");

button.Click();
// Вывод:
// Button clicked!
// Handler 1 triggered
// Handler 2 triggered
```

### Custom Events

```csharp
// Свой делегат для события
public delegate void DownloadCompleteDelegate(string filename, double sizeMB);

public class FileDownloader
{
    public event DownloadCompleteDelegate OnDownloadComplete;

    public void Download(string filename)
    {
        Console.WriteLine($"Downloading {filename}...");
        System.Threading.Thread.Sleep(1000);  // Имитируем загрузку
        double sizeMB = 50.5;
        OnDownloadComplete?.Invoke(filename, sizeMB);
    }
}

// Использование
var downloader = new FileDownloader();

downloader.OnDownloadComplete += (file, size) =>
    Console.WriteLine($"✅ {file} ({size}MB) готов!");

downloader.OnDownloadComplete += (file, size) =>
    Console.WriteLine($"📊 Сохраняем лог загрузки");

downloader.Download("video.mp4");
// Вывод:
// Downloading video.mp4...
// ✅ video.mp4 (50.5MB) готов!
// 📊 Сохраняем лог загрузки
```

---

## 6. Практические примеры для QA

### Пример 1: Custom WebDriver с Callbacks

```csharp
public class WebDriver
{
    private Action<string> onElementFound;
    private Action<string> onElementNotFound;

    public WebDriver(Action<string> found, Action<string> notFound)
    {
        onElementFound = found;
        onElementNotFound = notFound;
    }

    public void FindElement(string xpath)
    {
        // Имитируем поиск элемента
        bool exists = xpath.Contains("login");
        
        if (exists)
            onElementFound($"✅ Элемент найден: {xpath}");
        else
            onElementNotFound($"❌ Элемент не найден: {xpath}");
    }
}

// Использование
var driver = new WebDriver(
    found => Console.WriteLine(found),
    notFound => { 
        Console.WriteLine(notFound);
        throw new Exception("Element not found");
    }
);

driver.FindElement("//*[@id='login']");      // ✅ Элемент найден
driver.FindElement("//*[@id='nonexistent']"); // ❌ Элемент не найден + Exception
```

### Пример 2: Retry Logic с Callbacks

```csharp
public class ApiClient
{
    public void MakeRequest(Func<bool> requestFunc, Action onSuccess, Action<int> onRetry, int maxRetries = 3)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            if (requestFunc())
            {
                onSuccess();
                return;
            }
            onRetry(i + 1);
        }
    }
}

// Использование
var client = new ApiClient();
int attempts = 0;

client.MakeRequest(
    requestFunc: () => {
        attempts++;
        return attempts == 2;  // Успехнём на второй попытке
    },
    onSuccess: () => Console.WriteLine("✅ Запрос успешен"),
    onRetry: (attempt) => Console.WriteLine($"⚠️ Попытка {attempt} провалилась, повторяем...")
);

// Вывод:
// ⚠️ Попытка 1 провалилась, повторяем...
// ✅ Запрос успешен
```

### Пример 3: Chain of Handlers (паттерн责任链)

```csharp
public class RequestHandler
{
    private Func<string, bool> canHandle;
    private Action<string> process;

    public RequestHandler(Func<string, bool> canHandle, Action<string> process)
    {
        this.canHandle = canHandle;
        this.process = process;
    }

    public bool Handle(string request)
    {
        if (canHandle(request))
        {
            process(request);
            return true;
        }
        return false;
    }
}

// Использование
var handlers = new List<RequestHandler>
{
    new RequestHandler(
        canHandle: r => r.StartsWith("LOGIN"),
        process: r => Console.WriteLine($"✅ Обрабатываем логин: {r}")
    ),
    new RequestHandler(
        canHandle: r => r.StartsWith("DATA"),
        process: r => Console.WriteLine($"📊 Обрабатываем данные: {r}")
    ),
    new RequestHandler(
        canHandle: r => r.StartsWith("ERROR"),
        process: r => Console.WriteLine($"❌ Обрабатываем ошибку: {r}")
    )
};

foreach (var request in new[] { "LOGIN alice", "DATA report", "ERROR 404" })
{
    foreach (var handler in handlers)
    {
        if (handler.Handle(request))
            break;
    }
}
```

---

## 7. LINQ и функциональное программирование

### Функциональный подход с Delegates

```csharp
public class DataProcessor
{
    public List<T> Transform<T, U>(List<U> data, Func<U, T> transformer)
    {
        var result = new List<T>();
        foreach (var item in data)
            result.Add(transformer(item));
        return result;
    }

    public List<T> Filter<T>(List<T> data, Predicate<T> predicate)
    {
        var result = new List<T>();
        foreach (var item in data)
            if (predicate(item))
                result.Add(item);
        return result;
    }

    public T Aggregate<T>(List<T> data, T initial, Func<T, T, T> aggregator)
    {
        T result = initial;
        foreach (var item in data)
            result = aggregator(result, item);
        return result;
    }
}

// Использование (это то же самое, что LINQ!)
var processor = new DataProcessor();
var numbers = new List<int> { 1, 2, 3, 4, 5 };

// Transform = Select
var doubled = processor.Transform(numbers, x => x * 2);  // { 2, 4, 6, 8, 10 }

// Filter = Where
var even = processor.Filter(numbers, x => x % 2 == 0);  // { 2, 4 }

// Aggregate = Aggregate/Sum/Fold
var sum = processor.Aggregate(numbers, 0, (acc, x) => acc + x);  // 15

// Тоже самое с LINQ (встроенным):
var doubled2 = numbers.Select(x => x * 2).ToList();
var even2 = numbers.Where(x => x % 2 == 0).ToList();
var sum2 = numbers.Sum();
```

### Composing Functions (композиция функций)

```csharp
// Функция, которая "склеивает" две функции
public static Func<TIn, TOut> Compose<TIn, TMid, TOut>(
    Func<TIn, TMid> f1, 
    Func<TMid, TOut> f2)
{
    return x => f2(f1(x));
}

// Пример: прочитать строку → парсить → умножить
Func<string, int> parseToInt = str => int.Parse(str);
Func<int, int> multiply = x => x * 2;

var composed = Compose(parseToInt, multiply);
int result = composed("5");  // 5 → 10

// Практический пример
Func<string, string> trim = s => s.Trim();
Func<string, string> toUpper = s => s.ToUpper();
Func<string, int> length = s => s.Length;

var pipeline = Compose(
    Compose(trim, toUpper),  // (trim → toUpper)
    length                    // → length
);

int len = pipeline("  hello  ");  // "hello" → "HELLO" → 5
```

---

## 8. Лучшие практики

### ✅ DO

```csharp
// 1. Используйте Action/Func вместо custom delegates (если просто)
Action<int> print = x => Console.WriteLine(x);  // ✅

// 2. Используйте lambda для простой логики
var positive = numbers.Where(x => x > 0);  // ✅

// 3. Используйте named functions для сложной логики
private bool IsValidEmail(string email)  // ✅ Сложная логика
{
    // ...20 строк проверок
}

// 4. Используйте ?? для null coalescing
action?.Invoke(data);  // ✅ Проверяет null перед вызовом

// 5. Документируйте callback-параметры
public void FetchData(
    string url,
    Action<string> onSuccess,     // ✅ Явно указано, что это callback
    Action<Exception> onError)
{
    // ...
}
```

### ❌ DON'T

```csharp
// 1. Не используйте lambda в сложных ситуациях
var users = data.Where(u => {  // ❌ Два { }? Используйте named function!
    if (u.Age > 18 && u.Status == "active" && u.Department == "IT") 
        return true;
    return false;
});

// 2. Не вызывайте делегат без проверки на null
action.Invoke(data);  // ❌ NullReferenceException если action == null
action?.Invoke(data);  // ✅ Безопасно

// 3. Не создавайте custom delegates просто так
public delegate void CustomAction();  // ❌ Используйте Action вместо этого

// 4. Не перегружайте callback-цепи
FetchData(url, 
    onSuccess: d => ProcessData(d),
    onError: e => HandleError(e),
    onTimeout: () => Retry(),
    onNotFound: () => ShowDefault(),
    onPermissionDenied: () => ShowLogin(),
    ...  // ❌ Слишком много callbacks! Используйте Result<T> паттерн
);

// 5. Не забывайте про closure при использовании lambda в циклах
for (int i = 0; i < 5; i++)
{
    actions.Add(() => Console.WriteLine(i));  // ❌ Все выведут 5!
}

// ✅ Правильно:
for (int i = 0; i < 5; i++)
{
    int copy = i;  // Копируем переменную
    actions.Add(() => Console.WriteLine(copy));  // Теперь OK
}
```

---

## 9. Частые ошибки в QA тестах

```csharp
// ❌ Ошибка 1: Забыли null check на callback
public void WaitForElement(Action<bool> onComplete)
{
    bool found = FindElement();
    onComplete(found);  // 💥 NullReferenceException если onComplete == null!
}

// ✅ Правильно:
public void WaitForElement(Action<bool> onComplete)
{
    bool found = FindElement();
    onComplete?.Invoke(found);  // Безопасно
}

// ❌ Ошибка 2: Callback не вызывается при исключении
public void FetchData(Action onSuccess)
{
    var data = WebDriver.FindElement();  // Если выбросит exception, onSuccess не вызовется!
    onSuccess();
}

// ✅ Правильно:
public void FetchData(Action<bool> onComplete, Action<Exception> onError)
{
    try
    {
        var data = WebDriver.FindElement();
        onComplete(true);
    }
    catch (Exception ex)
    {
        onError(ex);
    }
}

// ❌ Ошибка 3: Closure в цикле
foreach (var element in elements)
{
    element.OnClick += () => Console.WriteLine(element.Name);  // Все обработчики выведут последний элемент!
}

// ✅ Правильно:
foreach (var element in elements)
{
    var localElement = element;  // Копируем
    element.OnClick += () => Console.WriteLine(localElement.Name);  // OK
}
```

---

## 10. ЧЕК-ЛИСТ ПРОВЕРКИ ЗНАНИЙ 🎯

### Вопрос 1: Что такое делегат и когда его использовать?

**Ответ:** Делегат — тип для хранения функции (как переменная хранит число). Используйте когда нужна гибкость: функция принимает разные операции, callbacks, events.

### Вопрос 2: В чем разница между Action и Func?

**Ответ:** Action = void (ничего не возвращает). Func = return (возвращает значение). Последний тип в Func — это return type.

### Вопрос 3: Когда использовать lambda, а когда named function?

**Ответ:** Lambda — простая логика в одной линии. Named function — сложная логика или используется несколько раз.

### Вопрос 4: Что такое callback?

**Ответ:** Callback — функция, которую вы передаёте и которая будет вызвана позже (часто при завершении операции).

### Вопрос 5: Как безопасно вызвать делегат, который может быть null?

**Ответ:** Используйте `?.Invoke()` вместо `.Invoke()` для null-safe вызова.

### Вопрос 6: Что такое closure и почему это проблема в циклах?

**Ответ:** Closure = lambda "захватывает" переменную из внешней области. В цикле все lambda захватят одну переменную, поэтому скопируйте в локальную.

### Вопрос 7: Как использовать делегаты в тестах?

**Ответ:** Для callbacks (успех/ошибка), обработки событий (клики, загрузки), retry-логики, mocking (Moq использует Action/Func).

### Вопрос 8: Что такое Event и как он отличается от обычного callback?

**Ответ:** Event — публичный интерфейс для callbacks. Только владелец может trigg Event, подписчики только подписываются (+=) или отписываются (-=).

### Вопрос 9: Почему LINQ использует Func/Predicate?

**Ответ:** LINQ методы (Where, Select, OrderBy) принимают Func/Predicate, чтобы быть гибкими. Где(x => x > 5) — lambda как аргумент.

### Вопрос 10: Как создать свой делегат?

**Ответ:** `public delegate ReturnType DelegateName(parameters);` затем используйте как тип переменной: `DelegateName myFunc = ...;`

---

## Файлы в проекте

- `Program.cs` — примеры всех концепций
- `README.md` — этот файл

---

**Стартовая точка:** Запустите Program.cs чтобы увидеть все примеры в действии.

**Готово к использованию!** ✅
