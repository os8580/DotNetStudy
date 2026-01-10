# Topic2 — Интерфейсы и инъекция зависимостей (Полный курс для начинающих)

## Цель

Понять, что такое интерфейсы, почему они важны, и как использовать инъекцию зависимостей (Dependency Injection, DI) в реальных проектах.

---

### Для полного новичка: быстрый маршрут

- Прочитайте разделы: "Что такое интерфейс?", "Зачем нужны интерфейсы?", "Инъекция зависимостей", "ServiceCollection/Provider", "SOLID DIP".
- Запустите Program.cs и сравните поведение с разными реализациями ILoginService.
- Вернитесь к чек‑листу: добавлены краткие ответы под каждым пунктом.

## 1. Что такое интерфейс? (Для самых начинающих)

### Аналогия из жизни

Представьте, что интерфейс — это **контракт** или **правила игры**:

```
Реальная жизнь:
- Интерфейс "Розетка" говорит: "Любое устройство с вилкой может подключиться ко мне"
- Холодильник, лампа, ноутбук — все имеют вилку, так что все работают!
- Производитель розетки не знает, какое устройство подключат, но он знает, что оно будет следовать контракту
```

### В программировании:

```csharp
// Интерфейс — контракт (что должно быть)
public interface ILoginService
{
    void Login(string username, string password);
    bool IsLoggedIn { get; }
}

// Реализация 1 — для UI (дом)
public class UiLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        // Вводим данные через UI
        Console.WriteLine($"Вход через форму: {username}");
    }

    public bool IsLoggedIn { get; set; }
}

// Реализация 2 — для API (квартира)
public class ApiLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        // Отправляем HTTP запрос
        Console.WriteLine($"Вход через API: {username}");
    }

    public bool IsLoggedIn { get; set; }
}

// Код, который использует интерфейс, работает с ОБОИМИ
public class LoginTest
{
    private ILoginService _service;

    public LoginTest(ILoginService service)
    {
        _service = service;  // Получили любую реализацию
    }

    public void TestLogin()
    {
        _service.Login("alice", "password123");
        Console.WriteLine(_service.IsLoggedIn);
    }
}

// МОЩЬ: Мы можем тестировать с фальшивой реализацией!
public class FakeLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        // Ничего не делаем, просто для теста
    }

    public bool IsLoggedIn => true;  // Всегда логинимся для теста
}

// Использование
ILoginService realService = new ApiLoginService();
LoginTest test = new LoginTest(realService);
test.TestLogin();

// Для теста используем фальшивую!
ILoginService fakeService = new FakeLoginService();
LoginTest testWithFake = new LoginTest(fakeService);
testWithFake.TestLogin();  // Тест работает, но без интернета!
```

---

## 2. Зачем нужны интерфейсы? (3 причины)

### Причина 1: Слабая связь (Loose Coupling)

? **Без интерфейса** (жесткая связь):

```csharp
public class LoginTest
{
    private ApiLoginService _service;  // ? Привязан к конкретному классу!

    public LoginTest()
    {
        _service = new ApiLoginService();
    }

    public void TestLogin()
    {
        _service.Login("alice", "password");
    }
}

// Проблемы:
// - Если ApiLoginService изменится, ломается LoginTest
// - Нельзя тестировать без интернета
// - Нельзя переключиться на другую реализацию
```

? **С интерфейсом** (слабая связь):

```csharp
public class LoginTest
{
    private ILoginService _service;  // ? Зависит от интерфейса!

    public LoginTest(ILoginService service)
    {
        _service = service;  // Может быть любая реализация
    }

    public void TestLogin()
    {
        _service.Login("alice", "password");
    }
}

// Преимущества:
// - Легко переключаться между реализациями
// - Легко тестировать с FakeLoginService
// - Код более гибкий и переиспользуемый
```

### Причина 2: Тестирование

```csharp
// Реальный сервис (требует интернет)
public class ApiLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        var client = new HttpClient();
        // Отправляем на реальный сервер...
    }

    public bool IsLoggedIn { get; set; }
}

// Фальшивый сервис для тестов (никакого интернета не нужно!)
public class FakeLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        // Ничего не делаем, пока что просто пропускаем
    }

    public bool IsLoggedIn { get; set; } = true;
}

// Тест работает и с реальным, и с фальшивым сервисом!
public class LoginTests
{
    [Test]
    public void TestLoginWithRealService()
    {
        ILoginService service = new ApiLoginService();
        var test = new LoginTest(service);
        test.TestLogin();  // Медленно, но реально
    }

    [Test]
    public void TestLoginWithFakeService()
    {
        ILoginService service = new FakeLoginService();
        var test = new LoginTest(service);
        test.TestLogin();  // Быстро, в памяти
    }
}
```

### Причина 3: Множественные реализации

```csharp
// Интерфейс
public interface ILoginService
{
    void Login(string username, string password);
    bool IsLoggedIn { get; }
}

// Реализация для веб-приложения
public class WebLoginService : ILoginService
{
    public void Login(string username, string password) { /* ... */ }
    public bool IsLoggedIn { get; set; }
}

// Реализация для мобильного приложения
public class MobileLoginService : ILoginService
{
    public void Login(string username, string password) { /* ... */ }
    public bool IsLoggedIn { get; set; }
}

// Реализация для десктопного приложения
public class DesktopLoginService : ILoginService
{
    public void Login(string username, string password) { /* ... */ }
    public bool IsLoggedIn { get; set; }
}

// Один код работает со всеми!
public class LoginPage
{
    public LoginPage(ILoginService service) { }
}
```

---

## 3. Инъекция зависимостей (Dependency Injection)

### Что это?

DI — это способ **передать зависимость** объекту вместо того, чтобы объект создавал её сам.

```
Аналогия:
- БЕЗ DI: Человек сам готовит ужин дома (создает зависимость)
- С DI: Вам доставляют готовый ужин (получаете зависимость из вне)
```

### 3 способа инъекции

#### 1?? Конструктор (рекомендуется)

```csharp
public class LoginTest
{
    private ILoginService _service;

    // Зависимость передается через конструктор
    public LoginTest(ILoginService service)
    {
        _service = service;
    }
}

// Использование
ILoginService service = new ApiLoginService();
LoginTest test = new LoginTest(service);  // Передали через конструктор
```

**Преимущества:**

- ? Зависимость видна в сигнатуре
- ? Объект полностью инициализирован после создания
- ? Легко тестировать

#### 2?? Свойство (для опциональных зависимостей)

```csharp
public class LoginTest
{
    // Может быть не установлено
    public ILoginService Service { get; set; }
}

// Использование
LoginTest test = new LoginTest();
test.Service = new ApiLoginService();  // Установили после создания
```

**Минусы:**

- ? Не ясно, необходимо ли это свойство
- ? Можно забыть установить

#### 3?? Метод (редко)

```csharp
public class LoginTest
{
    private ILoginService _service;

    public void SetService(ILoginService service)
    {
        _service = service;
    }
}
```

### Полный пример с DI

```csharp
// Шаг 1: Определяем интерфейс
public interface ILoginService
{
    void Login(string username, string password);
}

// Шаг 2: Реализуем интерфейс
public class ApiLoginService : ILoginService
{
    public void Login(string username, string password)
    {
        Console.WriteLine($"API Login: {username}");
    }
}

// Шаг 3: Используем через DI
public class LoginTest
{
    private ILoginService _service;

    // Конструктор с инъекцией
    public LoginTest(ILoginService service)
    {
        _service = service;
    }

    public void Test()
    {
        _service.Login("alice", "password");
    }
}

// Шаг 4: Используем в Program.cs
class Program
{
    static void Main()
    {
        // Создаем сервис
        ILoginService service = new ApiLoginService();

        // Передаем в тест
        LoginTest test = new LoginTest(service);

        // Запускаем тест
        test.Test();
    }
}
```

---

## 4. ServiceCollection и ServiceProvider (для больших проектов)

Когда зависимостей много, удобнее использовать контейнер DI:

```csharp
using Microsoft.Extensions.DependencyInjection;

// Регистрируем зависимости
var services = new ServiceCollection();

// Регистрируем как ILoginService -> ApiLoginService
services.AddScoped<ILoginService, ApiLoginService>();

// Создаем контейнер
ServiceProvider provider = services.BuildServiceProvider();

// Получаем объект (все зависимости внедрены автоматически!)
ILoginService service = provider.GetRequiredService<ILoginService>();

// Используем
LoginTest test = new LoginTest(service);
test.Test();
```

### Жизненные циклы (Lifetimes)

| Тип           | Как работает                    | Пример            |
| ------------- | ------------------------------- | ----------------- |
| **Transient** | Новый объект каждый раз         | Временные объекты |
| **Scoped**    | Один объект на область (запрос) | Подключение к БД  |
| **Singleton** | Один объект на всё приложение   | Кэш, конфигурация |

```csharp
// Transient — новый объект каждый раз
services.AddTransient<ILoginService, ApiLoginService>();

// Scoped — один на запрос (для веб)
services.AddScoped<ILoginService, ApiLoginService>();

// Singleton — один на всё приложение
services.AddSingleton<ILoginService, ApiLoginService>();

var provider = services.BuildServiceProvider();

var service1 = provider.GetRequiredService<ILoginService>();
var service2 = provider.GetRequiredService<ILoginService>();

// Transient: service1 != service2 (разные объекты)
// Scoped: service1 == service2 (в одной области)
// Singleton: service1 == service2 (всегда один объект)
```

---

## 5. SOLID принцип: Dependency Inversion (DIP)

Это 5-й принцип из SOLID:

> **"Зависимость высокоуровневых модулей не должна быть от низкоуровневых. Обе должны зависеть от абстракции (интерфейса)"**

### Плохо ?

```csharp
// LoginTest зависит напрямую от ApiLoginService
public class LoginTest
{
    private ApiLoginService _service = new ApiLoginService();

    public void Test()
    {
        _service.Login("alice", "password");
    }
}
```

### Хорошо ?

```csharp
// LoginTest зависит от абстракции ILoginService
public class LoginTest
{
    private ILoginService _service;

    public LoginTest(ILoginService service)
    {
        _service = service;
    }

    public void Test()
    {
        _service.Login("alice", "password");
    }
}
```

---

## 6. Практический пример для начинающих

```csharp
// ========== ИНТЕРФЕЙС ==========
public interface IWebDriver
{
    void Open(string url);
    string GetTitle();
}

// ========== РЕАЛИЗАЦИЯ 1 (реальная) ==========
public class ChromeDriver : IWebDriver
{
    private string _currentUrl;
    private string _title;

    public void Open(string url)
    {
        // Реально открываем браузер
        _currentUrl = url;
        _title = "Chrome - " + url;
        Console.WriteLine($"?? Открыли в Chrome: {url}");
    }

    public string GetTitle()
    {
        return _title;
    }
}

// ========== РЕАЛИЗАЦИЯ 2 (для тестов) ==========
public class FakeDriver : IWebDriver
{
    private string _title = "Fake Title";

    public void Open(string url)
    {
        Console.WriteLine($"?? Фальшивый открыл: {url} (без интернета)");
    }

    public string GetTitle()
    {
        return _title;
    }
}

// ========== СТРАНИЦА (зависит от интерфейса) ==========
public class LoginPage
{
    private IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Login(string username, string password)
    {
        _driver.Open("https://example.com/login");
        Console.WriteLine($"Логинимся как {username}...");
    }

    public string GetTitle()
    {
        return _driver.GetTitle();
    }
}

// ========== ИСПОЛЬЗОВАНИЕ ==========
class Program
{
    static void Main()
    {
        // Тест с реальным браузером
        Console.WriteLine("=== С РЕАЛЬНЫМ БРАУЗЕРОМ ===");
        IWebDriver realDriver = new ChromeDriver();
        LoginPage page1 = new LoginPage(realDriver);
        page1.Login("alice", "password");
        Console.WriteLine(page1.GetTitle());

        Console.WriteLine("\n=== С ФАЛЬШИВЫМ ДЛЯ ТЕСТА ===");
        // Тест с фальшивым (быстрее!)
        IWebDriver fakeDriver = new FakeDriver();
        LoginPage page2 = new LoginPage(fakeDriver);
        page2.Login("bob", "secret");
        Console.WriteLine(page2.GetTitle());
    }
}

// Вывод:
// === С РЕАЛЬНЫМ БРАУЗЕРОМ ===
// ?? Открыли в Chrome: https://example.com/login
// Логинимся как alice...
// Chrome - https://example.com/login
//
// === С ФАЛЬШИВЫМ ДЛЯ ТЕСТА ===
// ?? Фальшивый открыл: https://example.com/login (без интернета)
// Логинимся как bob...
```

---

## 7. Частые ошибки новичков

### ? Ошибка 1: Создание зависимости внутри класса

```csharp
public class LoginTest
{
    private ApiLoginService _service;

    public LoginTest()
    {
        _service = new ApiLoginService();  // ? Привязаны к конкретному классу!
    }
}

// Проблема: Не можем использовать FakeLoginService для тестов
```

### ? Ошибка 2: Забыли передать зависимость

```csharp
public class LoginTest
{
    private ILoginService _service;

    public LoginTest()
    {
        // ? Откуда появился _service? Он же null!
    }

    public void Test()
    {
        _service.Login("alice", "password");  // ?? NullReferenceException!
    }
}
```

### ? Ошибка 3: Слишком много зависимостей

```csharp
public class Page
{
    public Page(IDriver d, IWait w, IClicker c, ITyper t,
                IScroller s, IValidator v, ILogger l,
                INotifier n, IAnalytics a, ICache ca)
    {
        // ?? 10 параметров! Это код-запах (code smell)
    }
}
```

**Решение**: Группируйте логические зависимости

```csharp
public interface IPageActions
{
    void Click(string selector);
    void Type(string selector, string text);
    void Scroll(int pixels);
}

public class Page
{
    public Page(IDriver driver, IPageActions actions)
    {
        // ? Намного понятнее!
    }
}
```

---

## 8. Лучшие практики

? **DO:**

- Зависимости от интерфейсов, а не от конкретных классов
- Внедряйте через конструктор
- Используйте интерфейсы для замены реализаций на тестирование
- Регистрируйте зависимости в ServiceCollection

? **DON'T:**

- Не создавайте зависимости внутри класса (new)
- Не передавайте больше 3-4 зависимостей в конструктор
- Не забывайте про null-checks при получении из ServiceProvider
- Не смешивайте создание объектов с логикой класса

---

## Файлы в проекте:

- `ILoginService.cs` — интерфейс
- `UiLoginService.cs`, `ApiLoginService.cs`, `MobileLoginService.cs` — реализации
- `LoginTest.cs` — класс, использующий DI
- `Program.cs` — конфигурация и использование

---

## ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ

### На вопрос "Что такое интерфейс?":

Краткий ответ: Интерфейс — контракт (набор сигнатур), определяет что класс должен делать, но не как; один интерфейс может иметь много реализаций.

- Интерфейс это **контракт** (набор правил)
- Интерфейс определяет **что** класс должен делать, но не **как**
- Интерфейс НЕ содержит реализацию (только сигнатуры методов)
- Класс может реализовать **несколько интерфейсов**
- Интерфейс начинается с префикса I (convention): ILoginService, ILogger, IDatabase

### На вопрос "Зачем нужны интерфейсы?":

Краткий ответ: Слабая связь, гибкость, тестируемость, полиморфизм и расширяемость — код зависит от абстракции, а реализацию можно менять.

- **Слабая связь (Loose Coupling)** класс не привязан к конкретной реализации
- **Гибкость** легко менять реализацию
- **Тестирование** можно использовать Mock/Fake реализации
- **Полиморфизм** один интерфейс, много реализаций
- **Расширяемость** легко добавлять новые реализации

### На вопрос "Что такое Dependency Injection?":

Краткий ответ: DI — передача зависимостей извне (конструктор/свойство/метод), класс не создаёт их сам; упрощает тестирование и замену реализаций.

- DI это **передача зависимостей** в класс вместо создания их внутри
- Зависимость передается через **конструктор** (рекомендуется)
- Зависимость может передаваться через **свойство** или **метод**
- **IoC (Inversion of Control)** класс НЕ контролирует создание своих зависимостей
- DI контейнер (ServiceCollection) автоматически создает и внедряет зависимости

### На вопрос "Какие три способа внедрения зависимостей?":

Краткий ответ: Constructor (основной), Property (опциональные), Method (редко); предпочтительно — конструктор.

- **Constructor Injection** (рекомендуется): public Page(IDriver driver) { ... }
- **Property Injection**: public IDriver Driver { get; set; }
- **Method Injection**: public void SetDriver(IDriver driver) { ... }

### На вопрос "Что такое DI контейнер?":

Краткий ответ: Контейнер регистрирует интерфейсы и их реализации, управляет созданием объектов и внедрением зависимостей (ServiceCollection/ServiceProvider).

- Контейнер управляет **созданием объектов** и **внедрением зависимостей**
- **ServiceCollection** в .NET регистрируем зависимости
- **ServiceProvider** получаем объекты из контейнера
- Контейнер знает, какую реализацию использовать для каждого интерфейса

### На вопрос "Какие три жизненных цикла в DI?":

Краткий ответ: Transient — каждый раз новый; Scoped — один на область (запрос); Singleton — один на всё приложение.

- **Transient** новый объект КАЖДЫЙ раз
- **Scoped** один объект НА ОБЛАСТЬ (запрос в веб-приложении)
- **Singleton** один объект НА ВСЕХ (на всё приложение)

### На вопрос "Расскажите про SOLID / Dependency Inversion Principle":

Краткий ответ: Зависеть от абстракции (интерфейса), а не от конкретного класса; высокоуровневый код не должен зависеть от низкоуровневых реализаций.

- **DIP** "Зависьте от абстракций, а не от конкретных реализаций"
- Высокоуровневые модули НЕ должны зависеть от низкоуровневых
- Оба должны зависеть от абстракции (интерфейса)
- **Плохо**: private ApiLoginService \_service = new ApiLoginService();
- **Хорошо**: public Page(ILoginService service) { ... }

### На вопрос "Как вы будете тестировать код с интерфейсами?":

Краткий ответ: Использовать Fake/Mock реализацию интерфейса, передав её через конструктор — тесты работают без внешних ресурсов.

- Создаю **Fake/Mock реализацию** интерфейса
- Передаю её через конструктор
- Код работает с обеими реализациями: реальной и фальшивой
- Тесты быстрые (не требуют интернета, БД и т.д.)

---

## Topic2_Interfaces полностью готов!

Вы изучили:

- Что такое интерфейсы и контракты
- Dependency Injection и его 3 способа
- DI контейнер (ServiceCollection, ServiceProvider)
- Жизненные циклы (Transient, Scoped, Singleton)
- SOLID принцип: Dependency Inversion Principle
- Практические примеры с 3 реализациями
- Как использовать для тестирования

Это фундамент профессионального кода!
