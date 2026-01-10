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

// ❌ Неудобно! Много переменных!
// Что если 100 человек? 1000?

// ✅ Решение — коллекция!
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

// ❌ Ошибка: индекс вне границ
// Console.WriteLine(numbers[5]);  // IndexOutOfRangeException!

// Длина массива
int length = numbers.Length;  // 5

// Изменение элемента
numbers[0] = 100;
Console.WriteLine(numbers[0]);  // 100
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

// ✅ Безопасное получение (если ключа нет)
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
Alice приходит → Встает в очередь → Alice first
Bob приходит → Встает после Alice
Charlie приходит → Встает после Bob

Alice уходит (обслужили первой)
Bob становится первым
Charlie становится вторым
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
Кладу Alice → Стек: [Alice]
Кладу Bob → Стек: [Alice, Bob]
Кладу Charlie → Стек: [Alice, Bob, Charlie]

Беру сверху → Charlie (последняя, которая положил)
Беру сверху → Bob
Беру сверху → Alice (первая, которую положил)
```

---

## 8. Сравнение коллекций

| Коллекция           | Доступ | Добавление | Удаление | Уникальность | Когда использовать      |
| ------------------- | ------ | ---------- | -------- | ------------ | ----------------------- |
| **Array**           | O(1)   | Нет        | Нет      | Нет          | Редко (размер известен) |
| **List<T>**         | O(1)   | O(n)       | O(n)     | Нет          | **ВСЕ СЛУЧАИ**          |
| **Dictionary<K,V>** | O(1)   | O(1)       | O(1)     | Да (ключи)   | Ключ-значение           |
| **HashSet<T>**      | O(1)   | O(1)       | O(1)     | Да           | Уникальные значения     |
| **Queue<T>**        | O(1)   | O(1)       | O(1)     | Нет          | FIFO очередь            |
| **Stack<T>**        | O(1)   | O(1)       | O(1)     | Нет          | LIFO стек               |

---

## 9. IEnumerable<T> и IReadOnlyList<T>

### Почему это важно для API?

```csharp
// ❌ Плохо: возвращаем List (клиент может его изменить!)
public List<User> GetUsers()
{
    List<User> users = new List<User> { /*...*/ };
    return users;  // Клиент может удалить элементы!
}

// ✅ Хорошо: возвращаем интерфейс (клиент не может изменить!)
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

### ❌ Ошибка 1: Индекс вне границ

```csharp
int[] array = { 1, 2, 3 };

// ❌ Ошибка: индексы идут от 0 до 2
// Console.WriteLine(array[3]);  // IndexOutOfRangeException!

// ✅ Правильно
if (index >= 0 && index < array.Length)
{
    Console.WriteLine(array[index]);
}
```

### ❌ Ошибка 2: Забыли .ToList() для материализации

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(n => n > 1);
numbers.Clear();

// ❌ Ошибка: query зависит от исходного списка!
foreach (var n in query)
{
    Console.WriteLine(n);  // Ничего не выведет!
}

// ✅ Правильно
var snapshot = numbers.Where(n => n > 1).ToList();  // Snapshot
numbers.Clear();
foreach (var n in snapshot)
{
    Console.WriteLine(n);  // { 2, 3 }
}
```

### ❌ Ошибка 3: Изменение List во время итерации

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// ❌ Ошибка: modifying collection while iterating!
foreach (var n in numbers)
{
    if (n == 3)
        numbers.Remove(n);  // InvalidOperationException!
}

// ✅ Правильно: итерируем по копии
foreach (var n in numbers.ToList())
{
    if (n == 3)
        numbers.Remove(n);  // OK
}
```

### ❌ Ошибка 4: Неправильное использование Dictionary

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>();

// ❌ Ошибка: прямой доступ к несуществующему ключу
// int age = ages["Alice"];  // KeyNotFoundException!

// ✅ Правильно
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

✅ **DO:**

- Используйте `List<T>` для большинства случаев
- Используйте `Dictionary<TKey, TValue>` для связи ключ-значение
- Используйте `HashSet<T>` если нужны уникальные значения
- Возвращайте `IEnumerable<T>` или `IReadOnlyList<T>` вместо конкретной коллекции
- Используйте LINQ вместо ручных циклов

❌ **DON'T:**

- Не используйте Array если заранее не знаете размер
- Не изменяйте коллекцию во время итерации по ней
- Не забывайте про `.ToList()` для материализации LINQ запросов
- Не используйте обычные коллекции в многопоточности (используйте ConcurrentCollections)
- Не возвращайте изменяемые коллекции (используйте IReadOnlyList)

---

### Для полного новичка: как пользоваться чек-листом

- Сначала изучите разделы: "Сравнение коллекций", "IEnumerable/IReadOnlyList", "LINQ с коллекциями", "Частые ошибки".
- Откройте Program.cs, запустите примеры и сверьте вывод — это даст интуицию.
- Затем переходите к чек-листу: вопросы идут от простого к сложному и сопровождаются краткими примерами.
- Нужный ответ всегда можно найти выше по документу или в итоговой таблице.

## 13. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ 🎯

### Вопрос 1: В чем разница между Array и List?

Краткий ответ: Array — фиксированный размер и нельзя добавлять/удалять; List<T> — динамический, удобен по умолчанию для большинства задач.

| Критерий               | Array                   | List<T>                |
| ---------------------- | ----------------------- | ---------------------- |
| **Размер**             | Фиксированный           | Динамический           |
| **Изменяемость**       | Нельзя добавить/удалить | Можно добавить/удалить |
| **Когда использовать** | Редко                   | **Всегда**             |

```csharp
int[] arr = new int[3];  // ❌ Нельзя добавить 4-й элемент

List<int> list = new List<int> { 1, 2, 3 };
list.Add(4);  // ✅ OK
```

---

### Вопрос 2: Dictionary - ключ-значение с O(1) поиском

Краткий ответ: Dictionary хранит пары ключ-значение с быстрым поиском по ключу; для безопасного чтения используйте TryGetValue.

```csharp
Dictionary<string, string> config = new Dictionary<string, string>
{
    ["url"] = "https://example.com",
    ["browser"] = "Chrome"
};

// Безопасное получение
if (config.TryGetValue("password", out string pass))
{
    Console.WriteLine(pass);
}
```

**Когда использовать:** Ключ-значение, кэши, конфиги

---

### Вопрос 3: HashSet - уникальные значения

Краткий ответ: HashSet хранит только уникальные элементы и удобно удаляет дубликаты.

```csharp
int[] numbers = { 1, 2, 2, 3, 3, 3 };
HashSet<int> unique = new HashSet<int>(numbers);

// Результат: { 1, 2, 3 } - только уникальные!
```

**Когда использовать:** Нужны только уникальные значения, удаление дубликатов

---

### Вопрос 4: FIFO vs LIFO

Краткий ответ: Queue — FIFO (первым пришёл — первым вышел); Stack — LIFO (последним пришёл — первым вышел).

**Queue (FIFO):**

```csharp
Queue<string> queue = new Queue<string>();
queue.Enqueue("Alice");   // Alice
queue.Enqueue("Bob");     // Alice, Bob
queue.Dequeue();          // Удалит Alice (первый, который добавили)
```

**Stack (LIFO):**

```csharp
Stack<string> stack = new Stack<string>();
stack.Push("Alice");      // Alice
stack.Push("Bob");        // Alice, Bob
stack.Pop();              // Удалит Bob (последний, который добавили)
```

---

### Вопрос 5: Безопасный доступ к Dictionary

Краткий ответ: Избегайте прямого индексирования; проверяйте ключ через ContainsKey или используйте TryGetValue, чтобы не ловить исключения.

```csharp
// ❌ Неправильно (KeyNotFoundException)
// int age = ages["Bob"];

// ✅ Правильно (TryGetValue)
if (ages.TryGetValue("Bob", out int age))
{
    Console.WriteLine(age);
}
```

---

### Вопрос 6: Не изменяй List во время foreach

Краткий ответ: Модификация коллекции внутри foreach вызывает ошибку; проходите по копии (ToList) или используйте for/RemoveAll.

```csharp
// ❌ Ошибка
foreach (var n in numbers)
{
    if (n == 3)
        numbers.Remove(n);  // InvalidOperationException!
}

// ✅ Правильно
foreach (var n in numbers.ToList())
{
    if (n == 3)
        numbers.Remove(n);  // OK
}
```

---

### Вопрос 7: Возвращай IEnumerable вместо List

Краткий ответ: Возвращайте IEnumerable для чтения без возможности изменить исходную коллекцию; List раскрывает изменяемое внутреннее состояние.

```csharp
// ❌ Неправильно
public List<User> GetUsers() { return users; }
GetUsers().Clear();  // Очистил оригинальный список!

// ✅ Правильно
public IEnumerable<User> GetUsers() { return users; }
GetUsers().Clear();  // ❌ Ошибка компиляции! Нет Clear()
```

---

### Вопрос 8: Сложность операций (Big O)

Краткий ответ: Dictionary/HashSet дают O(1) для поиска/добавления; List хорош для последовательного доступа, но удаление из середины — O(n).

| Операция       | Array | List   | Dictionary | HashSet |
| -------------- | ----- | ------ | ---------- | ------- |
| **Доступ**     | O(1)  | O(1)   | —          | —       |
| **Поиск**      | O(n)  | O(n)   | O(1)       | O(1)    |
| **Добавление** | —     | O(1)\* | O(1)       | O(1)    |
| **Удаление**   | —     | O(n)   | O(1)       | O(1)    |

---

### Вопрос 9: Когда использовать каждую коллекцию?

Краткий ответ: List — по умолчанию; Dictionary — ключ-значение/кэш; HashSet — уникальные; Queue/Stack — очереди/стековые сценарии.

**List<T>** — по умолчанию

```csharp
List<string> bugs = new List<string>();
```

**Dictionary<K,V>** — ключ-значение

```csharp
Dictionary<string, string> config = new Dictionary<string, string>();
```

**HashSet<T>** — уникальные значения

```csharp
HashSet<int> unique = new HashSet<int>();
```

**Queue<T>** — FIFO очередь

```csharp
Queue<string> tasks = new Queue<string>();
```

**Stack<T>** — LIFO стек

```csharp
Stack<string> history = new Stack<string>();
```

---

## 14. ПРАКТИЧЕСКИЕ СОВЕТЫ ДЛЯ ИНТЕРВЬЮ

### ✅ Что хорошо сказать:

1. **"List динамический, начинает с начальной емкости"**

   - При добавлении элемента переаллоцируется и удваивает размер
   - Это дороговато, но амортизированно O(1)

2. **"Dictionary на основе хеш-таблицы с O(1) поиском"**

   - Все ключи должны быть уникальны
   - TryGetValue безопаснее, чем прямой доступ

3. **"Всегда возвращайте IEnumerable или IReadOnlyList"**
   - Защита от неправильного использования
   - Гибкость при смене реализации

### ❌ Чего не нужно говорить:

1. "Array и List - это одно и то же"
2. "Dictionary нужен редко"
3. "HashSet автоматически сортирует" (НЕПРАВИЛЬНО)

---

## 15. ИТОГОВАЯ ТАБЛИЦА

| Коллекция      | Когда использовать  | Поиск | Добавление | Удаление |
| -------------- | ------------------- | ----- | ---------- | -------- |
| **List<T>**    | Общего назначения   | O(n)  | O(1)\*     | O(n)     |
| **Dictionary** | Ключ-значение       | O(1)  | O(1)       | O(1)     |
| **HashSet**    | Уникальные значения | O(1)  | O(1)       | O(1)     |
| **Queue**      | FIFO очередь        | —     | O(1)       | O(1)     |
| **Stack**      | LIFO стек           | —     | O(1)       | O(1)     |

---

## 16. СВЯЗЬ С ДРУГИМИ ТЕМАМИ

- **Topic2 (Interfaces):** Работа с IEnumerable, IReadOnlyList
- **Topic3 (Polymorphism):** Хранение разных типов в List<Base>
- **Topic5 (LINQ):** Фильтрация и трансформация коллекций
- **Topic6 (Generics):** List<T> - это обобщенный тип
