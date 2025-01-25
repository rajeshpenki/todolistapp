using System.Collections.Generic;

namespace ToDoList.Server.Model
{
    public interface IDataStorage<T>
    {
        Task<List<T>> GetAll();
        
        Task<bool> Add(T item);

        Task<bool> Remove(T item);

        Task<bool> Update(T item);
    }
}