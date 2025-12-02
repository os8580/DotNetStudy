using Topic2_Interfaces;

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