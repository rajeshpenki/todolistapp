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

        public List<ToDoTask> GetAllTaskList()
        {
            return _context.GetTasks();
        }

        public bool AddTask(ToDoTask task) {

            return _context.AddTask(task); ;
        }

        public bool DeleteTask(int id) {

            return _context.DeleteTask(id); ; ;
        }

        public bool UpdateTask(ToDoTask task) {

            return _context.UpdateTask(task); ; ;
        }
    }
}
