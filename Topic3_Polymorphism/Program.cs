using Topic3_Polymorphism;

// 3. ПОЛИМОРФИЗМ В ДЕЙСТВИИ

// Создаем список БАЗОВОГО типа (Browser).
// Но кладем в него РАЗНЫХ наследников.

List<Browser> browsers = new List<Browser>
{
    new Chrome(),
    new Firefox(),
    new Safari()
};

Console.WriteLine("--- Начинаем Кросс-браузерное тестирование ---\n");

// Нам плевать, какой конкретно браузер сейчас в цикле.
// Мы просто знаем, что это "Browser", и у него есть метод Launch().

foreach (Browser currentBrowser in browsers)
{
    Console.WriteLine($"-> Тест запущен на: {currentBrowser.Name}");

    // ВОТ ОН, ПОЛИМОРФИЗМ:
    // Строка кода одна и та же, но поведение разное в зависимости от объекта.
    currentBrowser.Launch();

    // Какая-то логика теста...
    Console.WriteLine("   (Выполняем клики, проверки...)");

    currentBrowser.Close();
    Console.WriteLine("---------------------------------\n");
}