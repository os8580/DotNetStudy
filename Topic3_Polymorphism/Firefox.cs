namespace Topic3_Polymorphism
{
    /// <summary>
    /// КОНКРЕТНАЯ РЕАЛИЗАЦИЯ полиморфизма для Mozilla Firefox.
    /// 
    /// Демонстрирует:
    /// 1. Наследование от Browser
    /// 2. Override Launch() - переопределяем с Firefox-специфичным поведением
    /// 3. Не переопределяем Close() - используем из базового класса
    /// 4. Совместимость полиморфизма - может использоваться как Browser
    /// 
    /// В контексте QA/Automation:
    /// - Firefox.Launch() запускает Firefox через geckodriver.exe (Selenium WebDriver)
    /// - Оранжевый логотип Firefox в окне
    /// 
    /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ:
    /// <code>
    /// // Способ 1: Как Firefox
    /// Firefox firefox = new Firefox();
    /// firefox.Launch();  // [Mozilla Firefox] Запуск: geckodriver.exe стартует...
    /// firefox.Close();   // [Mozilla Firefox] Закрытие процесса браузера (стандартное)...
    /// 
    /// // Способ 2: Полиморфизм
    /// Browser browser = new Firefox();  // Полиморфная переменная
    /// browser.Launch();                 // Вызовет Firefox.Launch() ✅
    /// browser.Close();                  // Вызовет Browser.Close() (нет override)
    /// 
    /// // Способ 3: Кросс-браузерное тестирование
    /// List&lt;Browser&gt; browsers = new List&lt;Browser&gt; 
    /// { 
    ///     new Chrome(), 
    ///     new Firefox(),    // Firefox в списке
    ///     new Safari() 
    /// };
    /// foreach (Browser b in browsers)
    ///     b.Launch();  // Firefox запустится как Firefox, остальные как они есть
    /// </code>
    /// 
    /// ПОЛИМОРФИЗМ В ДЕЙСТВИИ:
    /// Один и тот же код (b.Launch()) вызовет разные методы в зависимости от типа:
    /// - Chrome → Chrome.Launch() (chromedriver.exe)
    /// - Firefox → Firefox.Launch() (geckodriver.exe)
    /// - Safari → Safari.Launch() (safaridriver)
    /// </summary>
    public class Firefox : Browser
    {
        /// <summary>
        /// Инициализирует Firefox браузер.
        /// Вызывает конструктор базового класса с названием "Mozilla Firefox".
        /// </summary>
        public Firefox() : base("Mozilla Firefox") { }

        /// <summary>
        /// OVERRIDE - переопределяем Launch() из Browser.
        /// 
        /// Firefox открывается с помощью geckodriver.exe (Selenium WebDriver для Firefox).
        /// Вызывается полиморфно при работе через тип Browser.
        /// </summary>
        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: geckodriver.exe стартует...");
            Console.WriteLine($"[{Name}] Окно открыто с оранжевым логотипом.");
        }
    }
}
