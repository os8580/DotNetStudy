// 1. ПОДГОТОВКА ДАННЫХ
// Представь, что мы получили это через driver.FindElements(...)
using Topic5_LINQ;

List<WebElement> elements = new List<WebElement>
{
    new WebElement("Login", true, "button"),
    new WebElement("Cancel", true, "button"),
    new WebElement("HiddenPopup", false, "div"),
    new WebElement("Submit", true, "button"),
    new WebElement("Profile", true, "link"),
    new WebElement("", true, "span") // Пустой элемент
};

Console.WriteLine("--- 1. ФИЛЬТРАЦИЯ (Where) ---");
// ЗАДАЧА: Получить только ВИДИМЫЕ кнопки.
var visibleButtons = elements
    .Where(el => el.Displayed && el.TagName == "button")
    .ToList();

Console.WriteLine($"Найдено видимых кнопок: {visibleButtons.Count}");
foreach (var btn in visibleButtons) Console.WriteLine($" - {btn.Text}");

Console.WriteLine("\n--- 2. ORDERBY и GROUPBY ---");
// OrderBy - сортировка по Text
var ordered = elements.OrderBy(el => el.Text).ToList();
Console.WriteLine("Элементы, отсортированные по Text:");
foreach (var e in ordered) Console.WriteLine($" - {e.TagName}: '{e.Text}'");

// GroupBy - группировка по TagName
var groups = elements.GroupBy(el => el.TagName);
Console.WriteLine("Элементы, сгруппированные по TagName:");
foreach (var g in groups)
{
    Console.WriteLine($"Group: {g.Key} (Count: {g.Count()})");
    foreach (var item in g) Console.WriteLine($"   - {item.Text}");
}

Console.WriteLine("\n--- 3. JOIN (пример объединения) ---");
// Пример: у нас есть внешний список, который содержит дополнительные метаданные по имени элемента
var meta = new List<(string Text, string Description)>
{
    ("Login", "Кнопка входа"),
    ("Submit", "Кнопка отправки формы"),
    ("Profile", "Ссылка на профиль")
};

var joined = elements
    .Join(meta, e => e.Text, m => m.Text, (e, m) => new { e.Text, e.TagName, m.Description })
    .ToList();

Console.WriteLine("Результат Join (элементы с описанием):");
foreach (var j in joined) Console.WriteLine($" - {j.TagName} '{j.Text}': {j.Description}");

Console.WriteLine("\n--- 4. DEFERRED vs IMMEDIATE EXECUTION ---");
// Deferred: запрос не выполняется до тех пор, пока мы не начнем итерировать
var query = elements.Where(e => e.Displayed);

// Добавим новый элемент до выполнения запроса
elements.Add(new WebElement("NewBtn", true, "button"));

Console.WriteLine("До ToList(): элемент добавлен, но запрос еще не выполнен");
Console.WriteLine($"Query count (deferred): {query.Count()} (выполнение происходит здесь)");

// Immediate: ToList() выполнит запрос прямо сейчас и сохранит результат
var snapshot = elements.Where(e => e.Displayed).ToList();
// Добавим элемент после snapshot
elements.Add(new WebElement("AfterSnapshot", true, "button"));

Console.WriteLine($"Snapshot count (immediate): {snapshot.Count} (не учитывает 'AfterSnapshot')");

Console.WriteLine("\n--- 5. РАНЕЕ РАССМОТРЕННЫЕ МЕТОДЫ ---");
var submitBtn = elements.FirstOrDefault(el => el.Text == "Submit");
if (submitBtn != null)
    Console.WriteLine($"Кнопка найдена! Текст: {submitBtn.Text}");
else
    Console.WriteLine("Кнопка Submit не найдена (возвращен null).");

var ghostBtn = elements.FirstOrDefault(el => el.Text == "Logout");
if (ghostBtn == null) Console.WriteLine("Кнопка Logout не найдена (и это нормально, null).");

List<string> texts = elements.Select(el => el.Text).ToList();
Console.WriteLine("Тексты всех элементов на странице:");
Console.WriteLine(string.Join(", ", texts));

bool hasHiddenDivs = elements.Any(el => !el.Displayed && el.TagName == "div");
Console.WriteLine($"Есть скрытые дивы? {hasHiddenDivs}");

int emptyCountPro = elements.Count(el => string.IsNullOrEmpty(el.Text));
Console.WriteLine($"Пустых элементов: {emptyCountPro}");

List<string> myArr = new List<string> { "Toyota", "BMW", "Toyota", "Honda" };
Console.WriteLine(string.Join(", ", myArr.Distinct()));
