using System;
using System.Collections.Generic;
using System.Linq;

namespace Topic12_Debugging
{
    /// <summary>
    /// Topic 12: Debugging - Отладка кода в Visual Studio Code / Visual Studio
    /// 
    /// Практические примеры для отладки с помощью breakpoints, stepping, variables inspection
    /// Все примеры - QA/тестирование ориентированные
    /// </summary>
    /// 
    // ============================================================================
    // ============ СЦЕНАРИЙ 1: Отладка логики логина ===========================
    // ============================================================================

    public class LoginDebugScenario
    {
        /// <summary>
        /// Используйте этот метод для отладки:
        /// 1. Поставьте breakpoint на строке с ParseCredentials
        /// 2. Step Over (F10) через каждую строку
        /// 3. Смотрите значения переменных в Watch окне
        /// 4. Когда credentials.IsValid = false, Step Into (F11) в IsCredentialsValid
        /// </summary>
        public bool TryLogin(string username, string password)
        {
            // BREAKPOINT ЗДЕСЬ - F9 чтобы включить/выключить
            var credentials = new Credentials { Username = username, Password = password };

            // Watch: посмотрите чему равны username и password
            bool isValid = IsCredentialsValid(credentials);

            // Условный breakpoint: остановиться только если isValid = false
            if (isValid)
            {
                Console.WriteLine($"✓ Login successful for user {username}");
                return true;
            }
            else
            {
                Console.WriteLine($"✗ Login failed for user {username}");
                return false;
            }
        }

        private bool IsCredentialsValid(Credentials credentials)
        {
            // ТОЧКА ДЛЯ STEP INTO (F11)
            bool usernameValid = !string.IsNullOrEmpty(credentials.Username);
            bool passwordValid = credentials.Password.Length >= 8;

            return usernameValid && passwordValid;
        }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsValid { get; set; }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 2: Отладка цикла и коллекций =======================
    // ============================================================================

    public class LoopDebugScenario
    {
        /// <summary>
        /// Сценарий для отладки цикла:
        /// 1. Поставьте breakpoint ВНУТРИ цикла (на Line 56)
        /// 2. Run (F5)
        /// 3. Inspect переменные i, item, totalPrice на каждой итерации
        /// 4. Используйте Debug Console (Ctrl+Shift+Y) для вычисления выражений
        /// 
        /// Попробуйте условный breakpoint: i > 2 (остановиться на итерации 3)
        /// </summary>
        public decimal CalculateOrderTotal(List<OrderItem> items)
        {
            decimal totalPrice = 0;
            int itemCount = 0;

            for (int i = 0; i < items.Count; i++)  // ← Breakpoint здесь
            {
                var item = items[i];
                decimal itemTotal = item.Price * item.Quantity;
                totalPrice += itemTotal;
                itemCount++;

                // BREAKPOINT в цикле - смотрите как меняются переменные
                Console.WriteLine($"Item {i}: {item.Name} = {itemTotal}");
            }

            return totalPrice;
        }
    }

    public class OrderItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 3: Отладка исключений (Exception Handling) =========
    // ============================================================================

    public class ExceptionDebugScenario
    {
        /// <summary>
        /// Сценарий для отладки исключений:
        /// 1. Поставьте breakpoint на Try блок
        /// 2. Когда исключение выбросится, Visual Studio остановит на нем
        /// 3. Посмотрите Stack Trace (Ctrl+Alt+T) - откуда пришло исключение
        /// 4. Смотрите Exception Details - что случилось
        /// 5. Step Out (Shift+F11) чтобы выйти из исключения
        /// </summary>
        public void ProcessUserData(List<User> users)
        {
            try
            {
                // BREAKPOINT здесь для отладки Exception
                foreach (var user in users)
                {
                    int age = int.Parse(user.AgeString);  // ← Может быть исключение ParseException!

                    if (age < 18)
                    {
                        throw new InvalidOperationException("User must be 18+");  // ← Может быть исключение!
                    }

                    Console.WriteLine($"User {user.Name} is {age} years old");
                }
            }
            catch (FormatException ex)
            {
                // BREAKPOINT здесь - когда Parse() вернет ошибку
                Console.WriteLine($"✗ Format error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Unexpected error: {ex.Message}");
            }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string AgeString { get; set; }  // "25", "invalid", "18"
        public int Age { get; set; }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 4: Отладка логики поиска элемента =================
    // ============================================================================

    public class WebElementDebugScenario
    {
        /// <summary>
        /// QA сценарий - отладка поиска элемента:
        /// 1. Breakpoint перед FindElement
        /// 2. Посмотрите что находится в WebElements коллекции (Expand в Watch)
        /// 3. Step Into FindElement чтобы увидеть логику поиска
        /// 4. Если Find вернул null - элемент не найден, посмотрите почему
        /// </summary>
        public WebElement FindElementByName(List<WebElement> elements, string elementName)
        {
            // Breakpoint здесь
            Console.WriteLine($"Searching for element: {elementName}");

            // Логирование для отладки - ответить на вопросы:
            // - Сколько элементов? (Watch: elements.Count)
            // - Какие имена? (Watch: elements.Select(e => e.Name))
            foreach (var element in elements)
            {
                Console.WriteLine($"  Found element: {element.Name} (visible: {element.IsVisible})");

                if (element.Name == elementName)  // Conditional breakpoint: element.Name != elementName
                {
                    if (!element.IsVisible)
                    {
                        Console.WriteLine($"  ✗ Element {elementName} found but NOT visible!");
                        return null;
                    }

                    Console.WriteLine($"  ✓ Element {elementName} found and visible");
                    return element;
                }
            }

            Console.WriteLine($"  ✗ Element {elementName} not found in list");
            return null;
        }
    }

    public class WebElement
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public string Xpath { get; set; }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 5: Отладка данных с помощью Logpoints =============
    // ============================================================================

    public class LogpointDebugScenario
    {
        /// <summary>
        /// Вместо breakpoint можно использовать Logpoint (логирование без остановки):
        /// 1. Right-click на номер строки (Line 173)
        /// 2. Add Logpoint
        /// 3. Введите: "User: {user.Name}, Age: {user.Age}"
        /// 4. Run программу - логирование будет в Debug Console
        /// 5. Преимущество: без остановки выполнения, быстрее отладка
        /// </summary>
        public void ProcessUsers(List<User> users)
        {
            foreach (var user in users)
            {
                // LOGPOINT ЗДЕСЬ вместо BREAKPOINT
                // Logpoint: "Processing user: {user.Name}"
                ProcessUser(user);
            }
        }

        private void ProcessUser(User user)
        {
            // LOGPOINT "User processed: {user.Name}"
            Console.WriteLine($"Processing user: {user.Name}");
        }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 6: Stack Trace - откуда пришла ошибка? =============
    // ============================================================================

    public class StackTraceDebugScenario
    {
        /// <summary>
        /// Stack Trace помогает найти, откуда пришла ошибка:
        /// 1. Когда исключение выбросится, посмотрите Debug > Windows > Exception Settings
        /// 2. Или посмотрите вывод в Debug Console
        /// 3. Stack Trace покажет: Main() → LoginWithRetry() → Login() → Parse() [ERROR]
        /// 4. Double-click на каждый уровень - прыгнет на строку кода
        /// </summary>
        public void Main()
        {
            try
            {
                LoginWithRetry("alice");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"Stack Trace:\n{ex.StackTrace}");
            }
        }

        private void LoginWithRetry(string username)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Login(username, "password123");
                    return;  // Успех!
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {i + 1} failed: {ex.Message}");
                }
            }
        }

        private void Login(string username, string password)
        {
            ValidatePassword(password);
            Console.WriteLine($"Login successful for {username}");
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters");
        }
    }

    // ============================================================================
    // ============ СЦЕНАРИЙ 7: Watch - инспектирование сложных объектов ========
    // ============================================================================

    public class WatchDebugScenario
    {
        /// <summary>
        /// Watch позволяет смотреть значения переменных во время отладки:
        /// 1. Breakpoint перед CalculateStats
        /// 2. Debug > Windows > Watch (или Ctrl+Shift+W)
        /// 3. Добавьте выражения:
        ///    - users.Count
        ///    - users[0].Name
        ///    - users.Where(u => u.Age > 18).Count()
        ///    - Math.Round(avgAge, 2)
        /// 4. Смотрите как меняются значения при Step Over
        /// </summary>
        public void CalculateStats(List<User> users)
        {
            // BREAKPOINT здесь
            int totalCount = users.Count;  // Watch: totalCount = ?
            int adultCount = users.Count(u => u.Age >= 18);  // Watch: adultCount = ?
            double avgAge = users.Average(u => u.Age);  // Watch: avgAge = ?

            // Step Over на каждой строке и смотрите Watch значения
            Console.WriteLine($"Total users: {totalCount}");
            Console.WriteLine($"Adults (18+): {adultCount}");
            Console.WriteLine($"Average age: {avgAge:F2}");

            double adultPercentage = (adultCount * 100.0) / totalCount;
            Console.WriteLine($"Adult percentage: {adultPercentage:F1}%");
        }
    }

    // ============================================================================
    // ============ COMMON BEGINNER MISTAKES - ТИПИЧНЫЕ ОШИБКИ ==================
    // ============================================================================

    public class CommonMistakes
    {
        /// <summary>
        /// Ошибка 1: NullReferenceException - null значение
        /// Как отладить:
        /// - Watch: проверьте что объект не null перед использованием
        /// - Conditional breakpoint: obj == null
        /// </summary>
        public void MistakeNullReference()
        {
            User user = null;  // ← Null значение!

            try
            {
                // Breakpoint здесь
                string name = user.Name;  // ← NullReferenceException!
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("✗ Mistake 1: NullReferenceException - проверьте что объект не null");
            }
        }

        /// <summary>
        /// Ошибка 2: Off-by-one error в цикле (забыли -1 индекс)
        /// Как отладить:
        /// - Breakpoint в цикле
        /// - Watch: i, list.Count
        /// - Видно когда i = list.Count, это ошибка
        /// </summary>
        public void MistakeOffByOne()
        {
            var items = new List<string> { "A", "B", "C" };  // 3 элемента (индексы 0,1,2)

            try
            {
                for (int i = 0; i <= items.Count; i++)  // ← Ошибка: должно быть i < items.Count
                {
                    // Breakpoint здесь
                    Console.WriteLine(items[i]);  // ← ArgumentOutOfRangeException когда i=3!
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("✗ Mistake 2: Off-by-one error - индекс выходит за границы");
            }
        }

        /// <summary>
        /// Ошибка 3: Условие никогда не выполняется (логическая ошибка)
        /// Как отладить:
        /// - Breakpoint на if
        /// - Watch: выражение условия
        /// - Step Over и смотрите берется ли if или else
        /// </summary>
        public void MistakeLogicError()
        {
            int count = 5;

            // Breakpoint здесь
            if (count > 10)  // ← Условие НИКОГДА не true (count=5)
            {
                Console.WriteLine("Count > 10");  // ← Этот блок никогда не выполняется!
            }
            else
            {
                Console.WriteLine("Count <= 10");  // ← Выполняется этот
            }

            Console.WriteLine("✗ Mistake 3: Логическая ошибка - условие неправильно");
        }
    }

    // ============================================================================
    // ============ MAIN ПРОГРАММА ==============================================
    // ============================================================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           Topic 12: Debugging - Практические примеры          ║");
            Console.WriteLine("║        для отладки кода в VS Code / Visual Studio            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

            Console.WriteLine("ИНСТРУКЦИИ ПО ОТЛАДКЕ:\n");
            Console.WriteLine("1. Запустите эту программу с F5 (Debug mode)");
            Console.WriteLine("2. Поставьте Breakpoint на интересующую строку: F9");
            Console.WriteLine("3. Шагайте по коду: F10 (Step Over), F11 (Step Into), Shift+F11 (Step Out)");
            Console.WriteLine("4. Смотрите переменные: Ctrl+Shift+W (Watch), или наведите мышь\n");
            Console.WriteLine(new string('─', 70) + "\n");

            // Сценарий 1: Логин
            Console.WriteLine("► Сценарий 1: Отладка логики логина");
            var loginScenario = new LoginDebugScenario();
            loginScenario.TryLogin("alice", "pass");  // ← Breakpoint здесь!
            loginScenario.TryLogin("bob", "password123");
            Console.WriteLine();

            // Сценарий 2: Цикл
            Console.WriteLine("► Сценарий 2: Отладка цикла");
            var loopScenario = new LoopDebugScenario();
            var items = new List<OrderItem>
            {
                new OrderItem { Name = "Laptop", Price = 1000, Quantity = 1 },
                new OrderItem { Name = "Mouse", Price = 50, Quantity = 2 },
                new OrderItem { Name = "Monitor", Price = 300, Quantity = 1 }
            };
            decimal total = loopScenario.CalculateOrderTotal(items);
            Console.WriteLine($"Total: ${total}\n");

            // Сценарий 3: Исключение
            Console.WriteLine("► Сценарий 3: Отладка исключений");
            var exceptionScenario = new ExceptionDebugScenario();
            var users = new List<User>
            {
                new User { Name = "Alice", AgeString = "25" },
                new User { Name = "Bob", AgeString = "invalid" },  // ← ParseException!
                new User { Name = "Charlie", AgeString = "15" }    // ← InvalidOperationException!
            };
            exceptionScenario.ProcessUserData(users);
            Console.WriteLine();

            // Сценарий 4: Поиск элемента
            Console.WriteLine("► Сценарий 4: Отладка поиска элемента (QA)");
            var elementScenario = new WebElementDebugScenario();
            var elements = new List<WebElement>
            {
                new WebElement { Name = "Login Button", IsVisible = true, Xpath = "//*[@id='login']" },
                new WebElement { Name = "Username Field", IsVisible = true, Xpath = "//*[@name='user']" },
                new WebElement { Name = "Password Field", IsVisible = false, Xpath = "//*[@name='pass']" }
            };
            var found = elementScenario.FindElementByName(elements, "Login Button");
            Console.WriteLine();

            // Сценарий 5: Logpoint
            Console.WriteLine("► Сценарий 5: Logpoint (логирование без остановки)");
            var logpointScenario = new LogpointDebugScenario();
            var testUsers = new List<User>
            {
                new User { Name = "Alice", AgeString = "25" },
                new User { Name = "Bob", AgeString = "30" }
            };
            logpointScenario.ProcessUsers(testUsers);
            Console.WriteLine();

            // Сценарий 6: Stack Trace
            Console.WriteLine("► Сценарий 6: Stack Trace (откуда пришла ошибка)");
            var stackScenario = new StackTraceDebugScenario();
            stackScenario.Main();
            Console.WriteLine();

            // Сценарий 7: Watch
            Console.WriteLine("► Сценарий 7: Watch инспектирование");
            var watchScenario = new WatchDebugScenario();
            watchScenario.CalculateStats(testUsers);
            Console.WriteLine();

            // Типичные ошибки
            Console.WriteLine("► Типичные ошибки (Common Mistakes):");
            var mistakes = new CommonMistakes();
            mistakes.MistakeNullReference();
            mistakes.MistakeOffByOne();
            mistakes.MistakeLogicError();
            Console.WriteLine();

            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   ✓ Все сценарии отладки продемонстрированы!                ║");
            Console.WriteLine("║   Теперь попробуйте отладить этот код сами:                  ║");
            Console.WriteLine("║   1. Поставьте Breakpoint (F9) на интересующую строку       ║");
            Console.WriteLine("║   2. Запустите F5 (Debug)                                    ║");
            Console.WriteLine("║   3. Используйте F10/F11 для шагания по коду                ║");
            Console.WriteLine("║   4. Смотрите переменные в Watch (Ctrl+Shift+W)             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }
    }
}
