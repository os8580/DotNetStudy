using System.Globalization;

namespace Topic10_DateTime
{
    public static class DateTimeOffsetDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- DateTimeOffset и часовые пояса ---");

            // Сейчас UTC и локальное
            var nowUtc = DateTime.UtcNow;
            var nowLocal = DateTime.Now;

            Console.WriteLine($"UTC: {nowUtc}");
            Console.WriteLine($"Local: {nowLocal}");

            // DateTimeOffset хранит момент времени с оффсетом
            var dto = DateTimeOffset.Now;
            Console.WriteLine($"DateTimeOffset.Now: {dto}");

            // Преобразование между часовыми поясами
            var moscow = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            var moscowTime = TimeZoneInfo.ConvertTime(dto, moscow);
            Console.WriteLine($"Время в Москве: {moscowTime}");

            // Парсинг с учетом культуры
            string dateText = "December 09, 2025";
            if (DateTime.TryParseExact(dateText, "MMMM dd, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            {
                Console.WriteLine($"Parsed: {parsed}");
            }
            else
            {
                Console.WriteLine("Не удалось распарсить дату.");
            }
        }
    }
}
