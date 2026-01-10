namespace Topic2_Interfaces
{
    /// <summary>
    /// ИНТЕРФЕЙС - это КОНТРАКТ (правила, которые должны соблюдать реализации)
    /// 
    /// Интерфейс говорит: "Если ты хочешь называться ILoginService,
    /// ты ОБЯЗАН реализовать этот метод и это свойство".
    /// 
    /// Интерфейс НЕ содержит реализацию (нет { } внутри),
    /// только сигнатуры методов и свойств.
    /// 
    /// Как использовать:
    /// public class ApiLoginService : ILoginService { ... }
    /// public class UiLoginService : ILoginService { ... }
    /// 
    /// После этого можно использовать:
    /// ILoginService service = new ApiLoginService();  // Работает!
    /// ILoginService service = new UiLoginService();   // Тоже работает!
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Метод логина - ОБЯЗАН быть реализован всеми реализациями
        /// </summary>
        void Login(string username, string password);

        /// <summary>
        /// Свойство - флаг авторизации
        /// Имеет только getter (get). Setter может быть в реализации.
        /// </summary>
        bool IsLoggedIn { get; }
    }
}
