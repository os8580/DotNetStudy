namespace Topic1_Classes
{
    public class User : Person
    {
        // DRY: параметрless-ctor использует дефолты базового Person
        public User() : base() {}

        // Явный конструктор для всех параметров
        public User(string name, int age, bool isActive)
            : base(name, age, isActive)
        {
        }

        // Переопределение абстрактного метода
        public override string GetSummary()
        {
            return $"User: {Name}, Age: {Age}, Active: {IsActive}";
        }
    }
}
