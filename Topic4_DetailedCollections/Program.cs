namespace Topic4_DetailedCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========================================
            // 1. ARRAY (МАССИВ)
            // ==========================================
            // Сценарий: Данные, которые мы точно знаем и они не изменятся.
            // Например, категории товаров в меню сайта.
            string[] staticCategories = new string[3] { "Engine", "Wheels", "Lights" };

            // Изменить можно (заменить "Engine" на "Motor")
            staticCategories[0] = "Motor";

            // Добавить НЕЛЬЗЯ. Это вызовет ошибку компиляции или рантайма, если пытаться хакнуть.
            // staticCategories.Add("Oil"); // <-- Такого метода нет у массива!

            Console.WriteLine($"[Array] Категория 1: {staticCategories[0]} (Длина: {staticCategories.Length})");


            // ==========================================
            // 2. LIST (СПИСОК)
            // ==========================================
            // Сценарий: Корзина покупок. То с пустым, то с полным.
            List<string> cart = new List<string>();

            cart.Add("Oil Filter");
            cart.Add("Brake Pads");
            cart.Add("Wiper Blades");

            // Легко удаляем
            cart.Remove("Brake Pads");

            Console.WriteLine($"[List] В корзине товаров: {cart.Count}");
            foreach (var item in cart)
            {
                Console.WriteLine($" - {item}");
            }


            // ==========================================
            // 3. DICTIONARY (СЛОВАРЬ)
            // ==========================================
            // Сценарий: Склад. Артикул (ID) -> Название детали.
            // Нам нужно быстро узнавать, что это за деталь по коду "A-101".
            Dictionary<string, string> warehouse = new Dictionary<string, string>();

            warehouse.Add("A-101", "Premium Tire");
            warehouse.Add("B-202", "Battery 12V");
            warehouse.Add("C-303", "Headlight LED");

            // Поиск мгновенный
            string searchCode = "B-202";
            if (warehouse.ContainsKey(searchCode))
            {
                Console.WriteLine($"[Dictionary] По коду {searchCode} найдено: {warehouse[searchCode]}");
            }

            // Попытка взять несуществующее (безопасно)
            if (!warehouse.TryGetValue("Z-999", out string itemValue))
            {
                Console.WriteLine("[Dictionary] Детали Z-999 нет на складе.");
            }


            // ==========================================
            // 4. IENUMERABLE (УНИВЕРСАЛЬНОСТЬ)
            // ==========================================
            Console.WriteLine("\n--- Работа универсального метода ---");

            // Мы передаем в метод и массив, и список. И всё работает.
            PrintAllItems(staticCategories); // Передали массив
            PrintAllItems(cart);             // Передали список

            Console.ReadKey();
        }

        // Этот метод принимает "Что угодно, что можно перебрать"
        // Ему все равно, массив это или лист.
        public static void PrintAllItems(IEnumerable<string> collection)
        {
            Console.WriteLine("-> Печать коллекции:");
            foreach (var item in collection)
            {
                Console.Write($"[{item}] ");
            }
            Console.WriteLine(); // перенос строки

            // Внимание: Здесь НЕТ метода .Add() или .Remove().
            // IEnumerable только для чтения (Read-only access via iteration).
            // collection.Add("New"); // Ошибка!
        }
    }
}