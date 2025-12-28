# Topic3 — Полиморфизм и наследование (Полный курс для начинающих)

## Цель
Понять, как один класс может наследовать от другого, переопределять методы, и почему это важно для избежания дублирования кода.

---

## 1. Что такое наследование? (Для самых начинающих)

### Аналогия из жизни
```
Животное — родитель
?? Собака — наследник (наследует от Животного)
?? Кошка — наследник
?? Птица — наследник

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
dog.Sleep();       // ? Sleep унаследована от Animal
dog.Eat();         // ? Eat унаследована от Animal
dog.MakeSound();   // ? Вызовет переопределенный MakeSound из Dog

Animal cat = new Cat { Name = "Whiskers" };
cat.MakeSound();   // ? Вызовет переопределенный MakeSound из Cat
```

---

## 2. Зачем нужно наследование? (3 причины)

### Причина 1: Избегайте дублирования (DRY)

? **Без наследования** (много повторений):
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

public class Bird
{
    public string Name { get; set; }
    
    // Снова одинаковые методы!
    public void Sleep() => Console.WriteLine("Zzz...");
    public void Eat() => Console.WriteLine("Nom nom nom");
    
    public void MakeSound() => Console.WriteLine("Chirp!");
}
```

? **С наследованием** (код не повторяется):
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

public class Bird : Animal
{
    public override void MakeSound() => Console.WriteLine("Chirp!");
}
```

### Причина 2: Полиморфизм (один код работает со всеми)

```csharp
// Один метод работает с любым Animal!
void PrintAnimalSound(Animal animal)
{
    Console.WriteLine($"{animal.Name} говорит:");
    animal.MakeSound();  // ? Вызовет правильный метод для каждого животного
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
    PrintAnimalSound(animal);  // ? Работает со Snake тоже!
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
// ? Animal animal = new Animal();  // Ошибка! Не можем создать абстрактный класс

// ? Можем создать Dog (который реализует MakeSound())
Animal dog = new Dog { Name = "Rex" };
dog.MakeSound();  // Woof!

// ? Если создадим класс Bird и забудим переопределить MakeSound():
public class Bird : Animal
{
    // Ошибка компиляции! Должны реализовать MakeSound()
}
```

### abstract vs virtual

| Особенность | virtual | abstract |
|-------------|---------|----------|
| **Может быть создан класс?** | ? Да | ? Нет |
| **Тело метода** | ? Есть | ? Нет |
| **Должны ли дочерние переопределять?** | ? Опционально | ? Обязательно |
| **Когда использовать?** | Есть общее поведение | Только контракт |

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
        Console.WriteLine("???  Очищаем кэш...");
    }
    
    // Виртуальный метод (может быть переопределен)
    public virtual void PrintConsole()
    {
        Console.WriteLine("?? [BROWSER] Log message");
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
        Console.WriteLine("?? Запускаем Chrome...");
        _windowHandle = "chrome_12345";
    }
    
    public override void Navigate(string url)
    {
        _currentUrl = url;
        Console.WriteLine($"?? Chrome переходит на {url}");
    }
    
    public override void Close()
    {
        Console.WriteLine("?? Закрываем Chrome");
    }
    
    public override void PrintConsole()
    {
        Console.WriteLine("?? [CHROME] Console message");
    }
}

// ========== РЕАЛИЗАЦИЯ 2: Firefox ==========
public class FirefoxBrowser : Browser
{
    public override void Launch()
    {
        Console.WriteLine("?? Запускаем Firefox...");
        _windowHandle = "firefox_67890";
    }
    
    public override void Navigate(string url)
    {
        _currentUrl = url;
        Console.WriteLine($"?? Firefox переходит на {url}");
    }
    
    public override void Close()
    {
        Console.WriteLine("?? Закрываем Firefox");
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
        Console.WriteLine($"??  Логинимся как {username}");
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
        chromeBrowser.ClearCache();  // ? Унаследованный метод
        
        LoginPage loginPage = new LoginPage(chromeBrowser);
        loginPage.Open();
        loginPage.Login("alice", "password");
        
        chromeBrowser.PrintConsole();  // ?? [CHROME] Console message
        chromeBrowser.Close();
        
        Console.WriteLine("\n=== ТЕСТ В FIREFOX ===");
        // Тот же тест в Firefox!
        Browser firefoxBrowser = new FirefoxBrowser();
        firefoxBrowser.Launch();
        firefoxBrowser.ClearCache();  // ? Работает одинаково!
        
        loginPage = new LoginPage(firefoxBrowser);
        loginPage.Open();
        loginPage.Login("bob", "secret");
        
        firefoxBrowser.PrintConsole();  // ?? [BROWSER] Log message (дефолтный)
        firefoxBrowser.Close();
    }
}

// Вывод:
// === ТЕСТ В CHROME ===
// ?? Запускаем Chrome...
// ???  Очищаем кэш...
// ?? Chrome переходит на https://example.com/login
// ??  Логинимся как alice
// ?? [CHROME] Console message
// ?? Закрываем Chrome
//
// === ТЕСТ В FIREFOX ===
// ?? Запускаем Firefox...
// ???  Очищаем кэш...
// ?? Firefox переходит на https://example.com/login
// ??  Логинимся как bob
// ?? [BROWSER] Log message
// ?? Закрываем Firefox
