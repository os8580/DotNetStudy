using Topic7_Exceptions;

Console.WriteLine("--- ПРИМЕР 1: Деление на ноль ---");

try
{
    int a = 10;
    int b = 0;
    Console.WriteLine("Пытаемся поделить...");
    int result = a / b; // <--- Тут программа ВЗРЫВАЕТСЯ

    // Эта строка уже не выполнится никогда:
    Console.WriteLine($"Результат: {result}");
}
catch (DivideByZeroException ex)
{
    // Мы ловим конкретный тип ошибки
    Console.WriteLine($"[ОШИБКА] Нельзя делить на ноль! Детали: {ex.Message}");
}
catch (Exception ex)
{
    // Ловим всё остальное (если вдруг упало по другой причине)
    Console.WriteLine($"[ОШИБКА] Случилось что-то странное: {ex.Message}");
}


Console.WriteLine("\n--- ПРИМЕР 2: Автотест (Почему важен finally) ---");

// Имитация "Браузера"
bool isBrowserOpen = true;
Console.WriteLine("[Setup] Браузер открыт.");

try
{
    Console.WriteLine("[Test] Ищем кнопку 'Купить'...");

    // Имитируем ситуацию: элемента нет на странице
    // Ключевое слово throw позволяет ВРУЧНУЮ создать ошибку
    throw new ElementNotFoundException("Кнопка 'Купить' не найдена (Selector: .btn-buy)");

    // Console.WriteLine("[Test] Клик по кнопке"); // Не выполнится
}
catch (ElementNotFoundException e)
{
    Console.WriteLine($"---> [Screenshot] Делаем скриншот ошибки: {e.Message}");
    // Важный момент: Мы поймали ошибку, залогировали, но тест формально "зеленый" (программа не упала).
    // В реальном фреймворке тут часто делают throw; чтобы тест все-таки пометился красным.
}
finally
{
    // ЭТО САМОЕ ВАЖНОЕ МЕСТО
    // Если бы не finally, и программа упала бы выше, браузер остался бы висеть.
    if (isBrowserOpen)
    {
        Console.WriteLine("[Teardown] Закрываем браузер (Driver.Quit).");
        isBrowserOpen = false;
    }
}

Console.WriteLine("\nКонец программы. (Если мы тут - значит crash не убил процесс)");