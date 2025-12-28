# Topic5 — LINQ (Язык запросов, примеры и советы) (Полный курс для начинающих)

## Цель
Понять принципы LINQ, основные операции, и отличие между отложенным и немедленным выполнением запросов. После прочтения вы будете писать красивые и эффективные запросы к данным.

---

## 1. Что такое LINQ? (Для самых начинающих)

### Аналогия
```
Без LINQ: "Дай мне список всех чисел больше 5 и верни только их удвоенные значения"
Нужно писать циклы вручную — много кода, много ошибок!

С LINQ: numbers.Where(n => n > 5).Select(n => n * 2)
Одна строка! Читается как предложение на английском!
```

### В программировании:
LINQ (Language Integrated Query) — это встроенный в C# язык запросов. Он работает с любыми коллекциями (`IEnumerable<T>`).

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// БЕЗ LINQ (много кода, много ошибок)
List<int> result = new List<int>();
foreach (var n in numbers)
{
    if (n > 5)
    {
        result.Add(n * 2);
    }
}
// result = { 12, 14, 16, 18, 20 }

// С LINQ (одна строка!)
var result2 = numbers
    .Where(n => n > 5)      // Фильтрация: только n > 5
    .Select(n => n * 2);    // Трансформация: удвоить каждое

// result2 = { 12, 14, 16, 18, 20 }
```

---

## 2. Отложенное vs Немедленное выполнение (ОЧЕНЬ ВАЖНО!)

### Отложенное выполнение (Deferred Execution)
Большинство LINQ операций **не выполняются сразу**!

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// ? DEFERRED: запрос еще не выполнен!
var query = numbers.Where(n => n > 3);

// Запрос выполнится только когда мы:
// 1) Итерируем (foreach)
foreach (var n in query)
{
    Console.WriteLine(n);  // Здесь выполнится Where
}

// 2) Вызовем метод материализации
List<int> list = query.ToList();       // Выполнится
int[] array = query.ToArray();         // Выполнится
int count = query.Count();             // Выполнится
int first = query.First();             // Выполнится
bool any = query.Any();                // Выполнится

// Пример из жизни:
// query = это "приказ выполнить фильтрацию когда понадобится"
// foreach / ToList() = это "ВЫПОЛНИ ПРИКАЗ СЕЙЧАС!"
```

### Пример: почему это важно?

```csharp
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };

// Создаем запрос (НЕ выполняется!)
var query = names.Where(n => {
    Console.WriteLine($"Проверяю: {n}");  // Это напечатается позже!
    return n.Length > 3;
});

Console.WriteLine("Запрос создан!");
// Вывод: "Запрос создан!" (Where еще не выполнен!)

Console.WriteLine("\nТеперь итерируем:");
foreach (var name in query)
{
    Console.WriteLine($"Результат: {name}");
}

// Вывод:
// Запрос создан!
// 
// Теперь итерируем:
// Проверяю: Alice
// Проверяю: Bob
// Результат: Bob
// Проверяю: Charlie
// Результат: Charlie
```

### Немедленное выполнение (Immediate Execution)

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// ? IMMEDIATE: выполняется СЕЙЧАС!
List<int> result = numbers.Where(n => n > 3).ToList();  // Выполнилось!

// Теперь result = { 4, 5 } (готовая коллекция)

// Даже если изменим исходный список, result не изменится
numbers.Add(6);
numbers.Add(7);
// result все еще { 4, 5 } — это snapshot!

// В отличие от отложенного выполнения:
var query = numbers.Where(n => n > 3);  // Пока не выполнен
numbers.Add(100);
// Когда выполним query, уже будет 100!
```

### Практическое правило:

```csharp
// Хотим работать с живыми данными (актуальными)? ? Отложенное
IEnumerable<User> activeUsers = users.Where(u => u.IsActive);

// Хотим snapshot (результат больше не изменится)? ? Немедленное
List<User> activeUsersSnapshot = users.Where(u => u.IsActive).ToList();
```

---

## 3. Основные операторы LINQ

### Where — Фильтрация

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Только четные числа
var even = numbers.Where(n => n % 2 == 0);  // { 2, 4, 6, 8, 10 }

// Числа больше 5
var greaterThan5 = numbers.Where(n => n > 5);  // { 6, 7, 8, 9, 10 }

// Несколько условий
var result = numbers.Where(n => n > 3 && n < 8);  // { 4, 5, 6, 7 }
```

### Select — Трансформация

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Удвоить каждое число
var doubled = numbers.Select(n => n * 2);  // { 2, 4, 6, 8, 10 }

// Превратить числа в строки
var strings = numbers.Select(n => $"Number: {n}");  // { "Number: 1", "Number: 2", ... }

// Работает с объектами
List<User> users = new List<User> 
{ 
    new User { Id = 1, Name = "Alice" },
    new User { Id = 2, Name = "Bob" }
};

var names = users.Select(u => u.Name);  // { "Alice", "Bob" }
var ids = users.Select(u => u.Id);      // { 1, 2 }

// Создавать новые объекты
var userSummaries = users.Select(u => new 
{
    FullInfo = $"{u.Id}: {u.Name}",
    NameLength = u.Name.Length
});
```

### OrderBy / OrderByDescending — Сортировка

```csharp
List<int> numbers = new List<int> { 5, 2, 8, 1, 9, 3 };

// Сортировка по возрастанию
var ascending = numbers.OrderBy(n => n);          // { 1, 2, 3, 5, 8, 9 }

// Сортировка по убыванию
var descending = numbers.OrderByDescending(n => n);  // { 9, 8, 5, 3, 2, 1 }

// С объектами
List<User> users = new List<User>
{
    new User { Name = "Charlie", Age = 25 },
    new User { Name = "Alice", Age = 30 },
    new User { Name = "Bob", Age = 22 }
};

var byName = users.OrderBy(u => u.Name);      // Alice, Bob, Charlie
var byAge = users.OrderBy(u => u.Age);        // Bob(22), Charlie(25), Alice(30)
var byAgeDesc = users.OrderByDescending(u => u.Age);  // Alice(30), Charlie(25), Bob(22)
```

### Take / Skip — Пагинация

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Первые 3
var first3 = numbers.Take(3);          // { 1, 2, 3 }

// Пропустить первые 2
var skipFirst2 = numbers.Skip(2);      // { 3, 4, 5, 6, 7, 8, 9, 10 }

// Пагинация: страница 2 по 3 элемента
int pageSize = 3;
int pageNumber = 2;  // Вторая страница (индекс с нуля)
var page = numbers
    .Skip(pageNumber * pageSize)       // Пропустить элементы предыдущих страниц
    .Take(pageSize);                   // Взять только 3 элемента
// { 7, 8, 9 }
```

### GroupBy — Группировка

```csharp
List<User> users = new List<User>
{
    new User { Name = "Alice", Department = "IT" },
    new User { Name = "Bob", Department = "IT" },
    new User { Name = "Charlie", Department = "HR" },
    new User { Name = "Diana", Department = "HR" }
};

// Группировать по отделу
var grouped = users.GroupBy(u => u.Department);

// Результат:
// IT: { Alice, Bob }
// HR: { Charlie, Diana }

foreach (var group in grouped)
{
    Console.WriteLine($"{group.Key}:");
    foreach (var user in group)
    {
        Console.WriteLine($"  - {user.Name}");
    }
}

// Подсчитать в каждой группе
var groupCounts = users
    .GroupBy(u => u.Department)
    .Select(g => new 
    { 
        Department = g.Key, 
        Count = g.Count() 
    });
// { {Department: "IT", Count: 2}, {Department: "HR", Count: 2} }
```

### FirstOrDefault / LastOrDefault — Получить элемент

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Первый элемент
int first = numbers.First();           // 1
int firstEven = numbers.First(n => n % 2 == 0);  // 2

// Первый элемент или null (если не найден)
int? firstOrNull = numbers.FirstOrDefault();     // 1
int? firstEvenOrNull = numbers.FirstOrDefault(n => n > 10);  // null

// Последний элемент
int last = numbers.Last();             // 5
int lastEven = numbers.Last(n => n % 2 == 0);   // 4
```

### Any / All — Проверка условия

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Есть ли хотя бы один элемент, удовлетворяющий условию?
bool hasEven = numbers.Any(n => n % 2 == 0);     // true
bool hasNegative = numbers.Any(n => n < 0);      // false

// Все ли элементы удовлетворяют условию?
bool allPositive = numbers.All(n => n > 0);      // true
bool allEven = numbers.All(n => n % 2 == 0);     // false
```

### Count — Подсчет

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Всего элементов
int total = numbers.Count();           // 10

// С условием (эффективнее, чем Where().Count())
int evenCount = numbers.Count(n => n % 2 == 0);  // 5

// ? Неправильно (неэффективно)
int badCount = numbers.Where(n => n % 2 == 0).Count();

// ? Правильно (быстрее)
int goodCount = numbers.Count(n => n % 2 == 0);
```

---

## 4. Цепочки операций (Method Chaining)

Операции можно комбинировать:

```csharp
List<User> users = new List<User>
{
    new User { Name = "Alice", Age = 30, Department = "IT" },
    new User { Name = "Bob", Age = 25, Department = "IT" },
    new User { Name = "Charlie", Age = 35, Department = "HR" },
    new User { Name = "Diana", Age = 22, Department = "HR" }
};

// Сложный запрос
var result = users
    .Where(u => u.Age > 23)               // Только > 23
    .OrderBy(u => u.Department)           // Сортировать по отделу
    .ThenBy(u => u.Name)                  // Потом по имени
    .Select(u => new { u.Name, u.Age })   // Выбрать только Name и Age
    .Take(3);                             // Первые 3
```

---

## 5. Практический пример для QA

```csharp
public class WebElement
{
    public string TagName { get; set; }
    public string Text { get; set; }
    public bool Displayed { get; set; }
    public int Width { get; set; }
}

List<WebElement> elements = new List<WebElement>
{
    new WebElement { TagName = "button", Text = "Click me", Displayed = true, Width = 100 },
    new WebElement { TagName = "input", Text = "", Displayed = false, Width = 200 },
    new WebElement { TagName = "button", Text = "Submit", Displayed = true, Width = 150 },
    new WebElement { TagName = "link", Text = "Home", Displayed = true, Width = 80 }
};

// Найти все видимые кнопки
var visibleButtons = elements
    .Where(e => e.Displayed && e.TagName == "button")
    .Select(e => e.Text);
// { "Click me", "Submit" }

// Сгруппировать по типу элемента и подсчитать
var groupedByTag = elements
    .GroupBy(e => e.TagName)
    .Select(g => new { Tag = g.Key, Count = g.Count() });
// { {Tag: "button", Count: 2}, {Tag: "input", Count: 1}, {Tag: "link", Count: 1} }

// Топ-2 элемента по ширине
var widestElements = elements
    .OrderByDescending(e => e.Width)
    .Take(2);

// Проверить, есть ли видимые кнопки
bool hasVisibleButtons = elements.Any(e => e.TagName == "button" && e.Displayed);
```

---

## 6. Частые ошибки новичков

### ? Ошибка 1: Забыли материализовать (ToList)
```csharp
List<User> users = /* ... */;

var activeUsers = users.Where(u => u.IsActive);
users.RemoveAll(u => !u.IsActive);

foreach (var user in activeUsers)
{
    // ? Ошибка! activeUsers зависит от исходного списка
    // После Remove, activeUsers может быть пустой!
}

// ? Правильно
var activeUsers = users.Where(u => u.IsActive).ToList();
```

### ? Ошибка 2: Неэффективная цепочка
```csharp
// ? Неэффективно (два прохода по данным)
int count = numbers.Where(n => n > 5).Count();

// ? Эффективно (один проход)
int count = numbers.Count(n => n > 5);
```

### ? Ошибка 3: Множественная материализация
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

var query = numbers.Where(n => n > 2);

int count = query.Count();      // Выполнение 1
var list = query.ToList();      // Выполнение 2
bool any = query.Any();         // Выполнение 3

// ? Неэффективно! Where выполняется 3 раза!

// ? Правильно
var snapshot = query.ToList();  // Одно выполнение
int count = snapshot.Count;
bool any = snapshot.Any();
```

---

## 7. Лучшие практики

? **DO:**
- Используйте `Count(predicate)` вместо `Where(...).Count()`
- Фильтруйте (Where) перед трансформацией (Select)
- Материализуйте (ToList) если нужен snapshot
- Читайте LINQ как предложение на английском

? **DON'T:**
- Не забывайте про отложенное выполнение
- Не материализуйте без нужды (ToList, ToArray)
- Не смешивайте LINQ с циклами без причины
- Не создавайте сложные запросы в одной линии (разбейте на несколько)

---

## Файлы в проекте:
- `Program.cs` — примеры LINQ запросов
- `WebElement.cs` — модель элемента страницы
