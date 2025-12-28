# Topic8 — Значимые и ссылочные типы (Value vs Reference) (Полный курс для начинающих)

## Цель
Понять разницу между значимыми типами (struct) и ссылочными типами (class), как работают `ref` и `out`, и какие проблемы могут возникнуть с многопоточностью.

---

## 1. Значимые типы (Value Types) vs Ссылочные типы (Reference Types)

### Аналогия из жизни

```
Значимые типы (int, double, struct):
- Вы держите деньги в руке
- Если вы дадите кому-то копию, у вас остается оригинал
- Каждый работает со своей копией денег

Ссылочные типы (class, string):
- Вы держите адрес ящика на почте
- Если вы дадите кому-то адрес, вы оба указываете на ОДИН ящик
- Если один изменит содержимое, второй увидит изменение
```

### В памяти

```
ЗНАЧИМЫЙ ТИП (Stack):
???????????????
? x: int = 5  ?  <- Переменная содержит значение прямо в памяти
???????????????

ССЫЛОЧНЫЙ ТИП (Heap):
Stack:                  Heap:
???????????????       ????????????????
? user: ref ?????????>? name: "Alice"?
???????????????       ????????????????
                      <- Переменная содержит ссылку на объект в памяти
```

---

## 2. Значимые типы (Value Types)

### Встроенные значимые типы
```csharp
int x = 5;              // Целые числа
double d = 3.14;        // Дробные числа
bool flag = true;       // Булевы значения
char c = 'a';           // Символы
DateTime date = DateTime.Now;  // Дата и время
```

### struct (структура)

```csharp
// Создаем свой значимый тип
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Использование
Point p1 = new Point(10, 20);
Point p2 = p1;  // ? Копируется VALUE (весь struct копируется)

p2.X = 100;

Console.WriteLine(p1.X);  // 10 (не изменилось!)
Console.WriteLine(p2.X);  // 100 (это другая копия)
```

### Когда использовать struct?
- ? Малые данные (точка, координаты, дата)
- ? Когда нужна копия, а не ссылка
- ? Не для больших объектов (неэффективно копировать)
- ? Не если часто передаются параметры

---

## 3. Ссылочные типы (Reference Types)

### class (класс)

```csharp
// Создаем ссылочный тип
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Использование
User user1 = new User { Name = "Alice", Age = 30 };
User user2 = user1;  // ? Копируется ССЫЛКА, не объект!

user2.Name = "Bob";

Console.WriteLine(user1.Name);  // "Bob" (изменилось!)
Console.WriteLine(user2.Name);  // "Bob" (одна и та же ссылка)

// Они указывают на ОДИН объект в памяти!
```

### Сравнение

```csharp
// Значимый тип
Point p1 = new Point(10, 20);
Point p2 = new Point(10, 20);
Console.WriteLine(p1 == p2);  // true (сравнивает значения)

// Ссылочный тип
User u1 = new User { Name = "Alice" };
User u2 = new User { Name = "Alice" };
Console.WriteLine(u1 == u2);  // false (разные объекты в памяти!)
Console.WriteLine(ReferenceEquals(u1, u2));  // false (разные ссылки)

User u3 = u1;
Console.WriteLine(ReferenceEquals(u1, u3));  // true (одна ссылка)
```

---

## 4. ref и out (передача по ссылке)

### Проблема: передача по значению

```csharp
void Increment(int x)
{
    x++;  // Изменяем копию, оригинал не меняется
}

int number = 5;
Increment(number);
Console.WriteLine(number);  // 5 (не изменилось!)
```

### Решение 1: ref (передача по ссылке)

```csharp
void Increment(ref int x)  // ref — работаем с ОРИГИНАЛОМ
{
    x++;
}

int number = 5;
Increment(ref number);  // ref нужен и здесь
Console.WriteLine(number);  // 6 (изменилось!)

// Реальный пример: Swap
void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

int x = 5, y = 10;
Swap(ref x, ref y);
Console.WriteLine($"{x}, {y}");  // 10, 5
```

### Решение 2: out (выходной параметр)

```csharp
// out похож на ref, но используется для ВЫХОДНЫХ значений
// Переменная может быть неинициализирована
void GetCoordinates(out int x, out int y)
{
    x = 10;  // Должны инициализировать!
    y = 20;
}

GetCoordinates(out int a, out int b);  // out нужен и здесь
Console.WriteLine($"{a}, {b}");  // 10, 20

// Популярный пример: int.TryParse
if (int.TryParse("123", out int result))
{
    Console.WriteLine($"Число: {result}");  // 123
}
else
{
    Console.WriteLine("Не число!");
}
```

### ref vs out

| Особенность | ref | out |
|------------|-----|-----|
| **Переменная инициализирована?** | ? Да | ? Нет |
| **Нужна инициализация в методе?** | ? Нет | ? Да |
| **Цель** | Передать и вернуть | Только вернуть |

```csharp
// ref — переменная должна быть инициализирована ДО вызова
int x = 5;
MethodWithRef(ref x);  // ? OK, x уже = 5

// out — переменная будет инициализирована В методе
MethodWithOut(out int y);  // ? OK, y пока не инициализирована
```

---

## 5. Nullable типы (T?)

### Проблема: как представить "отсутствие значения"?

```csharp
int age = null;  // ? Ошибка! int не может быть null

// Решение:
int? age = null;  // ? OK! Nullable int
```

### Использование

```csharp
int? number = null;

// Проверка наличия значения
if (number.HasValue)
{
    Console.WriteLine($"Значение: {number.Value}");
}
else
{
    Console.WriteLine("Значение отсутствует");
}

// Или короче
if (number != null)
{
    Console.WriteLine($"Значение: {number}");
}

// Значение по умолчанию
int value = number ?? 0;  // Если number null, используй 0
```

### Nullable struct vs class

```csharp
// Struct
int? x = 5;
x = null;  // ? OK

// Class (уже может быть null)
User? user = null;  // ? OK (class уже ссылочный тип)
```

---

## 6. Многопоточность и ссылочные типы (ВАЖНО!)

### Проблема: Race Condition

```csharp
public class Counter
{
    public int Value { get; set; }
}

Counter counter = new Counter { Value = 0 };

// Два потока одновременно инкрементируют
Task t1 = Task.Run(() => {
    for (int i = 0; i < 1000; i++)
    {
        counter.Value++;  // ? Race condition!
    }
});

Task t2 = Task.Run(() => {
    for (int i = 0; i < 1000; i++)
    {
        counter.Value++;  // ? Race condition!
    }
});

Task.WaitAll(t1, t2);
Console.WriteLine(counter.Value);  // Может быть 1000, 1500, 1999... ??
// Должно быть 2000!
```

### Решение 1: lock

```csharp
public class SafeCounter
{
    private int value;
    private object lockObj = new object();
    
    public int Value
    {
        get { return value; }
    }
    
    public void Increment()
    {
        lock (lockObj)  // Только один поток одновременно
        {
            value++;
        }
    }
}

SafeCounter counter = new SafeCounter();

Task t1 = Task.Run(() => {
    for (int i = 0; i < 1000; i++)
        counter.Increment();  // ? Безопасно
});

Task t2 = Task.Run(() => {
    for (int i = 0; i < 1000; i++)
        counter.Increment();  // ? Безопасно
});

Task.WaitAll(t1, t2);
// counter.Value = 2000 ? Правильно!
```

### Решение 2: Concurrent Collections

```csharp
using System.Collections.Concurrent;

ConcurrentDictionary<string, int> dict = new();

Task t1 = Task.Run(() => {
    for (int i = 0; i < 1000; i++)
    {
        dict.TryAdd($"key{i}", i);  // ? Потокобезопасно
    }
});

Task t2 = Task.Run(() => {
    for (int i = 1000; i < 2000; i++)
    {
        dict.TryAdd($"key{i}", i);  // ? Потокобезопасно
    }
});

Task.WaitAll(t1, t2);
Console.WriteLine(dict.Count);  // 2000 ?
```

---

## 7. Практический пример для QA

```csharp
public class TestContext
{
    // Значимый тип
    public struct TestCase
    {
        public string Name { get; set; }
        public bool Passed { get; set; }
    }
    
    // Ссылочный тип
    private List<TestCase> results = new();
    private object lockObj = new();
    
    public void RecordResult(string testName, bool passed)
    {
        lock (lockObj)  // Потокобезопасность
        {
            results.Add(new TestCase { Name = testName, Passed = passed });
        }
    }
    
    public int GetPassedCount()
    {
        lock (lockObj)
        {
            return results.Count(r => r.Passed);
        }
    }
}
```

---

## 8. Частые ошибки новичков

### ? Ошибка 1: Забыли ref при вызове
```csharp
void ChangeValue(ref int x) { x = 100; }

int number = 5;
// ? ChangeValue(number);  // Ошибка! Забыли ref

// ?
ChangeValue(ref number);  // ref обязателен
```

### ? Ошибка 2: Race condition в многопоточности
```csharp
// ? Небезопасно
for (int i = 0; i < 1000; i++)
{
    Task.Run(() => counter.Value++);  // Race condition!
}

// ? Безопасно
for (int i = 0; i < 1000; i++)
{
    Task.Run(() => { lock (obj) { counter.Value++; } });
}
```

### ? Ошибка 3: Ожидание null для значимого типа
```csharp
int x = 5;
if (x == null)  // ? int не может быть null!
{
    // Это никогда не выполнится
}

// ? Правильно
int? x = 5;
if (x == null)
{
    // Теперь может быть null
}
```

---

## 9. Лучшие практики

? **DO:**
- Используйте `class` по умолчанию для объектов
- Используйте `struct` только для малых значимых типов
- Используйте `lock` для защиты ссылочных типов в многопоточности
- Используйте `int?` если нужно представить отсутствие значения
- Проверяйте race conditions в многопоточном коде

? **DON'T:**
- Не создавайте большие struct (неэффективно копировать)
- Не забывайте `ref` при вызове метода
- Не игнорируйте потокобезопасность
- Не используйте обычные коллекции в многопоточности

---

## Файлы в проекте:
- `Program.cs` — примеры value и reference типов
- `User.cs` — ссылочный тип
- `Coordinate.cs` — значимый тип (struct)
