namespace Topic2_Interfaces
{
    /// <summary>
    /// ТЕСТОВЫЙ КЛАСС - демонстрирует инъекцию зависимостей
    /// 
    /// КЛЮЧЕВОЕ НАБЛЮДЕНИЕ:
    /// Этот класс НЕ знает про UiLoginService, ApiLoginService или MobileLoginService!
    /// Он знает только об интерфейсе ILoginService.
    /// 
    /// Это называется DEPENDENCY INJECTION (DI) или INVERSION OF CONTROL (IoC):
    /// - Класс не создает свои зависимости (new ...)
    /// - Зависимости "внедряются" через конструктор (передаются снаружи)
    /// 
    /// ПРЕИМУЩЕСТВА:
    /// 1. Легко тестировать - просто передайте фальшивую реализацию
    /// 2. Легко менять реализацию - передайте другой класс, реализующий интерфейс
    /// 3. Слабая связность - класс не привязан к конкретной реализации
    /// </summary>
    public class LoginTest
    {
        /// <summary>
        /// ВНИМАНИЕ: Зависит от ИНТЕРФЕЙСА, а не от конкретного класса!
        /// Это - главное правило DI и SOLID (Dependency Inversion Principle)
        /// </summary>
        private ILoginService _loginService;

        /// <summary>
        /// КОНСТРУКТОР С ИНЪЕКЦИЕЙ
        /// 
        /// Мы передаем любую реализацию ILoginService:
        /// - new UiLoginService() → работает
        /// - new ApiLoginService() → работает
        /// - new MobileLoginService() → работает
        /// - new FakeLoginService() → работает (для тестов!)
        /// 
        /// Класс LoginTest ОДИН ДЛЯ ВСЕХ!
        /// </summary>
        public LoginTest(ILoginService service)
        {
            _loginService = service;
        }

        public void Run()
        {
            Console.WriteLine("\n--- Запуск Теста ---");
            _loginService.Login("admin", "12345");

            if (_loginService.IsLoggedIn)
            {
                Console.WriteLine("Тест пройден: Пользователь авторизован.");
            }
        }
    }
}
