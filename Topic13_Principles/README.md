# Topic13 — Принципы проектирования: SOLID, KISS, DRY (Полный курс для начинающих)

## Цель

Понять основные принципы проектирования кода, которые делают его более читаемым, тестируемым и поддерживаемым. Эти принципы применяются ВЕЗДЕ в профессиональной разработке.

---

### Для полного новичка: быстрый маршрут

- **Прочитайте разделы:** DRY, SOLID (5 принципов), KISS
- **Главное правило:** "Делай просто, не повторяйся, думай о расширяемости"
- **Запустите примеры в Program.cs** и сравните хороший/плохой код
- **Вернитесь к чек-листу** (конец документа): вопросы про практическое применение.

---

## 1. DRY (Don't Repeat Yourself) — Не повторяйся

### Суть

**Если вы копируете код в третий раз — напишите функцию!**

### Пример 1: Плохо ❌

```csharp
[Test]
public void TestLoginWithValidCredentials()
{
    var driver = new ChromeDriver();
    driver.Navigate().GoToUrl("https://example.com/login");
    driver.FindElement(By.Id("username")).SendKeys("alice");
    driver.FindElement(By.Id("password")).SendKeys("password123");
    driver.FindElement(By.Id("submit")).Click();

    Assert.IsTrue(driver.FindElement(By.Id("dashboard")).Displayed);
}

[Test]
public void TestLogout()
{
    var driver = new ChromeDriver();
    driver.Navigate().GoToUrl("https://example.com/login");
    driver.FindElement(By.Id("username")).SendKeys("alice");
    driver.FindElement(By.Id("password")).SendKeys("password123");
    driver.FindElement(By.Id("submit")).Click();
    // ← Один и тот же код логина!

    driver.FindElement(By.Id("logout")).Click();
    Assert.IsTrue(driver.FindElement(By.Id("login_form")).Displayed);
}
```

**Проблема:** код логина повторяется в каждом тесте! Если поменяется селектор — нужно менять везде.

### Пример 2: Хорошо ✅

```csharp
// Вспомогательный метод — единственный источник правды
private void LoginAsUser(IWebDriver driver, string username, string password)
{
    driver.Navigate().GoToUrl("https://example.com/login");
    driver.FindElement(By.Id("username")).SendKeys(username);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.Id("submit")).Click();
}

[Test]
public void TestLoginWithValidCredentials()
{
    var driver = new ChromeDriver();
    LoginAsUser(driver, "alice", "password123");

    Assert.IsTrue(driver.FindElement(By.Id("dashboard")).Displayed);
}

[Test]
public void TestLogout()
{
    var driver = new ChromeDriver();
    LoginAsUser(driver, "alice", "password123");  // ← Переиспользуем!

    driver.FindElement(By.Id("logout")).Click();
    Assert.IsTrue(driver.FindElement(By.Id("login_form")).Displayed);
}
```

**Решение:** Один метод `LoginAsUser()` — все тесты его используют. Код изменить один раз, везде сработает.

### DRY в Action:

```csharp
// ❌ Плохо: три разных функции validation
public bool ValidateEmail1(string email) { return email.Contains("@"); }
public bool ValidateEmail2(string email) { return email.Contains("@"); }
public bool ValidateEmail3(string email) { return email.Contains("@"); }

// ✅ Хорошо: одна функция, используется везде
public bool ValidateEmail(string email) { return email.Contains("@"); }
```

---

## 2. KISS (Keep It Simple, Stupid) — Делай просто

### Суть

**Если код сложно объяснить — значит, он писан неправильно.**

### Пример 1: Плохо ❌

```csharp
// Сложная цепочка с вложенными условиями
public bool IsUserAllowedToAccess(User user, Resource resource)
{
    return (user != null && user.IsActive) &&
           ((user.Role == "Admin") ||
            (user.Department == resource.Department && resource.IsPublic) ||
            (user.Id == resource.OwnerId));
}

// Что это проверяет? Кому разрешен доступ?
// Нужна минута, чтобы разобраться...
```

### Пример 2: Хорошо ✅

```csharp
// Простой и понятный код
public bool IsUserAllowedToAccess(User user, Resource resource)
{
    if (user == null || !user.IsActive)
        return false;

    // Админ всегда может
    if (user.Role == "Admin")
        return true;

    // Владелец может всегда
    if (user.Id == resource.OwnerId)
        return true;

    // Публичные ресурсы в своем отделе
    if (resource.IsPublic && user.Department == resource.Department)
        return true;

    return false;
}

// Ясно: 1) null? нет. 2) Админ? да. 3) Владелец? да. 4) Публичный в отделе? да. Иначе нет.
```

### KISS в Action:

```csharp
// ❌ Неправильно: одна большая функция на 200 строк
public void ProcessOrder(Order order) { /* все логика здесь */ }

// ✅ Правильно: маленькие понятные функции
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    CalculateTotal(order);
    ApplyDiscount(order);
    CreateInvoice(order);
}
```

### Правило большого пальца:

**Если функция делает более 1-2 вещей → разделите её!**

---

## 3. SOLID Принципы

SOLID — аббревиатура из 5 принципов проектирования.

---

## 3.1 S — Single Responsibility Principle (SRP)

### Суть

**Класс должен иметь ОДНУ причину для изменения.**

Каждый класс должен отвечать за ОДНО.

### Пример: Плохо ❌

```csharp
public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        // Проверка заказа
        if (order.Items.Count == 0)
            throw new Exception("Order is empty");

        // Расчет стоимости
        decimal total = order.Items.Sum(i => i.Price * i.Quantity);
        order.Total = total;

        // Сохранение в БД
        var connection = new SqlConnection("server=...");
        connection.Open();
        // SQL команды...
        connection.Close();

        // Отправка email
        var smtpClient = new SmtpClient();
        smtpClient.Send(new MailMessage("orders@company.com", order.CustomerEmail));
    }
}
```

**Проблема:** OrderProcessor делает ВСЁ: валидацию, расчеты, сохранение в БД, email. Если поменяется способ сохранения в БД, нужно менять этот класс? Нарушение SRP!

### Пример: Хорошо ✅

```csharp
// Каждый класс — одна ответственность

public class OrderValidator
{
    public void Validate(Order order)
    {
        if (order.Items.Count == 0)
            throw new Exception("Order is empty");
    }
}

public class PriceCalculator
{
    public decimal Calculate(Order order)
    {
        return order.Items.Sum(i => i.Price * i.Quantity);
    }
}

public class OrderRepository
{
    public void Save(Order order)
    {
        var connection = new SqlConnection("server=...");
        // сохранение в БД
    }
}

public class EmailNotifier
{
    public void SendOrderConfirmation(Order order)
    {
        var smtpClient = new SmtpClient();
        smtpClient.Send(new MailMessage("orders@company.com", order.CustomerEmail));
    }
}

// Теперь OrderProcessor простой: координирует работу
public class OrderProcessor
{
    private OrderValidator validator = new OrderValidator();
    private PriceCalculator calculator = new PriceCalculator();
    private OrderRepository repository = new OrderRepository();
    private EmailNotifier notifier = new EmailNotifier();

    public void ProcessOrder(Order order)
    {
        validator.Validate(order);
        order.Total = calculator.Calculate(order);
        repository.Save(order);
        notifier.SendOrderConfirmation(order);
    }
}
```

**Выигрыш:**

- OrderValidator отвечает за валидацию. Если логика валидации меняется — меняем только его.
- PriceCalculator отвечает за расчеты. Если формула расчета меняется — меняем только его.
- OrderRepository отвечает за сохранение. Если переходим на другую БД — меняем только его.

---

## 3.2 O — Open/Closed Principle (OCP)

### Суть

**Класс должен быть ОТКРЫТ для расширения, но ЗАКРЫТ для изменения.**

Когда нужна новая функция, добавляйте код, а не меняйте старый!

### Пример: Плохо ❌

```csharp
public class ReportGenerator
{
    public string Generate(string format)
    {
        if (format == "PDF")
        {
            return "PDF report...";
        }
        else if (format == "Excel")
        {
            return "Excel report...";
        }
        else if (format == "Word")
        {
            return "Word report...";
        }
    }
}

// Нужен новый формат JSON?
// Нужно менять класс ReportGenerator → ЗАКРЫТО для изменения!
```

### Пример: Хорошо ✅

```csharp
// Интерфейс — контракт
public interface IReportFormatter
{
    string Format(Report report);
}

// Реализации
public class PdfFormatter : IReportFormatter
{
    public string Format(Report report) => "PDF report...";
}

public class ExcelFormatter : IReportFormatter
{
    public string Format(Report report) => "Excel report...";
}

public class JsonFormatter : IReportFormatter  // ← Новый формат просто добавляем!
{
    public string Format(Report report) => "JSON report...";
}

// Генератор не меняется!
public class ReportGenerator
{
    public string Generate(IReportFormatter formatter, Report report)
    {
        return formatter.Format(report);
    }
}

// Использование
var generator = new ReportGenerator();
var pdfReport = generator.Generate(new PdfFormatter(), report);
var jsonReport = generator.Generate(new JsonFormatter(), report);  // ← Просто добавили!
```

**Выигрыш:**

- ReportGenerator **ОТКРЫТ** для расширения (добавляем новые форматы)
- ReportGenerator **ЗАКРЫТ** для изменения (сам класс не меняется)

---

## 3.3 L — Liskov Substitution Principle (LSP)

### Суть

**Подкласс должен корректно заменяться своим надклассом.**

Если B наследует A, то B должен работать везде, где ожидается A.

### Пример: Плохо ❌

```csharp
public abstract class Bird
{
    public abstract void Fly();
}

public class Sparrow : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Sparrow flies high");
    }
}

public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException("Penguins cannot fly!");  // ← НАРУШЕНИЕ LSP!
    }
}

// Код ломается
public void MakeBirdsFly(List<Bird> birds)
{
    foreach (var bird in birds)
    {
        bird.Fly();  // Пингвин? Исключение!
    }
}
```

### Пример: Хорошо ✅

```csharp
// Правильная иерархия
public abstract class Bird { }

public abstract class FlyingBird : Bird
{
    public abstract void Fly();
}

public abstract class NonFlyingBird : Bird
{
    public abstract void Swim();
}

public class Sparrow : FlyingBird
{
    public override void Fly() => Console.WriteLine("Sparrow flies");
}

public class Penguin : NonFlyingBird
{
    public override void Swim() => Console.WriteLine("Penguin swims");
}

// Теперь всё корректно!
public void MakeFlyingBirdsFly(List<FlyingBird> birds)
{
    foreach (var bird in birds)
    {
        bird.Fly();  // Все умеют летать
    }
}

public void MakeNonFlyingBirdsSwim(List<NonFlyingBird> birds)
{
    foreach (var bird in birds)
    {
        bird.Swim();  // Все умеют плавать
    }
}
```

---

## 3.4 I — Interface Segregation Principle (ISP)

### Суть

**Не заставляйте класс реализовывать методы, которые ему не нужны.**

Лучше много маленьких интерфейсов, чем один большой!

### Пример: Плохо ❌

```csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
    void GetPaid();
}

public class Robot : IWorker
{
    public void Work() => Console.WriteLine("Robot works");
    public void Eat() => throw new NotImplementedException();  // ← Robot не ест!
    public void Sleep() => throw new NotImplementedException(); // ← Robot не спит!
    public void GetPaid() => throw new NotImplementedException(); // ← Robot не получает зарплату!
}
```

### Пример: Хорошо ✅

```csharp
// Маленькие специализированные интерфейсы
public interface IWorkable { void Work(); }
public interface IEatable { void Eat(); }
public interface ISleepable { void Sleep(); }
public interface IPayable { void GetPaid(); }

public class Human : IWorkable, IEatable, ISleepable, IPayable
{
    public void Work() => Console.WriteLine("Human works");
    public void Eat() => Console.WriteLine("Human eats");
    public void Sleep() => Console.WriteLine("Human sleeps");
    public void GetPaid() => Console.WriteLine("Human gets paid");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot works");
    // Robot не реализует лишние интерфейсы!
}
```

**Выигрыш:** Robot не должен реализовывать методы, которые ему не нужны.

---

## 3.5 D — Dependency Inversion Principle (DIP)

### Суть

**Зависьте от абстракций, а не от конкретных классов.**

High-level modules не должны зависеть от low-level modules. Обе должны зависеть от абстракции.

### Пример: Плохо ❌

```csharp
public class EmailService
{
    public void SendEmail(string email, string message)
    {
        // Отправляем через SMTP
        Console.WriteLine($"Email sent to {email}");
    }
}

public class Notifier
{
    private EmailService emailService = new EmailService();  // ← ЖЕСТКАЯ зависимость!

    public void Notify(string email, string message)
    {
        emailService.SendEmail(email, message);  // ← Только EmailService!
    }
}

// Нужен SMS? Меняйте Notifier!
```

### Пример: Хорошо ✅

```csharp
// Абстракция
public interface INotificationService
{
    void Send(string recipient, string message);
}

// Реализации
public class EmailService : INotificationService
{
    public void Send(string email, string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsService : INotificationService
{
    public void Send(string phone, string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

// Зависит от абстракции!
public class Notifier
{
    private INotificationService notificationService;

    public Notifier(INotificationService service)
    {
        notificationService = service;
    }

    public void Notify(string recipient, string message)
    {
        notificationService.Send(recipient, message);
    }
}

// Использование
var emailNotifier = new Notifier(new EmailService());
var smsNotifier = new Notifier(new SmsService());  // ← Просто передали другую реализацию!
```

---

## 4. Практические примеры в QA

### Пример 1: Page Object Pattern (SRP + OCP + DIP)

```csharp
// ❌ Плохо: все в одном классе
public class LoginTest
{
    [Test]
    public void TestLogin()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://example.com");
        driver.FindElement(By.Id("username")).SendKeys("alice");
        driver.FindElement(By.Id("password")).SendKeys("pass123");
        driver.FindElement(By.Id("submit")).Click();
        // Проверка...
    }
}

// ✅ Хорошо: Page Object Pattern
public class LoginPage
{
    private IWebDriver driver;

    public LoginPage(IWebDriver driver) => this.driver = driver;

    public void Login(string username, string password)
    {
        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("submit")).Click();
    }
}

public class LoginTest
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://example.com");
    }

    [Test]
    public void TestLogin()
    {
        var page = new LoginPage(driver);
        page.Login("alice", "pass123");
        // Проверка...
    }
}
```

**SOLID в действии:**

- **S:** LoginPage отвечает за взаимодействие с элементами
- **O:** Легко добавить новые методы (Logout, ForgotPassword)
- **L:** Можем заменить Chrome на Firefox (если правильно спроектировано)
- **I:** IWebDriver интерфейс — маленький и специализированный
- **D:** Зависит от IWebDriver, а не от конкретного браузера

---

## 5. Когда применять эти принципы

| Принцип  | Когда применять                       | Когда НЕ применять                  |
| -------- | ------------------------------------- | ----------------------------------- |
| **DRY**  | **ВСЕГДА** — копировать плохо         | Когда код логически отличается      |
| **KISS** | **ВСЕГДА** — простой код = понятный   | Когда нужна оптимизация (редко!)    |
| **SRP**  | Классы с более чем 200 строк          | Для простых утилит (может быть 1-2) |
| **OCP**  | Когда планируете расширение           | Для одноразового кода               |
| **LSP**  | Иерархия наследования с полиморфизмом | Когда нет наследования              |
| **ISP**  | Интерфейсы с более чем 5 методами     | Для простых контрактов (2-3 метода) |
| **DIP**  | Контейнеры DI, модульная архитектура  | Простые скрипты, прототипы          |

---

## 6. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ 🎯

### Вопрос 1: Что такое DRY и зачем оно нужно?

**Краткий ответ:** DRY (Don't Repeat Yourself) — не копируй код. Если пишешь один и тот же код в третий раз, напиши функцию. Выигрыш: изменяешь один раз, везде работает.

### Вопрос 2: Чем отличается KISS от других принципов?

**Краткий ответ:** KISS (Keep It Simple, Stupid) — пиши простой код, который легко понять. Если функция делает более 2 вещей, разделяй. Если условие сложное, разбивай на части.

### Вопрос 3: Что такое Single Responsibility Principle?

**Краткий ответ:** SRP — класс должен иметь одну причину для изменения. Класс = одна ответственность. Если меняется логика сохранения в БД, не должна меняться логика валидации.

### Вопрос 4: Как применить Open/Closed Principle в Page Object?

**Краткий ответ:** Используйте интерфейсы (ILoginPage) и наследование. Базовый LoginPage — не меняется. Новый LoginPageV2 — добавляешь. Расширение без изменения старого кода.

### Вопрос 5: Что нарушает Liskov Substitution Principle?

**Краткий ответ:** Когда подкласс не может заменить надкласс. Пример: Bird → Penguin. Penguin.Fly() выбросит исключение. LSP нарушен. Решение: отдельные иерархии FlyingBird и NonFlyingBird.

### Вопрос 6: Interface Segregation vs Полнофункциональный интерфейс?

**Краткий ответ:** ISP — много маленьких интерфейсов лучше, чем один большой. Класс реализует только нужные. IWorkable, IEatable, ISleepable вместо одного IWorker.

### Вопрос 7: Как Dependency Inversion помогает тестированию?

**Краткий ответ:** Зависит от интерфейса, а не от реальной реализации. В тестах передаешь Mock реализацию. В production — реальную. Код не меняется.

### Вопрос 8: Практический пример: как рефакторить монолитный класс по SOLID?

**Краткий ответ:** 1) SRP — выдели классы по ответственности. 2) OCP — используй интерфейсы. 3) DIP — передавай зависимости через конструктор. 4) ISP — маленькие интерфейсы. 5) LSP — правильная иерархия.

### Вопрос 9: Когда НЕ применять SOLID (overengineering)?

**Краткий ответ:** Простые утилиты, прототипы, одноразовые скрипты — не усложняй. SOLID для production кода, архитектуры, долгоживущих проектов.

### Вопрос 10: DRY vs Copy-Paste в тестах — когда копировать допустимо?

**Краткий ответ:** Копируй ТОЛЬКО если логика действительно разная. Например, TestLogin и TestLoginFailed — похожи, но не одинаковы. Используй вспомогательные методы для одинаковых частей.

---

## Файлы в проекте:

- `Program.cs` — примеры нарушения и правильного применения SOLID
- Примеры Page Object Pattern с SOLID принципами

---

## Итоговая таблица

| Принцип  | Суть                                   | Проблема без него       |
| -------- | -------------------------------------- | ----------------------- |
| **DRY**  | Не повторяй код                        | Копипаста везде         |
| **KISS** | Пиши просто                            | Код как спагетти        |
| **SRP**  | Класс = одна ответственность           | Класс на 1000 строк     |
| **OCP**  | Открыт для расширения, закрыт для изм. | Меняешь старый код      |
| **LSP**  | Подкласс = надкласс                    | Исключения в подклассе  |
| **ISP**  | Маленькие интерфейсы                   | Классы не реализуют все |
| **DIP**  | Завись от абстракций                   | Жесткие связи           |

---

## Связь с другими темами:

- **Topic1 (Classes):** SRP — класс делает одно
- **Topic2 (Interfaces):** DIP и ISP — интерфейсы как контракты
- **Topic3 (Polymorphism):** OCP и LSP — наследование и подстановка
- **Topic6 (Generics):** DIP — обобщенные типы для слабой связи
