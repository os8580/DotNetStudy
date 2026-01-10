namespace Topic3_Polymorphism
{
    /// <summary>
    /// АБСТРАКТНЫЙ базовый класс для всех браузеров.
    /// 
    /// Демонстрирует:
    /// 1. Abstract класс - нельзя создать напрямую (new Browser() - ОШИБКА!)
    /// 2. Abstract методы - ДОЛЖНЫ быть реализованы в дочерних классах
    /// 3. Virtual методы - МОЖНО переопределить или оставить реализацию базового класса
    /// 4. Полиморфизм - один метод, разное поведение в зависимости от типа объекта
    /// 
    /// В реальной жизни используется для:
    /// - Задания контракта: все браузеры должны иметь Launch() и Close()
    /// - Избегания дублирования: общие свойства (Name) в одном месте
    /// - Работы с полиморфизмом: List&lt;Browser&gt; может содержать Chrome, Firefox, Safari
    /// 
    /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ:
    /// <code>
    /// // НЕПРАВИЛЬНО - нельзя создать абстрактный класс
    /// Browser browser = new Browser("test");  // ❌ Ошибка компиляции!
    /// 
    /// // ПРАВИЛЬНО - создаем конкретный браузер
    /// Browser chrome = new Chrome();           // ✅ OK
    /// Browser firefox = new Firefox();         // ✅ OK
    /// 
    /// // Полиморфизм
    /// List&lt;Browser&gt; browsers = new List&lt;Browser&gt; 
    /// { 
    ///     new Chrome(), 
    ///     new Firefox(), 
    ///     new Safari() 
    /// };
    /// foreach (Browser b in browsers)
    /// {
    ///     b.Launch();  // Каждый браузер запустится по-своему!
    ///     b.Close();   // Safari закроется по-особенному, остальные стандартно
    /// }
    /// </code>
    /// </summary>
    public abstract class Browser
    {
        public string Name { get; private set; }

        /// <summary>
        /// Инициализирует базовый класс браузера с названием.
        /// </summary>
        /// <param name="name">Название браузера (e.g., "Google Chrome", "Mozilla Firefox")</param>
        public Browser(string name)
        {
            Name = name;
        }

        /// <summary>
        /// АБСТРАКТНЫЙ метод - ДОЛЖЕН быть реализован в каждом дочернем классе.
        /// 
        /// Обозначает, что каждый браузер открывается УНИКАЛЬНЫМ способом:
        /// - Chrome использует chromedriver.exe
        /// - Firefox использует geckodriver.exe
        /// - Safari использует safaridriver
        /// 
        /// Компилятор будет ругаться если класс наследует Browser и не реализует Launch().
        /// </summary>
        public abstract void Launch();

        /// <summary>
        /// ВИРТУАЛЬНЫЙ метод - имеет реализацию, но может быть переопределен.
        /// 
        /// Демонстрирует:
        /// 1. Есть реализация по умолчанию (для большинства браузеров)
        /// 2. Но Safari переопределяет (override) с жестким закрытием
        /// 3. Chrome и Firefox используют реализацию из базового класса
        /// 
        /// ПРИМЕРЫ:
        /// - Chrome.Close() - использует базовую реализацию (стандартное закрытие)
        /// - Safari.Close() - переопределяет (Force Quit через Terminal)
        /// </summary>
        public virtual void Close()
        {
            Console.WriteLine($"[{Name}] Закрытие процесса браузера (стандартное)...");
        }
    }
}
