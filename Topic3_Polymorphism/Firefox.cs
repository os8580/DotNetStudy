namespace Topic3_Polymorphism
{
    public class Firefox : Browser
    {
        public Firefox() : base("Mozilla Firefox") { }
        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: geckodriver.exe стартует...");
            Console.WriteLine($"[{Name}] Окно открыто с оранжевым логотипом.");
        }
    }

}
