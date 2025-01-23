using Microsoft.AspNetCore.Mvc;
using ToDoList.Server.Business;
using ToDoList.Server.DTO;
using ToDoList.Server.Repository;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;

        private readonly IToDoListRepo _dolistRepo;

        public TasksController(ILogger<TasksController> logger, IToDoListRepo dolistrepo)
        {
            _logger = logger;
            _dolistRepo = dolistrepo;
            
        }

        [HttpGet()]
        public List<TaskDTO> GetTask()
        {
            return _dolistRepo.GetAllTaskList().Select(item => new TaskDTO() { Id = item.Id,Name = item.Name, Completed = item.Completed }).ToList();
        }

        [HttpPost()]
        public bool AddTask(TaskDTO task)
        {
            var newtask = new ToDoTask() { Name = task.Name, Completed = task.Completed };
            return _dolistRepo.AddTask(newtask);
        }

        

        [HttpDelete("{id}")]
        public bool DeleteTask(int id)
        {
            
            return _dolistRepo.DeleteTask(id);
        }


        [HttpPost("UpdateTask")]
        public bool UpdateTask(TaskDTO task)
        {
            var updatetask = new ToDoTask() { Id = task.Id , Name = task.Name, Completed = task.Completed };
            return _dolistRepo.UpdateTask(updatetask);
        }
    }
}
