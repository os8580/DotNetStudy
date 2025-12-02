using Topic1_Classes;

// default user
Console.WriteLine("DEFAULT USER");
var defaultUser = new User();
Console.WriteLine($"Имя: {defaultUser.Name}, Возраст: {defaultUser.Age}, Статус: {defaultUser.IsActive}");
Console.WriteLine(defaultUser.GetSummary());
Console.WriteLine("--------------------------------------------");

// user with all parameters
Console.WriteLine("USER1");
var user1 = new User("Alice", 30, true);
Console.WriteLine($"Имя: {user1.Name}, Возраст: {user1.Age}, Статус: {user1.IsActive}");
Console.WriteLine(user1.GetSummary());
Console.WriteLine("--------------------------------------------");

// user with some parameters (DRY вариант через инициализатор)
Console.WriteLine("USER2");
var user2 = new User { Age = 30, IsActive = true };
Console.WriteLine($"Имя: {user2.Name}, Возраст: {user2.Age}, Статус: {user2.IsActive}");
Console.WriteLine(user2.GetSummary());
Console.WriteLine("--------------------------------------------");

// user with named parameters in different order
Console.WriteLine("USER3");
var user3 = new User(age: 45, isActive: true, name: "Tigran");
Console.WriteLine($"Имя: {user3.Name}, Возраст: {user3.Age}, Статус: {user3.IsActive}");
Console.WriteLine(user3.GetSummary());
Console.WriteLine("--------------------------------------------");

// employee with all parameters
Console.WriteLine("EMPLOYEE1");
var emp1 = new Employee("manager", 5000);
Console.WriteLine($"Имя: {emp1.Name}, Возраст: {emp1.Age}, Статус: {emp1.IsActive}, Позиция: {emp1.Position}, Зарплата: {emp1.Salary}");
Console.WriteLine(emp1.GetSummary());
Console.WriteLine("--------------------------------------------");

// employee with named params
Console.WriteLine("EMPLOYEE2");
var emp2 = new Employee(salary: 4000);
Console.WriteLine($"Имя: {emp2.Name}, Возраст: {emp2.Age}, Статус: {emp2.IsActive}, Позиция: {emp2.Position}, Зарплата: {emp2.Salary}");
Console.WriteLine(emp2.GetSummary());
Console.WriteLine("--------------------------------------------");

// emp with all named params
Console.WriteLine("EMPLOYEE3");
var emp3 = new Employee(salary: 7000, isActive: true, position: "developer", age: 29, name: "Mike");
Console.WriteLine($"Имя: {emp3.Name}, Возраст: {emp3.Age}, Статус: {emp3.IsActive}, Позиция: {emp3.Position}, Зарплата: {emp3.Salary}");
Console.WriteLine(emp3.GetSummary());
Console.WriteLine("--------------------------------------------");

// emp with all params
Console.WriteLine("EMPLOYEE4");
Employee emp4 = new("QA", 7000, "Petya", 37, true);
Console.WriteLine($"Имя: {emp4.Name}, Возраст: {emp4.Age}, Статус: {emp4.IsActive}, Позиция: {emp4.Position}, Зарплата: {emp4.Salary}");
Console.WriteLine("Edit everything");
emp4.Name = "Petya1";
emp4.Age = 44;
emp4.IsActive = false;
emp4.SetPosition("QA LEAD");
emp4.SetSalary(9999);
Console.WriteLine(emp4.GetSummary());
Console.WriteLine("--------------------------------------------");

// Демонстрация виртуального метода ToggleActive через базовый Person (полиморфизм)
Console.WriteLine("TOGGLE DEMO");
Person[] people = { user1, emp1, emp4 };
foreach (var p in people)
{
    Console.WriteLine($"Before toggle: {p.GetSummary()}");
    p.ToggleActive();
    Console.WriteLine($"After toggle:  {p.GetSummary()}");
    Console.WriteLine();
}