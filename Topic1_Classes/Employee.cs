namespace Topic1_Classes
{
    public class Employee : User
    {
        // Собственные свойства сотрудника
        public string? Position { get; private set; }
        public decimal Salary { get; private set; }

        // Один конструктор: задаёт свои поля и (при необходимости) базовые
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
