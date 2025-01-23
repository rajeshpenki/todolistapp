using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Server.Model
{
    public class InMemoryDataStorage<T> : IDataStorage<T> where T : class
    {
        private readonly List<T> _items = new List<T>();

        public List<T> GetAll()
        {
            return _items;
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public void Update(T item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Equals(item));
            if (existingItem != null)
            {
                _items.Remove(existingItem);
                _items.Add(item);
            }
        }
    }
}