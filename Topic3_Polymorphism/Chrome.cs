namespace Topic3_Polymorphism
{
    // 2. РЕАЛИЗАЦИЯ ПОЛИМОРФИЗМА (Разные формы одного класса)
    public class Chrome : Browser
    {
        public Chrome() : base("Google Chrome") { }

        public override void Launch()
        {
            Console.WriteLine($"[{Name}] Запуск: chromedriver.exe стартует...");
            Console.WriteLine($"[{Name}] Окно открыто с белым фоном.");
        }
    }
}
