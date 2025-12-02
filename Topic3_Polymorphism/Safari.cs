namespace Topic3_Polymorphism
{
    // Safari закроем по-особенному
    public class Safari : Browser
    {
        public Safari() : base("Apple Safari") { }

        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: safaridriver стартует на MacOS...");
        }

        // ПЕРЕОПРЕДЕЛЯЕМ метод закрытия (Override)
        public override void Close()
        {
            Console.WriteLine($"[{Name}] Force Quit: Убиваем процесс жестко через Terminal.");
        }
    }
}
