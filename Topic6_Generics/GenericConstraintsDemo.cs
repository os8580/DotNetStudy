namespace Topic6_Generics
{
    /// <summary>
    /// Пример generic-фабрики: создает экземпляры типа <typeparamref name="T"/> с ограничением <c>new()</c>.
    /// </summary>
    /// <typeparam name="T">Тип создаваемого объекта. Должен иметь публичный конструктор без параметров.</typeparam>
    public class Factory<T> where T : new()
    {
        /// <summary>
        /// Создает новый экземпляр типа <typeparamref name="T"/>.
        /// </summary>
        public T Create() => new T();
    }

    /// <summary>
    /// Базовый generic-репозиторий для хранения объектов ссылочных типов.
    /// Демонстрирует ограничение <c>where T : class</c>.
    /// </summary>
    /// <typeparam name="T">Тип сущности (класс).</typeparam>
    public class Repository<T> where T : class
    {
        private List<T> _items = new List<T>();

        /// <summary>
        /// Добавляет элемент в репозиторий.
        /// </summary>
        public void Add(T item) => _items.Add(item);

        /// <summary>
        /// Возвращает все элементы как последовательность.
        /// </summary>
        public IEnumerable<T> GetAll() => _items;
    }

    /// <summary>
    /// Базовая сущность с полем идентификатора для примеров generic-ограничений.
    /// </summary>
    public class BaseEntity { public int Id { get; set; } }

    /// <summary>
    /// Репозиторий для сущностей, наследующих <see cref="BaseEntity"/>.
    /// Позволяет искать по идентификатору благодаря ограничению <c>where T : BaseEntity</c>.
    /// </summary>
    /// <typeparam name="T">Тип сущности, производный от <see cref="BaseEntity"/>.</typeparam>
    public class EntityRepository<T> where T : BaseEntity
    {
        private List<T> _items = new List<T>();

        /// <summary>
        /// Добавляет сущность в репозиторий.
        /// </summary>
        public void Add(T item) => _items.Add(item);

        /// <summary>
        /// Ищет первую сущность по идентификатору.
        /// </summary>
        public T? FindById(int id) => _items.FirstOrDefault(i => i.Id == id);
    }
}
