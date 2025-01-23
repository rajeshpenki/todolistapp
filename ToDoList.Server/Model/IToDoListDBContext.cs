using ToDoList.Server.Business;

namespace ToDoList.Server.Model
{
    public interface IToDoListDBContext
    {

        List<ToDoTask> GetTasks();

        bool AddTask(ToDoTask task);

        bool DeleteTask(int id);

        bool UpdateTask(ToDoTask task);

    }
}
