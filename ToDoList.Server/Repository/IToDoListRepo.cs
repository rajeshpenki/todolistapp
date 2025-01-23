using ToDoList.Server.Business;

namespace ToDoList.Server.Repository
{
    public interface IToDoListRepo
    {
        List<ToDoTask> GetAllTaskList();

        bool AddTask(ToDoTask task);


        bool DeleteTask(int id);

        bool UpdateTask(ToDoTask task);       
    }
}
