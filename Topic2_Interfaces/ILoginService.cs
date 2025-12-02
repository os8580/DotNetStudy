namespace Topic2_Interfaces
{
    // 1. ОПРЕДЕЛЯЕМ ИНТЕРФЕЙС (КОНТРАКТ)
    // Любой, кто хочет называться "Сервис Логина", ОБЯЗАН уметь делать Login.
    public interface ILoginService
    {
        // Только сигнатура. Никакого тела { ... }
        void Login(string username, string password);

        //Свойство тоже может быть частью контракта
        bool IsLoggedIn { get; }
    }
}
