using ToDoList.Server.Business;

namespace ToDoList.Server.Repository
{
    public interface IToDoListRepo
    {
        Task<List<ToDoTask>> GetAllTaskList();

        Task<bool>  AddTask(ToDoTask task);

        Task<bool>  DeleteTask(int id);

        Task<bool>  UpdateTask(ToDoTask task);       
    }
}
