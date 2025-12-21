namespace Topic7_Exceptions
{
    // Простой фейковый драйвер для демонстрации IDisposable
    public class FakeDriver : IDisposable
    {
        public bool IsClosed { get; private set; }

        public FakeDriver()
        {
            IsClosed = false;
            Console.WriteLine("[FakeDriver] Открыт");
        }

        public void DoWork()
        {
            if (IsClosed) throw new ObjectDisposedException(nameof(FakeDriver));
            Console.WriteLine("[FakeDriver] Выполняю работу...");
        }

        public void Dispose()
        {
            if (!IsClosed)
            {
                Console.WriteLine("[FakeDriver] Закрывается (Dispose)");
                IsClosed = true;
            }
        }
    }
}
