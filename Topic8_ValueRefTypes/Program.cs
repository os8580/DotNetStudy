// ==========================================
// 1. VALUE TYPES (Независимость)
// ==========================================
using Topic8_ValueRefTypes;

Console.WriteLine("--- 1. Значимые типы (int) ---");
int a = 10;
int b = a; // Создалась КОПИЯ числа 10

b = 999; // Меняем копию

Console.WriteLine($"A: {a}"); // 10 (Оригинал не пострадал)
Console.WriteLine($"B: {b}"); // 999


// ==========================================
// 2. REFERENCE TYPES (Связь)
// ==========================================
Console.WriteLine("\n--- 2. Ссылочные типы (User class) ---");
User user1 = new User();
user1.Name = "Alex";

User user2 = user1; // Копируется ТОЛЬКО ССЫЛКА (адрес в памяти)
                    // Теперь user1 и user2 смотрят на одного и того же человечка в куче.

user2.Name = "Maria"; // Меняем имя через вторую переменную

Console.WriteLine($"User 1 Name: {user1.Name}"); // Maria (!)
Console.WriteLine($"User 2 Name: {user2.Name}"); // Maria
Console.WriteLine("Оригинал изменился, потому что объект один!");


// ==========================================
// 3. NULLABLE TYPES (Работа с пустотой)
// ==========================================
Console.WriteLine("\n--- 3. Nullable типы ---");

// Сценарий: Мы получаем возраст из Базы Данных.
// int dbAge = null; // ОШИБКА КОМПИЛЯЦИИ!

int? ageFromDb = null; // А так можно. ? делает int "nullable".

// Как безопасно работать с nullable?
if (ageFromDb.HasValue)
{
    Console.WriteLine($"Возраст: {ageFromDb.Value}");
}
else
{
    Console.WriteLine("Возраст в базе не указан (NULL).");
}

// Лайфхак для QA (Null Coalescing Operator ??)
// "Если слева null, то возьми то, что справа"
// Часто используется для дефолтных значений в конфигах
int safeAge = ageFromDb ?? 18;
Console.WriteLine($"Итоговый возраст для теста: {safeAge}");


// ==========================================
// 4. ПОДВОХ СО STRING (Строки - предатели)
// ==========================================
Console.WriteLine("\n--- 4. Нюанс со строками ---");
// string - это Ссылочный тип (Reference).
// НО! Он неизменяемый (Immutable).
// Поэтому ведет себя похоже на Значимый.

string s1 = "Hello";
string s2 = s1;

s2 = "World"; // Тут создается НОВАЯ строка в памяти, а не меняется старая.

Console.WriteLine($"S1: {s1}"); // Hello
Console.WriteLine($"S2: {s2}"); // World
                                // На собеседовании отвечай: "String - ссылочный тип, но иммутабельный, поэтому при изменении создается новый объект".