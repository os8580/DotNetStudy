namespace Topic6_Generics
{
    /// <summary>
    /// Обобщенная коробка для хранения значения произвольного типа.
    /// Обеспечивает типобезопасность без приведения типов.
    /// </summary>
    /// <typeparam name="T">Тип содержимого коробки.</typeparam>
    public class GenericBox<T>
    {
        /// <summary>
        /// Содержимое коробки. Тип соответствует параметру <typeparamref name="T"/>.
        /// </summary>
        public T Content;

        /// <summary>
        /// Создает экземпляр коробки с указанным содержимым.
        /// </summary>
        /// <param name="content">Начальное содержимое коробки.</param>
        public GenericBox(T content)
        {
            Content = content;
        }

        /// <summary>
        /// Возвращает текущее содержимое коробки.
        /// </summary>
        /// <returns>Значение типа <typeparamref name="T"/>.</returns>
        public T GetContent()
        {
            return Content;
        }
    }
}
