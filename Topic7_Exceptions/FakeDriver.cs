namespace Topic7_Exceptions
{
    /// <summary>
    /// Упрощенный драйвер для демонстрации шаблона освобождения ресурсов через IDisposable.
    /// </summary>
    public class FakeDriver : IDisposable
    {
        /// <summary>
        /// Признак закрытия драйвера.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Создает экземпляр драйвера.
        /// </summary>
        public FakeDriver()
        {
            IsClosed = false;
            Console.WriteLine("[FakeDriver] Created");
        }

        /// <summary>
        /// Выполняет тестовые действия; выбрасывает ObjectDisposedException, если драйвер уже закрыт.
        /// </summary>
        public void DoWork()
        {
            if (IsClosed) throw new ObjectDisposedException(nameof(FakeDriver));
            Console.WriteLine("[FakeDriver] Doing work...");
        }

        /// <summary>
        /// Освобождает ресурсы драйвера. Повторные вызовы безопасны.
        /// </summary>
        public void Dispose()
        {
            if (!IsClosed)
            {
                Console.WriteLine("[FakeDriver] Disposed");
                IsClosed = true;
            }
        }
    }
}
