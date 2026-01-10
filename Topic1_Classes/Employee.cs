namespace Topic1_Classes
{
    public class Employee : User
    {
        // Собственные свойства сотрудника
        public string? Position { get; private set; }
        public decimal Salary { get; private set; }

        /// <summary>
        /// ОСНОВНОЙ конструктор Employee
        /// Демонстрирует BASE в цепочке наследования: Employee -> User -> Person
        /// 
        /// Порядок выполнения:
        /// 1. : base(name, age, isActive) вызывает User конструктор
        /// 2. User видит : base(name, age, isActive) и вызывает Person конструктор
        /// 3. Person инициализируется первым (самый верхний в иерархии)
        /// 4. Затем выполняется User конструктор
        /// 5. В конце выполняется Employee конструктор (инициализирует Position и Salary)
        /// 
        /// ИТОГ: инициализация идёт СНИЗУ ВВЕРХ по иерархии классов
        /// </summary>
        public Employee(string? position = null, decimal salary = 0,
                        string name = "default name", int age = 0, bool isActive = false)
            : base(name, age, isActive)
        {
            Position = position;
            Salary = salary;
        }

        public void SetPosition(string position)
        {
            Position = position;
        }

        public void SetSalary(decimal salary)
        {
            Salary = salary;
        }

        // Переопределение абстрактного метода
        public override string GetSummary()
        {
            return $"Employee: {Name}, Age: {Age}, Active: {IsActive}, Position: {Position}, Salary: {Salary}";
        }
    }
}
