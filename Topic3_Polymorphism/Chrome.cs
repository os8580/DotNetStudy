namespace Topic3_Polymorphism
{
    /// <summary>
    /// КОНКРЕТНАЯ РЕАЛИЗАЦИЯ полиморфизма для Google Chrome.
    /// 
    /// Демонстрирует:
    /// 1. Наследование от Browser (public class Chrome : Browser)
    /// 2. Override ключевое слово - переопределяем абстрактный метод Launch()
    /// 3. Конкретная реализация - Chrome открывается с chromedriver.exe
    /// 4. Не переопределяем Close() - используем реализацию из Browser (закрытие по умолчанию)
    /// 5. Полиморфизм - можно использовать как Browser chrome = new Chrome()
    /// 
    /// В контексте QA/Automation:
    /// - Chrome.Launch() - запускает Chrome через Selenium WebDriver
    /// - Белый фон, стандартное закрытие (из базового класса)
    /// 
    /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ:
    /// <code>
    /// // Способ 1: Создаем как Chrome
    /// Chrome chrome = new Chrome();
    /// chrome.Launch();  // [Google Chrome] Запуск: chromedriver.exe стартует...
    /// chrome.Close();   // [Google Chrome] Закрытие процесса браузера (стандартное)...
    /// 
    /// // Способ 2: Полиморфизм - работаем через базовый тип Browser
    /// Browser browser = new Chrome();  // В переменной Chrome, но тип Browser
    /// browser.Launch();                // Вызовет Chrome.Launch()! ✅ Полиморфизм
    /// browser.Close();                 // Вызовет Browser.Close() (нет override)
    /// 
    /// // Способ 3: В списке браузеров
    /// List&lt;Browser&gt; browsers = new List&lt;Browser&gt; { new Chrome(), new Firefox() };
    /// foreach (Browser b in browsers)
    ///     b.Launch();  // Chrome запустится как Chrome, Firefox как Firefox
    /// </code>
    /// 
    /// ЗАПОМНИТЕ: Вызов виртуального метода (Launch, Close) определяется РЕАЛЬНЫМ типом объекта,
    /// а не типом переменной! Если переменная Browser содержит Chrome, то вызовется Chrome.Launch().
    /// </summary>
    public class Chrome : Browser
    {
        /// <summary>
        /// Инициализирует Chrome браузер.
        /// Вызывает конструктор базового класса (Browser) с названием "Google Chrome".
        /// </summary>
        public Chrome() : base("Google Chrome") { }

        /// <summary>
        /// OVERRIDE - переопределяем абстрактный метод Launch() из Browser.
        /// 
        /// Chrome открывается с помощью chromedriver.exe (Selenium WebDriver для Chrome).
        /// Этот метод вызывается полиморфно при работе через базовый тип Browser.
        /// </summary>
        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: chromedriver.exe стартует...");
            Console.WriteLine($"[{Name}] Окно открыто с белым фоном.");
        }
    }
}
