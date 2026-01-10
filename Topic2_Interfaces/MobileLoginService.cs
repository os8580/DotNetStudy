namespace Topic2_Interfaces
{
    /// <summary>
    /// РЕАЛИЗАЦИЯ 3: Mobile Login Service
    /// 
    /// Третья реализация ILoginService для мобильных приложений
    /// 
    /// Это демонстрирует главную силу интерфейсов:
    /// - У нас есть 3 РАЗНЫХ реализации (UI, API, Mobile)
    /// - Все они реализуют ОДИН интерфейс (ILoginService)
    /// - LoginTest работает с ЛЮБОЙ из них без изменения кода!
    /// 
    /// Это называется ПОЛИМОРФИЗМ - один интерфейс, много реализаций
    /// </summary>
    public class MobileLoginService : ILoginService
    {
        /// <summary>
        /// Флаг авторизации - ОБЯЗАТЕЛЕН, так как реализуем ILoginService
        /// </summary>
        public bool IsLoggedIn { get; private set; }

        public void Login(string username, string password)
        {
            Console.WriteLine("[Mobile Driver] Открываю браузер...");
            Console.WriteLine($"[Mobile Driver] Ищу поле Username, ввожу '{username}'");
            Console.WriteLine($"[Mobile Driver] Ищу поле Password, ввожу '***'");
            Console.WriteLine("[Mobile Driver] Кликаю кнопку 'Войти'");

            IsLoggedIn = true;
            Console.WriteLine("[Mobile Driver] Успешный вход через интерфейс сайта.");
        }
    }
}
