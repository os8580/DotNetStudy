using Topic6_Generics;

Console.WriteLine("--- 1. Проблемы без дженериков ---");

// Кладем яблоко в старую коробку
OldBox box1 = new OldBox(new Apple());

// Пытаемся достать.
// box1.Content - это object. Компьютер не знает, что там Apple.
// Console.WriteLine(box1.Content.Color); // ОШИБКА КОМПИЛЯЦИИ! object не имеет поля Color.

// Нам приходится делать приведение типов (Cast) вручную:
Apple apple = (Apple)box1.Content;
Console.WriteLine($"Достали яблоко: {apple.Color}");

// А ТЕПЕРЬ ОПАСНОСТЬ:
// Представь, что мы перепутали коробки и пытаемся достать Кирпич из коробки с Яблоком.
// Компилятор это пропустит (ему плевать, object есть object).
try
{
    Brick brick = (Brick)box1.Content; // БА-БАХ! Ошибка упадет только при запуске программы.
}
catch (InvalidCastException)
{

    Console.WriteLine("[Ошибка Runtime] Нельзя превратить Яблоко в Кирпич!");
}

Console.WriteLine("\n--- 2. Решение с Generics ---");

// Создаем коробку КОНКРЕТНО ДЛЯ ЯБЛОК.
// Вместо T подставляется Apple.
GenericBox<Apple> safeBox = new GenericBox<Apple>(new Apple());

// 1. Нам не надо кастовать. Метод сразу возвращает Apple.
Console.WriteLine($"Цвет: {safeBox.GetContent().Color}");

// 2. Попробуем положить туда кирпич?
// safeBox.Content = new Brick(); // КРАСНОЕ ПОДЧЕРКИВАНИЕ! Ошибка компиляции.
// Мы защищены от глупых ошибок еще до запуска.