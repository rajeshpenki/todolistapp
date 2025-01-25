using ToDoList.Server.Business;
using ToDoList.Server.Model;

namespace ToDoList.Server.Repository
{
    public class ToDoListRepo : IToDoListRepo
    {
        public readonly IToDoListDBContext _context;
        public ToDoListRepo(IToDoListDBContext context) 
        {
            _context = context;
        }

        public async Task<List<ToDoTask>> GetAllTaskList()
        {
            return await _context.GetTasks();
        }

        public async Task<bool> AddTask(ToDoTask task) {

            return await _context.AddTask(task);
        }

        public async Task<bool> DeleteTask(int id) {

            return await _context.DeleteTask(id);
        }

        public async Task<bool> UpdateTask(ToDoTask task) {

            return await _context.UpdateTask(task);
        }
    }
}
