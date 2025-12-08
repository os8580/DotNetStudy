// 1. ПОДГОТОВКА ДАННЫХ
// Представь, что мы получили это через driver.FindElements(...)
using Topic5_LINQ;

List<WebElement> elements = new List<WebElement>
{
    new WebElement("Login", true, "button"),
    new WebElement("Cancel", true, "button"),
    new WebElement("HiddenPopup", false, "div"),
    new WebElement("Submit", true, "button"),
    new WebElement("", true, "span") // Пустой элемент
};

Console.WriteLine("--- 1. ФИЛЬТРАЦИЯ (Where) ---");
// ЗАДАЧА: Получить только ВИДИМЫЕ кнопки.

// Старый способ (Цикл - много кода):
/*
var visibleButtons = new List<WebElement>();
foreach(var el in elements) {
    if(el.Displayed && el.TagName == "button") visibleButtons.Add(el);
}
*/

// Новый способ (LINQ):
// Читается как английское предложение:
// "Из элементов, ГДЕ показан И тег равен button -> в список"
var visibleButtons = elements
    .Where(el => el.Displayed && el.TagName == "button")
    .ToList();

Console.WriteLine($"Найдено видимых кнопок: {visibleButtons.Count}");
foreach (var btn in visibleButtons) Console.WriteLine($" - {btn.Text}");

Console.WriteLine("\n--- 2. ПОИСК ОДНОГО (FirstOrDefault) ---");
// ЗАДАЧА: Найти кнопку с текстом "Submit".

// FirstOrDefault вернет Элемент, если найдет. Или null, если не найдет.
// (Есть еще просто .First(), но он выбросит Ошибку (Exception), если ничего не найдет. Это опасно!)
var submitBtn = elements.FirstOrDefault(el => el.Text == "Submit");

if (submitBtn != null)
    Console.WriteLine($"Кнопка найдена! Текст: {submitBtn.Text}");
else
    Console.WriteLine($"Кнопка найдена! Текст: {submitBtn.Text}");

// Попробуем найти несуществующее
var ghostBtn = elements.FirstOrDefault(el => el.Text == "Logout");
if (ghostBtn == null) Console.WriteLine("Кнопка Logout не найдена (и это нормально, null).");


Console.WriteLine("\n--- 3. ТРАНСФОРМАЦИЯ (Select) ---");
// ЗАДАЧА: Мне не нужны сами объекты кнопок. Мне нужен просто СПИСОК ИХ НАЗВАНИЙ (List<string>).    

// Select берет каждый элемент и "вытаскивает" из него то, что ты попросишь.
List<string> texts = elements.Select(el => el.Text).ToList();

Console.WriteLine("Тексты всех элементов на странице:");
Console.WriteLine(string.Join(", ", texts)); // Вывод через запятую


Console.WriteLine("\n--- 4. ПРОВЕРКА (Any) ---");
// ЗАДАЧА: Проверить, есть ли на странице хоть один скрытый div.
// Возвращает true/false. Офигенно для Assert.

bool hasHiddenDivs = elements.Any(el => !el.Displayed && el.TagName == "div");
Console.WriteLine($"Есть скрытые дивы? {hasHiddenDivs}");


// Способ А: Сначала отфильтровать (Where), потом посчитать (Count)
// Читается: "Взять элементы, ГДЕ текст равен пустоте, и вернуть их количество"
int emptyCount1 = elements.Where(el => el.Text == "").Count();

// Способ Б: Сразу посчитать с условием (Более короткий и правильный)
// Читается: "Посчитать элементы, у которых текст равен пустоте"
int emptyCount2 = elements.Count(el => el.Text == "");

Console.WriteLine($"Пустых элементов (способ 1): {emptyCount1}");
Console.WriteLine($"Пустых элементов (способ 2): {emptyCount2}");

// ПРОФЕССИОНАЛЬНЫЙ НЮАНС:
// Вместо el.Text == "" лучше использовать string.IsNullOrEmpty(el.Text)
// Это спасет, если Text будет равен null (а не просто пустой строке).
int emptyCountPro = elements.Count(el => string.IsNullOrEmpty(el.Text));

List<string> myArr = new List<string>
{
    "Toyota",
    "BMW",
    "Toyota",
    "Honda"
};

// Pseudocode:
// - Distinct returns IEnumerable<string>, which Console.WriteLine won't format.
// - Convert the enumerable to a readable string via string.Join(", ", ...).
// - Print the joined result.

Console.WriteLine(string.Join(", ", myArr.Distinct()));
Console.WriteLine(string.Join(", ", myArr));
