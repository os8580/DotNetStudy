namespace Topic2_Interfaces
{
    // 3. РЕАЛИЗАЦИЯ 2: API ЛОГИН (Имитация HttpClient)
    public class ApiLoginService : ILoginService
    {
        public bool IsLoggedIn { get; private set; }

        public void Login(string username, string password)
        {
            Console.WriteLine("[API Client] Формирую JSON тело запроса...");
            Console.WriteLine($"[API Client] Отправляю POST /api/v1/login с юзером '{username}'");
            Console.WriteLine("[API Client] Получен код 200 OK и Токен.");

            IsLoggedIn = true;
            Console.WriteLine("[API Client] Успешный вход через Backend (без браузера).");
        }
    }
}
