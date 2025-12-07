namespace Topic5_LINQ
{
    // Имитация Веб-Элемента (как в Selenium)
    public class WebElement
    {
        public string Text { get; set; }
        public bool Displayed { get; set; }
        public string TagName { get; set; }

        public WebElement(string text, bool displayed, string tagName)
        {
            Text = text;
            Displayed = displayed;
            TagName = tagName;
        }
    }
}
