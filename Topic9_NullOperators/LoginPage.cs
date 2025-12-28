namespace Topic9_NullOperators
{
    // Вспомогательные классы
    public class Button
    {
        public string? Text { get; set; }

        public void Click()
        {
            Console.WriteLine($"Clicked button: {Text ?? "Unknown"}");
        }
    }

    public class TextField
    {
        public void Type(string text)
        {
            Console.WriteLine($"Typed: {text}");
        }
    }

    // Имитация страницы
    public class LoginPage
    {
        public Button? LoginButton { get; set; }
        public TextField? UsernameField { get; set; }
        public string? BaseUrl { get; set; }

        public string? Title { get; set; }

        // Метод может вернуть null, если элемент не найден
        public string? GetErrorMessage()
        {
            return null; // Допустим, ошибки сейчас нет
        }
    }
}
