# Topic3 — Полиморфизм и наследование (Полный курс для начинающих)

## Цель

Понять, как один класс может наследовать от другого, переопределять методы, и почему это важно для избегания дублирования кода.

---

## 1. Что такое наследование? (Для самых начинающих)

### Аналогия из жизни

```
Животное — родитель
↓ Собака — наследник (наследует от Животного)
↓ Кошка — наследник
↓ Птица — наследник

Все животные едят, спят, дышат (общее поведение).
Но собака лает, кошка мяукает, птица чирикает (разное поведение).
```

### В программировании:

```csharp
// Родительский класс (базовый класс)
public class Animal
{
    public string Name { get; set; }

    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");

    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic sound");
    }
}

// Дочерний класс (наследует от Animal)
public class Dog : Animal
{
    // Переопределяем MakeSound() для собаки
    public override void MakeSound()
    {
        Console.WriteLine("Woof! Woof!");
    }
}

// Дочерний класс (наследует от Animal)
public class Cat : Animal
{
    // Переопределяем MakeSound() для кошки
    public override void MakeSound()
    {
        Console.WriteLine("Meow!");
    }
}

// Использование
Animal dog = new Dog { Name = "Rex" };
dog.Sleep();       // ✅ Sleep унаследована от Animal
dog.Eat();         // ✅ Eat унаследована от Animal
dog.MakeSound();   // ✅ Вызовет переопределенный MakeSound() из Dog

Animal cat = new Cat { Name = "Whiskers" };
cat.MakeSound();   // ✅ Вызовет переопределенный MakeSound() из Cat
```

---

## 2. Зачем нужно наследование? (3 причины)

### Причина 1: Избегайте дублирования (DRY)

❌ **Без наследования** (много повторений):

```csharp
public class Dog
{
    public string Name { get; set; }

    // Одинаковые методы...
    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");

    public void MakeSound() => Console.WriteLine("Woof!");
}

public class Cat
{
    public string Name { get; set; }

    // Одинаковые методы повторены!
    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");

    public void MakeSound() => Console.WriteLine("Meow!");
}
```

✅ **С наследованием** (код не повторяется):

```csharp
// Базовый класс с общим кодом
public class Animal
{
    public string Name { get; set; }

    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");

    public virtual void MakeSound() { }
}

// Каждый класс только переопределяет отличия
public class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("Woof!");
}

public class Cat : Animal
{
    public override void MakeSound() => Console.WriteLine("Meow!");
}
```

### Причина 2: Полиморфизм (один код работает со всеми)

```csharp
// Один метод работает с любым Animal!
void PrintAnimalSound(Animal animal)
{
    Console.WriteLine($"{animal.Name} говорит:");
    animal.MakeSound();  // ✅ Вызовет правильный метод для каждого животного
}

// Использование
Animal dog = new Dog { Name = "Rex" };
Animal cat = new Cat { Name = "Whiskers" };
Animal bird = new Bird { Name = "Tweety" };

PrintAnimalSound(dog);    // Rex говорит: Woof!
PrintAnimalSound(cat);    // Whiskers говорит: Meow!
PrintAnimalSound(bird);   // Tweety говорит: Chirp!

// Или в цикле:
List<Animal> animals = new List<Animal> { dog, cat, bird };
foreach (var animal in animals)
{
    PrintAnimalSound(animal);  // Каждый издает свой звук!
}
```

### Причина 3: Расширяемость

```csharp
// Новое животное? Просто добавляем класс!
public class Snake : Animal
{
    public override void MakeSound() => Console.WriteLine("Hisss!");
}

// Весь остальной код работает без изменений!
animals.Add(new Snake { Name = "Sssandro" });
foreach (var animal in animals)
{
    PrintAnimalSound(animal);  // ✅ Работает со Snake тоже!
}
```

---

## 3. virtual и override

### Что это?

**virtual** — "этот метод можно переопределить"
**override** — "я переопределяю виртуальный метод"

```csharp
public class Animal
{
    // virtual — дочерние классы могут переопределить
    public virtual void MakeSound()
    {
        Console.WriteLine("Generic sound");
    }
}

public class Dog : Animal
{
    // override — переопределяем виртуальный метод
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}

// Использование
Animal animal = new Animal();
animal.MakeSound();  // Generic sound

Animal dog = new Dog();
dog.MakeSound();     // Woof! (переопределенный метод)
```

### Важно: Полиморфизм

```csharp
// Самое важное свойство наследования!

Animal dog = new Dog();
dog.MakeSound();  // Вызовет Dog.MakeSound(), хотя тип переменной — Animal!

// Это зависит от РЕАЛЬНОГО типа объекта (Dog), а не объявленного типа (Animal)
```

---

## 4. abstract класс (абстрактный класс)

Иногда базовый класс не должен создаваться напрямую. Используйте `abstract`:

```csharp
// Абстрактный класс — нельзя создать напрямую
public abstract class Animal
{
    public string Name { get; set; }

    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");

    // Абстрактный метод — ДОЛЖЕН быть переопределен в дочерних классах
    public abstract void MakeSound();
}

// Использование
// ❌ Animal animal = new Animal();  // Ошибка! Не можем создать абстрактный класс

// ✅ Можем создать Dog (который реализует MakeSound())
Animal dog = new Dog { Name = "Rex" };
dog.MakeSound();  // Woof!

// ❌ Если создадим класс Bird и забудем переопределить MakeSound():
public class Bird : Animal
{
    // Ошибка компиляции! Должны реализовать MakeSound()
}
```

### abstract vs virtual

| Особенность                            | virtual              | abstract        |
| -------------------------------------- | -------------------- | --------------- |
| **Может быть создан класс?**           | ✅ Да                | ❌ Нет          |
| **Тело метода**                        | ✅ Есть              | ❌ Нет          |
| **Должны ли дочерние переопределять?** | ❌ Опционально       | ✅ Обязательно  |
| **Когда использовать?**                | Есть общее поведение | Только контракт |

```csharp
// virtual — есть реализация
public virtual void MakeSound() => Console.WriteLine("Some sound");

// abstract — только сигнатура
public abstract void MakeSound();  // Нет тела!
```

---

## 5. Практический пример для QA/Automation

### Сценарий: Работа с разными браузерами

```csharp
// ========== БАЗОВЫЙ КЛАСС ==========
public abstract class Browser
{
    protected string _windowHandle;
    protected string _currentUrl;

    public string CurrentUrl => _currentUrl;

    // Общее поведение
    public void ClearCache()
    {
        Console.WriteLine("🗑️  Очищаем кэш...");
    }

    // Виртуальный метод (может быть переопределен)
    public virtual void PrintConsole()
    {
        Console.WriteLine("🖥️ [BROWSER] Log message");
    }

    // Абстрактные методы (ДОЛЖНЫ быть реализованы)
    public abstract void Launch();
    public abstract void Navigate(string url);
    public abstract void Close();
}

// ========== РЕАЛИЗАЦИЯ 1: Chrome ==========
public class ChromeBrowser : Browser
{
    public override void Launch()
    {
        Console.WriteLine("🔧 Запускаем Chrome...");
        _windowHandle = "chrome_12345";
    }

    public override void Navigate(string url)
    {
        _currentUrl = url;
        Console.WriteLine($"⬅️  Chrome переходит на {url}");
    }

    public override void Close()
    {
        Console.WriteLine("❌ Закрываем Chrome");
    }

    public override void PrintConsole()
    {
        Console.WriteLine("🖥️ [CHROME] Console message");
    }
}

// ========== РЕАЛИЗАЦИЯ 2: Firefox ==========
public class FirefoxBrowser : Browser
{
    public override void Launch()
    {
        Console.WriteLine("🔧 Запускаем Firefox...");
        _windowHandle = "firefox_67890";
    }

    public override void Navigate(string url)
    {
        _currentUrl = url;
        Console.WriteLine($"⬅️  Firefox переходит на {url}");
    }

    public override void Close()
    {
        Console.WriteLine("❌ Закрываем Firefox");
    }
}

// ========== PAGE OBJECT ==========
public class LoginPage
{
    private Browser _browser;

    public LoginPage(Browser browser)
    {
        _browser = browser;
    }

    public void Open()
    {
        _browser.Navigate("https://example.com/login");
    }

    public void Login(string username, string password)
    {
        Console.WriteLine($"🔓  Логинимся как {username}");
    }
}

// ========== ИСПОЛЬЗОВАНИЕ ==========
class Program
{
    static void Main()
    {
        // Тест в Chrome
        Console.WriteLine("=== ТЕСТ В CHROME ===");
        Browser chromeBrowser = new ChromeBrowser();
        chromeBrowser.Launch();
        chromeBrowser.ClearCache();  // ✅ Унаследованный метод

        LoginPage loginPage = new LoginPage(chromeBrowser);
        loginPage.Open();
        loginPage.Login("alice", "password");

        chromeBrowser.PrintConsole();  // 🖥️ [CHROME] Console message
        chromeBrowser.Close();

        Console.WriteLine("\n=== ТЕСТ В FIREFOX ===");
        // Тот же тест в Firefox!
        Browser firefoxBrowser = new FirefoxBrowser();
        firefoxBrowser.Launch();
        firefoxBrowser.ClearCache();  // ✅ Работает одинаково!

        loginPage = new LoginPage(firefoxBrowser);
        loginPage.Open();
        loginPage.Login("bob", "secret");

        firefoxBrowser.PrintConsole();  // 🖥️ [BROWSER] Log message (дефолтный)
        firefoxBrowser.Close();
    }
}

// Вывод:
// === ТЕСТ В CHROME ===
// 🔧 Запускаем Chrome...
// 🗑️  Очищаем кэш...
// ⬅️  Chrome переходит на https://example.com/login
// 🔓  Логинимся как alice
// 🖥️ [CHROME] Console message
// ❌ Закрываем Chrome
//
// === ТЕСТ В FIREFOX ===
// 🔧 Запускаем Firefox...
// 🗑️  Очищаем кэш...
// ⬅️  Firefox переходит на https://example.com/login
// 🔓  Логинимся как bob
// 🖥️ [BROWSER] Log message
// ❌ Закрываем Firefox
```

---

## 6. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ 🎯

### Для полного новичка: как пользоваться чек-листом

- Просмотрите разделы: наследование, virtual/override, abstract класс, браузерный пример.
- Соотнесите каждый вопрос с соответствующим разделом выше.
- Ниже под каждым вопросом есть краткий ответ для быстрой подготовки.

### Вопрос 1: В чем разница между abstract и virtual?

Краткий ответ: abstract — нет реализации, обязателен override в наследнике; virtual — есть реализация по умолчанию, override опционален.

| Особенность                  | virtual                      | abstract                       |
| ---------------------------- | ---------------------------- | ------------------------------ |
| **Может быть создан класс?** | ✅ Да                        | ❌ Нет                         |
| **Тело метода**              | ✅ Есть                      | ❌ Только сигнатура            |
| **Override обязателен?**     | ❌ Опционально               | ✅ Обязательно                 |
| **Когда использовать?**      | Есть реализация по умолчанию | Только контракт без реализации |

```csharp
public class Animal
{
    public virtual void Eat() => Console.WriteLine("Eating");  // virtual
    public abstract void MakeSound();  // abstract - нет тела!
}
```

---

### Вопрос 2: Что такое полиморфизм?

Краткий ответ: Один интерфейс — разные реализации; общий код вызывает разные методы в зависимости от реального типа объекта.

**Определение:** "Много форм" — один интерфейс, разные реализации

```csharp
// Один код работает по-разному в зависимости от типа объекта
Animal dog = new Dog();
Animal cat = new Cat();

dog.MakeSound();  // "Woof!"
cat.MakeSound();  // "Meow!"

// Один метод работает со всеми:
List<Animal> animals = new List<Animal> { dog, cat };
foreach (var animal in animals)
{
    animal.MakeSound();  // Каждый издает свой звук!
}
```

---

### Вопрос 3: Как работает вызов виртуального метода?

Краткий ответ: Выбирается реализация по реальному типу объекта (Dog), а не по типу переменной (Animal).

**Правило:** Вызов определяется РЕАЛЬНЫМ типом объекта, а не типом переменной

```csharp
// Переменная типа Animal, но содержит Dog
Animal animal = new Dog();

// Вызвется Dog.MakeSound(), НЕ Animal.MakeSound()!
animal.MakeSound();  // "Woof!"
```

---

### Вопрос 4: Обязательно ли переопределять virtual метод?

Краткий ответ: Нет; если не переопределять, используется реализация базового класса.

**Ответ:** Нет, это опционально

```csharp
public class Animal
{
    public virtual void Eat()
    {
        Console.WriteLine("Eating");
    }
}

public class Dog : Animal
{
    // Если не переопределяем, используется Animal.Eat()
}

public class Cat : Animal
{
    public override void Eat()
    {
        Console.WriteLine("Cat eating carefully");
    }
}
```

**В нашем примере с браузерами:**

- Chrome и Firefox НЕ переопределяют Close() → используют Browser.Close()
- Safari ПЕРЕОПРЕДЕЛЯЕТ Close() → использует Safari.Close() (Force Quit)

---

### Вопрос 5: Что будет, если забыть переопределить abstract метод?

Краткий ответ: Ошибка компиляции (требуется обязательная реализация методового контракта).

**Ошибка компиляции!** 🔴

```csharp
public abstract class Browser
{
    public abstract void Launch();
}

// ❌ ОШИБКА КОМПИЛЯТОРА!
public class Safari : Browser
{
    // Забыли переопределить Launch()
}

// Сообщение об ошибке:
// CS0534: 'Safari' does not implement inherited abstract member 'Browser.Launch()'
```

---

### Вопрос 6: Как работает наследование конструкторов?

Краткий ответ: Дочерний конструктор вызывает родительский через `base(...)`; сначала выполняется база, затем наследник.

**Дочерний класс ДОЛЖЕН вызвать конструктор базового класса через `base()`:**

```csharp
public abstract class Browser
{
    public string Name { get; protected set; }

    public Browser(string name)
    {
        Name = name;
    }
}

public class Chrome : Browser
{
    // Используем base() для вызова конструктора Browser
    public Chrome() : base("Google Chrome") { }
}

// Что происходит при new Chrome():
// 1. Вызывается Chrome()
// 2. Chrome() вызывает base("Google Chrome")
// 3. Browser(string name) устанавливает Name = "Google Chrome"
// 4. Chrome объект создан с Name = "Google Chrome"
```

---

### Вопрос 7: Можно ли хранить объекты разных типов в одном списке?

Краткий ответ: Да, если они наследуются от общей базы; `List<Browser>` принимает `Chrome`, `Firefox`, `Safari`.

**Да! Благодаря полиморфизму:**

```csharp
// Переменная типа Browser может содержать Chrome, Firefox, Safari
List<Browser> browsers = new List<Browser>
{
    new Chrome(),      // Chrome - это Browser
    new Firefox(),     // Firefox - это Browser
    new Safari()       // Safari - это Browser
};

// Один цикл работает со всеми!
foreach (Browser b in browsers)
{
    b.Launch();  // Каждый запустится по-своему
    b.Close();   // Safari закроется Force Quit, остальные обычно
}
```

---

### Вопрос 8: Когда использовать abstract класс, а когда интерфейс?

Краткий ответ: abstract — есть общее поведение/поля/конструктор; interface — только контракт без реализации, можно реализовать несколько.

| Критерий                        | abstract класс                  | interface                      |
| ------------------------------- | ------------------------------- | ------------------------------ |
| **Может иметь реализацию?**     | ✅ Да                           | ❌ Нет                         |
| **Может иметь поля?**           | ✅ Да                           | ❌ Нет                         |
| **Может иметь конструктор?**    | ✅ Да                           | ❌ Нет                         |
| **Множественное наследование?** | ❌ Нет (только от одного)       | ✅ Да (от нескольких)          |
| **Зачем использовать?**         | Группировать с ОБЩИМ поведением | Задать контракт без реализации |

**Пример:**

```csharp
// Abstract - есть общее поведение (Name, ClearCache)
public abstract class Browser
{
    public string Name { get; protected set; }
    public void ClearCache() { }
    public abstract void Launch();
}

// Interface - только контракт, нет реализации
public interface IDriver
{
    void GoToUrl(string url);
    void Click(string selector);
}
```

---

### Вопрос 9: Как проверить реальный тип объекта?

Краткий ответ: Используйте `is` (pattern matching) или `GetType()`; предпочтительно `is`, можно сразу получить переменную конкретного типа.

```csharp
Browser browser = new Chrome();

// Способ 1: GetType()
if (browser.GetType() == typeof(Chrome))
{
    Console.WriteLine("Это Chrome!");
}

// Способ 2: is оператор (рекомендуется)
if (browser is Chrome)
{
    Console.WriteLine("Это Chrome!");
}

// Способ 3: is с присваиванием (pattern matching)
if (browser is Chrome chromeInstance)
{
    Console.WriteLine($"Это Chrome: {chromeInstance.Name}");
}

// Практический пример
foreach (Browser b in browsers)
{
    if (b is Safari)
    {
        Console.WriteLine($"Safari нужно Force Quit: {b.Name}");
    }
}
```

---

## 7. ПРАКТИЧЕСКИЕ СОВЕТЫ ДЛЯ ИНТЕРВЬЮ

### ✅ Что хорошо сказать:

1. **"Полиморфизм позволяет писать расширяемый код"**

   - Добавили новый браузер (Safari)? Старый код работает!
   - Не нужно менять foreach, не нужно писать новые методы

2. **"abstract гарантирует реализацию требуемого"**

   - Если забыли Launch() — ошибка компилятора ловит!
   - Типобезопасность на этапе компиляции

3. **"virtual позволяет переиспользовать общий код"**
   - Chrome и Firefox не переопределяют Close() — экономия
   - Safari переопределяет — специальная логика где нужна

### ❌ Чего не нужно говорить:

1. "abstract — это полностью виртуальный класс"
2. "virtual методы всегда переопределяются"
3. "Используйте проверки типа везде"

---

## 8. ИТОГОВАЯ ТАБЛИЦА

| Концепция               | Суть                                  | Пример                          | Зачем                        |
| ----------------------- | ------------------------------------- | ------------------------------- | ---------------------------- |
| **Наследование**        | Класс получает функционал             | `class Chrome : Browser`        | Переиспользование кода (DRY) |
| **Abstract**            | Нельзя создать, ДОЛЖНЫ переопределить | `public abstract void Launch()` | Гарантировать реализацию     |
| **Virtual**             | Можно переопределить, опционально     | `public virtual void Close()`   | Позволить кастомизацию       |
| **Override**            | Переопределяем метод                  | `public override void Launch()` | Своя реализация для типа     |
| **Полиморфизм**         | Один интерфейс, разные реализации     | `Animal animal = new Dog()`     | Расширяемый код              |
| **Список разных типов** | Хранить Chrome, Firefox, Safari       | `List<Browser>`                 | Обработка группы объектов    |

---

## 9. СВЯЗЬ С ДРУГИМИ ТЕМАМИ

- **Topic1 (Classes):** Наследование — это расширение классов
- **Topic2 (Interfaces):** Интерфейсы — абстрактные контракты без реализации
- **Topic4 (Collections):** Хранение объектов разных типов в `List<T>`
- **Topic5 (LINQ):** Работа с полиморфными коллекциями через LINQ
