namespace Topic6_Generics
{
    // Демонстрация ограничений для обобщений
    public class Factory<T> where T : new()
    {
        public T Create() => new T();
    }

    public class Repository<T> where T : class
    {
        private List<T> _items = new List<T>();
        public void Add(T item) => _items.Add(item);
        public IEnumerable<T> GetAll() => _items;
    }

    // Пример ограничения наследования
    public class BaseEntity { public int Id { get; set; } }
    public class EntityRepository<T> where T : BaseEntity
    {
        private List<T> _items = new List<T>();
        public void Add(T item) => _items.Add(item);
        public T? FindById(int id) => _items.FirstOrDefault(i => i.Id == id);
    }
}
