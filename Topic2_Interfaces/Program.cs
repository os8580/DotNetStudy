using Topic2_Interfaces;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Сценарий 1: Мы хотим прогнать UI тест (как делает юзер)");
// Создаем конкретную реализацию UI
ILoginService uiService = new UiLoginService();
// Отдаем её тесту
LoginTest test1 = new LoginTest(uiService);
test1.Run();

Console.WriteLine("\n--------------------------------------\n");

Console.WriteLine("Сценарий 2: Мы хотим прогнать API тест (для скорости)");
// Создаем конкретную реализацию API
ILoginService apiService = new ApiLoginService();
// Тот же самый класс теста работает с новой реализацией без изменений кода теста!
LoginTest test2 = new LoginTest(apiService);
test2.Run();

Console.WriteLine("\n--------------------------------------\n");

Console.WriteLine("Сценарий 3: Мы хотим прогнать Мобильный тест (как делает юзер на смартфоне)");
// Создаем конкретную реализацию API
ILoginService mobileService = new MobileLoginService();
// Тот же самый класс теста работает с новой реализацией без изменений кода теста!
LoginTest test3 = new LoginTest(mobileService);
test3.Run();

Console.WriteLine("\n--------------------------------------\n");

// Дополнительный демонстрационный пример: ServiceCollection (DI контейнер)
Console.WriteLine("\nСценарий 4: Используем ServiceCollection (DI)");
var services = new ServiceCollection();
// Регистрируем конкретную реализацию для ILoginService
services.AddTransient<ILoginService, ApiLoginService>();
// Регистрируем LoginTest как сервис, чтобы DI смог построить его
services.AddTransient<LoginTest>();

using var provider = services.BuildServiceProvider();
// Получаем экземпляр LoginTest — DI автоматически подставит ApiLoginService
var diTest = provider.GetRequiredService<LoginTest>();
diTest.Run();

Console.WriteLine("\n(Примечание: ServiceCollection демонстрирует, как можно централизованно регистрировать зависимости и подменять реализации.)");