namespace Topic6_Generics
{
    // ==========================================
    // 1. ПЛОХОЙ КЛАСС (Без дженериков)
    // ==========================================
    public class OldBox
    {
        // Храним что угодно (object - это отец всех типов)
        public object Content;

        public OldBox(object content)
        {
            Content = content;
        }
    }
}
