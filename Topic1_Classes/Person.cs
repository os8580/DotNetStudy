namespace Topic1_Classes
{
    // Абстрактная базовая сущность для всех людей/пользователей
    public abstract class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }

        protected Person(string name = "default name", int age = 0, bool isActive = false)
        {
            Name = name;
            Age = age;
            IsActive = isActive;
        }

        // Пустой конструктор для дефолтных значений (DRY)
        protected Person() : this("default name", 0, false) { }

        // Абстрактный метод: каждый наследник даёт свою текстовую сводку
        public abstract string GetSummary();

        // Виртуальное поведение: можно переопределить при особой логике
        public virtual void Deactivate()
        {
            IsActive = false;
        }

        public virtual void ToggleActive()
        {
            IsActive = !IsActive;
        }

        public override string ToString() => GetSummary();
    }
}
