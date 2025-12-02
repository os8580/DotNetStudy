namespace Topic3_Polymorphism
{
    // 1. АБСТРАКТНЫЙ КЛАСС (Или Интерфейс)
    // Задает общее поведение для всех браузеров
    public abstract class Browser
    {
        public string Name { get; private set; }

        public Browser(string name)
        {
            Name = name;
        }

        // Абстрактный метод: каждый браузер открывается по-своему
        public abstract void Launch();

        // Виртуальный метод: можно переопределить, а можно оставить общим
        public virtual void Close()
        {
            Console.WriteLine($"[{Name}] Закрытие процесса браузера (стандартное)...");
        }
    }
}
