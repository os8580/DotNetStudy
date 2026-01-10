using System;
using System.Collections.Generic;
using System.Linq;

namespace Topic13_Principles
{
    /// <summary>
    /// Topic 13: SOLID, KISS, DRY - Design Principles
    /// 
    /// Демонстрирует нарушение и правильное применение принципов проектирования.
    /// Все примеры - QA/тестирование ориентированные.
    /// </summary>
    /// 
    // ============================================================================
    // ============ DRY (Don't Repeat Yourself) ПРИМЕРЫ =======================
    // ============================================================================

    public class DryExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Повторение кода логина в каждом тесте
        /// </summary>
        public class BadDryTestSuite
        {
            public void TestLoginWithValidCredentials()
            {
                // Код логина - скопирован
                var username = "alice";
                var password = "password123";
                Console.WriteLine($"Logging in with {username}");
                // ... логика логина...
            }

            public void TestLogout()
            {
                // Этот же код логина скопирован!
                var username = "alice";
                var password = "password123";
                Console.WriteLine($"Logging in with {username}");
                // ... логика логина...

                // ... логика логаута...
            }

            public void TestUserProfile()
            {
                // И еще раз скопирован код логина!
                var username = "alice";
                var password = "password123";
                Console.WriteLine($"Logging in with {username}");
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Один метод для логина, переиспользуется везде
        /// </summary>
        public class GoodDryTestSuite
        {
            private void LoginAsUser(string username, string password)
            {
                Console.WriteLine($"Logging in with {username}");
                // ... логика логина...
            }

            public void TestLoginWithValidCredentials()
            {
                LoginAsUser("alice", "password123");
                Console.WriteLine("✓ Login successful");
            }

            public void TestLogout()
            {
                LoginAsUser("alice", "password123");
                Console.WriteLine("✓ Logout successful");
            }

            public void TestUserProfile()
            {
                LoginAsUser("alice", "password123");
                Console.WriteLine("✓ Profile displayed");
            }
        }
    }

    // ============================================================================
    // ============ KISS (Keep It Simple, Stupid) ПРИМЕРЫ =======================
    // ============================================================================

    public class KissExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Сложная логика в одной функции
        /// </summary>
        public class BadKissAccessControl
        {
            public bool IsUserAllowedToAccess(User user, Resource resource)
            {
                // Очень сложное условие, трудно понять!
                return (user != null && user.IsActive) &&
                       ((user.Role == "Admin") ||
                        (user.Department == resource.Department && resource.IsPublic) ||
                        (user.Id == resource.OwnerId && resource.HasRestrictedAccess == false) ||
                        (user.SuperRole == "SuperAdmin"));
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Простая логика, легко понять, легко менять
        /// </summary>
        public class GoodKissAccessControl
        {
            public bool IsUserAllowedToAccess(User user, Resource resource)
            {
                // Проверка 1: пользователь существует и активен
                if (user == null || !user.IsActive)
                    return false;

                // Проверка 2: админ имеет полный доступ
                if (user.Role == "Admin" || user.SuperRole == "SuperAdmin")
                    return true;

                // Проверка 3: владелец имеет доступ
                if (user.Id == resource.OwnerId)
                    return true;

                // Проверка 4: публичные ресурсы доступны в отделе
                if (resource.IsPublic && user.Department == resource.Department)
                    return true;

                // Остальное - нет доступа
                return false;
            }
        }

        /// <summary>
        /// ❌ ПЛОХО: Большая функция, делает много вещей
        /// </summary>
        public void BadProcessOrder(Order order)
        {
            // 1. Валидация
            if (order.Items.Count == 0)
                throw new Exception("Order is empty");

            // 2. Расчет стоимости
            decimal total = order.Items.Sum(i => i.Price * i.Quantity);
            order.Total = total;

            // 3. Применение скидки
            if (order.Customer.IsVip)
                order.Total *= 0.9m;  // 10% скидка для VIP

            // 4. Сохранение в БД
            Console.WriteLine("Saving to database...");

            // 5. Отправка email
            Console.WriteLine($"Sending confirmation email to {order.Customer.Email}");

            // 6. Обновление инвентаря
            foreach (var item in order.Items)
            {
                Console.WriteLine($"Decreasing inventory for {item.ProductId}");
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Каждая функция - одна вещь!
        /// </summary>
        private void ValidateOrder(Order order)
        {
            if (order.Items.Count == 0)
                throw new Exception("Order is empty");
        }

        private decimal CalculateTotal(Order order)
        {
            return order.Items.Sum(i => i.Price * i.Quantity);
        }

        private decimal ApplyDiscount(Order order, decimal total)
        {
            return order.Customer.IsVip ? total * 0.9m : total;
        }

        private void SaveOrder(Order order)
        {
            Console.WriteLine("Saving to database...");
        }

        private void NotifyCustomer(Order order)
        {
            Console.WriteLine($"Sending confirmation email to {order.Customer.Email}");
        }

        private void UpdateInventory(Order order)
        {
            foreach (var item in order.Items)
                Console.WriteLine($"Decreasing inventory for {item.ProductId}");
        }

        public void GoodProcessOrder(Order order)
        {
            ValidateOrder(order);
            var total = CalculateTotal(order);
            order.Total = ApplyDiscount(order, total);
            SaveOrder(order);
            NotifyCustomer(order);
            UpdateInventory(order);
        }
    }

    // ============================================================================
    // ============ S - Single Responsibility Principle ПРИМЕРЫ ==================
    // ============================================================================

    public class SrpExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Один класс делает ВСЕ
        /// </summary>
        public class BadUserManager
        {
            public void CreateUser(string username, string email, string password)
            {
                // 1. Валидация
                if (string.IsNullOrEmpty(username))
                    throw new Exception("Username is empty");

                // 2. Расчеты
                var hashedPassword = "HASHED_" + password;

                // 3. Сохранение в БД
                Console.WriteLine($"INSERT INTO Users VALUES ('{username}', '{email}', '{hashedPassword}')");

                // 4. Отправка email
                Console.WriteLine($"Sending welcome email to {email}");

                // 5. Логирование
                Console.WriteLine($"[LOG] User {username} created");
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Каждый класс - одна ответственность
        /// </summary>
        /// 
        public interface IUserValidator
        {
            void Validate(string username, string email);
        }

        public class UserValidator : IUserValidator
        {
            public void Validate(string username, string email)
            {
                if (string.IsNullOrEmpty(username))
                    throw new Exception("Username is empty");
                if (!email.Contains("@"))
                    throw new Exception("Invalid email");
            }
        }

        public interface IUserRepository
        {
            void Save(User user);
        }

        public class UserRepository : IUserRepository
        {
            public void Save(User user)
            {
                Console.WriteLine($"INSERT INTO Users VALUES ('{user.Username}', '{user.Email}')");
            }
        }

        public interface IPasswordHasher
        {
            string Hash(string password);
        }

        public class PasswordHasher : IPasswordHasher
        {
            public string Hash(string password) => "HASHED_" + password;
        }

        public interface IEmailService
        {
            void SendWelcomeEmail(string email);
        }

        public class EmailService : IEmailService
        {
            public void SendWelcomeEmail(string email)
            {
                Console.WriteLine($"Sending welcome email to {email}");
            }
        }

        public interface ILogger
        {
            void Log(string message);
        }

        public class ConsoleLogger : ILogger
        {
            public void Log(string message)
            {
                Console.WriteLine($"[LOG] {message}");
            }
        }

        public class GoodUserManager
        {
            private IUserValidator validator;
            private IUserRepository repository;
            private IPasswordHasher hasher;
            private IEmailService emailService;
            private ILogger logger;

            public GoodUserManager(
                IUserValidator validator,
                IUserRepository repository,
                IPasswordHasher hasher,
                IEmailService emailService,
                ILogger logger)
            {
                this.validator = validator;
                this.repository = repository;
                this.hasher = hasher;
                this.emailService = emailService;
                this.logger = logger;
            }

            public void CreateUser(string username, string email, string password)
            {
                validator.Validate(username, email);

                var hashedPassword = hasher.Hash(password);
                var user = new User { Username = username, Email = email, Password = hashedPassword };

                repository.Save(user);
                emailService.SendWelcomeEmail(email);
                logger.Log($"User {username} created");
            }
        }
    }

    // ============================================================================
    // ============ O - Open/Closed Principle ПРИМЕРЫ ============================
    // ============================================================================

    public class OcpExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Закрыт для расширения (нужно менять класс для новых форматов)
        /// </summary>
        public class BadReportGenerator
        {
            public string Generate(string format)
            {
                if (format == "PDF")
                    return "PDF report content";
                else if (format == "Excel")
                    return "Excel report content";
                else if (format == "Word")
                    return "Word report content";
                else
                    throw new Exception($"Unknown format: {format}");

                // Новый формат JSON? Нужно менять класс!
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Открыт для расширения, закрыт для изменения
        /// </summary>
        /// 
        public interface IReportFormatter
        {
            string Format(string data);
        }

        public class PdfFormatter : IReportFormatter
        {
            public string Format(string data) => $"PDF: {data}";
        }

        public class ExcelFormatter : IReportFormatter
        {
            public string Format(string data) => $"Excel: {data}";
        }

        public class WordFormatter : IReportFormatter
        {
            public string Format(string data) => $"Word: {data}";
        }

        // ← Легко добавить новый без изменения старого!
        public class JsonFormatter : IReportFormatter
        {
            public string Format(string data) => $"{{\"report\": \"{data}\"}}";
        }

        public class GoodReportGenerator
        {
            private Dictionary<string, IReportFormatter> formatters;

            public GoodReportGenerator()
            {
                formatters = new Dictionary<string, IReportFormatter>
                {
                    { "PDF", new PdfFormatter() },
                    { "Excel", new ExcelFormatter() },
                    { "Word", new WordFormatter() },
                    { "JSON", new JsonFormatter() }
                };
            }

            public string Generate(string format, string data)
            {
                if (!formatters.TryGetValue(format, out var formatter))
                    throw new Exception($"Unknown format: {format}");

                return formatter.Format(data);
            }
        }
    }

    // ============================================================================
    // ============ L - Liskov Substitution Principle ПРИМЕРЫ ====================
    // ============================================================================

    public class LspExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Penguin нарушает контракт Bird.Fly()
        /// </summary>
        public abstract class BadBird
        {
            public abstract void Fly();
        }

        public class Sparrow : BadBird
        {
            public override void Fly()
            {
                Console.WriteLine("Sparrow flies high");
            }
        }

        public class BadPenguin : BadBird
        {
            public override void Fly()
            {
                throw new NotImplementedException("Penguins cannot fly!");  // ← НАРУШЕНИЕ LSP!
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Правильная иерархия, каждый делает то, что может
        /// </summary>
        /// 
        public abstract class Bird { }

        public abstract class FlyingBird : Bird
        {
            public abstract void Fly();
        }

        public abstract class NonFlyingBird : Bird
        {
            public abstract void Swim();
        }

        public class GoodSparrow : FlyingBird
        {
            public override void Fly()
            {
                Console.WriteLine("Sparrow flies high");
            }
        }

        public class GoodPenguin : NonFlyingBird
        {
            public override void Swim()
            {
                Console.WriteLine("Penguin swims fast");
            }
        }

        public void MakeFlyingBirdsFly(List<FlyingBird> birds)
        {
            foreach (var bird in birds)
            {
                bird.Fly();  // Все умеют летать - безопасно!
            }
        }
    }

    // ============================================================================
    // ============ I - Interface Segregation Principle ПРИМЕРЫ ==================
    // ============================================================================

    public class IspExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Большой интерфейс, Robot не может реализовать все методы
        /// </summary>
        public interface IBadWorker
        {
            void Work();
            void Eat();
            void Sleep();
            void GetPaid();
        }

        public class Robot : IBadWorker  // ← Нарушение ISP
        {
            public void Work() => Console.WriteLine("Robot works");
            public void Eat() => throw new NotImplementedException();
            public void Sleep() => throw new NotImplementedException();
            public void GetPaid() => throw new NotImplementedException();
        }

        /// <summary>
        /// ✅ ХОРОШО: Маленькие специализированные интерфейсы
        /// </summary>
        /// 
        public interface IWorkable
        {
            void Work();
        }

        public interface IEatable
        {
            void Eat();
        }

        public interface ISleepable
        {
            void Sleep();
        }

        public interface IPayable
        {
            void GetPaid();
        }

        public class Human : IWorkable, IEatable, ISleepable, IPayable
        {
            public void Work() => Console.WriteLine("Human works");
            public void Eat() => Console.WriteLine("Human eats");
            public void Sleep() => Console.WriteLine("Human sleeps");
            public void GetPaid() => Console.WriteLine("Human gets paid");
        }

        public class GoodRobot : IWorkable  // ← Robot реализует ТОЛЬКО то, что нужно!
        {
            public void Work() => Console.WriteLine("Robot works");
        }
    }

    // ============================================================================
    // ============ D - Dependency Inversion Principle ПРИМЕРЫ ===================
    // ============================================================================

    public class DipExample
    {
        /// <summary>
        /// ❌ ПЛОХО: Жесткая зависимость от конкретного класса
        /// </summary>
        public class EmailService
        {
            public void Send(string email, string message)
            {
                Console.WriteLine($"Email sent to {email}: {message}");
            }
        }

        public class BadNotifier
        {
            private EmailService emailService = new EmailService();

            public void Notify(string recipient, string message)
            {
                emailService.Send(recipient, message);
                // Нужен SMS? Менять класс!
            }
        }

        /// <summary>
        /// ✅ ХОРОШО: Зависит от интерфейса, а не от реализации
        /// </summary>
        /// 
        public interface INotificationService
        {
            void Send(string recipient, string message);
        }

        public class GoodEmailService : INotificationService
        {
            public void Send(string recipient, string message)
            {
                Console.WriteLine($"Email sent to {recipient}: {message}");
            }
        }

        public class SmsService : INotificationService
        {
            public void Send(string recipient, string message)
            {
                Console.WriteLine($"SMS sent to {recipient}: {message}");
            }
        }

        public class GoodNotifier
        {
            private INotificationService notificationService;

            public GoodNotifier(INotificationService service)
            {
                notificationService = service;
            }

            public void Notify(string recipient, string message)
            {
                notificationService.Send(recipient, message);
                // Легко добавить новый сервис без изменения Notifier!
            }
        }
    }

    // ============================================================================
    // ============ PAGE OBJECT PATTERN - ПРАКТИЧЕСКИЙ ПРИМЕР ====================
    // ============================================================================

    /// <summary>
    /// Правильный Page Object Pattern - применяет ВСЕ SOLID принципы!
    /// </summary>
    /// 
    public interface ILoginPage
    {
        void Login(string username, string password);
        bool IsLoginFormDisplayed();
    }

    public interface IBasePage
    {
        void WaitForPageLoad();
    }

    public class LoginPage : ILoginPage, IBasePage
    {
        private string usernameSelector = "#username";
        private string passwordSelector = "#password";
        private string submitSelector = "#submit";

        public void Login(string username, string password)
        {
            Console.WriteLine($"Entering username: {username}");
            Console.WriteLine($"Entering password: {password}");
            Console.WriteLine("Clicking submit button");
        }

        public bool IsLoginFormDisplayed()
        {
            Console.WriteLine("Checking if login form is displayed");
            return true;
        }

        public void WaitForPageLoad()
        {
            Console.WriteLine("Waiting for page to load...");
        }
    }

    public class LoginTest
    {
        private ILoginPage loginPage;

        public LoginTest(ILoginPage page)
        {
            loginPage = page;
        }

        public void TestValidLogin()
        {
            // SRP: LoginPage отвечает за взаимодействие с элементами
            // DIP: Зависим от интерфейса ILoginPage
            // ISP: Интерфейс имеет только нужные методы
            loginPage.Login("alice", "password123");
            Console.WriteLine("✓ Login test passed");
        }
    }

    // ============================================================================
    // ============ MAIN ПРОГРАММА - ДЕМОНСТРАЦИЯ ===============================
    // ============================================================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║    SOLID, KISS, DRY - Design Principles for QA Automation     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

            // DRY Пример
            Console.WriteLine("► DRY (Don't Repeat Yourself):");
            Console.WriteLine("  ❌ BadDryTestSuite: Код логина повторяется в каждом тесте");
            Console.WriteLine("  ✅ GoodDryTestSuite: Один метод LoginAsUser(), переиспользуется везде\n");

            var goodDry = new DryExample.GoodDryTestSuite();
            goodDry.TestLoginWithValidCredentials();
            goodDry.TestLogout();
            goodDry.TestUserProfile();

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // KISS Пример
            Console.WriteLine("► KISS (Keep It Simple, Stupid):");
            Console.WriteLine("  ❌ BadKissAccessControl: Сложное условие в одной строке");
            Console.WriteLine("  ✅ GoodKissAccessControl: Простые проверки, легко понять\n");

            var kiss = new KissExample();
            kiss.GoodProcessOrder(new Order
            {
                Items = new List<OrderItem> { new OrderItem { Price = 100, Quantity = 2, ProductId = 1 } },
                Customer = new Customer { Email = "test@test.com", IsVip = false }
            });

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // SRP Пример
            Console.WriteLine("► S - Single Responsibility Principle:");
            Console.WriteLine("  ❌ BadUserManager: Один класс делает ВСЕ");
            Console.WriteLine("  ✅ GoodUserManager: Каждый класс отвечает за одно\n");

            var goodUserManager = new SrpExample.GoodUserManager(
                new SrpExample.UserValidator(),
                new SrpExample.UserRepository(),
                new SrpExample.PasswordHasher(),
                new SrpExample.EmailService(),
                new SrpExample.ConsoleLogger()
            );
            goodUserManager.CreateUser("alice", "alice@test.com", "securepass123");

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // OCP Пример
            Console.WriteLine("► O - Open/Closed Principle:");
            Console.WriteLine("  ❌ BadReportGenerator: Нужно менять для новых форматов");
            Console.WriteLine("  ✅ GoodReportGenerator: Открыта для расширения\n");

            var generator = new OcpExample.GoodReportGenerator();
            Console.WriteLine(generator.Generate("PDF", "Test Report"));
            Console.WriteLine(generator.Generate("JSON", "Test Report"));

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // LSP Пример
            Console.WriteLine("► L - Liskov Substitution Principle:");
            Console.WriteLine("  ❌ BadPenguin: Penguin.Fly() выбросит исключение");
            Console.WriteLine("  ✅ GoodPenguin: Правильная иерархия\n");

            var flyingBirds = new List<LspExample.FlyingBird>
            {
                new LspExample.GoodSparrow(),
                new LspExample.GoodSparrow()
            };
            var lsp = new LspExample();
            lsp.MakeFlyingBirdsFly(flyingBirds);

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // ISP Пример
            Console.WriteLine("► I - Interface Segregation Principle:");
            Console.WriteLine("  ❌ Robot: Должен реализовать IBadWorker.Eat(), Sleep(), GetPaid()");
            Console.WriteLine("  ✅ GoodRobot: Реализует ТОЛЬКО IWorkable\n");

            IspExample.GoodRobot robot = new();
            robot.Work();

            IspExample.Human human = new();
            human.Work();
            human.Eat();

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // DIP Пример
            Console.WriteLine("► D - Dependency Inversion Principle:");
            Console.WriteLine("  ❌ BadNotifier: Зависит от EmailService напрямую");
            Console.WriteLine("  ✅ GoodNotifier: Зависит от интерфейса INotificationService\n");

            // Легко переключаться между реализациями!
            DipExample.INotificationService emailService = new DipExample.GoodEmailService();
            var notifier1 = new DipExample.GoodNotifier(emailService);
            notifier1.Notify("user@test.com", "Hello!");

            DipExample.INotificationService smsService = new DipExample.SmsService();
            var notifier2 = new DipExample.GoodNotifier(smsService);
            notifier2.Notify("+1234567890", "Hello!");

            Console.WriteLine("\n" + new string('─', 70) + "\n");

            // PAGE OBJECT PATTERN Пример
            Console.WriteLine("► PAGE OBJECT PATTERN - Практический SOLID пример:\n");

            ILoginPage loginPage = new LoginPage();
            var test = new LoginTest(loginPage);
            test.TestValidLogin();

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ✓ Все принципы SOLID, KISS, DRY продемонстрированы!          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }
    }

    // ============================================================================
    // ============ ВСПОМОГАТЕЛЬНЫЕ КЛАССЫ ======================================
    // ============================================================================

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        public string SuperRole { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public string Department { get; set; } = "IT";
    }

    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Department { get; set; }
        public bool HasRestrictedAccess { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVip { get; set; }
    }

    public interface INotificationService
    {
        void Send(string recipient, string message);
    }
}
