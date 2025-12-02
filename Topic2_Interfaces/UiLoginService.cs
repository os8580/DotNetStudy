namespace Topic2_Interfaces
{
    // 2. РЕАЛИЗАЦИЯ 1: UI ЛОГИН (Имитация Selenium)
    public class UiLoginService : ILoginService
    {
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
