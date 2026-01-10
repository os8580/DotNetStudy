namespace Topic2_Interfaces
{
    /// <summary>
    /// РЕАЛИЗАЦИЯ 2: API Login Service
    /// 
    /// Это альтернативная реализация ILoginService для работы с API
    /// (имитирует работу HttpClient для отправки запросов на сервер)
    /// 
    /// ВАЖНО: Код LoginTest работает одинаково с UiLoginService и ApiLoginService!
    /// Это и есть мощь интерфейсов - один код для разных реализаций.
    /// </summary>
    public class ApiLoginService : ILoginService
    {
        /// <summary>
        /// Флаг авторизации - ОБЯЗАТЕЛЕН, так как реализуем ILoginService
        /// </summary>
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
