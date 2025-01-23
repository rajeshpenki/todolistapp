using System.Collections.Generic;

namespace ToDoList.Server.Model
{
    public interface IDataStorage<T>
    {
        List<T> GetAll();
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}