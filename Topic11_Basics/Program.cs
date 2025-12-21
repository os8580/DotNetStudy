// Демонстрация основных примитивов, работы со строками, операторов и конверсий
using System;

// 1. Типы данных
int i = 10;
double d = 3.14;
bool flag = true;
char c = 'A';
string s = "hello";
object obj = i; // upcast

Console.WriteLine($"int: {i}, double: {d}, bool: {flag}, char: {c}, string: {s}");

// 2. Работа со строкой
string name = "Иван";
string greeting = $"Привет, {name.ToUpper()}!";
string combined = string.Concat("a", "b", "c");
string replaced = greeting.Replace("ИВАН", "Пользователь");
Console.WriteLine(greeting);
Console.WriteLine($"Combined: {combined}, Replaced: {replaced}");

// 3. Операторы (арифметические, логические, тернарный)
int a = 5, b = 2;
int sum = a + b;
int div = a / b; // целочисленное деление
double divReal = (double)a / b;
bool cond = (a > b) && (b > 0);
string ternary = (a > b) ? "a>b" : "a<=b";
Console.WriteLine($"sum={sum}, div={div}, divReal={divReal}, cond={cond}, ternary={ternary}");

// 4. Инструкции: ветвления и итерации
if (a > b)
{
    Console.WriteLine("a больше b");
}
else
{
    Console.WriteLine("a не больше b");
}

for (int idx = 0; idx < 3; idx++)
{
    Console.WriteLine($"for idx={idx}");
}

int count = 0;
while (count < 2)
{
    Console.WriteLine($"while count={count}");
    count++;
}

string role = "admin";
switch (role)
{
    case "admin": Console.WriteLine("Role is admin"); break;
    case "user": Console.WriteLine("Role is user"); break;
    default: Console.WriteLine("Unknown role"); break;
}

// 5. Модификатор static
Console.WriteLine($"Pi from static helper: {Math.PI}");
Console.WriteLine($"StaticCounter before: {StaticDemo.Counter}");
StaticDemo.Inc();
Console.WriteLine($"StaticCounter after: {StaticDemo.Counter}");

// 6. Приведение и конвертация типов
int fromDouble = (int)3.99; // явное приведение (отрезается дробная часть)
string numText = "123";
int parsed = int.Parse(numText);
int tryParsed;
if(int.TryParse("bad", out tryParsed)) Console.WriteLine(tryParsed); else Console.WriteLine("Parse failed");

Console.WriteLine($"fromDouble={fromDouble}, parsed={parsed}");

// 7. Nullable
int? maybe = null;
int safe = maybe ?? -1; // null-coalescing
Console.WriteLine($"nullable maybe={maybe}, safe={safe}");


// Вспомогательный статический класс
public static class StaticDemo
{
    public static int Counter = 0;
    public static void Inc() => Counter++;
}
