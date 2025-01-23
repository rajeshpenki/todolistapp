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

        public ToDoListDBContext(IDataStorage<ToDoTask> dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public List<ToDoTask> GetTasks()
        {
            return _dataStorage.GetAll();
        }

        public bool AddTask(ToDoTask task)
        {
            try
            {
                if (_dataStorage.GetAll().All(item => item.Id != task.Id))
                {
                    var maxId = _dataStorage.GetAll().OrderByDescending(item => item.Id).FirstOrDefault()?.Id ?? 0;
                    task.Id = maxId + 1;
                    _dataStorage.Add(task);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public bool DeleteTask(int id)
        {
            try
            {
                var task = _dataStorage.GetAll().FirstOrDefault(item => item.Id == id);
                if (task != null)
                {
                    _dataStorage.Remove(task);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public bool UpdateTask(ToDoTask task)
        {
            try
            {
                var existingTask = _dataStorage.GetAll().FirstOrDefault(item => item.Id == task.Id);
                if (existingTask != null)
                {
                    _dataStorage.Update(task);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}