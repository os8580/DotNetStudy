namespace Topic7_Exceptions
{
    /// <summary>
    /// Пользовательское исключение для имитации ошибки "элемент не найден" (как в UI‑тестах/Selenium).
    /// </summary>
    public class ElementNotFoundException : Exception
    {
        /// <summary>
        /// Создает новое исключение с текстом сообщения о ненайденном элементе.
        /// </summary>
        /// <param name="message">Детали: селектор/описание элемента.</param>
        public ElementNotFoundException(string message) : base(message) { }
    }
}
