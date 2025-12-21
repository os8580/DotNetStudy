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


            // ==========================================
            // 5. QUEUE (ОЧЕРЕДЬ)
            // ==========================================
            // Сценарий: Обработка заявок. FIFO (Первый пришёл - первый вышел).
            Queue<string> requestQueue = new Queue<string>();

            requestQueue.Enqueue("Заявка от клиента #1");
            requestQueue.Enqueue("Заявка от клиента #2");
            requestQueue.Enqueue("Заявка от клиента #3");

            Console.WriteLine($"[Queue] Количество заявок в очереди: {requestQueue.Count}");
            while (requestQueue.Count > 0)
            {
                Console.WriteLine($"Обрабатываем: {requestQueue.Dequeue()}");
            }


            // ==========================================
            // 6. STACK (СТЕК)
            // ==========================================
            // Сценарий: Отмена действий. Последний штрих - отмена.
            Stack<string> actionStack = new Stack<string>();

            actionStack.Push("Добавить товар в корзину");
            actionStack.Push("Удалить товар из корзины");
            actionStack.Push("Изменить количество товара");

            Console.WriteLine($"[Stack] Количество действий в стеке: {actionStack.Count}");
            while (actionStack.Count > 0)
            {
                Console.WriteLine($"Отмена: {actionStack.Pop()}");
            }


            // ==========================================
            // 7. HASHSET (ХЭШ-НАБОР)
            // ==========================================
            // Сценарий: Уникальные теги для товара.
            HashSet<string> tags = new HashSet<string>();

            tags.Add("Распродажа");
            tags.Add("Новинка");
            tags.Add("Хит продаж");
            tags.Add("Распродажа"); // Игнор, так как дубликат

            Console.WriteLine($"[HashSet] Количество уникальных тегов: {tags.Count}");
            foreach (var tag in tags)
            {
                Console.WriteLine($" - {tag}");
            }


            // ==========================================
            // 8. SORTEDLIST (СОРТИРОВАННЫЙ СПИСОК)
            // ==========================================
            // Сценарий: Хранение конфигураций. Имя -> Значение.
            SortedList<string, string> configuration = new SortedList<string, string>();

            configuration.Add("База_данных", "SQLExpress");
            configuration.Add("Порт", "1433");
            configuration.Add("Пользователь", "admin");

            Console.WriteLine($"[SortedList] Количество конфигураций: {configuration.Count}");
            foreach (var item in configuration)
            {
                Console.WriteLine($" - {item.Key}: {item.Value}");
            }
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