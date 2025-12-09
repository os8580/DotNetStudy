namespace Topic9_NullOperators
{
    // Имитация страницы
    public class LoginPage
    {
        public string Title = "Login page";

        // Метод может вернуть null, если элемент не найден
        public string GetErrorMessage()
        {
            return null; // Допустим, ошибки сейчас нет
        }
    }
}
