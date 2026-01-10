namespace Topic1_Classes
{
    public class User : Person
    {
        /// <summary>
        /// Упрощённый конструктор User (БЕЗ параметров)
        /// Демонстрирует BASE: вызывает конструктор РОДИТЕЛЬСКОГО класса (Person)
        /// base() вызовет конструктор Person без параметров
        /// </summary>
        public User() : base() {}

        /// <summary>
        /// ОСНОВНОЙ конструктор User (СО ВСЕМИ параметрами)
        /// Демонстрирует BASE: вызывает конструктор РОДИТЕЛЬСКОГО класса (Person)
        /// base(name, age, isActive) передаёт параметры в Person конструктор
        /// Порядок выполнения: Person инициализируется ДО User
        /// </summary>
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
