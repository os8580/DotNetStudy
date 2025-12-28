# Topic1 — Классы и конструкторы (Полный курс для начинающих)

## Цель
Понять, как создавать классы, использовать конструкторы, работать с полями и свойствами в C#.

---

## 1. Что такое класс? (Для самых начинающих)

### Аналогия из жизни
Представьте, что класс — это **чертеж дома**:
- Чертеж описывает, как должен выглядеть дом (какие комнаты, окна, двери)
- Каждый построенный по чертежу дом — это **объект** (экземпляр класса)
- Все дома похожи, но это разные дома

```csharp
// Класс — чертеж
public class Person
{
    public string Name;
    public int Age;
}

// Объекты — конкретные люди
Person alice = new Person();  // Первый объект
Person bob = new Person();    // Второй объект
```

### Структура класса
```
public class Person
{
    // ПОЛЯ (данные)
    public string Name;
    
    // СВОЙСТВА (управляемый доступ)
    public int Age { get; set; }
    
    // КОНСТРУКТОР (инициализация)
    public Person(string name, int age) { }
    
    // МЕТОДЫ (действия)
    public void PrintInfo() { }
}
```

---

## 2. Поля (Fields) — как переменные в классе

### Что это такое?
Поля — это переменные, которые хранят данные объекта.

```csharp
public class User
{
    // Публичное поле — все могут видеть и менять
    public string Name;
    
    // Приватное поле — только этот класс может видеть и менять
    private int _age;
}

// Использование
User user = new User();
user.Name = "Alice";              // ? OK — поле публичное
// user._age = 25;                // ? Ошибка — поле приватное
```

### Приватное vs публичное
```
Представьте кошелек:
- Публичное поле = банкнота в открытом кошельке (любой может взять и потратить)
- Приватное поле = деньги на защищенном счете (только вы можете их трогать)
```

---

## 3. Конструкторы — инициализация объекта

### Что такое конструктор?
Конструктор — это специальный метод, который **вызывается автоматически** при создании объекта. Он инициализирует данные объекта.

```csharp
public class Person
{
    public string Name;
    public int Age;
    
    // Конструктор БЕЗ параметров (по умолчанию)
    public Person()
    {
        Name = "Unknown";
        Age = 0;
    }
    
    // Конструктор С параметрами
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

// Использование
Person p1 = new Person();              // Вызвана первая версия (без параметров)
Person p2 = new Person("Alice", 30);   // Вызвана вторая версия (с параметрами)
```

### Перегрузка конструктора (Overloading)
Вы можете создать **несколько конструкторов** с разным количеством параметров:

```csharp
public class Employee
{
    public string Name;
    public string Position;
    public decimal Salary;
    
    // Конструктор 1 — только имя
    public Employee(string name)
    {
        Name = name;
        Position = "Unknown";
        Salary = 0;
    }
    
    // Конструктор 2 — имя и должность
    public Employee(string name, string position)
    {
        Name = name;
        Position = position;
        Salary = 0;
    }
    
    // Конструктор 3 — всё
    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }
}
```

### Проблема с повторением кода
Это выглядит очень повторяющимся! Решение — использовать `this(...)`:

```csharp
public class Employee
{
    public string Name;
    public string Position;
    public decimal Salary;
    
    // Основной конструктор
    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }
    
    // Другие конструкторы вызывают основной (DRY принцип)
    public Employee(string name) : this(name, "Unknown", 0) { }
    
    public Employee(string name, string position) : this(name, position, 0) { }
}

// Все работает одинаково
Employee e1 = new Employee("Alice");
Employee e2 = new Employee("Bob", "Manager");
Employee e3 = new Employee("Charlie", "Lead", 100000);
```

---

## 4. Свойства (Properties) — безопасный доступ к данным

### Проблема с полями
```csharp
public class Person
{
    public int Age;  // ? Проблема!
}

Person p = new Person();
p.Age = -50;  // ?? Отрицательный возраст! Никто не проверил!
```

### Решение — свойства
Свойства — это **управляемый доступ** к данным через `get` и `set`:

```csharp
public class Person
{
    private int _age;  // Приватное поле
    
    // Свойство с проверкой (валидацией)
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0)
                _age = value;
            else
                throw new ArgumentException("Возраст не может быть отрицательным!");
        }
    }
}

// Использование
Person p = new Person();
p.Age = 30;   // ? OK
// p.Age = -50;  // ? Исключение! (обработано свойством)
```

### Краткая форма (Auto-property)
Если вам не нужна проверка, можно написать короче:

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Только для чтения (readonly свойства)
Иногда нужно создать свойство, которое нельзя изменить:

```csharp
public class Person
{
    // Можно читать, но нельзя менять (только в конструкторе)
    public string Name { get; init; }
    
    public Person(string name)
    {
        Name = name;  // ? OK — в конструкторе можно
    }
}

Person p = new Person("Alice");
// p.Name = "Bob";  // ? Ошибка! init запрещает изменение
```

### readonly для полей
```csharp
public class Config
{
    // Константа для конфигурации
    public readonly string ApiUrl = "https://api.example.com";
}

// Можно читать, но нельзя менять
// config.ApiUrl = "...";  // ? Ошибка!
```

---

## 5. Модификаторы доступа (Access Modifiers)

| Модификатор | Видно где? | Пример использования |
|------------|-----------|----------------------|
| `public` | Везде | Методы, которые вызывают другие классы |
| `private` | Только в этом классе | Внутренние поля для хранения данных |
| `protected` | В этом классе и классах-наследниках | Для наследования (Topic 3) |
| `internal` | В этой сборке (проекте) | Вспомогательные классы |

```csharp
public class User
{
    public string Email { get; set; }       // Публичное — видно всем
    private string _password;               // Приватное — скрыто
    
    public bool Login(string password)
    {
        // Внутри класса можем использовать приватное поле
        return password == _password;
    }
}
```

---

## 6. Практический пример — Page Object для новичков

```csharp
public class LoginPage
{
    // Приватные поля для хранения данных
    private string _username;
    private string _password;
    private bool _isLoggedIn;
    
    // Публичное свойство для имени пользователя
    public string Username 
    { 
        get { return _username; }
        set { _username = value; }
    }
    
    // Только для чтения
    public bool IsLoggedIn { get { return _isLoggedIn; } }
    
    // Конструктор
    public LoginPage()
    {
        _username = "";
        _password = "";
        _isLoggedIn = false;
    }
    
    // Метод с проверкой
    public bool Login(string password)
    {
        if (string.IsNullOrEmpty(_username))
            throw new ArgumentException("Username не может быть пустой!");
        
        _password = password;
        _isLoggedIn = true;
        return true;
    }
}

// Использование
LoginPage page = new LoginPage();
page.Username = "alice";
page.Login("secret123");
Console.WriteLine(page.IsLoggedIn);  // true
```

---

## 7. Частые ошибки новичков

### ? Ошибка 1: Забыли инициализировать поле
```csharp
public class Person
{
    public string Name;  // null по умолчанию!
}

Person p = new Person();
Console.WriteLine(p.Name.Length);  // ?? NullReferenceException!
```

**Решение**: Инициализируйте в конструкторе или используйте свойства с значениями по умолчанию.

### ? Ошибка 2: Публичное поле с вредным значением
```csharp
public class User
{
    public int Age;  // ? Публичное поле — опасно!
}

User u = new User();
u.Age = -100;  // ?? Никто не проверил!
```

**Решение**: Используйте свойства для валидации:
```csharp
public class User
{
    private int _age;
    
    public int Age
    {
        get { return _age; }
        set { _age = value >= 0 ? value : 0; }
    }
}
```

### ? Ошибка 3: Забыли вызвать конструктор
```csharp
Person p;  // Переменная декларирована, но объект не создан
// p.Name = "Alice";  // ?? NullReferenceException!

Person p = new Person();  // ? Правильно
p.Name = "Alice";
