namespace Topic3_Polymorphism
{
    /// <summary>
    /// КОНКРЕТНАЯ РЕАЛИЗАЦИЯ полиморфизма для Apple Safari.
    /// 
    /// Демонстрирует ПОЛНЫЙ OVERRIDE - переопределяем оба метода (Launch и Close).
    /// 
    /// Ключевые особенности:
    /// 1. Override Launch() - Safari запускается иначе (на MacOS через safaridriver)
    /// 2. Override Close() - Safari закрывается ЖЕСТКО через Force Quit (особенность MacOS)
    /// 3. Safari более требователен к ресурсам и нуждается в специальной обработке
    /// 4. Демонстрирует полиморфизм в действии - для Safari работают оба метода
    /// 
    /// КОНТРАСТ С Chrome И Firefox:
    /// - Chrome.Launch() → Chrome открывается, Chrome.Close() → базовое закрытие
    /// - Firefox.Launch() → Firefox открывается, Firefox.Close() → базовое закрытие
    /// - Safari.Launch() → Safari открывается, Safari.Close() → ЖЕСТКОЕ закрытие Force Quit
    /// 
    /// В контексте QA/Automation:
    /// - Safari.Launch() запускает Safari через safaridriver (Selenium WebDriver для Safari)
    /// - Safari работает только на MacOS
    /// - Safari.Close() требует Force Quit для надежного завершения процесса
    /// 
    /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ:
    /// <code>
    /// // Способ 1: Как Safari
    /// Safari safari = new Safari();
    /// safari.Launch();  // [Apple Safari] Запуск: safaridriver стартует на MacOS...
    /// safari.Close();   // [Apple Safari] Force Quit: Убиваем процесс жестко через Terminal.
    /// 
    /// // Способ 2: Полиморфизм - видны оба override
    /// Browser browser = new Safari();
    /// browser.Launch();  // Вызовет Safari.Launch() ✅
    /// browser.Close();   // Вызовет Safari.Close() ✅ (не базовый Close!)
    /// 
    /// // Способ 3: Кросс-браузерное тестирование
    /// List&lt;Browser&gt; browsers = new List&lt;Browser&gt; 
    /// { 
    ///     new Chrome(),
    ///     new Firefox(),
    ///     new Safari()  // Safari в списке
    /// };
    /// foreach (Browser b in browsers)
    /// {
    ///     b.Launch();  // Safari.Launch() для Safari
    ///     b.Close();   // Safari.Close() для Safari (Force Quit!)
    /// }
    /// </code>
    /// 
    /// ПОЛИМОРФИЗМ В ЧИСТОМ ВИДЕ:
    /// Один список Browser содержит Chrome, Firefox, Safari.
    /// Один цикл foreach вызывает b.Launch() и b.Close().
    /// Но каждый браузер ведет себя по-своему благодаря override!
    /// </summary>
    public class Safari : Browser
    {
        /// <summary>
        /// Инициализирует Safari браузер.
        /// Вызывает конструктор базового класса с названием "Apple Safari".
        /// </summary>
        public Safari() : base("Apple Safari") { }

        /// <summary>
        /// OVERRIDE - переопределяем Launch() из Browser.
        /// 
        /// Safari запускается через safaridriver на MacOS (не через chromedriver/geckodriver).
        /// Это отличает Safari от Chrome и Firefox, которые более кроссплатформенны.
        /// </summary>
        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: safaridriver стартует на MacOS...");
        }

        /// <summary>
        /// OVERRIDE Close() - вот что делает Safari особенным!
        /// 
        /// Chrome и Firefox используют Close() из базового класса (стандартное закрытие).
        /// Но Safari требует ЖЕСТКОГО Force Quit, потому что иногда зависает.
        /// 
        /// Это демонстрирует главную идею полиморфизма:
        /// - Один интерфейс (Close)
        /// - Разная реализация в зависимости от типа (Safari vs Chrome vs Firefox)
        /// - Код работает корректно благодаря override
        /// 
        /// ПРАКТИЧЕСКИЙ ПРИМЕР:
        /// List&lt;Browser&gt; browsers = new List&lt;Browser&gt; { new Chrome(), new Safari() };
        /// foreach (Browser b in browsers)
        ///     b.Close();  // Chrome закроется нормально, Safari - Force Quit
        /// </summary>
        public override void Close()
        {
            Console.WriteLine($"[{Name}] Force Quit: Убиваем процесс жестко через Terminal.");
        }
    }
}
