# Topic4 — Коллекции (Полный курс для начинающих)

## Цель
Понять, как работают коллекции в C#, когда использовать каждую из них, и как выбрать правильную коллекцию для конкретной задачи.

---

## 1. Что такое коллекция? (Для самых начинающих)

### Аналогия
```
Переменная = коробка с одним предметом
int age = 30;

Коллекция = большой шкаф с множеством предметов
List<int> ages = new List<int> { 20, 25, 30, 35, 40 };
```

### Проблема без коллекций

```csharp
// Нужно хранить возрасты 5 человек
int age1 = 20;
int age2 = 25;
int age3 = 30;
int age4 = 35;
int age5 = 40;

// ? Неудобно! Много переменных!
// Что если 100 человек? 1000?

// Решение — коллекция!
List<int> ages = new List<int> { 20, 25, 30, 35, 40 };
// Все 5 значений в одной переменной!
```

---

## 2. Array (Массив) — Основы

### Создание массива

```csharp
// Массив из 5 целых чисел
int[] numbers = new int[5];  // По умолчанию заполнена нулями: { 0, 0, 0, 0, 0 }

// Массив с инициализацией
int[] ages = new int[] { 20, 25, 30, 35, 40 };  // 5 элементов

// Краткая форма (компилятор определит размер)
int[] scores = { 90, 85, 95, 88 };  // 4 элемента

// Массив строк
string[] names = { "Alice", "Bob", "Charlie" };

// Пустой массив
int[] empty = new int[0];
```

### Доступ к элементам

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// Индекс начинается с 0!
Console.WriteLine(numbers[0]);  // 10 (первый элемент)
Console.WriteLine(numbers[1]);  // 20 (второй элемент)
Console.WriteLine(numbers[4]);  // 50 (последний элемент)

// ? Ошибка: индекс вне границ
// Console.WriteLine(numbers[5]);  // IndexOutOfRangeException!

// Длина массива
int length = numbers.Length;  // 5

// Изменение элемента
numbers[0] = 100;
Console.WriteLine(numbers[0]);  // 100
```

### Итерация по массиву

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// For — с индексом
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine($"numbers[{i}] = {numbers[i]}");
}

// Foreach — без индекса (проще!)
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// Foreach с LINQ
numbers.ForEach(n => Console.WriteLine(n));
```

### Проблемы с массивом

```csharp
int[] numbers = new int[3] { 10, 20, 30 };

// ? Нельзя добавить элемент (размер фиксирован!)
// numbers[3] = 40;  // Ошибка!

// ? Нельзя удалить элемент (размер фиксирован!)

// Размер создается при создании и не меняется!
// Это неудобно для реальных приложений!
```

---

## 3. List<T> — Динамический массив (РЕКОМЕНДУЕТСЯ!)

### Создание List

```csharp
// Пустой список
List<int> numbers = new List<int>();

// С инициализацией
List<int> ages = new List<int> { 20, 25, 30, 35, 40 };

// С начальной емкостью (оптимизация)
List<string> names = new List<string>(10);  // Зарезервировано место для 10 элементов

// Из другой коллекции
int[] array = { 1, 2, 3 };
List<int> listFromArray = new List<int>(array);

// Из результата LINQ
List<int> evens = numbers.Where(n => n % 2 == 0).ToList();
```

### Основные методы

```csharp
List<int> numbers = new List<int> { 10, 20, 30 };

// Добавление элемента в конец
numbers.Add(40);
Console.WriteLine(numbers);  // { 10, 20, 30, 40 }

// Добавление элемента в конкретную позицию
numbers.Insert(1, 15);
Console.WriteLine(numbers);  // { 10, 15, 20, 30, 40 }

// Удаление элемента по значению
numbers.Remove(15);
Console.WriteLine(numbers);  // { 10, 20, 30, 40 }

// Удаление элемента по индексу
numbers.RemoveAt(0);
Console.WriteLine(numbers);  // { 20, 30, 40 }

// Очистка списка
numbers.Clear();
Console.WriteLine(numbers);  // { }

// Проверка наличия элемента
List<int> nums = new List<int> { 10, 20, 30 };
bool contains = nums.Contains(20);  // true

// Найти индекс элемента
int index = nums.IndexOf(20);  // 1

// Количество элементов
int count = nums.Count;  // 3

// Получить элемент
int first = nums[0];
int last = nums[nums.Count - 1];
```

### Пример для QA

```csharp
public class TestResults
{
    private List<string> results = new List<string>();
    
    public void AddResult(string testName, bool passed)
    {
        string result = passed ? $"? {testName}" : $"? {testName}";
        results.Add(result);
    }
    
    public void PrintResults()
    {
        Console.WriteLine("=== TEST RESULTS ===");
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
    
    public int GetPassedCount()
    {
        return results.Count(r => r.StartsWith("?"));
    }
}

// Использование
TestResults tr = new TestResults();
tr.AddResult("Login Test", true);
tr.AddResult("Logout Test", false);
tr.AddResult("Profile Test", true);
tr.PrintResults();
// ======================
// ? Login Test
// ? Logout Test
// ? Profile Test
```

---

## 4. Dictionary<TKey, TValue> — Ключ-значение

### Создание Dictionary

```csharp
// Пустой словарь
Dictionary<string, int> ages = new Dictionary<string, int>();

// С инициализацией
Dictionary<string, int> scores = new Dictionary<string, int>
{
    { "Alice", 90 },
    { "Bob", 85 },
    { "Charlie", 95 }
};

// Альтернативный синтаксис инициализации
Dictionary<string, string> cities = new Dictionary<string, string>
{
    ["USA"] = "New York",
    ["UK"] = "London",
    ["Russia"] = "Moscow"
};
```

### Основные методы

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "Alice", 30 },
    { "Bob", 25 }
};

// Добавление
ages.Add("Charlie", 35);

// Добавление или обновление (проще!)
ages["Diana"] = 28;  // Если ключа нет — добавит, если есть — обновит

// Получение значения
int aliceAge = ages["Alice"];  // 30

// ? Безопасное получение (если ключа нет)
if (ages.TryGetValue("Eve", out int eveAge))
{
    Console.WriteLine($"Eve возраст: {eveAge}");
}
else
{
    Console.WriteLine("Eve не найдена");
}

// Проверка наличия ключа
bool hasAlice = ages.ContainsKey("Alice");  // true

// Проверка наличия значения
bool hasAge30 = ages.ContainsValue(30);  // true (Alice)

// Удаление
ages.Remove("Bob");

// Очистка
ages.Clear();

// Количество элементов
int count = ages.Count;

// Итерация
foreach (var pair in ages)
{
    Console.WriteLine($"{pair.Key}: {pair.Value} лет");
}

// Только ключи
foreach (var key in ages.Keys)
{
    Console.WriteLine(key);
}

// Только значения
foreach (var value in ages.Values)
{
    Console.WriteLine(value);
}
```

### Пример для QA

```csharp
public class LoginCredentials
{
    private Dictionary<string, string> credentials = new Dictionary<string, string>
    {
        { "alice", "password123" },
        { "bob", "secure456" },
        { "charlie", "secret789" }
    };
    
    public bool Login(string username, string password)
    {
        if (credentials.TryGetValue(username, out string correctPassword))
        {
            return password == correctPassword;
        }
        return false;
    }
    
    public void RegisterUser(string username, string password)
    {
        credentials[username] = password;  // Добавит или обновит
    }
}

// Использование
LoginCredentials auth = new LoginCredentials();
Console.WriteLine(auth.Login("alice", "password123"));  // true
Console.WriteLine(auth.Login("alice", "wrong"));        // false

auth.RegisterUser("diana", "newpass");
Console.WriteLine(auth.Login("diana", "newpass"));      // true
```

---

## 5. HashSet<T> — Уникальные значения

### Создание HashSet

```csharp
// Пустой набор
HashSet<int> uniqueNumbers = new HashSet<int>();

// С инициализацией
HashSet<string> colors = new HashSet<string> { "Red", "Green", "Blue" };

// Из другой коллекции (удалит дубликаты!)
int[] numbersWithDuplicates = { 1, 2, 2, 3, 3, 3 };
HashSet<int> unique = new HashSet<int>(numbersWithDuplicates);
Console.WriteLine(string.Join(", ", unique));  // 1, 2, 3 (дубликаты удалены!)
```

### Основные методы

```csharp
HashSet<int> numbers = new HashSet<int> { 10, 20, 30 };

// Добавление (если уже есть — ничего не произойдет)
numbers.Add(20);  // Не добавится, так как уже есть
numbers.Add(40);  // Добавится

// Проверка наличия
bool contains = numbers.Contains(20);  // true

// Удаление
numbers.Remove(20);

// Очистка
numbers.Clear();

// Количество элементов
int count = numbers.Count;

// Итерация
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

### Операции множеств

```csharp
HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4 };
HashSet<int> set2 = new HashSet<int> { 3, 4, 5, 6 };

// Объединение (Union) — все элементы из обоих
set1.UnionWith(set2);
Console.WriteLine(string.Join(", ", set1));  // 1, 2, 3, 4, 5, 6

// Пересечение (Intersection) — только общие элементы
HashSet<int> s1 = new HashSet<int> { 1, 2, 3, 4 };
HashSet<int> s2 = new HashSet<int> { 3, 4, 5, 6 };
s1.IntersectWith(s2);
Console.WriteLine(string.Join(", ", s1));  // 3, 4

// Разность (Except) — элементы только из первого
HashSet<int> s3 = new HashSet<int> { 1, 2, 3, 4 };
HashSet<int> s4 = new HashSet<int> { 3, 4, 5, 6 };
s3.ExceptWith(s4);
Console.WriteLine(string.Join(", ", s3));  // 1, 2
```

### Пример для QA

```csharp
public class ClickedElements
{
    private HashSet<string> clickedIds = new HashSet<string>();
    
    public void ClickElement(string elementId)
    {
        clickedIds.Add(elementId);  // Дубликаты автоматически игнорируются
    }
    
    public bool WasClicked(string elementId)
    {
        return clickedIds.Contains(elementId);
    }
    
    public int UniqueClickCount()
    {
        return clickedIds.Count;  // Только уникальные клики!
    }
}

// Использование
ClickedElements tracker = new ClickedElements();
tracker.ClickElement("button1");
tracker.ClickElement("button1");  // Дубликат — не добавится
tracker.ClickElement("button2");
tracker.ClickElement("button1");  // Дубликат — не добавится

Console.WriteLine(tracker.UniqueClickCount());  // 2 (не 4!)
```

---

## 6. Queue<T> — Очередь (FIFO)

### Создание Queue

```csharp
// Пустая очередь
Queue<string> queue = new Queue<string>();

// С инициализацией
Queue<int> numbers = new Queue<int> { 10, 20, 30 };
```

### Основные методы

```csharp
Queue<string> queue = new Queue<string>();

// Добавление в конец очереди
queue.Enqueue("Alice");
queue.Enqueue("Bob");
queue.Enqueue("Charlie");

// Получение из начала (и удаление)
string first = queue.Dequeue();  // "Alice"

// Посмотреть первый без удаления
string next = queue.Peek();  // "Bob"

// Проверка наличия элементов
bool isEmpty = queue.Count == 0;

// Очистка
queue.Clear();
```

### FIFO — First In, First Out

```
Очередь как в магазине:
Alice приходит ? Встает в очередь ? Alice first
Bob приходит ? Встает после Alice
Charlie приходит ? Встает после Bob

Alice уходит (обслужили первой)
Bob становится первым
Charlie становится вторым
```

### Пример для QA

```csharp
public class TaskQueue
{
    private Queue<string> tasks = new Queue<string>();
    
    public void QueueTask(string taskName)
    {
        tasks.Enqueue(taskName);
        Console.WriteLine($"Задача добавлена: {taskName}");
    }
    
    public string ExecuteNextTask()
    {
        if (tasks.Count > 0)
        {
            return tasks.Dequeue();  // Первая в очереди — первая выполняется
        }
        return "Нет задач";
    }
    
    public void PrintQueue()
    {
        Console.WriteLine("Очередь задач:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"  - {task}");
        }
    }
}

// Использование
TaskQueue queue = new TaskQueue();
queue.QueueTask("Open page");
queue.QueueTask("Click button");
queue.QueueTask("Verify text");

string task = queue.ExecuteNextTask();  // "Open page" (первая)
task = queue.ExecuteNextTask();         // "Click button"
task = queue.ExecuteNextTask();         // "Verify text"
```

---

## 7. Stack<T> — Стек (LIFO)

### Создание Stack

```csharp
// Пустой стек
Stack<string> stack = new Stack<string>();

// С инициализацией
Stack<int> numbers = new Stack<int> { 10, 20, 30 };
```

### Основные методы

```csharp
Stack<string> stack = new Stack<string>();

// Добавление в вершину стека
stack.Push("Alice");
stack.Push("Bob");
stack.Push("Charlie");

// Получение с вершины (и удаление)
string top = stack.Pop();  // "Charlie"

// Посмотреть вершину без удаления
string next = stack.Peek();  // "Bob"

// Проверка наличия элементов
bool isEmpty = stack.Count == 0;

// Очистка
stack.Clear();
```

### LIFO — Last In, First Out

```
Стек как стопка тарелок:
Кладу Alice ? Стек: [Alice]
Кладу Bob ? Стек: [Alice, Bob]
Кладу Charlie ? Стек: [Alice, Bob, Charlie]

Беру сверху ? Charlie (последняя, которая положил)
Беру сверху ? Bob
Беру сверху ? Alice (первая, которую положил)
```

### Пример для QA

```csharp
public class BrowserHistory
{
    private Stack<string> history = new Stack<string>();
    
    public void VisitPage(string url)
    {
        history.Push(url);
        Console.WriteLine($"Перешли на: {url}");
    }
    
    public string GoBack()
    {
        if (history.Count > 0)
        {
            return history.Pop();  // Последняя страница
        }
        return "Нет истории";
    }
    
    public string CurrentPage()
    {
        if (history.Count > 0)
        {
            return history.Peek();  // Текущая страница
        }
        return "История пуста";
    }
}

// Использование
BrowserHistory browser = new BrowserHistory();
browser.VisitPage("Google");
browser.VisitPage("GitHub");
browser.VisitPage("StackOverflow");

Console.WriteLine(browser.CurrentPage());  // StackOverflow
browser.GoBack();  // Вернулись на GitHub
Console.WriteLine(browser.CurrentPage());  // GitHub
```

---

## 8. Сравнение коллекций

| Коллекция | Доступ | Добавление | Удаление | Уникальность | Когда использовать |
|-----------|--------|-----------|----------|------------|-------------------|
| **Array** | O(1) | Нет | Нет | Нет | Редко (размер известен) |
| **List<T>** | O(1) | O(n) | O(n) | Нет | **ВСЕ СЛУЧАИ** |
| **Dictionary<K,V>** | O(1) | O(1) | O(1) | Да (ключи) | Ключ-значение |
| **HashSet<T>** | O(1) | O(1) | O(1) | Да | Уникальные значения |
| **Queue<T>** | O(1) | O(1) | O(1) | Нет | FIFO очередь |
| **Stack<T>** | O(1) | O(1) | O(1) | Нет | LIFO стек |

---

## 9. IEnumerable<T> и IReadOnlyList<T>

### Почему это важно для API?

```csharp
// ? Плохо: возвращаем List (клиент может его изменить!)
public List<User> GetUsers()
{
    List<User> users = new List<User> { /*...*/ };
    return users;  // Клиент может удалить элементы!
}

// ? Хорошо: возвращаем интерфейс (клиент не может изменить!)
public IEnumerable<User> GetUsers()
{
    List<User> users = new List<User> { /*...*/ };
    return users;  // Клиент может только читать!
}

public IReadOnlyList<User> GetUsersList()
{
    List<User> users = new List<User> { /*...*/ };
    return users.AsReadOnly();  // Защита от изменений!
}
```

### Использование

```csharp
// IEnumerable — только итерация
public void PrintUsers(IEnumerable<User> users)
{
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}

// IReadOnlyList — итерация и доступ по индексу
public void PrintFirstUser(IReadOnlyList<User> users)
{
    if (users.Count > 0)
    {
        Console.WriteLine(users[0].Name);
    }
}
```

---

## 10. LINQ с коллекциями

### Фильтрация и трансформация

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Where — фильтрация
var evens = numbers.Where(n => n % 2 == 0);  // { 2, 4, 6, 8, 10 }

// Select — трансформация
var doubled = numbers.Select(n => n * 2);  // { 2, 4, 6, 8, ... }

// Комбинирование
var result = numbers
    .Where(n => n > 3)       // Только > 3
    .Select(n => n * n);     // Возвести в квадрат
// { 16, 25, 36, 49, 64, 81, 100 }
```

### GroupBy и Join

```csharp
List<User> users = new List<User>
{
    new User { Name = "Alice", Department = "IT", Salary = 100000 },
    new User { Name = "Bob", Department = "IT", Salary = 90000 },
    new User { Name = "Charlie", Department = "HR", Salary = 80000 }
};

// GroupBy — группировка
var grouped = users
    .GroupBy(u => u.Department)
    .Select(g => new 
    { 
        Department = g.Key, 
        Count = g.Count(),
        AvgSalary = g.Average(u => u.Salary)
    });

// Результат:
// { Department = "IT", Count = 2, AvgSalary = 95000 }
// { Department = "HR", Count = 1, AvgSalary = 80000 }
```

---

## 11. Частые ошибки новичков

### ? Ошибка 1: Индекс вне границ

```csharp
int[] array = { 1, 2, 3 };

// ? Ошибка: индексы идут от 0 до 2
// Console.WriteLine(array[3]);  // IndexOutOfRangeException!

// ? Правильно
if (index >= 0 && index < array.Length)
{
    Console.WriteLine(array[index]);
}
```

### ? Ошибка 2: Забыли .ToList() для материализации

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(n => n > 1);
numbers.Clear();

// ? Ошибка: query зависит от исходного списка!
foreach (var n in query)
{
    Console.WriteLine(n);  // Ничего не выведет!
}

// ? Правильно
var snapshot = numbers.Where(n => n > 1).ToList();  // Snapshot
numbers.Clear();
foreach (var n in snapshot)
{
    Console.WriteLine(n);  // { 2, 3 }
}
```

### ? Ошибка 3: Изменение List во время итерации

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// ? Ошибка: modifying collection while iterating!
foreach (var n in numbers)
{
    if (n == 3)
        numbers.Remove(n);  // ?? InvalidOperationException!
}

// ? Правильно: итерируем по копии
foreach (var n in numbers.ToList())
{
    if (n == 3)
        numbers.Remove(n);  // OK
}
```

### ? Ошибка 4: Неправильное использование Dictionary

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>();

// ? Ошибка: прямой доступ к несуществующему ключу
// int age = ages["Alice"];  // KeyNotFoundException!

// ? Правильно
if (ages.TryGetValue("Alice", out int age))
{
    Console.WriteLine(age);
}
else
{
    Console.WriteLine("Alice не найдена");
}
```

---

## 12. Лучшие практики

? **DO:**
- Используйте `List<T>` для большинства случаев
- Используйте `Dictionary<TKey, TValue>` для связи ключ-значение
- Используйте `HashSet<T>` если нужны уникальные значения
- Возвращайте `IEnumerable<T>` или `IReadOnlyList<T>` вместо конкретной коллекции
- Используйте LINQ вместо ручных циклов

? **DON'T:**
- Не используйте Array если заранее не знаете размер
- Не изменяйте коллекцию во время итерации по ней
- Не забывайте про `.ToList()` для материализации LINQ запросов
- Не используйте обычные коллекции в многопоточности (используйте ConcurrentCollections)
- Не возвращайте изменяемые коллекции (используйте IReadOnlyList)

---

## Файлы в проекте:
- `Program.cs` — примеры всех коллекций
- `CollectionComparison.cs` — сравнение коллекций
- `LINQWithCollections.cs` — примеры LINQ с коллекциями

