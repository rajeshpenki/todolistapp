using ToDoList.Server.Business;

namespace ToDoList.Server.Model
{
    public interface IToDoListDBContext
    {

        Task<List<ToDoTask>> GetTasks();

        Task<bool> AddTask(ToDoTask task);

        Task<bool> DeleteTask(int id);

        Task<bool> UpdateTask(ToDoTask task);

    }
}
