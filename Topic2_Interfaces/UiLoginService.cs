namespace Topic2_Interfaces
{
    /// <summary>
    /// РЕАЛИЗАЦИЯ 1: UI Login Service
    /// 
    /// Это реальная реализация ILoginService для работы с браузером
    /// (имитирует работу инструмента вроде Selenium)
    /// 
    /// Класс "говорит": "Я реализую интерфейс ILoginService,
    /// значит у меня ЕСТЬ метод Login() и свойство IsLoggedIn"
    /// </summary>
    public class UiLoginService : ILoginService
    {
        /// <summary>
        /// Флаг авторизации - ОБЯЗАТЕЛЕН, так как реализуем ILoginService
        /// private set означает, что менять его снаружи нельзя, только через метод Login()
        /// </summary>
        public bool IsLoggedIn { get; private set; }

        public void Login(string username, string password)
        {
            Console.WriteLine("[UI Driver] Открываю браузер...");
            Console.WriteLine($"[UI Driver] Ищу поле Username, ввожу '{username}'");
            Console.WriteLine($"[UI Driver] Ищу поле Password, ввожу '***'");
            Console.WriteLine("[UI Driver] Кликаю кнопку 'Войти'");

            IsLoggedIn = true;
            Console.WriteLine("[UI Driver] Успешный вход через интерфейс сайта.");
        }
    }
}
