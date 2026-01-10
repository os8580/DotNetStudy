namespace Topic5_LINQ
{
    /// <summary>
    /// Имитация Веб-Элемента (как в Selenium WebDriver).
    /// 
    /// Используется для демонстрации LINQ запросов в контексте QA/Automation.
    /// 
    /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ:
    /// <code>
    /// List&lt;WebElement&gt; elements = new List&lt;WebElement&gt;
    /// {
    ///     new WebElement("Login", true, "button"),
    ///     new WebElement("Submit", true, "button"),
    ///     new WebElement("HiddenDiv", false, "div")
    /// };
    /// 
    /// // Найти все видимые кнопки
    /// var visibleButtons = elements
    ///     .Where(e => e.Displayed && e.TagName == "button")
    ///     .Select(e => e.Text)
    ///     .ToList();  // { "Login", "Submit" }
    /// </code>
    /// </summary>
    public class WebElement
    {
        /// <summary>
        /// Текст элемента (видимое содержимое на странице).
        /// 
        /// Примеры:
        /// - "Login" для кнопки входа
        /// - "Submit" для кнопки отправки
        /// - "" для пустого элемента
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Видимость элемента на странице.
        /// 
        /// true = элемент видимый пользователю
        /// false = элемент скрыт (может быть позади других элементов или display:none)
        /// 
        /// Часто используется в LINQ запросах для фильтрации видимых элементов:
        /// <code>
        /// var visibleElements = elements.Where(e => e.Displayed);
        /// </code>
        /// </summary>
        public bool Displayed { get; set; }

        /// <summary>
        /// HTML тег элемента.
        /// 
        /// Примеры: "button", "input", "div", "span", "link", "a"
        /// 
        /// Часто используется для фильтрации по типу:
        /// <code>
        /// var buttons = elements.Where(e => e.TagName == "button");
        /// var inputs = elements.Where(e => e.TagName == "input");
        /// </code>
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Инициализирует новый WebElement с указанными параметрами.
        /// </summary>
        /// <param name="text">Текст элемента</param>
        /// <param name="displayed">Видимость элемента</param>
        /// <param name="tagName">HTML тег элемента</param>
        public WebElement(string text, bool displayed, string tagName)
        {
            Text = text;
            Displayed = displayed;
            TagName = tagName;
        }
    }
}
