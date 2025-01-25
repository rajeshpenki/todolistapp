using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Server.Business;
using ToDoList.Server.Repository;

namespace ToDoList.Server.Model
{
    public class ToDoListDBContext : IToDoListDBContext
    {
        private readonly IDataStorage<ToDoTask> _dataStorage;

        private readonly ILogger<ToDoListDBContext> _logger;

        public ToDoListDBContext(IDataStorage<ToDoTask> dataStorage, ILogger<ToDoListDBContext> logger)
        {
            _dataStorage = dataStorage;
            _logger = logger;
        }

        public Task<List<ToDoTask>> GetTasks()
        {
            return _dataStorage.GetAll();
        }

        public async Task<bool> AddTask(ToDoTask task)
        {
            var returnvalue = false;
            try
            {
                var tasks = await _dataStorage.GetAll();
                if (tasks.All(item => item.Id != task.Id))
                {
                    var maxId = tasks.OrderByDescending(item => item.Id).FirstOrDefault()?.Id ?? 0;
                    task.Id = maxId + 1;
                    await _dataStorage.Add(task);
                    returnvalue = true;
                }
            }
            catch (Exception)
            {
                returnvalue = false;
            }

            return returnvalue;
        }

        public async Task<bool>  DeleteTask(int id)
        {
            var returnvalue = false;
            try
            {
                var tasks = await _dataStorage.GetAll();
                var task = tasks.FirstOrDefault(item => item.Id == id);
                if (task != null)
                {
                    await _dataStorage.Remove(task);
                    returnvalue = true;
                }
            }
            catch (Exception)
            {
                returnvalue = false;
            }

            return returnvalue;
        }

        public async Task<bool>  UpdateTask(ToDoTask task)
        {
            var returnvalue = false;            
            try
            {
                var tasks = await _dataStorage.GetAll();
                var existingTask = tasks.FirstOrDefault(item => item.Id == task.Id);
                if (existingTask != null)
                {
                    await _dataStorage.Update(task);
                    returnvalue = true;
                }
            }
            catch (Exception)
            {
                returnvalue= false;
            }

            return returnvalue;
        }
    }
}