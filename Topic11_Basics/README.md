# Topic11 — Основы программирования (полный обзор и восстановление памяти)

## Цель

Вспомнить и закрепить все основные концепции C#, которые вы изучили в Topics 1-10. Этот файл — путеводитель и шпаргалка для новичка.

---

### Для полного новичка: быстрый маршрут

- **Это итоговая тема** — обзор всех Topics 1-10. Не ожидайте новых концепций, только повторение.
- **Прочитайте разделы по порядку:** типы данных, строки, операторы, классы, наследование, интерфейсы, коллекции, generics, исключения, value/ref типы, null-операторы, DateTime.
- **Запустите Program.cs** и посмотрите живые примеры: `dotnet run`
- **Используйте как шпаргалку:** при забывчивости о синтаксисе или использовании.
- **Вернитесь к чек-листу** (конец документа): 10 вопросов для проверки понимания.

---

## Содержание

1. [Основные типы данных](#1-основные-типы-данных)
2. [Работа со строками](#2-работа-со-строками)
3. [Операторы и управление потоком](#3-операторы-и-управление-потоком)
4. [Классы и объекты](#4-классы-и-объекты)
5. [Наследование и полиморфизм](#5-наследование-и-полиморфизм)
6. [Интерфейсы и DI](#6-интерфейсы-и-di)
7. [Коллекции и LINQ](#7-коллекции-и-linq)
8. [Обобщения (Generics)](#8-обобщения-generics)
9. [Исключения и ресурсы](#9-исключения-и-ресурсы)
10. [Значимые и ссылочные типы](#10-значимые-и-ссылочные-типы)
11. [Null-операторы](#11-null-операторы)
12. [DateTime и TimeSpan](#12-datetime-и-timespan)
13. [Лучшие практики и принципы](#13-лучшие-практики-и-принципы)

---

## 1. Основные типы данных

### Встроенные типы

```csharp
// Целые числа
int age = 25;              // -2.1 млрд до 2.1 млрд
long bigNumber = 9999999999;  // Очень большие числа

// Дробные числа
double price = 19.99;      // Точность ~15 цифр
decimal money = 19.99m;    // Точность для денег (28 цифр)

// Логические значения
bool isActive = true;
bool isDeleted = false;

// Символы
char letter = 'A';

// Строки
string name = "Alice";

// Дата и время
DateTime now = DateTime.Now;
TimeSpan duration = TimeSpan.FromHours(2);
```

### Классы vs Структуры

```csharp
// class — ссылочный тип (на HEAP), может быть null
public class User
{
    public string Name { get; set; }
}

User user1 = null;  // OK
User user2 = new User();

// struct — значимый тип (на STACK), не может быть null (но может быть int?)
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

Point p1 = new Point();  // Всегда инициализирован
Point? p2 = null;  // Nullable struct

// Правило: используй class по умолчанию, struct для малых данных
```

### object и var

```csharp
// object — базовый тип для всего в .NET
object anything = 42;     // Может быть int
object something = "text"; // Может быть string

// var — автоматический вывод типа (из контекста)
var number = 42;      // Компилятор определит: это int
var text = "hello";   // Это string

// var только для локальных переменных, не для параметров/свойств
var collection = new List<int>();  // Это List<int>
```

---

## 2. Работа со строками

### Основные методы

```csharp
string text = "Hello, World!";

// Длина
int length = text.Length;  // 13

// Преобразование
string upper = text.ToUpper();      // "HELLO, WORLD!"
string lower = text.ToLower();      // "hello, world!"

// Поиск
bool contains = text.Contains("World");      // true
int index = text.IndexOf("World");           // 7
bool startsWith = text.StartsWith("Hello");  // true
bool endsWith = text.EndsWith("!");          // true

// Извлечение части
string sub = text.Substring(0, 5);  // "Hello"

// Замена
string replaced = text.Replace("World", "C#");  // "Hello, C#!"

// Разделение на части
string[] parts = "a,b,c".Split(',');  // { "a", "b", "c" }

// Удаление пробелов
string trimmed = "  hello  ".Trim();  // "hello"
```

### Форматирование и интерполяция

```csharp
string name = "Alice";
int age = 30;

// Интерполяция (рекомендуется)
string greeting = $"Привет, {name}! Тебе {age} лет";

// Форматирование
string formatted = string.Format("Привет, {0}! Тебе {1} лет", name, age);

// С форматом
decimal price = 19.99m;
string priceText = $"Цена: {price:C}";  // Цена: ₽19.99
string percentage = $"Прогресс: {0.75:P}";  // Прогресс: 75.00%
```

### StringBuilder для частых изменений

```csharp
// ❌ Неэффективно (строка неизменяемая!)
string result = "";
for (int i = 0; i < 1000; i++)
{
    result += i + ", ";  // Создает новую строку 1000 раз!
}

// ✅ Эффективно
var sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    sb.Append(i).Append(", ");  // Модифицирует существующий буфер
}
string result = sb.ToString();
```

### Проверка пустых строк

```csharp
string text = null;
string empty = "";
string whitespace = "   ";

// Проверка на null или пустую
if (string.IsNullOrEmpty(text))  // true если null или ""
{
    Console.WriteLine("Текст пустой");
}

// Проверка на null, пустую или только пробелы
if (string.IsNullOrWhiteSpace(whitespace))  // true если null, "", или только пробелы
{
    Console.WriteLine("Текст не содержит символов");
}
```

---

## 3. Операторы и управление потоком

### Условные операторы

```csharp
int age = 18;

// if-else
if (age >= 18)
{
    Console.WriteLine("Совершеннолетний");
}
else if (age >= 13)
{
    Console.WriteLine("Подросток");
}
else
{
    Console.WriteLine("Ребенок");
}

// Тернарный оператор (краткий if-else)
string category = age >= 18 ? "Взрослый" : "Не взрослый";

// switch (для многих вариантов)
string day = "Monday";
string message = day switch
{
    "Monday" => "Начало недели",
    "Friday" => "Почти выходные!",
    "Saturday" or "Sunday" => "Выходной!",
    _ => "Обычный день"
};
```

### Логические операторы

```csharp
bool a = true;
bool b = false;

Console.WriteLine(a && b);   // AND: false (оба должны быть true)
Console.WriteLine(a || b);   // OR: true (хотя бы один true)
Console.WriteLine(!a);       // NOT: false (инверсия)

// Практический пример
if (age >= 18 && hasLicense)  // Оба условия должны быть true
{
    Console.WriteLine("Можно водить");
}
```

### Циклы

```csharp
// for — когда знаем количество итераций
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);  // 0, 1, 2, 3, 4
}

// foreach — для итерации по коллекции
int[] numbers = { 1, 2, 3, 4, 5 };
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// while — пока условие истинно
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}

// do-while — выполнится хотя бы один раз
int x = 0;
do
{
    Console.WriteLine(x);
    x++;
} while (x < 5);

// break и continue
for (int i = 0; i < 10; i++)
{
    if (i == 3)
        break;  // Выйти из цикла
    if (i == 1)
        continue;  // Пропустить итерацию
    Console.WriteLine(i);  // 0, 2
}
```

---

## 4. Классы и объекты

### Создание класса

```csharp
public class User
{
    // Поля (данные)
    public string Name;
    private int age;  // private — только этот класс видит

    // Свойства (управляемый доступ)
    public int Age
    {
        get { return age; }
        set { age = value >= 0 ? value : 0; }  // Валидация
    }

    // Конструктор (инициализация)
    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Метод (действие)
    public void PrintInfo()
    {
        Console.WriteLine($"{Name}, {Age} лет");
    }
}

// Использование
User user = new User("Alice", 30);
user.PrintInfo();  // Alice, 30 лет
```

### Модификаторы доступа

```csharp
public class Example
{
    public string Public { get; set; }       // Везде
    private string Private { get; set; }     // Только этот класс
    protected string Protected { get; set; } // Этот класс и наследники
    internal string Internal { get; set; }   // В этой сборке
}
```

### Перегрузка методов (Overloading)

```csharp
public class Math
{
    // Одна версия для int
    public static int Add(int a, int b)
    {
        return a + b;
    }

    // Другая версия для double
    public static double Add(double a, double b)
    {
        return a + b;
    }
}

Math.Add(5, 3);      // Вызовет первую версию (int)
Math.Add(5.5, 3.5);  // Вызовет вторую версию (double)
```

### Static (статические члены)

```csharp
public class Counter
{
    public static int Total = 0;  // Один на все объекты!
    public int Value = 0;         // У каждого объекта свой

    public Counter()
    {
        Total++;  // Увеличиваем общий счетчик
    }
}

Counter c1 = new Counter();
Counter c2 = new Counter();

Console.WriteLine(Counter.Total);  // 2 (всех объектов)
Console.WriteLine(c1.Value);       // 0 (отдельный)
Console.WriteLine(c2.Value);       // 0 (отдельный)

// ❌ Минус: Static сложно тестировать, избегайте!
```

---

## 5. Наследование и полиморфизм

### Наследование от класса

```csharp
// Базовый класс
public class Animal
{
    public string Name { get; set; }

    public virtual void MakeSound()
    {
        Console.WriteLine("Some sound");
    }
}

// Класс-наследник
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}

// Использование
Animal dog = new Dog { Name = "Rex" };
dog.MakeSound();  // Woof! (вызовет переопределенный метод)
```

### abstract (абстрактные классы)

```csharp
// Нельзя создать напрямую
public abstract class Shape
{
    public abstract double GetArea();  // Должен быть реализован
}

// Реализуем
public class Circle : Shape
{
    public double Radius { get; set; }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

// Circle c = new Circle();  // OK
// Shape s = new Shape();    // Ошибка! abstract нельзя создать
```

---

## 6. Интерфейсы и DI

### Интерфейсы

```csharp
// Контракт (что должно быть)
public interface IRepository
{
    void Save(User user);
    User GetById(int id);
}

// Реализация
public class UserRepository : IRepository
{
    public void Save(User user)
    {
        Console.WriteLine($"Сохранили {user.Name}");
    }

    public User GetById(int id)
    {
        return new User("Alice", 30);
    }
}

// Использование через интерфейс
IRepository repo = new UserRepository();
repo.Save(new User("Bob", 25));
```

### Dependency Injection

```csharp
// Сервис зависит от интерфейса, а не от конкретного класса
public class UserService
{
    private IRepository repository;

    // Передаем через конструктор (DI)
    public UserService(IRepository repository)
    {
        this.repository = repository;
    }

    public void CreateUser(string name, int age)
    {
        var user = new User(name, age);
        repository.Save(user);
    }
}

// Использование
IRepository repo = new UserRepository();
UserService service = new UserService(repo);
service.CreateUser("Alice", 30);
```

---

## 7. Коллекции и LINQ

### Основные коллекции

```csharp
// List<T> — динамический массив
List<int> numbers = new List<int> { 1, 2, 3 };
numbers.Add(4);
numbers.Remove(1);

// Dictionary<K,V> — ключ-значение
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "Alice", 30 },
    { "Bob", 25 }
};
ages["Charlie"] = 35;
int age = ages["Alice"];  // 30

// HashSet<T> — уникальные значения
HashSet<int> unique = new HashSet<int> { 1, 2, 2, 3 };  // { 1, 2, 3 }

// Queue<T> — очередь (FIFO)
Queue<string> queue = new Queue<string>();
queue.Enqueue("Alice");
string first = queue.Dequeue();  // "Alice"

// Stack<T> — стек (LIFO)
Stack<string> stack = new Stack<string>();
stack.Push("Alice");
string last = stack.Pop();  // "Alice"
```

### LINQ

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Where — фильтрация
var evens = numbers.Where(n => n % 2 == 0);  // { 2, 4, 6, 8, 10 }

// Select — трансформация
var doubled = numbers.Select(n => n * 2);  // { 2, 4, 6, 8, ... }

// OrderBy — сортировка
var sorted = numbers.OrderByDescending(n => n);  // { 10, 9, 8, ... }

// GroupBy — группировка
var grouped = numbers.GroupBy(n => n % 2);  // Четные и нечетные

// Take/Skip — пагинация
var page1 = numbers.Take(3);           // { 1, 2, 3 }
var page2 = numbers.Skip(3).Take(3);   // { 4, 5, 6 }

// FirstOrDefault — первый или null
int first = numbers.FirstOrDefault(n => n > 5);  // 6

// Any/All — проверки
bool hasEven = numbers.Any(n => n % 2 == 0);      // true
bool allPositive = numbers.All(n => n > 0);       // true

// Материализация (выполнение)
List<int> result = numbers.Where(n => n > 5).ToList();  // Теперь это List
```

---

## 8. Обобщения (Generics)

### Простые примеры

```csharp
// Generic класс
public class Container<T>
{
    private T value;

    public void Set(T val) => value = val;
    public T Get() => value;
}

// Использование
Container<int> intContainer = new Container<int>();
intContainer.Set(42);
int number = intContainer.Get();  // 42

Container<string> stringContainer = new Container<string>();
stringContainer.Set("Hello");
string text = stringContainer.Get();  // "Hello"
```

### Generic с ограничениями

```csharp
// T должен быть классом
public class Repository<T> where T : class
{
    public void Add(T item) { }
}

// T должен реализовывать интерфейс
public class Logger<T> where T : ILoggable
{
    public void LogItem(T item)
    {
        item.Log();
    }
}

// T должен иметь конструктор без параметров
public class Factory<T> where T : new()
{
    public T Create() => new T();
}
```

---

## 9. Исключения и ресурсы

### Try-Catch-Finally

```csharp
try
{
    int result = 10 / int.Parse("0");  // Ошибка!
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Нельзя делить на ноль");
}
catch (FormatException ex)
{
    Console.WriteLine("Неправильный формат");
}
finally
{
    Console.WriteLine("Это выполнится ВСЕГДА");
}
```

### IDisposable и using

```csharp
public class FileHandler : IDisposable
{
    private StreamReader reader;

    public void Dispose()
    {
        reader?.Close();
        Console.WriteLine("Ресурс освобожден");
    }
}

// using гарантирует вызов Dispose()
using var handler = new FileHandler();
// Используем handler
// После выхода из блока вызовется Dispose()
```

### Пользовательские исключения

```csharp
public class InvalidUsernameException : Exception
{
    public InvalidUsernameException(string message) : base(message) { }
}

// Использование
throw new InvalidUsernameException("Username не может быть пустым");
```

---

## 10. Значимые и ссылочные типы

### Разница

```csharp
// Значимый (Value) — struct, int, double, bool
struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

Point p1 = new Point { X = 10, Y = 20 };
Point p2 = p1;  // Копируется VALUE
p2.X = 100;

Console.WriteLine(p1.X);  // 10 (не изменилось)
Console.WriteLine(p2.X);  // 100

// Ссылочный (Reference) — class, string
class User
{
    public string Name { get; set; }
}

User u1 = new User { Name = "Alice" };
User u2 = u1;  // Копируется ССЫЛКА
u2.Name = "Bob";

Console.WriteLine(u1.Name);  // "Bob" (изменилось!)
Console.WriteLine(u2.Name);  // "Bob"
```

### ref и out

```csharp
// ref — передача по ссылке
void Increment(ref int x)
{
    x++;
}

int number = 5;
Increment(ref number);
Console.WriteLine(number);  // 6

// out — выходной параметр
void Divide(int a, int b, out int quotient, out int remainder)
{
    quotient = a / b;
    remainder = a % b;
}

Divide(10, 3, out int q, out int r);
Console.WriteLine($"{q} остаток {r}");  // 3 остаток 1
```

### Nullable типы

```csharp
int? age = null;  // Может быть null

if (age.HasValue)
{
    Console.WriteLine($"Возраст: {age.Value}");
}

// Значение по умолчанию
int actualAge = age ?? 0;  // Если null, то 0
```

---

## 11. Null-операторы

### Null-conditional (?.

)

```csharp
User user = null;

// Безопасно
string name = user?.Name;  // null (не выбросит исключение)

User user2 = new User { Name = "Alice" };
name = user2?.Name;  // "Alice"
```

### Null-coalescing (??)

```csharp
string text = null;

// Значение по умолчанию
string display = text ?? "Unknown";  // "Unknown"

string text2 = "Hello";
display = text2 ?? "Unknown";  // "Hello"
```

### Null-coalescing assignment (??=)

```csharp
List<string> items = null;

// Инициализируем если null
items ??= new List<string>();
items.Add("item1");
```

---

## 12. DateTime и TimeSpan

### DateTime

```csharp
// Текущая дата и время
DateTime now = DateTime.Now;
DateTime today = DateTime.Today;

// Конкретный момент
DateTime date = new DateTime(2024, 3, 15, 14, 30, 0);

// Арифметика (возвращает НОВЫЙ DateTime)
DateTime tomorrow = date.AddDays(1);
DateTime nextHour = date.AddHours(1);

// Форматирование
string formatted = date.ToString("dd.MM.yyyy HH:mm:ss");

// Парсинг
DateTime parsed = DateTime.Parse("15.03.2024");
bool success = DateTime.TryParseExact("2024-03-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dt);

// Сравнение
if (tomorrow > today)
{
    Console.WriteLine("Завтра позже, чем сегодня");
}

// Только дата (без времени)
if (date.Date == today)
{
    Console.WriteLine("Одинаковые даты");
}
```

### TimeSpan

```csharp
// Создание
TimeSpan duration = new TimeSpan(2, 30, 45);  // 2:30:45
TimeSpan twoHours = TimeSpan.FromHours(2);
TimeSpan fiveDays = TimeSpan.FromDays(5);

// Компоненты
Console.WriteLine(duration.Hours);     // 2
Console.WriteLine(duration.TotalHours);  // 2.5125

// Арифметика
TimeSpan sum = TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30);
TimeSpan diff = TimeSpan.FromHours(2) - TimeSpan.FromMinutes(30);

// Разница между датами
DateTime start = new DateTime(2024, 3, 15);
DateTime end = new DateTime(2024, 3, 18);
TimeSpan span = end - start;  // 3 дня
Console.WriteLine(span.TotalDays);  // 3
```

### Stopwatch для производительности

```csharp
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

// Код...
System.Threading.Thread.Sleep(2000);

sw.Stop();

Console.WriteLine($"Прошло: {sw.ElapsedMilliseconds} мс");
Console.WriteLine($"Прошло: {sw.Elapsed.TotalSeconds} секунд");
```

---

## 13. Лучшие практики и принципы

### SOLID принципы

#### S — Single Responsibility Principle

```csharp
// ❌ Плохо: класс делает слишком много
public class User
{
    public void CreateUser() { }
    public void SaveToDatabase() { }
    public void SendEmail() { }
    public void ValidatePassword() { }
}

// ✅ Хорошо: каждый класс делает одно
public class UserCreator
{
    public void Create(User user) { }
}

public class UserRepository
{
    public void Save(User user) { }
}

public class EmailService
{
    public void Send(User user) { }
}

public class PasswordValidator
{
    public bool IsValid(string password) { }
}
```

#### O — Open/Closed Principle

```csharp
// ❌ Плохо: нужно изменять класс при добавлении нового платежа
public class PaymentProcessor
{
    public void Process(string type, decimal amount)
    {
        if (type == "CreditCard")
        {
            // код для кредитной карты
        }
        else if (type == "PayPal")
        {
            // код для PayPal
        }
    }
}

// ✅ Хорошо: открыт для расширения, закрыт для изменения
public interface IPaymentMethod
{
    void Process(decimal amount);
}

public class CreditCardPayment : IPaymentMethod
{
    public void Process(decimal amount) { }
}

public class PayPalPayment : IPaymentMethod
{
    public void Process(decimal amount) { }
}

public class PaymentProcessor
{
    public void Process(IPaymentMethod method, decimal amount)
    {
        method.Process(amount);
    }
}
```

#### L — Liskov Substitution Principle

```csharp
// ✅ Правильно: все наследники могут заменить родителя
public class Animal
{
    public virtual void Eat() { }
}

public class Dog : Animal
{
    public override void Eat() { }  // Вполне себе может есть
}

public class Cat : Animal
{
    public override void Eat() { }  // Может есть
}

// Этот код работает с ЛЮБЫМ Animal
void FeedAnimal(Animal animal)
{
    animal.Eat();  // Не важно, Dog или Cat
}
```

#### I — Interface Segregation Principle

```csharp
// ❌ Плохо: большой интерфейс
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

// ✅ Хорошо: маленькие интерфейсы
public interface IWorker
{
    void Work();
}

public interface INeedFood
{
    void Eat();
}

public interface INeedRest
{
    void Sleep();
}

public class Employee : IWorker, INeedFood, INeedRest
{
    public void Work() { }
    public void Eat() { }
    public void Sleep() { }
}
```

#### D — Dependency Inversion Principle

```csharp
// ❌ Плохо: зависимость от конкретного класса
public class UserService
{
    private MySQLDatabase database = new MySQLDatabase();  // Привязаны!
}

// ✅ Хорошо: зависимость от интерфейса
public interface IDatabase
{
    void Save(User user);
}

public class UserService
{
    private IDatabase database;  // Может быть любая реализация!

    public UserService(IDatabase database)
    {
        this.database = database;
    }
}
```

### DRY (Don't Repeat Yourself)

```csharp
// ❌ Плохо: повторение кода
public class ValidationHelper
{
    public bool ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;
        if (username.Length < 3)
            return false;
        return true;
    }

    public bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        if (!email.Contains("@"))
            return false;
        return true;
    }
}

// ✅ Хорошо: общий метод
public class ValidationHelper
{
    public bool IsNotEmpty(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public bool MinLength(string value, int length)
    {
        return value.Length >= length;
    }

    public bool Contains(string value, string substring)
    {
        return value.Contains(substring);
    }
}
```

### KISS (Keep It Simple, Stupid)

```csharp
// ❌ Слишком сложно
public class ComplexValidator
{
    public bool Validate(User user)
    {
        return (string.IsNullOrWhiteSpace(user.Name) == false)
            && (user.Age > 0 && user.Age < 150)
            && (!user.Email.Contains("@@") && user.Email.Contains("@"));
    }
}

// ✅ Проще
public class SimpleValidator
{
    public bool ValidateName(string name) => !string.IsNullOrWhiteSpace(name);
    public bool ValidateAge(int age) => age > 0 && age < 150;
    public bool ValidateEmail(string email) => email.Contains("@");

    public bool Validate(User user)
    {
        return ValidateName(user.Name)
            && ValidateAge(user.Age)
            && ValidateEmail(user.Email);
    }
}
```

---

## Краткая шпаргалка по темам

| Тема               | Когда использовать             | Главный класс       |
| ------------------ | ------------------------------ | ------------------- |
| **Классы**         | Создание объектов с поведением | `class`             |
| **Интерфейсы**     | Определение контракта          | `interface`         |
| **Наследование**   | Переиспользование кода         | `:`                 |
| **Полиморфизм**    | Вызов разного кода по типу     | `virtual/override`  |
| **Generics**       | Типобезопасные коллекции       | `<T>`               |
| **LINQ**           | Запросы к коллекциям           | `.Where().Select()` |
| **Исключения**     | Обработка ошибок               | `try-catch`         |
| **DateTime**       | Работа с датами                | `DateTime`          |
| **TimeSpan**       | Интервалы времени              | `TimeSpan`          |
| **null-операторы** | Безопасная работа с null       | `?.`, `??`          |

---

## Лучшие практики для QA/Automation

```csharp
// 1. Используйте Page Object Model
public class LoginPage
{
    public TextField UsernameField { get; private set; }
    public Button LoginButton { get; private set; }

    public void Login(string username, string password) { }
}

// 2. Используйте DI для сервисов
public class LoginTest
{
    private readonly IWebDriver driver;
    private readonly ILogger logger;

    public LoginTest(IWebDriver driver, ILogger logger)
    {
        this.driver = driver;
        this.logger = logger;
    }
}

// 3. Безопасная работа с null
string text = element?.Text ?? "Unknown";

// 4. Используйте TryParse для преобразований
if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
{
    // Используем date
}

// 5. Используйте Stopwatch для производительности
Stopwatch sw = Stopwatch.StartNew();
// Код...
sw.Stop();
Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(5));

// 6. Используйте using для ресурсов
using var driver = new ChromeDriver();
// Используем driver
// Автоматически закроется
```

---

## 14. ЧЕК-ЛИСТ ИТОГОВОЙ ПРОВЕРКИ ЗНАНИЙ 🎯

### Вопрос 1: Какие основные типы данных вы знаете и когда их использовать?

**Краткий ответ:** `int` (целые), `double`/`decimal` (дроби; decimal для денег), `bool` (true/false), `char` (символ), `string` (текст), `DateTime` (дата-время). Выбирайте в зависимости от того, что представляет данные.

### Вопрос 2: В чем разница между class и struct?

**Краткий ответ:** `class` — ссылочный тип (на heap, может быть null). `struct` — значимый тип (на stack, не может быть null). Использует class по умолчанию, struct только для малых однородных данных.

### Вопрос 3: Что такое ООП и какие его столпы?

**Краткий ответ:** ООП — объектно-ориентированное программирование. Столпы: **инкапсуляция** (скрытие деталей), **наследование** (переиспользование кода), **полиморфизм** (разные реализации одного интерфейса), **абстракция** (обобщение).

### Вопрос 4: Как работает наследование и почему оно нужно?

**Краткий ответ:** `class Child : Parent { }` — Child получает все от Parent. Нужно для переиспользования кода. `virtual` в родителе + `override` в ребенке позволяет заменять поведение.

### Вопрос 5: Что такое интерфейс и как его использовать с Dependency Injection?

**Краткий ответ:** Интерфейс — контракт (список методов/свойств). `class Service : IService { }` — реализует контракт. DI: внедрять интерфейс, не конкретный класс. `ServiceCollection` регистрирует сопоставления интерфейс → реализация.

### Вопрос 6: Какие операторы вы знаете и как их использовать?

**Краткий ответ:** Арифметические `+`, `-`, `*`, `/`; Сравнения `==`, `!=`, `<`, `>`; Логические `&&`, `||`, `!`; Присваивания `=`, `+=`, `-=`; Ternary `condition ? true_value : false_value`.

### Вопрос 7: Как управлять потоком программы (if, for, while)?

**Краткий ответ:** `if (condition) { }` — условное выполнение; `for (int i = 0; i < 10; i++) { }` — цикл с счетчиком; `while (condition) { }` — цикл пока условие верно; `foreach (var item in collection) { }` — перебор коллекции.

### Вопрос 8: Что такое коллекции и когда использовать List, Dictionary, HashSet?

**Краткий ответ:** `List<T>` — упорядоченный список; `Dictionary<K,V>` — пары ключ-значение; `HashSet<T>` — уникальные значения без порядка. LINQ: `Where`, `Select`, `GroupBy`, `Join` для обработки.

### Вопрос 9: Как обрабатывать исключения и почему это важно?

**Краткий ответ:** `try { } catch (Exception ex) { } finally { }` — перехватывает ошибки. `using` — гарантирует освобождение ресурсов. Ловите специфичные исключения, не общие.

### Вопрос 10: Как вы применяете все это в QA-автотестах?

**Краткий ответ:** Типы данных для тестовых данных, классы для Page Objects, интерфейсы для drivers, коллекции для хранения элементов, LINQ для фильтрации, исключения для проверки ошибок, DateTime для дат, null-операторы для безопасности.

---

## Файлы в проекте:

- `Program.cs` — примеры всех концепций
