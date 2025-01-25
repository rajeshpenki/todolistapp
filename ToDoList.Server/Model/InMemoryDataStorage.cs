using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Server.Model
{
    public class InMemoryDataStorage<T> : IDataStorage<T> where T : class
    {
        private readonly List<T> _items;

        private readonly ILogger<InMemoryDataStorage<T>> _logger;

        public InMemoryDataStorage(ILogger<InMemoryDataStorage<T>> logger)
        {
            _items = new List<T>();
            _logger = logger;
        }        

        public async Task<List<T>> GetAll()
        {
            return await Task.FromResult(_items);
        }

        public async Task<bool> Add(T item)
        {
            var returnvalue = false;
            try
            {
                _items.Add(item);
                returnvalue = true;
            }
            catch (Exception ex) { 
            returnvalue = false; 
            _logger.LogError(ex, "Error in Add method"); }
            return await Task.FromResult(returnvalue);
        }

        public async Task<bool> Remove(T item)
        {
            var returnvalue = false;
            try
            {
                _items.Remove(item);
                returnvalue = true;
            }
            catch (Exception ex) {  returnvalue = false; 
            _logger.LogError(ex, "Error in Add method"); }
            return await Task.FromResult(returnvalue);            
        }

        public async Task<bool> Update(T item)
        {
            var returnvalue = false;

            try
            {
                var existingItem = _items.FirstOrDefault(i => i.Equals(item));
                if (existingItem != null)
                {
                    _items.Remove(existingItem);
                    _items.Add(item);
                }
            }
            catch (Exception ex){  returnvalue = false; 
            _logger.LogError(ex, "Error in Add method"); }
            return await Task.FromResult(returnvalue);          
        }
    }
}