# Topic1 — Классы и конструкторы (Полный курс для начинающих)

## Цель

Понять, как создавать классы, использовать конструкторы, работать с полями и свойствами в C#.

---

### Для полного новичка: быстрый маршрут

- Прочитайте разделы: "Что такое класс?", "Конструкторы", "this и base", "static", "Свойства".
- Запустите Program.cs и посмотрите вывод по свойствам и цепочке конструкторов.
- Вернитесь к чек‑листу: под ключевыми пунктами добавлены краткие ответы.

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
user.Name = "Alice";              //  OK — поле публичное
// user._age = 25;                //  Ошибка — поле приватное
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

## 3.1 ВАЖНО: `this` и `base` в конструкторах (Объяснение по запросу)

Это КРИТИЧЕСКИ ВАЖНАЯ тема! Здесь объясняется как работает инициализация в наследовании.

### ✨ Что такое `this` в конструкторе?

`this` — это **ссылка на текущий объект** (сам класс). Когда вы пишете `this(...)`, вы **вызываете другой конструктор того же класса**.

#### Зачем это нужно?

Представьте: у вас есть 3 конструктора с разным количеством параметров. Без `this` каждый будет дублировать логику. С `this` — вы вызываете основной конструктор из других, экономя код (принцип **DRY**).

#### Пример 1: `this` без наследования

```csharp
public class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }

    // Основной конструктор (делает всю работу)
    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
        Console.WriteLine("✓ Основной конструктор вызван");
    }

    // Упрощённый конструктор: вызывает основной через this
    public Employee(string name)
        : this(name, "Junior", 0)  //  Вызовет основной конструктор!
    {
        // Вы можете добавить дополнительную логику ЗА конструктором
        Console.WriteLine($"✓ Упрощённый конструктор для {name}");
    }
}

// Использование
Employee emp = new Employee("Alice");  // Сначала выполнится основной, потом упрощённый
// Вывод:
// ✓ Основной конструктор вызван
// ✓ Упрощённый конструктор для Alice
```

**Порядок выполнения:**

1. `: this(name, "Junior", 0)` — сначала вызывает основной конструктор
2. Основной конструктор исполняется полностью
3. Затем выполняется тело упрощённого конструктора

---

### ⭐ Что такое `base` в конструкторе?

`base` — это **ссылка на класс-родитель** (базовый класс). Когда вы пишете `base(...)`, вы **вызываете конструктор родительского класса**.

#### Зачем это нужно?

Когда класс **наследуется** от другого класса, перед тем как инициализировать свои поля, нужно **сначала инициализировать родительский класс**. Это делается через `base`.

#### Пример 2: `base` с наследованием

Представьте иерархию:

```
Person (базовый класс)
  └─ User (наследуется от Person)
       └─ Employee (наследуется от User)
```

```csharp
// БАЗОВЫЙ КЛАСС
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Конструктор базового класса
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine($"✓ Person инициализирован: {name}");
    }
}

// ПРОМЕЖУТОЧНЫЙ КЛАСС (наследуется от Person)
public class User : Person
{
    public bool IsActive { get; set; }

    // Конструктор User: сначала инициализирует Person через base
    public User(string name, int age, bool isActive)
        : base(name, age)  //  Вызывает конструктор Person
    {
        IsActive = isActive;
        Console.WriteLine($"✓ User инициализирован: активен = {isActive}");
    }
}

// ФИНАЛЬНЫЙ КЛАСС (наследуется от User)
public class Employee : User
{
    public string Position { get; set; }

    // Конструктор Employee: сначала инициализирует User через base
    public Employee(string name, int age, bool isActive, string position)
        : base(name, age, isActive)  //  Вызывает конструктор User
    {
        Position = position;
        Console.WriteLine($"✓ Employee инициализирован: {position}");
    }
}

// Использование
Employee emp = new Employee("Alice", 30, true, "Developer");
// Вывод (ПОРЯДОК ОЧЕНЬ ВАЖЕН):
// ✓ Person инициализирован: Alice
// ✓ User инициализирован: активен = True
// ✓ Employee инициализирован: Developer
```

**Порядок выполнения (ЭТО ВАЖНО!):**

1. Вызываем `Employee("Alice", 30, true, "Developer")`
2. Конструктор Employee видит `: base(name, age, isActive)`
3. Вызывается конструктор User с параметрами `("Alice", 30, true)`
4. Конструктор User видит `: base(name, age)`
5. Вызывается конструктор Person с параметрами `("Alice", 30)`
6. **Person конструктор выполняется полностью** (самый верхний класс)
7. **Затем выполняется User конструктор**
8. **Затем выполняется Employee конструктор**

✨ **ИТОГ**: Инициализация идёт **снизу вверх по иерархии**, но **выполнение — сверху вниз**.

---

### 🎯 Пример 3: `base` + `this` вместе

Это можно комбинировать! Вот реальный пример из вашего проекта (похоже на Person → User → Employee):

```csharp
public abstract class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Основной конструктор Person
    protected Person(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine($"? Person: {name}, {age} лет");
    }

    // Упрощённый конструктор Person (через this)
    protected Person() : this("default", 0)
    {
        Console.WriteLine("? Person: дефолтные значения");
    }
}

public class User : Person
{
    public bool IsActive { get; set; }

    // Основной конструктор User
    public User(string name, int age, bool isActive)
        : base(name, age)  //  base: вызывает Person(name, age)
    {
        IsActive = isActive;
        Console.WriteLine($"? User: активен = {isActive}");
    }

    // Упрощённый конструктор User (через this)
    public User() : this("John", 25, false)  //  this: вызывает основной User
    {
        Console.WriteLine("? User: дефолтный конструктор");
    }
}

// Использование
Console.WriteLine("--- Создание User с параметрами ---");
User user1 = new User("Alice", 30, true);
// Вывод:
//  Person: Alice, 30 лет
//  User: активен = True

Console.WriteLine("\n--- Создание User без параметров ---");
User user2 = new User();
// Вывод:
//  Person: John, 25 лет
//  User: активен = False
//  User: дефолтный конструктор
```

---

### 📋 Таблица сравнения `this` vs `base`

| Свойство               | `this`                                     | `base`                                          |
| ---------------------- | ------------------------------------------ | ----------------------------------------------- |
| **Вызывает**           | Конструктор **того же класса**             | Конструктор **родительского класса**            |
| **Когда использовать** | Несколько версий конструктора (перегрузка) | Наследование (класс расширяет другой класс)     |
| **Пример**             | `public User() : this("default", 0)`       | `public Employee(string pos) : base(name, age)` |
| **Может быть рядом**   | НЕТ — либо `this`, либо `base`             | НЕТ — либо `this`, либо `base`                  |

⚠️ **ВАЖНО**: В одном конструкторе можно использовать ТОЛЬКО ИЛИ `this(...)` ИЛИ `base(...)`, но не оба!

---

### 🐛 Частые ошибки с `this` и `base`

#### Ошибка 1: Забыли вызвать `base`

```csharp
public class Employee : Person
{
    public string Position { get; set; }

    //  Ошибка! Поля Person не инициализированы!
    public Employee(string position)
    {
        Position = position;  // OK, но Person не готов
    }
}

// Результат: Name и Age будут null/0 (не инициализированы)
Employee emp = new Employee("Developer");
Console.WriteLine(emp.Name);  // null — ошибка!
```

**Решение**: Используйте `base`:

```csharp
public class Employee : Person
{
    public string Position { get; set; }

    //  ПРАВИЛЬНО!
    public Employee(string position)
        : base("default", 0)  //  Инициализируем Person
    {
        Position = position;
    }
}
```

#### Ошибка 2: Использовали и `this`, и `base` одновременно

```csharp
//  КОМПИЛЯЦИЯ ПРОВАЛИТСЯ!
public Employee(string pos) : base(name, age) : this(pos, 0)
{
}
```

**Решение**: Выберите один:

```csharp
// Вариант 1: Используем base
public Employee(string pos) : base("default", 0)
{
    Position = pos;
}

// Или вариант 2: Используем this
public Employee(string pos) : this(pos, "Junior", 5000)
{
}
```

#### Ошибка 3: Забыли параметры при вызове `base`

```csharp
//  Ошибка!
public Employee(string name, int age) : base()  //  base() требует параметры!
{
}
```

Если в родительском классе конструктор требует параметры, вы должны их передать через `base`.

---

### 🏗️ Структура нашего проекта (Topic1_Classes)

Посмотрите, как это применяется в вашем коде:

**Person.cs** (базовый класс):

```csharp
protected Person(string name = "default name", int age = 0, bool isActive = false)
{
    Name = name;
    Age = age;
    IsActive = isActive;
}

protected Person() : this("default name", 0, false) { }  //  this
```

**User.cs** (наследуется от Person):

```csharp
public User(string name, int age, bool isActive)
    : base(name, age, isActive)  //  base: вызывает Person
{
}

public User() : base() { }  //  base: вызывает Person()
```

**Employee.cs** (наследуется от User):

```csharp
public Employee(string? position = null, decimal salary = 0,
                string name = "default name", int age = 0, bool isActive = false)
    : base(name, age, isActive)  //  base: вызывает User
{
    Position = position;
    Salary = salary;
}
```

✨ **ВИДИТЕ ЦЕПОЧКУ?**

- Employee → User → Person
- При создании Employee сначала инициализируется Person, потом User, потом Employee
- Каждый класс добавляет свои поля и логику

---

### ✅ Итоговый чек-лист для собеседования

**На вопрос про `this` и `base`:**

- ✅ `this(...)` вызывает другой конструктор **того же класса**
- ✅ `base(...)` вызывает конструктор **родительского класса**
- ✅ Используются для избежания дублирования кода (DRY)
- ✅ В одном конструкторе может быть либо `this`, либо `base`, но не оба
- ✅ При наследовании сначала инициализируется базовый класс, потом производный
- ✅ Если не вызвать `base(...)`, поля родителя будут не инициализированы

---

## 3.2 Модификатор `static` — общее для всех объектов

`static` — это **общее хранилище для всех объектов класса**. Если вы пометите поле или метод словом `static`, они будут существовать **на уровне класса**, а не на уровне отдельного объекта.

### Аналогия: семья и дом

```
Обычное (не-static) поле: каждый человек в семье имеет свой паспорт
Static поле: у всей семьи один адрес дома (общий для всех)
```

### Пример 1: `static` поле

```csharp
public class User
{
    public string Name { get; set; }              // Обычное поле (для каждого объекта)
    public static int TotalUsers { get; set; }    // Static поле (общее для всех объектов)

    public User(string name)
    {
        Name = name;
        TotalUsers++;  // Увеличиваем общий счётчик
    }
}

// Использование
User user1 = new User("Alice");
Console.WriteLine($"Всего пользователей: {User.TotalUsers}");  // 1

User user2 = new User("Bob");
Console.WriteLine($"Всего пользователей: {User.TotalUsers}");  // 2

User user3 = new User("Charlie");
Console.WriteLine($"Всего пользователей: {User.TotalUsers}");  // 3

// ВНИМАНИЕ: обращаемся к static через ИМЯ КЛАССА, а не объекта
// User.TotalUsers  — правильно
// user1.TotalUsers — работает, но считается плохим стилем
```

### Пример 2: `static` метод

```csharp
public class Calculator
{
    // Static метод — можно вызвать без создания объекта
    public static int Add(int a, int b)
    {
        return a + b;
    }

    // Обычный метод — нужен объект
    public int Multiply(int a, int b)
    {
        return a * b;
    }
}

// Использование
int sum = Calculator.Add(5, 3);  // ℹ Вызовом без создания объекта!
Console.WriteLine(sum);  // 8

Calculator calc = new Calculator();
int product = calc.Multiply(5, 3);  // Нужен объект для обычного метода
Console.WriteLine(product);  // 15

// Примеры из реальной жизни:
int parsed = int.Parse("123");           // Parse — static метод
string upper = "hello".ToUpper();        // ToUpper — обычный метод
```

### Пример 3: `static` конструктор

Иногда нужно инициализировать static данные **один раз при запуске**. Для этого используется static конструктор:

```csharp
public class Configuration
{
    public static string ApiUrl { get; set; }
    public static string ApiKey { get; set; }

    // Static конструктор — вызывается ровно ОДИН раз, автоматически
    static Configuration()
    {
        ApiUrl = "https://api.example.com";
        ApiKey = "secret123";
        Console.WriteLine("✓ Configuration инициализирована");
    }
}

// Использование
Console.WriteLine(Configuration.ApiUrl);  // https://api.example.com
// ✓ Configuration инициализирована (выведется один раз)

Console.WriteLine(Configuration.ApiUrl);  // https://api.example.com
// (static конструктор НЕ вызывается снова)
```

### Таблица: Static vs Instance

| Аспект             | Static                           | Instance (Обычное)           |
| ------------------ | -------------------------------- | ---------------------------- |
| **Где существует** | На уровне класса                 | На уровне объекта            |
| **Создаётся**      | При загрузке класса              | При создании каждого объекта |
| **Как вызвать**    | `ClassName.Member`               | `object.Member`              |
| **Сколько копий**  | Одна на все объекты              | По одной в каждом объекте    |
| **Пример**         | `User.TotalUsers`, `int.Parse()` | `user.Name`, `user.Age`      |

### ✅ Когда использовать `static`?

✅ **Используйте static для:**

- Счётчиков (TotalUsers, RequestCount)
- Конфигурации (ApiUrl, Version)
- Утилит (Calculator.Add, StringHelper.IsEmail)
- Констант (Math.PI, DateTime.MaxValue)

❌ **НЕ используйте static для:**

- Данных конкретного объекта (Name, Age, Email)
- Состояния объекта (IsActive, Position)
- Чего-то, что меняется от объекта к объекту

### 🐛 Частые ошибки с `static`

#### Ошибка 1: Использовали static для данных объекта

```csharp
//  Ошибка!
public class User
{
    public static string Name { get; set; }  // Общее для ВСЕХ пользователей!
}

User user1 = new User { Name = "Alice" };
User user2 = new User { Name = "Bob" };

Console.WriteLine(user1.Name);  // "Bob" — в объекте Alice!
// ПРОБЛЕМА: когда изменили Name в user2, изменилось и в user1!
```

#### Ошибка 2: Забыли, что static может быть заменён

```csharp
public class Config
{
    public static string ApiUrl = "https://api.example.com";
}

// Где-то в коде...
Config.ApiUrl = "https://evil.com";  //  ИЗМЕНИЛИ! Теперь везде зло-URL

// РЕШЕНИЕ: используйте readonly или private setter
public class Config
{
    public static string ApiUrl { get; } = "https://api.example.com";
    // Теперь не сможем изменить!
}
```

---

## 4. Свойства (Properties) — безопасный доступ к данным

### Проблема с полями

```csharp
public class Person
{
    public int Age;  //  Проблема!
}

Person p = new Person();
p.Age = -50;  //  Отрицательный возраст! Никто не проверил!
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
p.Age = 30;   //  OK
// p.Age = -50;  //  Исключение! (обработано свойством)
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
        Name = name;  //  OK — в конструкторе можно
    }
}

Person p = new Person("Alice");
// p.Name = "Bob";  //  Ошибка! init запрещает изменение
```

### readonly для полей

```csharp
public class Config
{
    // Константа для конфигурации
    public readonly string ApiUrl = "https://api.example.com";
}

// Можно читать, но нельзя менять
// config.ApiUrl = "...";  //  Ошибка!
```

---

## 5. Модификаторы доступа (Access Modifiers)

| Модификатор | Видно где?                          | Пример использования                   |
| ----------- | ----------------------------------- | -------------------------------------- |
| `public`    | Везде                               | Методы, которые вызывают другие классы |
| `private`   | Только в этом классе                | Внутренние поля для хранения данных    |
| `protected` | В этом классе и классах-наследниках | Для наследования (Topic 3)             |
| `internal`  | В этой сборке (проекте)             | Вспомогательные классы                 |

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

**Полный рабочий пример находится в файле [LoginPage.cs](LoginPage.cs)**

Это реальный класс, который используется в Program.cs для демонстрации Properties:

```csharp
public class LoginPage
{
    // Приватные поля для хранения данных
    private string _username;
    private string _password;
    private bool _isLoggedIn;

    // Публичное свойство для имени пользователя (с валидацией!)
    public string Username
    {
        get { return _username; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Username не может быть пустым!");
            _username = value;
        }
    }

    // Свойство пароля (с валидацией минимальной длины)
    public string Password
    {
        get { return _password; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                throw new ArgumentException("Пароль должен быть минимум 6 символов!");
            _password = value;
        }
    }

    // Только для чтения (нельзя изменить напрямую!)
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

    public void Logout()
    {
        _isLoggedIn = false;
        _password = "";
    }
}

// Использование
LoginPage page = new LoginPage();
page.Username = "alice";           // ✓ OK — установка через свойство
page.Login("secret123");           // ✓ OK — вход
Console.WriteLine(page.IsLoggedIn);  // true

// page.Username = "";              // ✗ Ошибка! Валидация сработала
// page.IsLoggedIn = false;         // ✗ Ошибка! IsLoggedIn только для чтения
```

**Что демонстрирует этот класс:**

- ✅ **Приватные поля** (\_username, \_password) — скрыты
- ✅ **Публичные свойства** (Username, Password) — видны снаружи
- ✅ **Валидация в set** — проверка перед присваиванием
- ✅ **Read-only свойство** (IsLoggedIn) — только get, нет set
- ✅ **Методы для контроля состояния** (Login, Logout)
- ✅ **Инкапсуляция** — скрываем внутренние детали

### Запуск примера

Когда вы запустите `dotnet run`, в конце вывода вы увидите:

```
=====================================================
ДЕМОНСТРАЦИЯ PROPERTIES (GET/SET) С ВАЛИДАЦИЕЙ
=====================================================

ПРИМЕР 1: Создание LoginPage с валидацией свойств
Начальное состояние: User: , Logged in: False
После установки Username: User: alice, Logged in: False
❌ Ошибка при установке пустого Username: Username не может быть пустым!

Попытка входа с коротким паролем:
❌ Ошибка: Пароль некорректен!

Вход с корректным паролем:
✓ Успешный вход: User: alice, Logged in: True

Ыход из системы (Logout):
✓ После выхода: User: alice, Logged in: False
```

Это показывает **как свойства защищают данные** через валидацию!

---

## 7. Частые ошибки новичков

### ? Ошибка 1: Забыли инициализировать поле

```csharp
public class Person
{
    public string Name;  // null по умолчанию!
}

Person p = new Person();
Console.WriteLine(p.Name.Length);  //  NullReferenceException!
```

**Решение**: Инициализируйте в конструкторе или используйте свойства с значениями по умолчанию.

### ? Ошибка 2: Публичное поле с вредным значением

```csharp
public class User
{
    public int Age;  //  Публичное поле — опасно!
}

User u = new User();
u.Age = -100;  //  Никто не проверил!
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
Person p;  // Переменная объявлена, но объект не создан
// p.Name = "Alice";  //  NullReferenceException!

Person p = new Person();  //  ПРАВИЛЬНО
p.Name = "Alice";
```

---

## ФИНАЛЬНЫЙ ЧЕК-ЛИСТ для собеседования

### На тему Классы и конструкторы вы должны знать:

**Что такое класс?**

- Краткий ответ: Класс — шаблон (чертеж) данных и поведения; объект — конкретный экземпляр этого шаблона.
- Класс это **шаблон** (чертёж) для создания объектов
- Объект это **конкретный экземпляр** класса

  **Конструкторы:**

- Краткий ответ: Конструкторы автоматически инициализируют объект; перегрузка даёт разные способы создания; `this(...)` вызывает другой конструктор того же класса; `base(...)` — конструктор родителя.
- Вызываются **автоматически** при создании объекта
- Используются для **инициализации полей**
- Можно создавать **несколько конструкторов** (перегрузка)
-     his(...) вызывает другой конструктор того же класса
- ase(...) вызывает конструктор родительского класса

  **Поля vs Свойства:**

- Краткий ответ: Поле — простая переменная; Свойство — управляемый доступ (get/set) с валидацией и инкапсуляцией.
- **Поле** обычная переменная
- **Свойство** управляемый доступ с get и set
- Используйте свойства для **валидации** и **инкапсуляции**

  **Модификаторы доступа:**

- Краткий ответ: public видно везде; private — только внутри класса; protected — в наследниках; internal — внутри проекта.
- public видно везде
- private видно только в этом классе
- protected видно в наследниках
- internal видно в проекте

  **Static:**

- Краткий ответ: static — общее для всех объектов (счётчики, конфигурация, утилиты); не для данных конкретного объекта.
- static = **общее для всех объектов**
- Используется для **счётчиков, конфигурации, утилит**
- НЕ используется для **данных конкретного объекта**
- static методы вызывают через имя класса: Calculator.Add()

  **Инкапсуляция (Encapsulation):**

- Краткий ответ: Скрывайте внутренние данные (private) и предоставляйте безопасный доступ через свойства.
- Скрывайте **внутренние данные** (private)
- Предоставляйте **управляемый доступ** через свойства
- Это позволяет **изменять реализацию** без изменения интерфейса

  **DRY принцип:**

- Краткий ответ: Не повторяйте код (Don't Repeat Yourself). Используйте `this(...)` и `base(...)` для переиспользования логики в конструкторах.

---

## Следующие шаги

1. Запустите **Program.cs** и посмотрите результаты
2. Создайте свой класс (например, Manager наследующий Employee)
3. Поэкспериментируйте с модификаторами доступа
4. Добавьте валидацию в свойства
5. Переходите на **Topic 2 (Interfaces)** естественное расширение классов

```

```
