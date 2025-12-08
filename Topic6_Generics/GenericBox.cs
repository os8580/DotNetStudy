namespace Topic6_Generics
{
    public class GenericBox<T>
    {
        // Поле имеет тип T (какой бы он ни был)
        public T Content;

        public GenericBox(T content)
        {
            Content = content;
        }

        // Метод возвращает T
        public T GetContent()
        {
            return Content;
        }
    }
}
