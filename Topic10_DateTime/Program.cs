using System.Globalization;

Console.WriteLine("--- 1. Создание и Иммутабельность (ГЛАВНАЯ ОШИБКА) ---");

DateTime now = DateTime.Now;
Console.WriteLine($"Сейчас: {now}");

// ОШИБКА НОВИЧКА:
now.AddDays(5); // Мы создали новую дату в памяти и... сразу потеряли её. Переменная now не изменилась.
Console.WriteLine($"Сейчас (после ошибочного добавления): {now}"); // Та же самая дата!

// ПРАВИЛЬНО:
DateTime future = now.AddDays(5);
Console.WriteLine($"Будущее (правильно): {future}");


Console.WriteLine("\n--- 2. Форматирование для Тестовых Данных ---");
// Сценарий: Нужно заполнить поле "Дата рождения" в форме регистрации.
// Форма требует формат: "Год/Месяц/День".

// Генерируем юзера, которому ровно 20 лет назад было ДР
DateTime birthday = DateTime.Now.AddYears(-20);
Console.WriteLine(birthday);

// Внимательно: MM - месяц, mm - минуты!
string inputForUi = birthday.ToString("yyyy/MM/dd");
Console.WriteLine($"Вводим в поле Input: '{inputForUi}'");


Console.WriteLine("\n--- 3. TimeSpan (Разница во времени) ---");
// Сценарий: Тест производительности. За сколько загрузилась страница?

DateTime start = DateTime.Now;

// Имитация бурной деятельности (задержка 1.5 сек)
System.Threading.Thread.Sleep(1500);

DateTime end = DateTime.Now;

// Вычитание одной даты из другой дает TimeSpan (Интервал)
TimeSpan duration = end - start;

Console.WriteLine($"Прошло секунд: {duration.TotalSeconds}");
Console.WriteLine($"Прошло миллисекунд: {duration.TotalMilliseconds}");

if (duration.TotalSeconds < 2)
{
    Console.WriteLine("[PASS] Страница загрузилась быстро.");
}
else
{
    Console.WriteLine("[FAIL] Слишком долго!");
}


Console.WriteLine("\n--- 4. Парсинг (Чтение с UI) ---");
// Сценарий: На сайте в таблице написана дата "December 07, 2025".
// Нам нужно проверить, что это именно СЕГОДНЯ.

string textFromElement = "December 09, 2025"; // Допустим, получили через element.Text

// Парсим. CultureInfo.InvariantCulture нужен, чтобы игнорировать настройки твоего ПК (русский язык)
// и читать английские названия месяцев.
DateTime parsedDate = DateTime.ParseExact(textFromElement, "MMMM dd, yyyy", CultureInfo.InvariantCulture);

// Сравниваем только Даты (без времени), иначе доли секунды все испортят
if (parsedDate.Date == DateTime.Today)
{
    Console.WriteLine($"[PASS] Дата на сайте верная: {parsedDate.ToShortDateString()}");
}
else
{
    Console.WriteLine($"[FAIL] Дата не совпадает! На сайте: {parsedDate}, Сегодня: {DateTime.Today}");
}