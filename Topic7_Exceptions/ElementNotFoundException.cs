namespace Topic7_Exceptions
{
    // Имитация ошибки Selenium (чтобы не подключать либу)
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message) { }
    }
}
