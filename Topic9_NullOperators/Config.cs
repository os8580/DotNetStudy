namespace Topic9_NullOperators
{
    // Имитация конфига, где какие-то поля могут быть не заполнены
    public class Config
    {
        public string? BaseUrl { get; set; }      // Может быть null
        public string? BrowserName { get; set; }  // Может быть null
        public int? Timeout { get; set; }         // Может быть null
        public string? ApiUrl { get; set; }       // Может быть null
    }
}
