using Topic7_Exceptions;

Console.WriteLine("--- ПРИМЕР 1: Деление на ноль ---");

try
{
    int a = 10;
    int b = 0;
    Console.WriteLine("Пытаемся поделить...");
    int result = a / b; // <--- Тут программа ВЗРЫВАЕТСЯ
    Console.WriteLine($"Результат: {result}");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"[ОШИБКА] Нельзя делить на ноль! Детали: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[ОШИБКА] Случилось что-то странное: {ex.Message}");
}

Console.WriteLine("\n--- ПРИМЕР 2: using / IDisposable и rethrow ---");

// Демонстрация using
try
{
    using (var driver = new FakeDriver())
    {
        driver.DoWork();

        // Имитируем ошибку
        throw new ElementNotFoundException("Кнопка 'Купить' не найдена (Selector: .btn-buy)");
    }
}
catch (ElementNotFoundException e)
{
    Console.WriteLine($"---> [Screenshot] Делаем скриншот ошибки: {e.Message}");
    // Если нужно, можно повторно выбросить исключение, сохранив стек:
    // throw;
}
finally
{
    Console.WriteLine("Finally: тут можно выполнить дополнительную финализацию, но using уже закрыл драйвер.");
}

Console.WriteLine("\nКонец программы.");