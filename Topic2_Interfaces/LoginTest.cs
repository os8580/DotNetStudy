namespace Topic2_Interfaces
{
    // 4. ТЕСТОВЫЙ КЛАСС
    public class LoginTest
    {
        // ВНИМАНИЕ: Тест зависит от ИНТЕРФЕЙСА, а не от конкретного класса.
        // Это называется "Слабая связность" (Loose Coupling).
        private ILoginService _loginService;

        // Мы принимаем любую реализацию: хоть UI, хоть API
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
