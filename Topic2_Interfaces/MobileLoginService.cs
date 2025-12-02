namespace Topic2_Interfaces
{
    public class MobileLoginService : ILoginService
    {
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
