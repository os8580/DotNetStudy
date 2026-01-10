# Topic10 — DateTime и TimeSpan (Полный курс для начинающих)

## Цель

Понять, как работать с датами, временем и интервалами времени в C#. После прочтения вы сможете правильно парсить, форматировать и сравнивать даты в автотестах.

---

### Для полного новичка: быстрый маршрут

- **Прочитайте разделы:** "Что такое DateTime?", "Создание и использование DateTime", "Форматирование DateTime", "TimeSpan".
- **Главное:** DateTime **неизменяемый** — нужно присваивать результат: `date = date.AddDays(1)`, не `date.AddDays(1)`.
- **Запустите Program.cs** и посмотрите примеры: `dotnet run`
- **Вернитесь к чек-листу** (конец документа): под каждым вопросом есть краткий ответ и примеры.

---

## 1. Что такое DateTime? (Для самых начинающих)

### Аналогия

```
DateTime = точный момент времени в истории
"15 марта 2024, 14:30:45" — это конкретный момент

TimeSpan = интервал времени
"2 часа", "5 дней", "3 часа 30 минут" — это длительность
```

### В программировании:

```csharp
// DateTime — конкретный момент времени
DateTime now = DateTime.Now;              // Текущая дата и время
Console.WriteLine(now);                   // 15.03.2024 14:30:45

// TimeSpan — интервал времени
TimeSpan duration = TimeSpan.FromHours(2);  // 2 часа
Console.WriteLine(duration);              // 02:00:00
```

---

## 2. DateTime — Создание и использование

### Способы создания

```csharp
// Текущая дата и время (локальное время компьютера)
DateTime now = DateTime.Now;
Console.WriteLine(now);  // 15.03.2024 14:30:45

// Текущая дата и время (UTC — универсальное время)
DateTime utcNow = DateTime.UtcNow;
Console.WriteLine(utcNow);  // 15.03.2024 11:30:45 (может отличаться)

// Сегодняшняя дата (время = 00:00:00)
DateTime today = DateTime.Today;
Console.WriteLine(today);  // 15.03.2024 00:00:00

// Конкретный момент времени
DateTime specificDate = new DateTime(2024, 3, 15);              // 15 марта 2024
DateTime specificDateWithTime = new DateTime(2024, 3, 15, 14, 30, 45);  // 15 марта, 14:30:45

// Парсинг из строки
DateTime parsed = DateTime.Parse("15.03.2024 14:30:45");
Console.WriteLine(parsed);  // 15.03.2024 14:30:45
```

### Свойства DateTime

```csharp
DateTime dt = new DateTime(2024, 3, 15, 14, 30, 45);

Console.WriteLine(dt.Year);        // 2024
Console.WriteLine(dt.Month);       // 3
Console.WriteLine(dt.Day);         // 15
Console.WriteLine(dt.Hour);        // 14
Console.WriteLine(dt.Minute);      // 30
Console.WriteLine(dt.Second);      // 45
Console.WriteLine(dt.DayOfWeek);   // Friday (пятница)
Console.WriteLine(dt.DayOfYear);   // 75 (день года)
```

### Арифметика с DateTime (ВАЖНО: DateTime неизменяемый!)

```csharp
DateTime date = new DateTime(2024, 3, 15, 14, 30, 45);

// Add методы возвращают НОВЫЙ DateTime!
DateTime tomorrow = date.AddDays(1);        // 16 марта
DateTime nextHour = date.AddHours(1);       // 14:30 + 1 час = 15:30
DateTime nextWeek = date.AddDays(7);        // На неделю позже

// Исходный date НЕ изменился!
Console.WriteLine(date);      // 15.03.2024 14:30:45
Console.WriteLine(tomorrow);  // 16.03.2024 14:30:45

// ? Ошибка новичков: забывают присвоить результат
DateTime startDate = DateTime.Now;
startDate.AddDays(1);  // ? Это ничего не сделает!
Console.WriteLine(startDate);  // Старая дата!

// ? Правильно
startDate = startDate.AddDays(1);  // Присваиваем результат
```

---

## 3. Форматирование DateTime (как выглядит дата)

### Стандартные форматы

```csharp
DateTime dt = new DateTime(2024, 3, 15, 14, 30, 45);

// Короткая дата
Console.WriteLine(dt.ToString("d"));  // 15.03.2024

// Длинная дата
Console.WriteLine(dt.ToString("D"));  // пятница, 15 марта 2024

// Короткое время
Console.WriteLine(dt.ToString("t"));  // 14:30

// Полное время
Console.WriteLine(dt.ToString("T"));  // 14:30:45

// Дата и время
Console.WriteLine(dt.ToString("g"));  // 15.03.2024 14:30

// ISO формат (универсальный)
Console.WriteLine(dt.ToString("O"));  // 2024-03-15T14:30:45.0000000
```

### Пользовательские форматы

```csharp
DateTime dt = new DateTime(2024, 3, 15, 14, 30, 45);

// Формат: DD.MM.YYYY HH:mm:ss
Console.WriteLine(dt.ToString("dd.MM.yyyy HH:mm:ss"));  // 15.03.2024 14:30:45

// Формат: MM/DD/YYYY (американский)
Console.WriteLine(dt.ToString("MM/dd/yyyy"));  // 03/15/2024

// Формат: MMMM d, yyyy (длинное название месяца)
Console.WriteLine(dt.ToString("MMMM d, yyyy"));  // March 15, 2024

// Формат: yyyy-MM-dd (ISO для баз данных)
Console.WriteLine(dt.ToString("yyyy-MM-dd"));  // 2024-03-15

// Часто используемые символы форматирования:
// d = день (одна цифра) ? 5
// dd = день (две цифры) ? 05
// M = месяц (одна цифра) ? 3
// MM = месяц (две цифры) ? 03
// MMM = название месяца (коротко) ? Mar
// MMMM = название месяца (полное) ? March
// yy = год (две цифры) ? 24
// yyyy = год (четыре цифры) ? 2024
// H = час (24-часовой) ? 14
// HH = час (24-часовой, две цифры) ? 14
// m = минута (одна цифра) ? 5
// mm = минута (две цифры) ? 05
// s = секунда ? 45
// ss = секунда (две цифры) ? 45
```

---

## 4. Парсинг DateTime (как превратить строку в DateTime)

### Простой парсинг

```csharp
// Parse — если уверены в формате (может выбросить исключение)
DateTime dt1 = DateTime.Parse("15.03.2024 14:30:45");
Console.WriteLine(dt1);  // 15.03.2024 14:30:45

// TryParse — безопаснее (не выбросит исключение)
bool success = DateTime.TryParse("15.03.2024 14:30:45", out DateTime dt2);
if (success)
{
    Console.WriteLine(dt2);  // 15.03.2024 14:30:45
}
else
{
    Console.WriteLine("Не удалось распарсить дату");
}

// ? Если парсинг не удался с Parse
// DateTime dt3 = DateTime.Parse("неправильная дата");  // ?? Исключение!

// ? С TryParse просто false
bool ok = DateTime.TryParse("неправильная дата", out DateTime dt3);  // ok = false
```

### ParseExact и TryParseExact (для точных форматов)

```csharp
// Когда вы ТОЧНО знаете формат данных (например, с веб-сайта)
string dateString = "15-03-2024";

// Нужно указать ТОЧНЫЙ формат
string format = "dd-MM-yyyy";

// ParseExact — если уверены
DateTime dt = DateTime.ParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture);
Console.WriteLine(dt);  // 15.03.2024 00:00:00

// TryParseExact — безопаснее
bool success = DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dt2);
if (success)
{
    Console.WriteLine(dt2);
}
```

### Реальный пример для QA

```csharp
// На веб-сайте дата в формате: "2024-03-15"
string dateFromWebsite = "2024-03-15";

// Мы парсим её в DateTime
string format = "yyyy-MM-dd";
bool success = DateTime.TryParseExact(dateFromWebsite, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate);

if (success)
{
    // Сравниваем с ожидаемой датой
    DateTime expectedDate = new DateTime(2024, 3, 15);
    if (parsedDate.Date == expectedDate.Date)  // .Date убирает время
    {
        Console.WriteLine("? Дата совпадает!");
    }
}
```

---

## 5. Сравнение DateTime

### Простое сравнение

```csharp
DateTime date1 = new DateTime(2024, 3, 15);
DateTime date2 = new DateTime(2024, 3, 16);
DateTime date3 = new DateTime(2024, 3, 15);

// Равенство
Console.WriteLine(date1 == date3);  // true (одинаковые даты)
Console.WriteLine(date1 == date2);  // false

// Не равенство
Console.WriteLine(date1 != date2);  // true

// Меньше / больше
Console.WriteLine(date1 < date2);   // true (15 марта раньше 16 марта)
Console.WriteLine(date1 > date2);   // false
Console.WriteLine(date1 <= date3);  // true
Console.WriteLine(date1 >= date3);  // true
```

### Сравнение только по дате (без времени)

```csharp
DateTime dt1 = new DateTime(2024, 3, 15, 10, 30, 0);
DateTime dt2 = new DateTime(2024, 3, 15, 14, 45, 0);
DateTime dt3 = new DateTime(2024, 3, 16, 10, 30, 0);

// ? Без .Date (сравнивает дату И время)
Console.WriteLine(dt1 == dt2);  // false (разное время)

// ? С .Date (сравнивает только дату)
Console.WriteLine(dt1.Date == dt2.Date);  // true (одна дата)
Console.WriteLine(dt1.Date == dt3.Date);  // false (разные даты)
```

### Сравнение с сегодня

```csharp
DateTime eventDate = new DateTime(2024, 3, 15);
DateTime today = DateTime.Today;

if (eventDate < today)
{
    Console.WriteLine("Событие в прошлом");
}
else if (eventDate > today)
{
    Console.WriteLine("Событие в будущем");
}
else
{
    Console.WriteLine("Событие сегодня");
}
```

---

## 6. DateTime.Kind и TimeZone (часовые пояса)

### Проблема: какое время имеется в виду?

```
DateTime.Now = 15.03.2024 14:30 (локальное время, например, московское)
DateTime.UtcNow = 15.03.2024 11:30 (UTC, отличается на 3 часа)

Они показывают ОДИНАКОВЫЙ момент времени!
Но в разных часовых поясах!
```

### DateTimeKind (указатель на часовой пояс)

```csharp
// Локальное время
DateTime local = new DateTime(2024, 3, 15, 14, 30, 0, DateTimeKind.Local);
Console.WriteLine(local.Kind);  // Local

// UTC время
DateTime utc = new DateTime(2024, 3, 15, 11, 30, 0, DateTimeKind.Utc);
Console.WriteLine(utc.Kind);  // Utc

// Неопределенное (мы не знаем, какой часовой пояс)
DateTime unspecified = new DateTime(2024, 3, 15, 14, 30, 0, DateTimeKind.Unspecified);
Console.WriteLine(unspecified.Kind);  // Unspecified

// Правило для распределенных систем:
// Всегда передавайте время в UTC!
// На клиенте преобразуйте в локальное время для отображения
```

### DateTimeOffset (DateTime с информацией о часовом поясе)

```csharp
// DateTime хранит только дату/время, но не знает пояс
DateTime dt = DateTime.Now;  // 14:30 в Москве, но компилятор не знает про Москву

// DateTimeOffset хранит дату/время И смещение от UTC
DateTimeOffset dto = DateTimeOffset.Now;  // 14:30 +03:00
Console.WriteLine(dto);  // 15.03.2024 14:30:00 +03:00

// Преобразование между системами
DateTime utcTime = DateTime.UtcNow;
DateTimeOffset dtoFromUtc = new DateTimeOffset(utcTime, TimeSpan.Zero);  // UTC пояс
DateTimeOffset dtoMoscow = new DateTimeOffset(DateTime.Now, TimeSpan.FromHours(3));  // +03:00

// Преобразование DateTimeOffset в DateTime
DateTime convertedDt = dto.DateTime;
```

### Лучшая практика для QA

```csharp
// Сравнивайте даты в UTC для надежности
DateTime webTime = DateTime.Parse("2024-03-15T14:30:00");  // Откуда-то с веб-сайта
DateTime expectedTime = new DateTime(2024, 3, 15, 14, 30, 0);

// ? Опасно: разные пояса
if (webTime == expectedTime) { }

// ? Безопаснее: преобразуйте в UTC для сравнения
if (webTime.ToUniversalTime() == expectedTime.ToUniversalTime()) { }
```

---

## 7. TimeSpan — Интервалы времени

### Создание TimeSpan

```csharp
// Конкретное значение
TimeSpan ts1 = new TimeSpan(2, 30, 45);  // 2 часа, 30 минут, 45 секунд
Console.WriteLine(ts1);  // 02:30:45

// Из дней
TimeSpan ts2 = TimeSpan.FromDays(5);     // 5 дней
Console.WriteLine(ts2);  // 5.00:00:00

// Из часов
TimeSpan ts3 = TimeSpan.FromHours(2.5);  // 2.5 часа
Console.WriteLine(ts3);  // 02:30:00

// Из минут
TimeSpan ts4 = TimeSpan.FromMinutes(90); // 90 минут
Console.WriteLine(ts4);  // 01:30:00

// Из секунд
TimeSpan ts5 = TimeSpan.FromSeconds(3600);  // 3600 секунд = 1 час
Console.WriteLine(ts5);  // 01:00:00

// Из миллисекунд
TimeSpan ts6 = TimeSpan.FromMilliseconds(1000);  // 1000 мс = 1 сек
Console.WriteLine(ts6);  // 00:00:01
```

### Арифметика с TimeSpan

```csharp
TimeSpan duration1 = TimeSpan.FromHours(2);
TimeSpan duration2 = TimeSpan.FromMinutes(30);

// Сложение
TimeSpan sum = duration1 + duration2;  // 2 часа + 30 минут = 2:30
Console.WriteLine(sum);  // 02:30:00

// Вычитание
TimeSpan diff = duration1 - duration2;  // 2 часа - 30 минут = 1:30
Console.WriteLine(diff);  // 01:30:00

// Умножение
TimeSpan doubled = duration1 * 2;  // 2 часа * 2 = 4 часа
Console.WriteLine(doubled);  // 04:00:00

// Деление
TimeSpan halved = duration1 / 2;  // 2 часа / 2 = 1 час
Console.WriteLine(halved);  // 01:00:00
```

### Извлечение компонентов

```csharp
TimeSpan ts = new TimeSpan(3, 5, 30, 45);  // 3 дня, 5 часов, 30 минут, 45 секунд

Console.WriteLine(ts.Days);         // 3
Console.WriteLine(ts.Hours);        // 5
Console.WriteLine(ts.Minutes);      // 30
Console.WriteLine(ts.Seconds);      // 45
Console.WriteLine(ts.Milliseconds); // 0

// Общее количество
Console.WriteLine(ts.TotalDays);    // 3.23... (дней)
Console.WriteLine(ts.TotalHours);   // 77.51... (часов)
Console.WriteLine(ts.TotalMinutes); // 4650.75 (минут)
Console.WriteLine(ts.TotalSeconds); // 279045 (секунд)
```

---

## 8. Практический пример: Измерение производительности

### С DateTime (неточно)

```csharp
DateTime start = DateTime.Now;
// Какой-то код...
System.Threading.Thread.Sleep(2000);  // Ждем 2 секунды
DateTime end = DateTime.Now;

TimeSpan elapsed = end - start;
Console.WriteLine($"Прошло: {elapsed.TotalSeconds} секунд");  // ~2 секунды
```

### С Stopwatch (рекомендуется)

```csharp
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

// Какой-то код...
System.Threading.Thread.Sleep(2000);  // Ждем 2 секунды

sw.Stop();

Console.WriteLine($"Прошло: {sw.ElapsedMilliseconds} мс");    // ~2000
Console.WriteLine($"Прошло: {sw.Elapsed.TotalSeconds} секунд");  // ~2

// Stopwatch точнее для измерения производительности!
```

### Пример для QA: проверка скорости загрузки

```csharp
Stopwatch sw = Stopwatch.StartNew();

// Загружаем страницу
// driver.Navigate().GoToUrl("https://example.com");

sw.Stop();

TimeSpan loadTime = sw.Elapsed;

// Проверяем, что страница загрузилась за разумное время
if (loadTime < TimeSpan.FromSeconds(5))
{
    Console.WriteLine("? Страница загрузилась быстро");
}
else
{
    Console.WriteLine("? Страница загружается медленно!");
}
```

---

## 9. Практический пример для QA

```csharp
public class DateValidationTest
{
    public void TestPageDate()
    {
        // Предположим, на странице написана дата: "2024-03-15"
        string dateFromPage = "2024-03-15";

        // Парсим в нужном формате
        bool parsed = DateTime.TryParseExact(
            dateFromPage,
            "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime pageDate
        );

        if (!parsed)
        {
            Console.WriteLine("Ошибка: не удалось распарсить дату");
            return;
        }

        // Проверяем, что это сегодня (сравниваем только даты, не время)
        if (pageDate.Date == DateTime.Today)
        {
            Console.WriteLine("? Дата на странице = сегодня");
        }
        else
        {
            Console.WriteLine("? Дата не совпадает");
        }
    }

    public void TestEventDate()
    {
        DateTime eventDate = new DateTime(2024, 4, 15);  // 15 апреля
        DateTime today = DateTime.Today;

        TimeSpan daysUntilEvent = eventDate - today;

        if (daysUntilEvent.TotalDays > 0)
        {
            Console.WriteLine($"До события осталось {daysUntilEvent.Days} дней");
        }
        else
        {
            Console.WriteLine("Событие уже прошло");
        }
    }

    public void TestPageLoadPerformance()
    {
        Stopwatch sw = Stopwatch.StartNew();

        // Здесь был бы код загрузки страницы
        System.Threading.Thread.Sleep(2000);

        sw.Stop();

        // Проверяем, что загрузилась быстро
        if (sw.Elapsed < TimeSpan.FromSeconds(5))
        {
            Console.WriteLine("? Страница загрузилась быстро");
        }
    }
}
```

---

## 10. Частые ошибки новичков

### ? Ошибка 1: Забыли, что DateTime неизменяемый

```csharp
DateTime date = new DateTime(2024, 3, 15);
date.AddDays(1);  // ? Это ничего не изменяет!
Console.WriteLine(date);  // 15.03.2024 (не изменилось!)

// ? Правильно
date = date.AddDays(1);  // Присвойте результат
Console.WriteLine(date);  // 16.03.2024
```

### ? Ошибка 2: Неправильный формат при парсинге

```csharp
// ? Ошибка: строка в формате "dd.MM.yyyy", а мы указали другой
string dateString = "15.03.2024";
DateTime dt = DateTime.ParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
// ?? Исключение!

// ? Правильно
DateTime dt = DateTime.ParseExact(dateString, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
```

### ? Ошибка 3: Сравнение DateTime с разным временем

```csharp
DateTime dt1 = new DateTime(2024, 3, 15, 10, 30, 0);
DateTime dt2 = new DateTime(2024, 3, 15, 14, 45, 0);

if (dt1 == dt2)  // ? false (разное время)
{
    // Не выполнится
}

// ? Если нужна только дата
if (dt1.Date == dt2.Date)  // ? true (одна дата)
{
    // Выполнится!
}
```

### ? Ошибка 4: Забыли .Date при сравнении дат

```csharp
DateTime today = DateTime.Today;  // 15.03.2024 00:00:00
DateTime now = DateTime.Now;      // 15.03.2024 14:30:45

if (today == now)  // ? false (разное время)
{
    // Не выполнится
}

if (today.Date == now.Date)  // ? true
{
    // Выполнится!
}
```

---

## 11. Лучшие практики

? **DO:**

- Используйте `DateTime.UtcNow` для сохранения в БД
- Используйте `TimeSpan.FromHours/Minutes/Seconds` для создания интервалов
- Используйте `DateTime.TryParseExact` с `CultureInfo.InvariantCulture` для надежного парсинга
- Сравнивайте только даты через `.Date` если время не важно
- Используйте `Stopwatch` для измерения производительности

? **DON'T:**

- Не забывайте, что DateTime неизменяемый (присваивайте результат)
- Не сравнивайте DateTime без учета времени, если оно важно
- Не игнорируйте часовые пояса в распределенных системах
- Не используйте `DateTime.Parse` без проверки формата
- Не используйте `string.Equals` для сравнения дат

---

## 12. ЧЕК-ЛИСТ ДЛЯ СОБЕСЕДОВАНИЯ 🎯

### Вопрос 1: Что такое DateTime и TimeSpan? Чем они отличаются?

**Краткий ответ:** `DateTime` — конкретный момент времени (15 марта 2024, 14:30:45). `TimeSpan` — интервал времени (2 часа, 5 дней). DateTime точка на временной оси, TimeSpan — расстояние между двумя точками.

### Вопрос 2: Какие способы создания DateTime существуют?

**Краткий ответ:** `DateTime.Now` (локальное время), `DateTime.UtcNow` (UTC), `DateTime.Today` (сегодня 00:00), конструктор `new DateTime(2024, 3, 15)`, парсинг `DateTime.Parse()`. Выбирайте в зависимости от нужного часового пояса.

### Вопрос 3: Почему DateTime называют "неизменяемым" и что это значит?

**Краткий ответ:** DateTime — неизменяемый (immutable): `AddDays()` создает **новый** DateTime и не меняет исходный. Ошибка: `date.AddDays(5);` ничего не сделает. Правильно: `date = date.AddDays(5);`

### Вопрос 4: Как форматировать DateTime для вывода в тест?

**Краткий ответ:** Используйте `ToString("формат")`: `"dd.MM.yyyy"` → 15.03.2024; `"yyyy-MM-dd"` → 2024-03-15; `"O"` → ISO стандарт. Помните: MM = месяц, mm = минуты.

### Вопрос 5: Как создать TimeSpan и что с ним делать?

**Краткий ответ:** `TimeSpan.FromHours(2)` или вычитание дат: `DateTime end - DateTime start`. Свойства: `TotalSeconds`, `TotalMilliseconds`, `Hours`, `Minutes`, `Seconds`. Полезно для проверки времени загрузки страницы.

### Вопрос 6: Как парсить строку в DateTime безопасно?

**Краткий ответ:** Используйте `DateTime.TryParseExact()` с явным форматом и `CultureInfo.InvariantCulture`. Пример: `DateTime.TryParseExact("15.03.2024", "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)`.

### Вопрос 7: Как сравнивать даты если время не важно?

**Краткий ответ:** Используйте свойство `.Date` которое обнуляет время: `date1.Date == date2.Date`. Без `.Date`: `date1 == date2` будет false если разное время.

### Вопрос 8: В каких случаях использовать DateTime.Now vs DateTime.UtcNow?

**Краткий ответ:** `Now` — локальное время (конечному пользователю). `UtcNow` — для сохранения в БД и логов (универсально). В автотестах обычно используйте `UtcNow` для консистентности.

### Вопрос 9: Как использовать DateTime в QA-автотестах?

**Краткий ответ:** Генерация тестовых данных (дата рождения, даты в полях), проверка временных меток в логах, измерение производительности (загрузка страницы), проверка истечения сертификатов, сравнение дат с допуском.

### Вопрос 10: Как Stopwatch помогает в тестировании?

**Краткий ответ:** `Stopwatch` измеряет время выполнения: `sw.Start(); /* код */; sw.Stop(); var ms = sw.ElapsedMilliseconds;` Полезен для проверки, что операция выполнилась быстро (или медленно).

---

## Файлы в проекте:

- `Program.cs` — примеры работы с DateTime и TimeSpan
- `DateTimeOffsetDemo.cs` — примеры работы с часовыми поясами
