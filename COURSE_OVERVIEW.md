# DotNetStudy - Полный курс C# для QA Automation

## 📚 Структура курса

Этот проект содержит **13 комплексных Topics** по C# для специалистов QA automation. Каждый Topic включает:

- ✅ **README.md** - подробное объяснение (400-1200 строк)
- ✅ **Program.cs** - практические примеры с комментариями
- ✅ **Beginner Navigation** - быстрый путь для новичков
- ✅ **Interview Checklist** - 8-10 вопросов для собеседования
- ✅ **Best Practices** - рекомендации и типичные ошибки
- ✅ **QA Examples** - примеры из практики автоматизации тестов

---

## 📋 Полный список Topics

### Topic 1: Classes (Классы)

**Цель:** Понять основы объектно-ориентированного программирования

- Классы и конструкторы
- Модификаторы доступа (public, private, internal, protected)
- Свойства и поля
- `this` и `base` ключевые слова
- Static члены класса
- Статические конструкторы
- **Важно для QA:** Создание Page Object classes для тестов

**Запустить:** `cd Topic1_Classes && dotnet run`

---

### Topic 2: Interfaces (Интерфейсы)

**Цель:** Проектировать абстрактные контракты для гибкого кода

- Что такое интерфейс и зачем он нужен
- Реализация интерфейсов
- Полиморфизм через интерфейсы
- Dependency Injection (DI)
- SOLID - Dependency Inversion Principle (DIP)
- **Важно для QA:** IWebDriver в Selenium, mock объекты для тестов

**Запустить:** `cd Topic2_Interfaces && dotnet run`

---

### Topic 3: Polymorphism (Полиморфизм)

**Цель:** Один интерфейс, разные реализации

- Наследование (базовый класс → подкласс)
- Переопределение методов (override)
- Абстрактные классы и методы
- Полиморфизм в действии
- **Важно для QA:** Базовые классы для разных браузеров (Chrome, Firefox, Safari)

**Запустить:** `cd Topic3_Polymorphism && dotnet run`

---

### Topic 4: Collections (Коллекции)

**Цель:** Работать с группами данных

- List<T> - динамические массивы
- Dictionary<K,V> - ключ-значение
- HashSet<T> - уникальные элементы
- Queue<T>, Stack<T>
- Итерация: foreach, LINQ
- **Важно для QA:** Сохранение найденных элементов, поиск данных

**Запустить:** `cd Topic4_Collections && dotnet run`

---

### Topic 4 Detailed: Collections (Детальное изучение)

**Цель:** Глубокое понимание коллекций и их производительности

- Сравнение Array vs List vs LinkedList
- Производительность различных коллекций
- Когда использовать какую коллекцию
- Thread-safe коллекции
- **Важно для QA:** Выбор правильной структуры данных для тестовых данных

**Запустить:** `cd Topic4_DetailedCollections && dotnet run`

---

### Topic 5: LINQ (Language Integrated Query)

**Цель:** Запросы к данным с использованием SQL-подобного синтаксиса

- Method syntax vs Query syntax
- Select, Where, FirstOrDefault, Count
- OrderBy, GroupBy, Join
- Deferred vs Immediate execution ⚠️ (критично!)
- **Важно для QA:** Фильтрация тестовых данных, поиск элементов

**Запустить:** `cd Topic5_LINQ && dotnet run`

---

### Topic 6: Generics (Обобщенные типы)

**Цель:** Написание универсального кода для разных типов

- Общие методы <T>
- Общие классы Container<T>
- Ограничения типов (where clause)
- Generic constraints: where T : class, IComparable, new()
- **Важно для QA:** Создание универсальных тестовых утилит

**Запустить:** `cd Topic6_Generics && dotnet run`

---

### Topic 7: Exceptions (Исключения)

**Цель:** Правильная обработка ошибок

- Try/Catch/Finally блоки
- Типы исключений (Exception, ArgumentException, NullReferenceException)
- Пользовательские исключения
- IDisposable и using statement
- **Важно для QA:** Перехват ошибок веб-драйвера, graceful shutdown

**Запустить:** `cd Topic7_Exceptions && dotnet run`

---

### Topic 8: Value vs Reference Types (Типы значений и ссылок)

**Цель:** Понять как работает память в C#

- Value types: int, double, bool, struct
- Reference types: class, array, string
- Nullable<T> - обнуляемые типы
- ref и out параметры
- Boxing и Unboxing
- **Важно для QA:** Понимание поведения объектов при передаче между методами

**Запустить:** `cd Topic8_ValueRefTypes && dotnet run`

---

### Topic 9: Null Operators (Операторы для работы с null)

**Цель:** Безопасная работа с null значениями

- Null-coalescing operator ?? (если null, то...)
- Null-conditional operator ?. (безопасный доступ)
- Null-coalescing assignment ??=
- Null-forgiving operator !
- **Важно для QA:** Проверки элементов, которые могут не быть найдены

**Запустить:** `cd Topic9_NullOperators && dotnet run`

---

### Topic 10: DateTime (Работа с датами и временем)

**Цель:** Работать с датами, временем, временными поясами

- DateTime: создание, форматирование
- TimeSpan: длительность
- DateTime.Parse и TryParse
- Временные зоны
- Сравнение и арифметика дат
- **Важно для QA:** Отметки времени логов, ожидание таймаутов

**Запустить:** `cd Topic10_DateTime && dotnet run`

---

### Topic 11: Basics (Основы - обзор)

**Цель:** Обзор всех фундаментальных концепций

- Основные типы данных (int, string, bool, double)
- Работа со строками (substring, split, replace)
- Все виды операторов (арифметические, логические, сравнения)
- Инструкции управления потоком (if, for, while, switch)
- **Важно для QA:** Основная база для всех тестов

**Запустить:** `cd Topic11_Basics && dotnet run`

---

### Topic 12: Debugging (Отладка кода)

**Цель:** Находить и фиксить ошибки в коде

- Breakpoints (обычные, условные)
- Stepping (F10 - over, F11 - into, Shift+F11 - out)
- Variables inspection и Watch
- Logpoints (логирование без остановки)
- Stack Trace для отслеживания ошибок
- Практические сценарии QA отладки
- **Важно для QA:** Найти почему тест упал, отладить логику

**Запустить:** `cd Topic12_Debugging && dotnet run`

---

### Topic 13: Principles (Принципы проектирования)

**Цель:** Писать чистый, поддерживаемый код

- **DRY** (Don't Repeat Yourself) - не копируй код
- **KISS** (Keep It Simple, Stupid) - пиши просто
- **SOLID** принципы:
  - **S** - Single Responsibility (одна ответственность)
  - **O** - Open/Closed (открыто для расширения)
  - **L** - Liskov Substitution (подстановка)
  - **I** - Interface Segregation (маленькие интерфейсы)
  - **D** - Dependency Inversion (зависимость от абстракций)
- **Важно для QA:** Page Object Pattern, Page Factory, чистая архитектура тестов

**Запустить:** `cd Topic13_Principles && dotnet run`

---

## 🚀 Как запустить проект

### Способ 1: Запустить отдельный Topic

```bash
cd Topic1_Classes
dotnet run
```

### Способ 2: Построить все Solution

```bash
# В корневой папке DotNetStudy
dotnet build DotNetStudy.sln

# Запустить конкретный проект
dotnet run --project Topic1_Classes/Topic1_Classes.csproj
```

### Способ 3: Открыть в Visual Studio Code

```bash
code .
```

Затем нажмите Ctrl+F5 для запуска любого Program.cs или F5 для отладки.

---

## 📖 Структура каждого Topic

Каждый Topic содержит:

### README.md

- **Beginner Navigation** - быстрый старт (5 мин чтения)
- **1-15 основных разделов** с примерами
- **Interview Checklist** - 8-10 вопросов для подготовки к собеседованию
- **Best Practices** - DO/DON'T
- **Common Mistakes** - частые ошибки новичков

### Program.cs

- **Практические примеры** к каждому разделу
- **Запустимый код** - `dotnet run` в каждой папке Topic
- **XML comments** (///) - документация методов
- **QA-ориентированные примеры** - Page Objects, login, поиск элементов

---

## ✅ Чек-лист изучения

Рекомендуемый порядок изучения:

- [ ] **Topic 1** - Классы (основа ООП)
- [ ] **Topic 2** - Интерфейсы (абстракции)
- [ ] **Topic 3** - Полиморфизм (наследование)
- [ ] **Topic 4** - Коллекции (группы данных)
- [ ] **Topic 4 Detailed** - Детальное изучение коллекций
- [ ] **Topic 5** - LINQ (запросы к данным)
- [ ] **Topic 6** - Generics (универсальный код)
- [ ] **Topic 7** - Исключения (обработка ошибок)
- [ ] **Topic 8** - Value vs Reference типы (память)
- [ ] **Topic 9** - Null операторы (безопасность)
- [ ] **Topic 10** - DateTime (даты и время)
- [ ] **Topic 11** - Basics (повторение основ)
- [ ] **Topic 12** - Debugging (отладка)
- [ ] **Topic 13** - Principles (архитектура кода)

---

## 🎯 Для QA специалистов

Этот курс специально подготовлен для automation QA с примерами:

1. **Page Object Pattern** (Topic 1, 3, 13)
2. **Selenium WebDriver паттерны** (Topic 2, 7, 8)
3. **Работа с элементами** (Topic 4, 5, 9)
4. **Test Data Management** (Topic 4, 5, 6)
5. **Exception handling в тестах** (Topic 7, 12)
6. **Логирование и отладка** (Topic 12)
7. **Чистая архитектура** (Topic 13)

---

## 📝 Использованные технологии

- **Framework:** .NET 6.0 (net6.0)
- **Language:** C# 10+
- **IDE:** Visual Studio Code или Visual Studio 2022+
- **Build:** dotnet CLI

---

## 📚 Доп. ресурсы

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Page Object Model](https://www.selenium.dev/documentation/webdriver/pom/)

---

## 💡 Советы для обучения

1. **Новичок?** Начните с "Beginner Navigation" в каждом README
2. **Собеседование?** Используйте Interview Checklists
3. **Практика?** Запустите Program.cs и модифицируйте примеры
4. **Отладка?** Используйте Topic 12 примеры с breakpoints
5. **Архитектура?** Применяйте Topic 13 принципы к своему коду

---

## ✨ Примеры реальных сценариев

### Сценарий 1: Поиск элемента на странице

**Topics:** 1 (Classes), 4 (Collections), 9 (Null Operators)

```csharp
// ✅ Правильный способ
var loginButton = elements.FirstOrDefault(e => e.Id == "login");
if (loginButton?.IsVisible ?? false)
{
    loginButton.Click();
}
```

### Сценарий 2: Работа с тестовыми данными

**Topics:** 4 (Collections), 5 (LINQ), 6 (Generics)

```csharp
// ✅ Правильный способ
var validUsers = users
    .Where(u => u.Status == "active")
    .OrderBy(u => u.CreatedDate)
    .Take(10)
    .ToList();
```

### Сценарий 3: Обработка ошибок WebDriver

**Topics:** 7 (Exceptions), 12 (Debugging)

```csharp
// ✅ Правильный способ
try
{
    driver.Navigate().GoToUrl(url);
}
catch (WebDriverTimeoutException ex)
{
    logger.Error($"Page load timeout: {ex.Message}");
    throw;
}
finally
{
    driver.Quit();
}
```

---

## 📞 Вопросы при изучении?

Каждый Topic содержит примеры. Если что-то непонятно:

1. Прочитайте **README.md** всего Topic
2. Запустите **Program.cs** примеры (`dotnet run`)
3. Модифицируйте код и экспериментируйте
4. Используйте **Topic 12 (Debugging)** - поставьте breakpoint и смотрите переменные

---

## 🏆 Итог курса

После изучения всех 13 Topics вы сможете:

✅ Писать объектно-ориентированный код на C#
✅ Использовать интерфейсы и абстракции
✅ Работать с коллекциями и LINQ
✅ Обрабатывать исключения и null значения
✅ Создавать чистую, поддерживаемую архитектуру (SOLID)
✅ Отлаживать свой код
✅ Применять эти знания в Selenium/Page Object тестах

---

**Дата создания:** 2024
**Версия:** 1.0
**Язык:** Русский / English (примеры на английском)
