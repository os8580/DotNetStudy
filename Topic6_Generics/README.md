# Topic6 — Обобщения (Generics) (Полный курс для начинающих)

## Цель

Понять, как использовать обобщенные типы (Generics), почему они важны, и как с их помощью писать переиспользуемый код.

---

### Для полного новичка: быстрый маршрут (10–15 минут)

- Прочитайте разделы: "Что такое Generics?", "Простые примеры Generics".
- Пробежитесь по "Ограничения (Constraints)": `class`, `struct`, `new()`, базовый класс/интерфейс.
- Запустите Program.cs и изучите разницу между `OldBox` и `GenericBox<T>`.
- Вернитесь к чек-листу в конце: вопросы связаны напрямую с этими разделами и примерами.

## 1. Что такое Generics? (Для самых начинающих)

### Аналогия из жизни

```
Коробка с надписью "КОРОБКА" может хранить что угодно:
- Книги? Да!
- Игрушки? Да!
- Электроника? Да!
- Но я не знаю, что там лежит, пока не открою!

Обобщенная коробка <T> с ярлычком (String/int/User):
- Коробка<int> хранит только целые числа
- Коробка<string> хранит только строки
- Я знаю, что там лежит, без открытия!
```

### В программировании:

Generics позволяют писать код, который работает с **любой тип**, но **безопасно**.

```csharp
// БЕЗ Generics (опасно)
public class Container
{
    private object value;  // object может быть чем угодно!

    public void Set(object val) => value = val;
    public object Get() => value;
}

Container container = new Container();
container.Set(42);
int number = (int)container.Get();  // ? Нужно кастовать! Может упасть!

container.Set("Hello");
int wrongCast = (int)container.Get();  // ?? InvalidCastException!

// С Generics (безопасно)
public class Container<T>
{
    private T value;

    public void Set(T val) => value = val;
    public T Get() => value;
}

Container<int> intContainer = new Container<int>();
intContainer.Set(42);
int number = intContainer.Get();  // ? Безопасно, без каста

Container<string> stringContainer = new Container<string>();
stringContainer.Set("Hello");
string text = stringContainer.Get();  // ? Безопасно
```

---

## 2. Простые примеры Generics

### Обобщенный класс

```csharp
// Generic класс с параметром T
public class Box<T>
{
    private T content;

    public void Put(T item)
    {
        content = item;
    }

    public T Get()
    {
        return content;
    }

    public void Show()
    {
        Console.WriteLine($"Content: {content}");
    }
}

// Использование
var intBox = new Box<int>();
intBox.Put(100);
Console.WriteLine(intBox.Get());  // 100

var stringBox = new Box<string>();
stringBox.Put("Hello");
Console.WriteLine(stringBox.Get());  // Hello

var userBox = new Box<User>();
userBox.Put(new User { Name = "Alice" });
Console.WriteLine(userBox.Get().Name);  // Alice
```

### Обобщенный метод

```csharp
public class Utility
{
    // Generic метод
    public static void PrintArray<T>(T[] array)
    {
        foreach (T item in array)
        {
            Console.WriteLine(item);
        }
    }

    // Swap два элемента
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

// Использование
int[] intArray = { 1, 2, 3 };
Utility.PrintArray(intArray);

string[] stringArray = { "a", "b", "c" };
Utility.PrintArray(stringArray);

int x = 5, y = 10;
Utility.Swap(ref x, ref y);
Console.WriteLine($"{x}, {y}");  // 10, 5
```

---

## 3. Ограничения (Constraints)

Иногда нужно ограничить, какие типы можно использовать:

### where T : class (только классы)

```csharp
public class Repository<T> where T : class  // T должен быть классом
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item);
}

// ? OK — User это класс
Repository<User> userRepo = new Repository<User>();

// ? Ошибка — int это struct (значимый тип)
// Repository<int> intRepo = new Repository<int>();
```

### where T : struct (только структуры)

```csharp
public class Validator<T> where T : struct  // T должен быть struct
{
    public bool IsValid(T value)
    {
        return !value.Equals(default(T));
    }
}

// ? OK — int это struct
Validator<int> intValidator = new Validator<int>();

// ? Ошибка — User это класс
// Validator<User> userValidator = new Validator<User>();
```

### where T : new() (должен иметь конструктор без параметров)

```csharp
public class Factory<T> where T : new()  // T должен иметь new()
{
    public T Create()
    {
        return new T();  // Можем вызвать конструктор
    }
}

public class MyClass { }

// ? OK — MyClass имеет конструктор без параметров
Factory<MyClass> factory = new Factory<MyClass>();
MyClass instance = factory.Create();

// ? Ошибка — User требует параметр в конструкторе
// public class User { public User(string name) { } }
// Factory<User> userFactory = new Factory<User>();
```

### where T : BaseType (наследует от типа)

```csharp
public class Animal { }
public class Dog : Animal { }

public class AnimalCage<T> where T : Animal  // T должен быть Animal или его наследником
{
    private List<T> animals = new List<T>();

    public void Add(T animal) => animals.Add(animal);
}

// ? OK — Dog наследует от Animal
AnimalCage<Dog> dogCage = new AnimalCage<Dog>();

// ? Ошибка — string не наследует от Animal
// AnimalCage<string> stringCage = new AnimalCage<string>();
```

### where T : IInterface (реализует интерфейс)

```csharp
public interface ILoggable
{
    void Log();
}

public class Logger
{
    public void LogItems<T>(List<T> items) where T : ILoggable
    {
        foreach (var item in items)
        {
            item.Log();  // ? Знаем, что есть метод Log()
        }
    }
}

public class User : ILoggable
{
    public string Name { get; set; }
    public void Log() => Console.WriteLine($"User: {Name}");
}

// ? OK
Logger logger = new Logger();
List<User> users = new List<User> { new User { Name = "Alice" } };
logger.LogItems(users);
```

### Несколько ограничений

```csharp
public class Repository<T>
    where T : class
    where T : IEntity  // T должен быть классом И реализовывать IEntity
{
    // ...
}
```

---

## 4. Практический пример: Generic Repository

```csharp
// Интерфейс для базовых сущностей
public interface IEntity
{
    int Id { get; set; }
}

// Модель данных
public class User : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Generic Repository
public class Repository<T> where T : class, IEntity
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
        Console.WriteLine($"Added {typeof(T).Name}: {item.Id}");
    }

    public T GetById(int id)
    {
        return items.FirstOrDefault(x => x.Id == id);
    }

    public List<T> GetAll()
    {
        return items;
    }

    public void Remove(int id)
    {
        var item = GetById(id);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine($"Removed {typeof(T).Name}: {id}");
        }
    }
}

// Использование
Repository<User> userRepo = new Repository<User>();

userRepo.Add(new User { Id = 1, Name = "Alice" });
userRepo.Add(new User { Id = 2, Name = "Bob" });

User user = userRepo.GetById(1);
Console.WriteLine(user?.Name);  // Alice

userRepo.Remove(1);
```

---

## 5. Generic List, Dictionary и другие коллекции

Уже знаете, что List, Dictionary — это Generic типы!

```csharp
// List<T> — Generic список
List<int> numbers = new List<int> { 1, 2, 3 };

// Dictionary<TKey, TValue> — Generic словарь
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "Alice", 30 },
    { "Bob", 25 }
};

// Можно создавать свои Generic коллекции
public class Stack<T>  // Generic стек
{
    private List<T> items = new List<T>();

    public void Push(T item) => items.Add(item);
    public T Pop() => items[items.Count - 1];
    public int Count => items.Count;
}
```

---

## 6. Covariance и Contravariance (продвинутая тема)

### Covariance (out T)

```csharp
public interface IProducer<out T>  // out = covariant
{
    T Produce();
}

public class StringProducer : IProducer<string>
{
    public string Produce() => "Hello";
}

// ? Covariance: IProducer<string> можно присвоить IProducer<object>
IProducer<object> producer = new StringProducer();

// Это работает потому что out гарантирует, что T только возвращается, не принимается
```

### Contravariance (in T)

```csharp
public interface IConsumer<in T>  // in = contravariant
{
    void Consume(T item);
}

public class ObjectConsumer : IConsumer<object>
{
    public void Consume(object item) => Console.WriteLine(item);
}

// ? Contravariance: IConsumer<object> можно присвоить IConsumer<string>
IConsumer<string> consumer = new ObjectConsumer();
consumer.Consume("Hello");  // Работает!
```

---

## 7. Частые ошибки новичков

### ? Ошибка 1: Забыли where T : new()

```csharp
public class Factory<T>
{
    public T Create()
    {
        return new T();  // ? Ошибка! T может не иметь конструктора
    }
}

// ? Правильно
public class Factory<T> where T : new()
{
    public T Create()
    {
        return new T();  // ? OK
    }
}
```

### ? Ошибка 2: Использование object вместо Generic

```csharp
public class Container
{
    private object value;  // ? Потеря типобезопасности

    public void Set(object val) => value = val;
    public object Get() => value;
}

// ? Правильно
public class Container<T>
{
    private T value;  // ? Типобезопасно

    public void Set(T val) => value = val;
    public T Get() => value;
}
```

### ? Ошибка 3: Слишком широкое ограничение

```csharp
// ? Слишком общий Repository
public class Repository<T> where T : class
{
    // Проблема: не знаем, есть ли Id или другие свойства
}

// ? Правильно — требуем IEntity
public class Repository<T> where T : class, IEntity
{
    public T GetById(int id) { /*...*/ }
}
```

---

## 8. Лучшие практики

? **DO:**

- Используйте Generics для написания переиспользуемого кода
- Указывайте осмысленные ограничения (constraints)
- Используйте Generic типы из System.Collections.Generic
- Читайте Generic параметры как переменные типов

? **DON'T:**

- Не используйте object если можно использовать Generic
- Не создавайте слишком сложные Generic типы для новичков
- Не забывайте про constraints когда нужно гарантировать свойства
- Не смешивайте Generic и non-generic версии одного класса

---

## Файлы в проекте:

- `GenericBox.cs` — простой Generic класс
- `GenericConstraintsDemo.cs` — примеры `Factory<T>`, `Repository<T>`, `BaseEntity`, `EntityRepository<T>`
- `Program.cs` — примеры использования

---

## 9. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ (Generics)

- Объясните своими словами: что такое Generics и чем они помогают новичку?
  - Ответ: Generics — способ параметризовать типы (например, List<T>), писать переиспользуемый и типобезопасный код без приведения типов и ошибок в рантайме.
- Покажите на мини‑примере: generic‑класс `Box<T>` и generic‑метод `Swap<T>()` — в чем разница?
  - Ответ: Класс параметризует весь тип (Box<T> хранит T), а метод — только операцию (Swap<T>(ref a, ref b) меняет местами любые T).
- Перечислите популярные ограничения (`class`, `struct`, `new()`, базовый класс, интерфейс) и приведите по одному простому случаю применения каждого.
  - Ответ: `class` — только ссылочные; `struct` — только значимые; `new()` — нужен конструктор без параметров; `BaseType` — наследование от базы; `IInterface` — требование реализации метода/контракта.
- Что гарантирует `where T : new()`? Почему не всегда стоит требовать конструктор без параметров?
  - Ответ: Гарантирует возможность `new T()`; избегайте, если типы создаются через фабрики/DI или не имеют пустого конструктора.
- Что вернет `default(T)` для `int` и для `string`? Почему это важно?
  - Ответ: Для `int` — 0; для `string` — null. Важно при инициализации, проверках на null и работе с обобщенными структурами/классами.
- Объясните понятия `out` (ковариантность) и `in` (контравариантность) простыми словами.
  - Ответ: `out` — тип только «производится» (можно вернуть более конкретный тип как базовый); `in` — тип только «потребляется» (можно принять базовый вместо конкретного).
- Почему лучше использовать `List<T>` и `Dictionary<TKey,TValue>` вместо `ArrayList` и `object`?
  - Ответ: Типобезопасность, проверка на этапе компиляции, отсутствие boxing/unboxing и приведения, лучше производительность.
- Чем Generics помогают производительности и безопасности типов (подсказка: без boxing/unboxing и без кастов)?
  - Ответ: Устраняют лишние преобразования типов, позволяют JIT оптимизировать код, предотвращают InvalidCastException до запуска.
- Назовите 3 частые ошибки с Generics у новичков и как их исправить (например, забытый `new()`, слишком широкие ограничения, использование `object`).
  - Ответ: 1) Нет `new()` — добавить ограничение или фабрику; 2) `where T : class` слишком широко — уточнить базу/интерфейс; 3) Использование `object` — заменить на Generic тип.
- Спроектируйте простой `Repository<T>` с ограничением на базовый тип/интерфейс и 2–3 метода.
  - Ответ: `Repository<T> where T : BaseEntity/IEntity` с методами `Add(T)`, `GetById(int)`, `IEnumerable<T> GetAll()`.
